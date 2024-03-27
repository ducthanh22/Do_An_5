
using AutoMapper;
using DAL.Interface;
using DTO;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Model;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DAL
{
    public class ProductsRepository : GenericRepository<Products>, IProductsRepository
    {
        public ProductsRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<GetProductsDto>> Search(string keyword, int page, int pageSize)
        {
            var query = from d in _DbContext.Set<Products>()

                        join c in _DbContext.Set<Color>() on d.Idcolor equals c.Id
                        join b in _DbContext.Set<Price>() on d.Id equals b.Idproduct into bGroup
                        from b in bGroup.DefaultIfEmpty()
                        join e in _DbContext.Set<Categories>() on d.Idcategories equals e.Id
                        join g in _DbContext.Set<Produces>() on d.Idproduces equals g.Id
                        where ( string.IsNullOrEmpty(keyword)|| d.Name.Contains(keyword))
                        select new GetProductsDto
                        {

                            Id = d.Id,
                            Name = d.Name,
                            Idcategories = d.Idcategories,
                            Idproduces = d.Idproduces,
                            Describe = d.Describe,
                            namecolor = c.NameColor,
                            Price_product = b.Price_product,
                            Image = d.Image,
                            Idcolor = d.Idcolor,
                            Namecategory = e.Name,
                            NameProduces = g.Name,
                            Created = d.Created,
                            ListSize = _DbContext.Size.Where(a => a.Idproduct == d.Id).Select(m => new SizeDto
                            {
                                Id = m.Id,
                                Idproduct = m.Idproduct,
                                NameSize = m.NameSize
                            }).ToList()

                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<GetProductsDto>
            {
                PageIndex = page,
                PageSize = pageSize,
                Keyword = keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;
        }
        public async Task<List<GetProductsDto>> Getalls()
        {
            try
            {
                var query = from d in _DbContext.Set<Products>()
                            
                            join c in _DbContext.Set<Color>() on d.Idcolor equals c.Id
                            join b in _DbContext.Set<Price>() on d.Id equals b.Idproduct into bGroup
                            from b in bGroup.DefaultIfEmpty()
                            join e in _DbContext.Set<Categories>() on d.Idcategories equals e.Id
                            join g in _DbContext.Set<Produces>() on d.Idproduces equals g.Id
                            select new GetProductsDto
                            {

                                Id = d.Id,
                                Name = d.Name,
                                Idcategories = d.Idcategories,
                                Idproduces = d.Idproduces,
                                Describe = d.Describe,
                                namecolor = c.NameColor,
                                Price_product = b.Price_product,
                                Image = d.Image,
                                Idcolor = d.Idcolor,
                                Namecategory = e.Name,
                                NameProduces = g.Name,
                                Created = d.Created,
                                ListSize = _DbContext.Size.Where(a => a.Idproduct == d.Id).Select(m => new SizeDto
                                {
                                    Id = m.Id,
                                    Idproduct = m.Idproduct,
                                    NameSize = m.NameSize
                                }).ToList()

                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // Xử lý hoặc ghi log lỗi ở đây
                Console.WriteLine("Lỗi trong phương thức Getalls: " + ex.Message);
                throw; // Rethrow ngoại lệ để bảo toàn thông tin lỗi và đưa ra cho lớp gọi xử lý tiếp
            }


        }

        public async Task<IQueryable<GetProductsDto>> GetByIds(Guid ids)
        {
            var query  = from d in _DbContext.Set<Products>()
                         
                         join c in _DbContext.Set<Color>() on d.Idcolor equals c.Id
                         join b in _DbContext.Set<Price>() on d.Id equals b.Idproduct into bGroup
                         from b in bGroup.DefaultIfEmpty()
                         join e in _DbContext.Set<Categories>() on d.Idcategories equals e.Id
                         join g in _DbContext.Set<Produces>() on d.Idproduces equals g.Id
                         where d.Id == ids
                        select new GetProductsDto
                        {

                            Id = d.Id,
                            Name = d.Name,
                            Idcategories = d.Idcategories,
                            Idproduces = d.Idproduces,
                            Describe = d.Describe,
                            namecolor = c.NameColor,
                            Price_product = b.Price_product,
                            Image = d.Image,
                            Idcolor = d.Idcolor,
                            Namecategory = e.Name,
                            NameProduces = g.Name,
                            Created = d.Created,
                            ListSize = _DbContext.Size.Where(a => a.Idproduct == d.Id).Select(m => new SizeDto
                            {
                                Id = m.Id,
                                Idproduct = m.Idproduct,
                                NameSize = m.NameSize
                            }).ToList()
                        };

           
            return query;
        }
        public async Task<List<GetProductsDto>> GetProductNew()
        {

            var query = (from d in _DbContext.Set<Products>()
                       
                         join c in _DbContext.Set<Color>() on d.Idcolor equals c.Id
                         join b in _DbContext.Set<Price>() on d.Id equals b.Idproduct into bGroup
                         from b in bGroup.DefaultIfEmpty()
                         join e in _DbContext.Set<Categories>() on d.Idcategories equals e.Id
                         join g in _DbContext.Set<Produces>() on d.Idproduces equals g.Id

                         orderby d.Created descending
                         select new GetProductsDto
                         {

                             Id = d.Id,
                             Name = d.Name,
                             Idcategories = d.Idcategories,
                             Idproduces = d.Idproduces,
                             Describe = d.Describe,
                             namecolor = c.NameColor,
                             Price_product = b.Price_product,
                             Image = d.Image,
                             Idcolor = d.Idcolor,
                             Namecategory = e.Name,
                             NameProduces = g.Name,
                             Created = d.Created,
                             ListSize = _DbContext.Size.Where(a=>a.Idproduct== d.Id).Select(m=>new SizeDto
                             {
                                 Id=m.Id,
                                 Idproduct=m.Idproduct,
                                 NameSize=m.NameSize
                             }).ToList()
                         }).Take(8);



            return await query.ToListAsync();
        }
        public async Task<ProductsDto> Creates(ProductsDto entity)
        {
            var product = new Products
            {
                Describe = entity.Describe,
                Name = entity.Name,
                Idcategories = entity.Idcategories,
                Idproduces = entity.Idproduces,
                Idcolor = entity.Idcolor,
                Image=entity.Image,
                Created = DateTime.Now,
            };
            _DbContext.Set<Products>().Add(product);
            await _DbContext.SaveChangesAsync();
            entity.Id = product.Id;
            var price = new Price
            {
                Idproduct = entity.Id,
                Price_product = entity.Price_product,
                Created = DateTime.Now,
            };
            _DbContext.Set<Price>().Add(price);
            await _DbContext.SaveChangesAsync();
            foreach (var item in entity.ListSize)
            {
                SizeDto sizeDto = new SizeDto
                {
                   Idproduct = product.Id,
                   NameSize = item.NameSize,
                   
                };

                var SizeEntity = _mapper.Map<Size>(sizeDto);
                await _DbContext.Size.AddAsync(SizeEntity);
                await _DbContext.SaveChangesAsync();
            }
           
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
                product.Idcolor = entity.Idcolor;
                product.Modified = DateTime.Now;
                _DbContext.Set<Products>().Update(product);
                await _DbContext.SaveChangesAsync();
            }
            var price = await _DbContext.Set<Price>().FirstOrDefaultAsync(p => p.Idproduct == entity.Id);
            if (price != null)
            {
                // Cập nhật giá cho đối tượng Price
                price.Price_product = entity.Price_product;
                price.Modified = DateTime.Now;
                _DbContext.Set<Price>().Update(price);
                await _DbContext.SaveChangesAsync();
            }
            foreach (var item in entity.ListSize)
            {
                SizeDto sizeDto = new SizeDto
                {
                    Id = item.Id,
                    Idproduct = entity.Id,
                    NameSize = item.NameSize,

                };
                var SizeEntity = _mapper.Map<Size>(sizeDto);
                var existingSize = await _DbContext.Size.FirstOrDefaultAsync(s => s.Id== item.Id && s.Idproduct== item.Idproduct );
                if (existingSize != null)
                {
                    existingSize.NameSize=item.NameSize;
                     _DbContext.Size.Update(existingSize);
                    await _DbContext.SaveChangesAsync();
                }
                else
                {
                    _DbContext.Size.Add(SizeEntity);
                    await _DbContext.SaveChangesAsync();
                }


            }
            return entity;
        }



        public async Task<UpLoadFile> UploadFile(UpLoadFile img)
        {
            try
            {
                var data = await _DbContext.Set<Products>().FindAsync(img.Id);

                // Tạo tên mới cho tệp tin hình ảnh
                var newFileName = $"{Guid.NewGuid().ToString()}-{img.Img.FileName}";
                var newFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", newFileName);

                // Kiểm tra nếu UpfileProduct đã có một hình ảnh
                if (!string.IsNullOrEmpty(data.Image))
                {
                    // Nếu đã có hình ảnh, xóa tệp tin hình ảnh cũ
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", data.Image);
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }
                // Lưu tệp tin hình ảnh mới
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await img.Img.CopyToAsync(stream);
                }

                // Cập nhật thông tin trong đối tượng UpfileProduct
                data.Image = newFileName;

                // Lấy sản phẩm từ cơ sở dữ liệu dựa trên ID
                var existingdata = await _DbContext.Products.FindAsync(data.Id);

                // Nếu sản phẩm được tìm thấy, cập nhật hình ảnh của nó
                if (existingdata != null)
                {
                    existingdata.Image = newFileName;
                    await _DbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception();
                }

                return img;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ, ghi log, hoặc ném một ngoại lệ cụ thể hơn tùy thuộc vào yêu cầu của ứng dụng
                throw ex;
            }


        }

    }
}