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
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace DAL
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
    
        public SaleRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper) {
   
        }
        public async Task<List<SaleDto>> CREATE(List<SaleDto> dto)
        {
            try {
                var now = DateTime.Now;

                foreach (var item in dto)
                {
                    int elapsedTime = (int)(item.Created-now)?.TotalMinutes;
                    if (elapsedTime > 0)
                    {
                        item.ActiveFlag = 2;
                    }
                    SaleDto saleDto = new SaleDto
                    {
                        IdProduct = item.IdProduct,
                        SalePrice = item.SalePrice,
                        percent = item.percent,
                        SaleTime = item.SaleTime,
                        ActiveFlag = item.ActiveFlag,
                        Created = item.Created,
                    };

                    var saleEntity = _mapper.Map<Sale>(saleDto);
                    await _DbContext.Sale.AddAsync(saleEntity);
                    await _DbContext.SaveChangesAsync();
                }
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating records", ex);
            }
        }

        public async Task<List<GetSaleDto>> GetSale(string? keyword, int active)
        {
            var query = from a in _DbContext.Sale
                        join b in _DbContext.Products on a.IdProduct equals b.Id
                        join c in _DbContext.Price on a.IdProduct equals c.Idproduct
                        where ((string.IsNullOrEmpty(keyword) && a.ActiveFlag == active) || (b.Name.Contains(keyword) && a.ActiveFlag == active))
                        orderby a.Created descending
                        select new GetSaleDto
                        {
                            IdProduct = a.IdProduct,
                            Id = a.Id,
                            SalePrice = a.SalePrice,
                            percent = a.percent,
                            SaleTime = a.SaleTime,
                            Name = b.Name,
                            Image = b.Image,
                            Price_product = c.Price_product,
                            ActiveFlag = a.ActiveFlag,
                            Created = a.Created
                        };
            return await query.ToListAsync();
        }

        public async Task<int> UpdateSalesPrices()
        {
            var now = DateTime.Now;
            var activeSales = await _DbContext.Sale.Where(s => s.ActiveFlag == 1 && s.Created <= now).ToListAsync();
            var awaitSales = await _DbContext.Sale.Where(s => s.ActiveFlag == 2 ).ToListAsync();
            int message = 0;
            foreach(var x in awaitSales)
            {
                int elapsedTime = (int)(x.Created - now)?.TotalMinutes;
                if (elapsedTime <= 0)
                {
                    x.ActiveFlag= 1;
                }
            }

            foreach (var sale in activeSales)
            {
                var elapsedTime = now - sale.Created;
                var remainingTime = sale.SaleTime - (int)elapsedTime?.TotalMinutes;

                if (remainingTime <= 0)
                {
                    sale.ActiveFlag = 0;
                    message =0;
                    continue; 
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
                sale.Time_remaining = remainingTime <= 0 ? 0 : remainingTime;
                message = remainingTime;
            }

            await _DbContext.SaveChangesAsync();


            return message;
        }





    }
}
