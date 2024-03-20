namespace VaxCentre.Server.Models
{
    public class Vaccine
    {
        public int ID { get; set; }
        public required string name { get; set; }
        public required string description { get; set; }
        public string? precaution { get; set; }
        public int? gapTime { get; set; }
    }
}
