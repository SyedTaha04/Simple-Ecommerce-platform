using System.Security.Claims;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Database;
using WebApplication3.Models;

namespace WebApplication3.Controllers;
[Route("api/[controller]")] 
[ApiController]
public class GoogleAuthController : ControllerBase
{
    private readonly ApplicationDbContext context;
    
    
    public GoogleAuthController(ApplicationDbContext context)
    {
        this.context = context;
        ;
        
    }
    [HttpPost]
    public async Task<IActionResult> GoogleAuth([FromBody] GoogleTokenDto googleToken ) 
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(googleToken.id_token, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { "648168589757-6vlsm9sbr3lsmu8avg7q3s8fbbshaj1l.apps.googleusercontent.com" } // <-- Replace with your real Google Client ID
            });

            // Extract user info
            var email = payload.Email;
            var fullName = payload.Name ?? "";
            var googleId = payload.Subject;

            string[] nameParts = fullName.Split(' ');
            string firstName = nameParts[0];
            string lastName = nameParts.Length > 1 ? string.Join(" ", nameParts.Skip(1)) : "";

            // Optional: Check if user already exists
            var existingUser = context.SignupPage.FirstOrDefault(u => u.Email == email);
            if (existingUser == null)
            {
                // Save new user
                var newUser = new SignUpPage
                {
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    GoogleId = googleId
                };

                context.SignupPage.Add(newUser);
                await context.SaveChangesAsync();
            }

            return Ok(new
            {
                message = "User signed in successfully",
                email,
                name = fullName
            });
        }
        catch (InvalidJwtException)
        {
            return Unauthorized("Invalid ID token.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "Internal server error.");
        }
        


    }
    
}