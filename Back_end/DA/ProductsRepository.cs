
using AutoMapper;
using DAL.Interface;
using DTO;
using Microsoft.EntityFrameworkCore;
using Model;
using Org.BouncyCastle.Crypto;

namespace DAL
{
    public class ProductsRepository : GenericRepository<Products>, IProductsRepository
    {
        public ProductsRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<ProductsDto>> Search(string keyword, int page, int pageSize)
        {

            var query = from d in _DbContext.Set<Products>()
                        join a in _DbContext.Set<Color>() on d.Id equals a.Id into aGroup
                        from a in aGroup.DefaultIfEmpty()
                        join b in _DbContext.Set<Price>() on d.Id equals b.Id into bGroup
                        from b in bGroup.DefaultIfEmpty()
                        join e in _DbContext.Set<Size>() on a.Id equals e.Id into eGroup
                        from e in eGroup.DefaultIfEmpty()
                        where ( d.Name.Contains(keyword))
                        select new ProductsDto
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Idcategories = d.Idcategories,
                            Idproduces = d.Idproduces,
                            Describe = d.Describe,
                            NameColor = a != null ? a.NameColor : null,
                            Price_product = b != null ? b.Price_product : 0,
                            NameSize = e != null ? e.NameSize : null
                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<ProductsDto>
            {
                PageIndex = page,
                PageSize = pageSize,
                Keyword = keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;
        }
        public async Task<List<ProductsDto>> Getalls()
        {
            var query = from d in _DbContext.Set<Products>()
                        join a in _DbContext.Set<Color>() on d.Id equals a.Id into aGroup
                        from a in aGroup.DefaultIfEmpty()
                        join b in _DbContext.Set<Price>() on d.Id equals b.Id into bGroup
                        from b in bGroup.DefaultIfEmpty()
                        join e in _DbContext.Set<Size>() on a.Id equals e.Id into eGroup
                        from e in eGroup.DefaultIfEmpty()
                        select new ProductsDto
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Idcategories = d.Idcategories,
                            Idproduces = d.Idproduces,
                            Describe = d.Describe,
                            NameColor = a != null ? a.NameColor : null, // Kiểm tra xem a có tồn tại không trước khi truy cập thuộc tính NameColor
                            Price_product = b != null ? b.Price_product : 0, // Kiểm tra xem b có tồn tại không trước khi truy cập thuộc tính Price_product
                            NameSize = e != null ? e.NameSize : null // Kiểm tra xem e có tồn tại không trước khi truy cập thuộc tính NameSize
                        };
            return await query.ToListAsync();
        }

        public async Task<IQueryable<ProductsDto>> GetByIds(int ids)
        {
            var query = from d in _DbContext.Set<Products>()
                        join a in _DbContext.Set<Color>() on d.Id equals a.Id into aGroup
                        from a in aGroup.DefaultIfEmpty()
                        join b in _DbContext.Set<Price>() on d.Id equals b.Id into bGroup
                        from b in bGroup.DefaultIfEmpty()
                        join e in _DbContext.Set<Size>() on a.Id equals e.Id into eGroup
                        from e in eGroup.DefaultIfEmpty()
                        where d.Id == ids
                        select new ProductsDto
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Idcategories = d.Idcategories,
                            Idproduces = d.Idproduces,
                            Describe = d.Describe,
                            NameColor = a != null ? a.NameColor : null, 
                            Price_product = b != null ? b.Price_product : 0, 
                            NameSize = e != null ? e.NameSize : null
                        };

           
            return query;
        }
        public async Task<ProductsDto> Creates(ProductsDto entity)
        {
            var product = new Products
            {
                Describe = entity.Describe,
                Name = entity.Name,
                Idcategories = entity.Idcategories,
                Idproduces = entity.Idproduces,
                Created = DateTime.Now,
        };
            _DbContext.Set<Products>().Add(product);
            await _DbContext.SaveChangesAsync();
            var price = new Price
            {
                Idproduct = product.Id,
                Price_product=entity.Price_product,
                Created = DateTime.Now,
        };
            _DbContext.Set<Price>().Add(price);
            await _DbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<ProductsDto> Updates(ProductsDto entity)
        {
            // Tìm thấy đối tượng Products dựa trên ID
            var product = await _DbContext.Set<Products>().FindAsync(entity.Id);
            if (product != null)
            {
                product.Describe = entity.Describe;
                product.Name = entity.Name;
                product.Idcategories = entity.Idcategories;
                product.Idproduces = entity.Idproduces;
                product.Modified = DateTime.Now;
                // Cập nhật đối tượng Products
                _DbContext.Set<Products>().Update(product);
                await _DbContext.SaveChangesAsync();
            }
            // Tìm thấy đối tượng Price dựa trên ID sản phẩm liên quan
            var price = await _DbContext.Set<Price>().FirstOrDefaultAsync(p => p.Idproduct == entity.Id);
            if (price != null)
            {
                // Cập nhật giá cho đối tượng Price
                price.Price_product = entity.Price_product;
                price.Modified = DateTime.Now;
                // Cập nhật đối tượng Price
                _DbContext.Set<Price>().Update(price);
                await _DbContext.SaveChangesAsync();
            }
            return entity;
        }



        

    }
}