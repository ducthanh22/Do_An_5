using BLL.Interface;
using DAL.Interface;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Product_typeBus: GenericBus<Product_type>,IProduct_typeBus
    {
        public IProduct_typeRepository _res;
        public Product_typeBus(IProduct_typeRepository res) :base(res){ 
            _res = res;
        }
        public async Task<BaseQuerieResponse<Product_typeDto>> Search(Paging paging)
        {
            return await _res.Search(paging);
        }
        public async Task<List<Product_typeDto>> GetByCategory(Guid id)
        {
            return await _res.GetByCategory(id);
        }
    }
}
