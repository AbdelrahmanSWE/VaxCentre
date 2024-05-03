using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VaxCentre.Server.Interfaces
{
    public interface IGenericRepository<T, TId> where T : class
    {
            Task<List<T>> GetAllAsync();
            Task<T?> GetByIdAsync(TId Id);
            Task<T?> CreateAsync(T entity);
            Task<T?> UpdateAsync(T entity, TId Id);
            Task<bool> DeleteAsync(TId Id);
            Task<bool> IsExist(TId Id);      
    }
}
}
