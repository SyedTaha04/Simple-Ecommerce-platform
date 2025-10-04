using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace WebApplication3.ExtraClasses;
using WebApplication3.Models;

public class JwtToken
{
    private readonly IConfiguration configuration;
    
    public JwtToken(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string Create(SignInDto signInDto)
    {
        string secretKey = this.configuration["Jwt:SecretKey"];
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)); 
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Email, signInDto.Email),
                new Claim(ClaimTypes.Role, signInDto.role),
                
            ]),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpireInMinutes")),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
        };
        JsonWebTokenHandler tokenHandler = new JsonWebTokenHandler();
        string token = tokenHandler.CreateToken(descriptor);
        return token;
    }


   
}