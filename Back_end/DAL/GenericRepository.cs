using Back_end.Model;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        protected Achino_DbContext _DbContext;

        public GenericRepository(Achino_DbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<IReadOnlyList<T >> GetAll()
        {
            return await _DbContext.Set<T>().ToListAsync();
        }

        public async Task<T> Getbyid(int id)
        {
            return await _DbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Create(T entity)
        {
            _DbContext.AddAsync(entity);
            await _DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _DbContext.Entry(entity).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await _DbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _DbContext.Set<T>().Remove(entity);
                await _DbContext.SaveChangesAsync();
            }
            return entity;
        }
       
        
    }
}
