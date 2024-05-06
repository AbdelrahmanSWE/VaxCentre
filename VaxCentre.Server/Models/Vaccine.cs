namespace VaxCentre.Server.Models
{
    public class Vaccine
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Precaution { get; set; }
        public int? GapTime { get; set; }
        public List<VaccineCentre>? AvailableIn { get; set; }
    }
}
