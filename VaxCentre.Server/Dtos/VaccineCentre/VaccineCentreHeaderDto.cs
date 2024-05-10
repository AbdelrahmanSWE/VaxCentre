using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace VaxCentre.Server.Dtos.VaccineCentre
{
    public class VaccineCentreHeaderDto
    {
        [Required]
        public int Id { get; set; }
        [Required, NotNull]
        public string? displayName {  get; set; }
    }
}
