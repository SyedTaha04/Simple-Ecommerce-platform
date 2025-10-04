using System.ComponentModel.DataAnnotations;

namespace WebApplication3;

public class SignUpPage
{
    
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Key]
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    [Required]
   
    public string PhoneNumber { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string? Password { get; set; }
    public string Role { get; set; } = "User";
    
    public string? GoogleId { get; set; } 
    
    public ICollection<Orders> Orders { get; set; }
    
    
}