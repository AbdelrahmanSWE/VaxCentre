using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Dtos.Vaccine
{
    public class CreateVaccineDto
    {
        [Required, NotNull]
        public string? Name { get; set; }
        [Required, NotNull]
        public string? Description { get; set; }
        public string? Precaution { get; set; }
        public int? GapTime { get; set; }
    }
}
