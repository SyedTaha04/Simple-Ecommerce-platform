using System.ComponentModel.DataAnnotations;
using Mysqlx.Crud;

namespace WebApplication3;

public class OrderItems
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public Orders Order { get; set; }
    [Required]
    public Products Product { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public int Price { get; set; }
    
}