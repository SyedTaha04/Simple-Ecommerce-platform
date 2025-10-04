using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Database;
using WebApplication3.ExtraClasses;
using WebApplication3.Models;

namespace WebApplication3.Controllers;
[Route("api/[controller]")] 
[ApiController]
public class UpdateProductController: ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly UploadImage uploadImage;
    
    public UpdateProductController(ApplicationDbContext context, UploadImage uploadImage)
    {
        this.context = context;
        this.uploadImage = uploadImage;
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> UpdateProductData(Guid id,  [FromBody] ProductDto productDto)
    {
        var updateProductData = await context.Database.ExecuteSqlRawAsync("Update Products set title = {0}, Description = {1}, Price = {2}, CategoryId = {3} where Id = {4}",
        productDto.Title,
        productDto.Description,
        productDto.Price,
        productDto.Category.Id,
        id);
        
        return Ok("Product data updated");
    }
    
}