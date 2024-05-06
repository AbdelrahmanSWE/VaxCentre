namespace VaxCentre.Server.Models
{
    public class VaccinationReciept
    {
        public int Id { get; set; }
        public Patient? Patient { get; set; }
        public Vaccine? Vaccine { get; set; }
        public VaccineCentre? VaccineCentre { get; set; }
        public DateTime VaccineDose1Date { get; set; }
        public int Dose1State { get; set; } = 0;
        public DateTime VaccineDose2Date { get; set; }
        public int Dose2State { get; set; } = 0;
    }
}
