namespace VaxCentre.Server.Models
{
    public class VaccineCentre : Account
    {
        public required string displayName { get; set; }
        public required string Address { get; set; }
        public List<Vaccine>? Vaccines { get; set; }
    }
}
