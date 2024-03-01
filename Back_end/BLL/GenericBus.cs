using BUS.Interface;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Interface;
using DTO;

namespace BUS
{
    public class GenericBus<T> : IGenericBUS<T> where T : class
    {
        private readonly IGenericRepository<T> _res;

        public GenericBus(IGenericRepository<T> res)
        {
            _res = res;
        }

        public async Task<List<T>> GetAll()
        {
            return await _res.GetAll();
        }

        public async Task<T> Getbyid(Guid id)
        {
            return await _res.Getbyid(id);
        }
        public async Task<T> Create(T entity)
        {
            return await _res.Create(entity);
        }

        public async Task<T> Update(T entity)
        {
            return await _res.Update(entity);
        }

        public async Task<T> Delete(Guid id)
        {
            return await _res.Delete(id);
        }
        


    }
}
