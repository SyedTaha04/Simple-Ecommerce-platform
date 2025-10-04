using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Database;
using WebApplication3.ExtraClasses;
using WebApplication3.Models;

namespace WebApplication3.Controllers;
[Route("api/[controller]")] 
[ApiController]
public class UpdateImageController: ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly UploadImage uploadImage;
    
    public UpdateImageController(ApplicationDbContext context, UploadImage uploadImage)
    {
        this.context = context;
        this.uploadImage = uploadImage;
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddImage([FromForm] UploadImage uploadImage, List<IFormFile> files, ImageDto imageDto, Guid id)
    {
       
           
        List<Images> saveImageToServer = uploadImage.SaveImage(files);
        foreach (var image in saveImageToServer)
        {
            Guid imageId = Guid.NewGuid();
            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO Images (Id,Url, ProductsId) VALUES ({0}, {1}, {2})",
                imageId,
                image.Url,
                id
            );
            
        }
        return Ok("Image added");
        
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteImage([FromBody] ImageDto imageDto, Guid id, UploadImage uploadImage)
    {
        var deleteImageUrl = await context.Database.ExecuteSqlRawAsync("Delete from Images where Url = {0} and ProductsId ={1}",
            imageDto.Url,
            id);
        
        var filePath = Path.Combine("wwwroot/images", imageDto.Url.TrimStart('/'));
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }

        if (deleteImageUrl > 0)
        {
            return Ok("Image deleted successfully");
            
        }
            
        return NotFound("Image not found for this product");
    }
}