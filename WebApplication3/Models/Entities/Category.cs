using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication3;

public class Category
{
    [Key]
    
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Name { get; set; }
    
    
}