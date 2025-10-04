using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Database;
using WebApplication3.Models;

namespace WebApplication3.Controllers;
[Route("api/[controller]")] 
[ApiController]
public class AddCategoryController: ControllerBase
{
    private readonly ApplicationDbContext context;

    public AddCategoryController(ApplicationDbContext context)
    {
        this.context = context;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult AddCategory([FromBody] CategoryDto categoryDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            Category newCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = categoryDto.Name,
            };
            var addProduct = context.Category.Add(newCategory);
            context.SaveChanges();
            return Ok(newCategory);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}

public class ProductRequestBody
{
    public string Name { get; set; }
    public int Price { get; set; }
    public Guid Category_Id { get; set; }
}
