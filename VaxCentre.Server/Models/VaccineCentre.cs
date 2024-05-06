namespace VaxCentre.Server.Models
{
    public class VaccineCentre : Account
    {
        public string? DisplayName { get; set; }
        public string? Address { get; set; }
        public List<Vaccine>? Vaccines { get; set; }
    }
}
