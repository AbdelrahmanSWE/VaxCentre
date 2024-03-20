namespace VaxCentre.Server.Models
{
    public class Admin
    {
        public int ID { get; set; }
        public required string username { get; set; }
        public required string password { get; set; }
    }
}
