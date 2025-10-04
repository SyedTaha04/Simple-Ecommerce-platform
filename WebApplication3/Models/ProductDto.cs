using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace WebApplication3.Models;

public class ProductDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    
    [Required]
    public int Price { get; set; }
    [Required]
    public Category Category { get; set; }
    
}

public enum Size
{
    XS,
    S,
    M,
    L,
    XL,
    XXL
}
