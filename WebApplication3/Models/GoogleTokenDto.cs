using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WebApplication3.Models;

public class GoogleTokenDto
{
    [Required]
    public string id_token { get; set; }
    
    
}