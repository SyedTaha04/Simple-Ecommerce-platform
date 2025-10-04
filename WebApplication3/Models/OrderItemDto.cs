using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public class OrderItemDto
{
    
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
}