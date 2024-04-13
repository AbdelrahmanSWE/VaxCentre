using System.ComponentModel.DataAnnotations;

namespace VaxCentre.Server.Models
{
    public class Patient : Account
    {
        public required string SSID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Address { get; set; }
        public required int AcceptState { get; set; } = 0;
    }
}
