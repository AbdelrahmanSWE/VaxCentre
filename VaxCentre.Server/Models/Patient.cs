namespace VaxCentre.Server.Models
{
    public class Patient
    {
        public required string SSID { get; set; }
        public required string password { get; set; }
        public required string firstName { get; set; }
        public required string lastName { get; set; }
        public required string email { get; set; }
        public required string address { get; set; }
        public string? phone { get; set; }
        public required int state { get; set; } = 0;
    }
}
