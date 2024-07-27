using AutoMapper;
using DAL.Interface;
using Model;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Order_detailRepository : GenericRepository<Order_detail>, IOrder_detailRepository
    {
        public Order_detailRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
