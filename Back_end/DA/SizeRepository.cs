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
    public class SizeRepository :GenericRepository<Size>,ISizeRepository
    {
        public SizeRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

    }
}
