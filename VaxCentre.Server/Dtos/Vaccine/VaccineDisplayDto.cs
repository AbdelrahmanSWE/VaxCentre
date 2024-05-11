namespace VaxCentre.Server.Dtos.Vaccine
{
    public class VaccineDisplayDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Precaution { get; set; }
        public int? GapTime { get; set; }
    }
}
