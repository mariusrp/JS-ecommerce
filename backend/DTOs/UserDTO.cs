using System.ComponentModel.DataAnnotations;

public class UserDTO {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
    public string Password { get; set; }
}


