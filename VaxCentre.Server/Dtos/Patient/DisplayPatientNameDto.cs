using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace VaxCentre.Server.Dtos.Patient
{
    public class DisplayPatientNameDto
    {
        [Required, NotNull]
        public int Id { get; set; }
        [Required, NotNull]
        public string? FirstName { get; set; }
        [Required, NotNull]
        public string? LastName { get; set; }
    }
}
