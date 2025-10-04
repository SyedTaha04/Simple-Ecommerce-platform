using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Database;
using WebApplication3.Models;
using WebApplication3.ExtraClasses;

namespace WebApplication3.Controllers;
[Route("api/[controller]")] 
[ApiController]
public class DeleteUserController : ControllerBase
{
    private readonly ILogger<DeleteUserController> logger;
    private readonly ApplicationDbContext dbcontext;
    private readonly PasswordHashing passwordHashing;
    public DeleteUserController(ILogger<DeleteUserController> logger, ApplicationDbContext dbcontext, PasswordHashing passwordHashing)
    {
        this.logger = logger;   
        this.dbcontext = dbcontext;
        this.passwordHashing = passwordHashing;
    }
    [HttpPost]
    public IActionResult DeleteUser([FromBody] DeleteUserDto deleteUser)
    {
        if (!ModelState.IsValid)
        {
            return NotFound("Failed to delete user");
        }
        try
        {
            var FetchingPassword = dbcontext.SignupPage.FromSqlRaw("Select * from signuppage where email = {0}", deleteUser.Email)
                .AsEnumerable()
                .FirstOrDefault();
            //var fetchData = dbcontext.SignupPage.Where(user => user.Email == deleteUser.Email).ToList().SingleOrDefault(); 
            dbcontext.SignupPage.FirstOrDefaultAsync(x => x.Email == deleteUser.Email);
            string DbHashedPassword = FetchingPassword.Password ;
            bool ValidPasword = passwordHashing.Verify(deleteUser.Password, DbHashedPassword);
            if (ValidPasword)
            {
                var deleteUserAccount =
                    dbcontext.Database.ExecuteSqlRaw("Delete from signuppage where email = {0}", deleteUser.Email);
                //var deleteAccount = dbcontext.SignupPage.Remove(fetchData);
                return Ok("Account has been deleted");
            }
            return BadRequest("Failed to delete user");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

