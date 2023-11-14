
using AutoMapper;
using Back_end.Model;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL
{
    public class ProductsRepository : GenericRepository<Products>, IProductsRepository
    {
        public ProductsRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}