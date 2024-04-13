using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace VaxCentre.Server.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        public string? UserName { get; set; }
        
        [Required]
        public string? Password { get; set; }
    }
}
