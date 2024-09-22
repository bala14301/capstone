using System;
using System.ComponentModel.DataAnnotations;

namespace DrugsAPI_New.Models
{
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string MobileNo { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }

    public User(string username, string emailId, string mobileNo, string password)
    {
        Username = username;
        Email = emailId;
        MobileNo = mobileNo;
        Password = password;
    }

    public User() { }

    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
}
}
