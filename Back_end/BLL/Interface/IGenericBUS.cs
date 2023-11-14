using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Interface
{
    public interface IGenericBUS<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Getbyid(int id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
     






    }
}
