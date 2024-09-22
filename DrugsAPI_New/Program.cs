using Drugs_API.Data;
using DrugsAPI_New.Repositories;
using DrugsAPI_New.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using DrugsAPI_New.Auth;

namespace DrugsAPI_New
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Set the port to 5119
            builder.WebHost.UseUrls("http://localhost:5119");

            // Add services to the container.
            //DefaultConnection
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
            new MySqlServerVersion(new Version(8, 0, 21)))); // Adjust version as needed  

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(new AllowAnonymousFilter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Drugs API", Version = "v1" });
                
                // Configure Swagger to use JWT Bearer Authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                // Add operation filter to exclude Auth controller from requiring authorization
                c.OperationFilter<SwaggerAuthorizationOperationFilter>();
            });

            // Add dependencies for all repository interfaces
            builder.Services.AddScoped<IDrugRepository, DrugRepository>();
            builder.Services.AddScoped<IRefillRepository, RefillRepository>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>(); // New line

            builder.Services.AddScoped<IDrugService, DrugService>();
            builder.Services.AddScoped<IRefillService, RefillService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<IPrescriptionService, PrescriptionService>(); // New line

            // Register services before building the app
            builder.Services.AddScoped<ApplicationDbContext>();
          

            // Configure JWT authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false, // Set to false to disable lifetime validation
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            // Modify the global authorization filter
            builder.Services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            // Add AuthService and UserRepository to the DI container
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Drugs API V1");
                });

                // Enable CORS if needed
                app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

                // Add exception handling middleware
                app.UseExceptionHandler("/Error");
                app.UseHsts();

                // Add missing dependencies
               // builder.Services.AddScoped<ApplicationDbContext>();
               // builder.Services.AddScoped<IDrugRepository, DrugRepository>();
               // builder.Services.AddScoped<IRefillRepository, RefillRepository>();
               // builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            }

            app.UseHttpsRedirection();

            // Use authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
