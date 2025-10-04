using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Database;
using WebApplication3.ExtraClasses;
using WebApplication3.Models;
using WebApplication3.ExtraClasses;

namespace WebApplication3.Controllers;
[Route("api/[controller]")] 
[ApiController]
public class SignInController:ControllerBase
{
    private readonly ApplicationDbContext dbcontext;
    private readonly PasswordHashing passwordHashing;
    private readonly JwtToken jwtToken;
    
    public SignInController(ApplicationDbContext dbContext, PasswordHashing passwordHashing,JwtToken jwtToken )
    {
        this.dbcontext = dbContext;
        this.passwordHashing = passwordHashing;
        this.jwtToken = jwtToken;
       
    }
    [HttpPost]
    public IActionResult SendSignInData([FromBody] SignInDto signInDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid data");  
            
        }
        

        try
        {
            var login = dbcontext.SignupPage.FromSqlRaw("SELECT * FROM signuppage where email = {0}",  signInDto.Email)
                .AsEnumerable()
                .FirstOrDefault();

            string DbHashedPassword = login.Password;

            if (login == null)
            {
                return NotFound("User not found");
                
            }
            bool ValidPassword = passwordHashing.Verify(signInDto.Password, DbHashedPassword);
            SignInDto tokenData = new SignInDto
            {
                Email = login.Email,
            };
            if (ValidPassword)
            {
                 string TokenResponse = jwtToken.Create(signInDto);
                 return Ok(new { Token = TokenResponse });
            }
            return BadRequest("Invalid password");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


    }

    
}