using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Database;
using WebApplication3.ExtraClasses;
using WebApplication3.Models;

namespace WebApplication3.Controllers;
[Route("api/[controller]")] 
[ApiController]
public class AddProductController:ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly UploadImage uploadImage;
    public AddProductController(ApplicationDbContext context, UploadImage uploadImage)
    {
        this.context = context;
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody]ProductDto productDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
                
            }
            var category = await context.Category.FindAsync(productDto.Category.Id);
            if (category == null)
                return NotFound("Category not found");
            var ProductInsertion = new Products
            {
                Title = productDto.Title,
                Description = productDto.Description,
                Price = productDto.Price,
                
            };
            context.Entry(ProductInsertion).Property("CategoryId").CurrentValue = productDto.Category.Id;

            var AddingProduct = context.AddAsync(ProductInsertion);
            await context.SaveChangesAsync();
            return Ok("Product added");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}