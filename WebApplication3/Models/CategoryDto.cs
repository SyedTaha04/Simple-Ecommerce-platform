using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public class CategoryDto
{
    [Required]
    public string Name { get; set; }
}