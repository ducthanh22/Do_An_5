using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IProduct_typeBus : IGenericBUS<Product_type>
    {
        Task<BaseQuerieResponse<Product_typeDto>> Search(Paging paging);
        Task<List<Product_typeDto>> GetByCategory(Guid id);
    }
}
