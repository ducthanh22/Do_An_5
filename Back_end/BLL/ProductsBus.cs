using Back_end.Model;
using BUS.Interface;
using DAL.Interface;
using DTO;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ProductsBus : GenericBus<Products>, IProductsBus
    {
        public IProductsRepository _res;
        public ProductsBus(IProductsRepository res) : base(res)
        {
            _res = res;
        }
        public async Task<BaseQuerieResponse<ProductsDto>> Search(string keywork, int page, int pageSize)
        {
            return await _res.Search(keywork, page, pageSize);
        }
    }
}
