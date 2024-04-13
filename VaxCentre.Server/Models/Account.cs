using Microsoft.AspNetCore.Identity;

namespace VaxCentre.Server.Models
{
    public class Account : IdentityUser
    {
        public String Name { get; set; }
    }
}
