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
    public class Detail_importbillRepository : GenericRepository<Detail_importbill>, IDetail_importbillRepository
    {
        public Detail_importbillRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<List<GetDetail_importbillDto>> GETBYID(Guid id)
        {
            var data = from a in _DbContext.Detail_importbill
                       join b in _DbContext.Importbill on a.IdImportbillId equals b.Id
                       join c in _DbContext.User on b.IdStaff equals c.Id
                       join d in _DbContext.Products on a.Idproduct equals d.Id
                       where a.IdImportbillId == id
                       select new GetDetail_importbillDto
                       {
                           Id = a.Id,
                           IdImportbillId = a.IdImportbillId,
                           Idsize = a.Idsize,
                           Idproduct = a.Idproduct,
                           image = d.Image,
                           productName = d.Name,
                           Price = a.Price,
                           Quantity = a.Quantity,
                           userName = c.UserName,
                           address = c.Address,
                           phone = c.PhoneNumber,
                           email = c.Email,
                           toTal = b.Price
                       };
            return await data.ToListAsync();
        }
    }
}
