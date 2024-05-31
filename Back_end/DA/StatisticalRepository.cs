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
    public class StatisticalRepository: IStatisticalRepository
    {
        public Achino_DbContext _DbContext;
        public IMapper _Mapper;
        public StatisticalRepository(Achino_DbContext DbContext, IMapper Mapper) { 
        _DbContext = DbContext;
            _Mapper = Mapper;
        }
        public async Task<StatisticalDto> Darhboarsh( DateTime? start , DateTime? end)
        {
            if ((!start.HasValue || start.Value == default(DateTime)) && (!end.HasValue || end.Value == default(DateTime)))
            {
                start = DateTime.Now.AddMonths(-6);
                end = DateTime.Now;
            }
            var totalOrder =  await _DbContext.Order.Where(x=>x.Created >= start && x.Created <=end).CountAsync();
            var totalCustomer = await _DbContext.User.Where(x =>x.Status == "2").CountAsync();
            var totalProduct = await _DbContext.Products.Where(x => x.Created >= start && x.Created <= end).CountAsync();
            var expected_Revenue= await _DbContext.Order.Where(x => x.Created >= start && x.Created <= end).SumAsync(x=> (double)x.Price);
            var totalRevenue = await _DbContext.Exportbill.Where(x => x.Created >= start && x.Created <= end).SumAsync(x => (double)x.Price);
          
            //doanh thu theo tháng
            var monthlyRevenues = await _DbContext.Exportbill.Where(x => x.Created >= start && x.Created <= end)
                .GroupBy(x => new { x.Created.Value.Year, x.Created.Value.Month })
                .Select(g => new Monthly_revenue
                {
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                    Revenue = g.Sum(x => (double)x.Price)
                })
                .ToListAsync();
            monthlyRevenues = monthlyRevenues.OrderBy(mr => mr.Date).ToList();

            var totalMonthlyRevenue = monthlyRevenues.Sum(x => x.Revenue);


            //doanh thu các danh muc
            var category = from a in _DbContext.Detail_exportbill
                           join b in _DbContext.Products on a.Idproduct equals b.Id
                           join c in _DbContext.Product_type on b.Idcategories equals c.Id
                           join d in _DbContext.Categorie on c.Idcategories equals d.Id
                           where a.Created >= start && a.Created <= end
                           group new { d, a } by new { d.Id, d.Name } into g
                           select new Category_revenue
                           {
                               nameCategory = g.Key.Name,
                               Revenue = g.Sum(x => (double)(x.a.Quantity * x.a.Price))
                           };

            var listcategory = await category.ToListAsync();

            var totalcategory = await category.SumAsync(x=>x.Revenue);
            ;

            // số lượng đánh giá
            var Rating = await _DbContext.Rating.Where(x => x.Created >= start && x.Created <= end)
                .GroupBy(x => new { x.Evaluate})
                .Select(g => new rating_rate
                {
                    name = g.Key.Evaluate,
                    quantity = g.Count()
                })
                .OrderBy(mr => mr.name)
                .ToListAsync();
            var totalRating = await _DbContext.Rating.Where(x => x.Created >= start && x.Created <= end).CountAsync();



            return new StatisticalDto
            {
                totalOrder = totalOrder,
                totalCustomer = totalCustomer,
                totalProduct = totalProduct,
                expected_Revenue = expected_Revenue,
                totalRevenue = totalRevenue,
                totalMonthlyrevenue = totalMonthlyRevenue,
                Monthlyrevenue = monthlyRevenues,
                totalRating = totalRating,
                rating_rate = Rating,
                CategoryRevenue = listcategory,
                totalRevenueCategory = totalcategory

            };


        }
    }
}
