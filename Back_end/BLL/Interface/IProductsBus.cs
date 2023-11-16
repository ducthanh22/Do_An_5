using Back_end.Model;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Interface
{
    public interface IProductsBus : IGenericBUS<Products>
    {
        Task<BaseQuerieResponse<ProductsDto>> Search(string keywork, int page, int pageSize);

    }
}
