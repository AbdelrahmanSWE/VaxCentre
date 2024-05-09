using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace VaxCentre.Server.Dtos.VaccineCentre
{
    public class UpdateVaccineCentreDto
    {
        public string? Address { get; set; }
        public string? DisplayName { get; set; }
    }
}
