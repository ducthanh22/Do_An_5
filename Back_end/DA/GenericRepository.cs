using AutoMapper;

using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;


namespace DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public Achino_DbContext _DbContext;

        public readonly IMapper _mapper;
        public GenericRepository(Achino_DbContext dbContext, IMapper mapper)
        {
            _DbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<T>> GetAll()
        {
            var result = await _DbContext.Set<T>().ToListAsync(); 

            return result;
        }

        public async Task<T> Getbyid(int id)
        {
            var result = await _DbContext.Set<T>().FindAsync(id); // Assuming _DbContext is of type DbContext
          
            return result;
        }
        public async Task<T> Create(T entity)
        {
            _DbContext.Set<T>().AddAsync(entity);
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
