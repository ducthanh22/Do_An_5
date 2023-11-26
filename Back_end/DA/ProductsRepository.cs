
using AutoMapper;
using Back_end.Model;
using DAL.Interface;
using DTO;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL
{
    public class ProductsRepository : GenericRepository<Products>, IProductsRepository
    {
        public ProductsRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<ProductsDto>> Search(string keyword, int page, int pageSize)
        {

            var query = from d in _DbContext.Set<Products>().AsQueryable()
                        where string.IsNullOrEmpty(keyword) || d.Name.Contains(keyword)
                        select new ProductsDto
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Idcategories = d.Idcategories,
                            Idproduces = d.Idproduces,
                            Describe=d.Describe
                           
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
        public async Task<UpLoadFile> UploadFile(UpLoadFile product)
        {
            try
            {
                // Tạo tên mới cho tệp tin hình ảnh
                var newFileName = $"{Guid.NewGuid().ToString()}-{product.Img.FileName}";
                var newFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", newFileName);

                // Kiểm tra nếu UpfileProduct đã có một hình ảnh
                if (!string.IsNullOrEmpty(product.Image))
                {
                    // Nếu đã có hình ảnh, xóa tệp tin hình ảnh cũ
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", product.Image);
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }
                // Lưu tệp tin hình ảnh mới
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await product.Img.CopyToAsync(stream);
                }

                // Cập nhật thông tin trong đối tượng UpfileProduct
                product.Image = newFileName;

                // Lấy sản phẩm từ cơ sở dữ liệu dựa trên ID
                var existingProduct = await _DbContext.Products.FindAsync(product.Id);

                // Nếu sản phẩm được tìm thấy, cập nhật hình ảnh của nó
                if (existingProduct != null)
                {
                    existingProduct.Image = newFileName;
                    await _DbContext.SaveChangesAsync();
                }
                else
                {
                    // Xử lý trường hợp không tìm thấy sản phẩm với ID đã chỉ định
                    // Bạn có thể muốn ném một ngoại lệ hoặc xử lý theo yêu cầu của ứng dụng
                }

                return product;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ, ghi log, hoặc ném một ngoại lệ cụ thể hơn tùy thuộc vào yêu cầu của ứng dụng
                throw ex;
            }


        }

    }
}