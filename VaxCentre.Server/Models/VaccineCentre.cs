namespace VaxCentre.Server.Models
{
    public class VaccineCentre
    {
        public int ID { get; set; }
        public required string displayName { get; set; }
        public required string username { get; set; }
        public required string password { get; set; }
        public required string address { get; set; }
    }
}
