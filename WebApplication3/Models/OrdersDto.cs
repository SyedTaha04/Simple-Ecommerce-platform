using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public class OrdersDto
{
    [Required]
    [EmailAddress]
    public string UserEmail { get; set; }
    
    [Required]
    public ICollection<OrderItemDto>  OrderItemDtos { get; set; }

}