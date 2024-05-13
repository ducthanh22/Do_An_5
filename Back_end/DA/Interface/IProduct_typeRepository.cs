using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IProduct_typeRepository : IGenericRepository<Product_type>
    {
        Task<BaseQuerieResponse<Product_typeDto>> Search(Paging paging);

    }
}
