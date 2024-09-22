using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using DrugsAPI_New.Services;
using DrugsAPI_New.Repositories;
using DrugsAPI_New.Models;


public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private static readonly HashSet<string> _invalidatedTokens = new HashSet<string>();

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

  
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key) || Encoding.UTF8.GetBytes(key).Length < 32)
            {
                throw new InvalidOperationException("JWT key is missing or too short. It should be at least 32 characters long.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        if (_invalidatedTokens.Contains(token))
        {
            return false;
        }
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool InvalidateToken(string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            _invalidatedTokens.Add(token);
            return true;
        } else
        {
            return false;
        }
    }

    public (bool isSuccess, string error) CreateUser(User user, string password)
    {
        user.Password = HashPassword(password);

        var result = _userRepository.CreateUser(user);

        if (result)
        {
            return (true, null);
        }
        else
        {
            return (false, "Failed to create user");
        }
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

}