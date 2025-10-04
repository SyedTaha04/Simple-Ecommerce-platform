using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Database;
using WebApplication3.Models;

namespace WebApplication3.Controllers;
[Route("api/[controller]")] 
[ApiController]
public class AddOrderController: ControllerBase
{
    private readonly ApplicationDbContext context;
    public AddOrderController(ApplicationDbContext context)
    {
        this.context = context;
        
    }
   [HttpPost]
    public async Task<IActionResult> UserOrders([FromBody] OrdersDto ordersDto )
    {
        var fetchingUserEmail = await  context.SignupPage.FromSqlRaw("SELECT * FROM SignUpPage WHERE Email = {0}", ordersDto.UserEmail).FirstOrDefaultAsync();
        
        if(fetchingUserEmail == null)
        {
            return BadRequest("User not found.");
        }
        var orderDetails = new Orders
        {
            Id = Guid.NewGuid(),
            SignUpPage = fetchingUserEmail,
            OrderDate = DateTime.UtcNow,
            TotalAmount = 0,
            orderItems = new List<OrderItems>(),
        };

        foreach (var item in ordersDto.OrderItemDtos)
        {
            var product = await context.Products.FromSqlRaw("Select * from products where Id = {0}", item.ProductId).
            FirstOrDefaultAsync();
            var productId = product.Id;
            var productPrice = product.Price;
            var quantity = item.Quantity;
            var orderAmount = productPrice * quantity;

            var orderItem = new OrderItems
            {
                Id = Guid.NewGuid(),
                Order = orderDetails,
                Product = product,
                Quantity = quantity,
                Price = orderAmount,
            };
            orderDetails.orderItems.Add(orderItem);
            orderDetails.TotalAmount += orderAmount;
        }
        context.Orders.Add(orderDetails);
        await context.SaveChangesAsync();

        return Ok(new
        {
            Message = "Order placed successfully",
            OrderId = orderDetails.Id,
            TotalAmount = orderDetails.TotalAmount
        });
        
    }
    
}