using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication3;

public class Orders
{
    [Key]
    public Guid  Id { get; set; }
    
    public SignUpPage SignUpPage { get; set; }
    
    [JsonIgnore]
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    
    public int TotalAmount { get; set; }
    
    public ICollection<OrderItems> orderItems { get; set; }
    
}