using AutoMapper;
using DAL.Interface;
using DTO;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Detail_exportbillRepository : GenericRepository<Detail_exportbill>, IDetail_exportbillRepository
    {
        public Detail_exportbillRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<countProduct> CountProduct(Guid id)
        {
            var query = from a in _DbContext.Detail_exportbill
                        where a.Idproduct == id
                        group a by a.Idproduct into g
                        select new countProduct
                        {
                            Idproduct = g.Key,
                            TotalQuantity = g.Sum(x => x.Quantity) 
                        };

            var result = await query.FirstOrDefaultAsync();

            if (result == null)
            {
                return new countProduct
                {
                    Idproduct = id,
                    TotalQuantity = 0
                };
            }
            return result;
        }
        public async Task<List<GetDetail_exportbillDto>>GETBYID(Guid id)
        {
            var data = from a in _DbContext.Detail_exportbill
                       join b in _DbContext.Exportbill on a.IdExportbill equals b.Id
                       join c in _DbContext.User on b.IdStaff equals c.Id
                       join d in _DbContext.Products on a.Idproduct equals d.Id
                       where a.IdExportbill == id
                       select new GetDetail_exportbillDto
                       {
                           Id = a.Id,
                           IdExportbill = a.IdExportbill,
                           Idsize = a.Idsize,
                           Idproduct = a.Idproduct,
                           image = d.Image,
                           productName = d.Name,
                           Price = a.Price,
                           Quantity = a.Quantity,
                           userName = c.UserName,
                           address = c.Address,
                           phone = c.PhoneNumber,
                           email=c.Email,
                           toTal= b.Price
                       };
            return await data.ToListAsync();
        }
        

    }
}
