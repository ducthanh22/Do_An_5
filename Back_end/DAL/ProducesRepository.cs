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
    public class ProducesRepository : GenericRepository<Produces>, IProducesRepository
    {
        public ProducesRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<ProducesDto>> Search(string keyword, int page, int pageSize)
        {

            var query = from d in _DbContext.Set<Produces>().AsQueryable()
                        where string.IsNullOrEmpty(keyword) || d.Name.Contains(keyword)
                        select new ProducesDto
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Email = d.Email,
                            Address = d.Address,
                            Phone=d.Phone,
                            Image=d.Image
                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<ProducesDto>
            {
                PageIndex = page,
                PageSize = pageSize,
                Keyword = keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;
        }
        public async Task<UpFile> UpImg(UpFile produces)
        {
            try
            {
                var data = await _DbContext.Set<Produces>().FindAsync(produces.Id);

                // Tạo tên mới cho tệp tin hình ảnh
                var newFileName = $"{Guid.NewGuid().ToString()}-{produces.Img.FileName}";
                var newFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Img_Brand", newFileName);

                // Kiểm tra nếu UpfileProduct đã có một hình ảnh
                if (data.Image !="string"&& data.Image!=null)
                {
                    // Nếu đã có hình ảnh, xóa tệp tin hình ảnh cũ
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Img_Brand", data.Image);
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }
                // Lưu tệp tin hình ảnh mới
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await produces.Img.CopyToAsync(stream);
                }

                // Cập nhật thông tin trong đối tượng UpfileProduct
                produces.Image = newFileName;

                // Lấy sản phẩm từ cơ sở dữ liệu dựa trên ID
                var existingproduces = await _DbContext.Produces.FindAsync(produces.Id);

                // Nếu sản phẩm được tìm thấy, cập nhật hình ảnh của nó
                if (existingproduces != null)
                {
                    existingproduces.Image = newFileName;
                    await _DbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception();
                }

                return produces;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ, ghi log, hoặc ném một ngoại lệ cụ thể hơn tùy thuộc vào yêu cầu của ứng dụng
                throw ex;
            }


        }
    }

   
}


