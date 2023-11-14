
using AutoMapper;
using Back_end.Model;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL
{
    public class CategoriesRepository : GenericRepository<Categories>, ICategoriesRepository
    {
        public CategoriesRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
    }
}