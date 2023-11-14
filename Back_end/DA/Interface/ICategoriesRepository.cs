using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Back_end.Model;
using DTO;

namespace DAL.Interface
{
    public interface ICategoriesRepository : IGenericRepository<Categories>
    {
        //Task<IEnumerable<Categories>> Search(string searchTerm, int page, int pageSize);
    }
}
