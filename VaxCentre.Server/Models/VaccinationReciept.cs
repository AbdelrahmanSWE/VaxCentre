namespace VaxCentre.Server.Models
{
    public class VaccinationReciept
    {
        public required Patient Patient { get; set; }
        public required Vaccine Vaccine { get; set; }
        public required VaccineCentre VaccineCentre { get; set; }
        public required DateOnly VaccineDose1Date { get; set; }
        public int Dose1State { get; set; } = 0;
        public DateOnly VaccineDose2Date { get; set; }
        public int Dose2State { get; set; } = 0;
    }
}
