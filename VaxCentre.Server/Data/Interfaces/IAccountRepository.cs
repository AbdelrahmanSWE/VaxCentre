using VaxCentre.Server.Models;

namespace VaxCentre.Server.Data.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> GetByUserNameAsync(string UserName);

    }
}
