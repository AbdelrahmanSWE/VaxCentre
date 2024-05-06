using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Data.Interfaces
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DBContext _context;
        public AccountRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<Account?> GetByUserNameAsync(string UserName)
        {
            try
            {
                var Result = await _context.Accounts.FirstOrDefaultAsync(x=> x.UserName!=null && x.UserName.Equals(UserName));
                if (Result != null) return Result;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the entity with Id.", ex);
            }
        }
    }
}
