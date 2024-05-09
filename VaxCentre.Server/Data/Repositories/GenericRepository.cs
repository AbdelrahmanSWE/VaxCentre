using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data.Interfaces;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DBContext _context;
        private DbSet<T> _entity;
        public GenericRepository(DBContext context) 
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public async Task<T?> CreateAsync(T obj)
        {
            try
            {
                await _entity.AddAsync(obj);
                var saveResult = await _context.SaveChangesAsync();
                if (saveResult > 0) return obj;
                return null;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"An error occurred while saving changes to the database. {obj.GetType()} in query: {obj}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred while creating the {obj.GetType()} in query: {obj}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var Deleted = await _entity.FindAsync(Id);
                if (Deleted == null) return false;
                _entity.Remove(Deleted);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while deleting the entity from the database.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }


        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                var Result = await _entity.ToListAsync();
                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all entities.", ex);
            }
        }

        public async Task<T?> GetByIdAsync(int Id)
        {
            try
            {
                var Result = await _entity.FindAsync(Id);
                if (Result != null) return Result;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the entity with Id {Id}.", ex);
            }
        }

        public async Task<bool> IsExist(int Id)
        {
            try
            {
                var Result = await _entity.FindAsync(Id);
                if (Result != null) return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking the existence of the vaccine with Id {Id}.", ex);
            }
        }

    }
}
