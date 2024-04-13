using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace VaxCentre.Server.Dtos.Account
{
    public class PatientRegisterDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required, NotNull]
        public string? Password { get; set; }
        [Required, NotNull]
        public string? FirstName { get; set; }
        [Required, NotNull]
        public string? LastName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required, NotNull]
        [StringLength(14,ErrorMessage="Enter a valid SSID number",MinimumLength=14)]
        public string? SSID { get; set;}
        [Required, NotNull]
        public string? Address { get; set;}
    }
}
