using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace WebApplication3;

public class Images

{
    [Key]
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string? Url { get; set; }
}