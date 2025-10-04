using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public class ImageDto
{
    [Required]
    public string? Url { get; set; }
    
}