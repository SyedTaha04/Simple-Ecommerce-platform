using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public class DeleteUserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password { get; set; }
}