using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace VaxCentre.Server.Dtos.Account
{
    public class CentreRegisterDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required, NotNull]
        public string? Password { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required, NotNull]
        public string? Address { get; set; }
        [Required, NotNull]
        public string? DisplayName { get; set; }
    }
}
