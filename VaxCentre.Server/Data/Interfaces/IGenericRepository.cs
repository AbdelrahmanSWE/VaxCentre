using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VaxCentre.Server.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int Id);
        Task<T?> CreateAsync(T entity);
        Task<bool> DeleteAsync(int Id);
        Task<bool> IsExist(int Id);
    }
}

