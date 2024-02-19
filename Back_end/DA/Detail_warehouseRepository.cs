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
    public class Detail_warehouseRepository : GenericRepository<Detail_warehouseDto>, IDetail_warehouseRepository
    {
        public Detail_warehouseRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
       
    }
}
