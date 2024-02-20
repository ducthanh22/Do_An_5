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
    public class ColorRepository : GenericRepository<Color>,IColorRepository
    {
        public ColorRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<UpLoadFile> UploadFile(UpLoadFile color)
        {
            try
            {
                var data = await _DbContext.Set<Color>().FindAsync(color.Id);

                // Tạo tên mới cho tệp tin hình ảnh
                var newFileName = $"{Guid.NewGuid().ToString()}-{color.Img.FileName}";
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
                    await color.Img.CopyToAsync(stream);
                }

                // Cập nhật thông tin trong đối tượng UpfileProduct
                color.Image = newFileName;

                // Lấy sản phẩm từ cơ sở dữ liệu dựa trên ID
                var existingcolor = await _DbContext.Color.FindAsync(color.Id);

                // Nếu sản phẩm được tìm thấy, cập nhật hình ảnh của nó
                if (existingcolor != null)
                {
                    existingcolor.Image = newFileName;
                    await _DbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception();
                }

                return color;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ, ghi log, hoặc ném một ngoại lệ cụ thể hơn tùy thuộc vào yêu cầu của ứng dụng
                throw ex;
            }


        }
    }
}
