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
    public class SizeRepository :GenericRepository<Size>,ISizeRepository
    {
        public SizeRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<List<SizeDto>>Getbyidproduct(Guid id)
        {
            var query = from a in _DbContext.Size
                        where a.Idproduct == id
                        select new SizeDto 
                        { 
                        Id = a.Id,
                        Idproduct = a.Idproduct,
                        NameSize = a.NameSize,
                        };

            return await query.ToListAsync();
        }

    }
}
