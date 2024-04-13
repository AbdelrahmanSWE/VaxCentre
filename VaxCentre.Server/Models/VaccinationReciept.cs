namespace VaxCentre.Server.Models
{
    public class VaccinationReciept
    {
        public string Id { get; set; }
        public required Patient Patient { get; set; }
        public required Vaccine Vaccine { get; set; }
        public required VaccineCentre VaccineCentre { get; set; }
        public required DateTime VaccineDose1Date { get; set; }
        public int Dose1State { get; set; } = 0;
        public DateTime VaccineDose2Date { get; set; }
        public int Dose2State { get; set; } = 0;
    }
}
