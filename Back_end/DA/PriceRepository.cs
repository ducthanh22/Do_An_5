using AutoMapper;
using DAL.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PriceRepository : GenericRepository<Price>, IPriceRepository
    {
        public PriceRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
