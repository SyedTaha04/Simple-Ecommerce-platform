using Microsoft.AspNetCore.Mvc;
using WebApplication3.Database;
using WebApplication3.Models;
using WebApplication3.ExtraClasses;

namespace WebApplication3.Controllers;
[Route("api/[controller]")] 
[ApiController]
public class SignUpPageController: ControllerBase
{
    private readonly ApplicationDbContext dbcontext;
    private readonly PasswordHashing passwordHashing;
    
    
    public SignUpPageController(ApplicationDbContext dbcontext, PasswordHashing passwordHashing)
    {
        this.dbcontext = dbcontext;
        this.passwordHashing = passwordHashing;
    }
    [HttpPost]
    public IActionResult SendSignUpData([FromBody] AddSignUpPageDto signUpPageDto)
    {
       
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        string hashedPassword = passwordHashing.HashPassword(signUpPageDto.Password);

        try
        {
            if (string.IsNullOrEmpty(signUpPageDto.Role))
            {
                signUpPageDto.Role = "User";  
            }
            if (string.IsNullOrEmpty(signUpPageDto.GoogleId) && string.IsNullOrEmpty(signUpPageDto.Password))
            {
                return BadRequest("Password is required for email/password signups.");
            }
            
            SignUpPage signUpPage = new SignUpPage
            {
                FirstName = signUpPageDto.FirstName,
                LastName = signUpPageDto.LastName,
                Email = signUpPageDto.Email,
                PhoneNumber = signUpPageDto.PhoneNumber,
                UserName = signUpPageDto.UserName,
                Role = signUpPageDto.Role,
                
                Password = hashedPassword,
            };
           
            dbcontext.SignupPage.Add(signUpPage);
            dbcontext.SaveChanges();
            return Ok();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
}