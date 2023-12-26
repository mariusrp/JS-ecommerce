using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; }

    public User() { }

    public User(string email, string password)
    {
        Email = email;
        PasswordHash = HashPassword(password);
    }
    
    private string HashPassword(string password)
    {
        // No hash function for now
        return password;
    }
}

