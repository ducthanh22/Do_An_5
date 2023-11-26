using Back_end.Model;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoriesRepository : GenericRepository<Categories>, ICategoriesRepository
    {
        public CategoriesRepository(Achino_DbContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<IEnumerable<Categories>> Search(string searchTerm, int page, int pageSize)
        {
            // Bắt đầu với truy vấn cơ bản
            var query = from a in _DbContext.Categorie_entities.AsQueryable()
                        where a.Name == searchTerm
                        select a;

            // Thực hiện tìm kiếm nếu có
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) );
            }

            // Thực hiện phân trang
            query = query.Skip((page - 1) * pageSize)
                         .Take(pageSize);

            // Thực hiện truy vấn và lấy kết quả
            var result = await query.ToListAsync();

            return result;
        }

    }

}
