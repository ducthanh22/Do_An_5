using AutoMapper;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using Model;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DAL
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
    
        public SaleRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper) {
   
        }


        public async Task<List<GetSaleDto>> GetSale()
        {
            var query = from a in _DbContext.Sale
                        join b in _DbContext.Products on a.IdProduct equals b.Id
                        join c in _DbContext.Price on a.IdProduct equals c.Idproduct
                        where a.ActiveFlag ==1
                        select new GetSaleDto
                        {
                            Id= a.Id,
                            SalePrice=a.SalePrice,
                            percent=a.percent,
                            SaleTime = a.SaleTime,
                            Name= b.Name,
                            Image=b.Image,
                            Price_product=c.Price_product
                        };

            
            return await query.ToListAsync();
        }

        public async Task<string> UpdateSalesPrices()
        {
            var now = DateTime.Now;
            var activeSales = await _DbContext.Sale.Where(s => s.ActiveFlag == 1 && s.Created <= now).ToListAsync();
            string message = "";

            foreach (var sale in activeSales)
            {
                var elapsedTime = now - sale.Created;
                var remainingTime = sale.SaleTime - (int)elapsedTime?.TotalMinutes;

                if (remainingTime <= 0)
                {
                    var originalPrice = await _DbContext.Price
                        .Where(p => p.Idproduct == sale.IdProduct)
                        .Select(p => p.Price_product)
                        .FirstOrDefaultAsync();

                    sale.SalePrice = originalPrice;
                    sale.ActiveFlag = 0;
                    message += "Sản phẩm " + sale.IdProduct + " đã hết sale.\n";
                    continue; // Tiếp tục với sản phẩm tiếp theo
                }
                else
                {
                    var price = await _DbContext.Price.FirstOrDefaultAsync(p => p.Idproduct == sale.IdProduct);
                    if (price != null)
                    {
                        float a = (float)sale.percent / 100;
                        int discountedPrice = (int)(price.Price_product - (price.Price_product * a));
                        sale.SalePrice = discountedPrice;
                    }
                }

                // Chuyển đổi thời gian còn lại thành chuỗi định dạng "giờ:phút:giây"
                var hours = remainingTime / 60;
                var minutes = remainingTime % 60;
                var remainingTimeString = $"{hours:D2} giờ {minutes:D2} phút";

                sale.Time_remaining = remainingTime <= 0 ? 0 : remainingTime;
                message += remainingTimeString + ".\n";
            }

            await _DbContext.SaveChangesAsync();

            // Lấy ra dòng văn bản đầu tiên từ thông điệp
            string firstLine = message.Split('\n').FirstOrDefault();
            return firstLine;
        }





    }
}
