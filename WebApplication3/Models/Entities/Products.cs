using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;
using System.Text.Json.Serialization;

namespace WebApplication3;

public class Products
{
    [Key]
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    
    [Required]
    public int Price { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Category Category { get; set; }
    public List<Images> Images { get; set; }
}

