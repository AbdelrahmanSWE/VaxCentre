using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VaxCentre.Server.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
            Task<List<T>> GetAllAsync();
            Task<T?> GetByIdAsync(int id);
            Task<T?> CreateAsync(T entity);
            Task<T?> UpdateAsync(T entity, int id);
            Task<bool> DeleteAsync(int id);
            Task<bool> IsExist(int id);      
    }
}
}
