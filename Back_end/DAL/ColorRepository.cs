using AutoMapper;
using DAL.Interface;
using Model;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ColorRepository : GenericRepository<Color>,IColorRepository
    {
        public ColorRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<ColorDto>>Search(Paging paging)
        {
            var query = from a in _DbContext.Color
                        where (string.IsNullOrEmpty(paging.Keyword)||a.NameColor == paging.Keyword)
                        select new ColorDto
                        {
                            Id = a.Id,
                            NameColor = a.NameColor,
                            Created = a.Created,
                            Modified = a.Modified,
                            ActiveFlag = a.ActiveFlag,
                            CreatedBy = a.CreatedBy,
                            ModifiedBy = a.ModifiedBy,
                            Colorformat = a.Colorformat,
                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<ColorDto>
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                Keyword = paging.Keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;

        }

       
    }
}
