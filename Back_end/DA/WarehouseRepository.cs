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
    public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<WarehouseDto>> Search(string keyword, int page, int pageSize)
        {

            var query = from d in _DbContext.Set<Warehouse>().AsQueryable()
                        where ( string.IsNullOrEmpty(keyword)||d.Name.Contains(keyword.ToLower()) ||d.Address.Contains(keyword.ToLower()))
                        select new WarehouseDto
                        {
                            Id = d.Id,
                            Name = d.Name,
                            
                            Address = d.Address,
                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<WarehouseDto>
            {
                PageIndex = page,
                PageSize = pageSize,
                Keyword = keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;
        }
    }
}
