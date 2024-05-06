using System.ComponentModel.DataAnnotations;

namespace VaxCentre.Server.Models
{
    public class Patient : Account
    {
        public string? SSID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public int AcceptState { get; set; } = 0;
    }
}
