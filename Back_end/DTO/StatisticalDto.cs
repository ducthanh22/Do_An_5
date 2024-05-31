using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class StatisticalDto
    {
        public double totalOrder { get; set; }
        public double totalCustomer { get; set; }
        public double totalProduct { get; set; }
        public double expected_Revenue { get; set; }
        public double totalRevenue { get; set; }
        public double totalMonthlyrevenue { get; set; }
        public double totalRating { get; set; }
        public double totalRevenueCategory { get; set; }


        public List<Monthly_revenue> Monthlyrevenue { get; set; }
        public List<Category_revenue> CategoryRevenue { get; set; }
        public List<rating_rate> rating_rate { get; set; }
    }
    public class Monthly_revenue
    {
        public DateTime Date { get; set; }
        public double Revenue { get; set; }
    }
    public class Category_revenue
    {
        public string nameCategory { get; set; }
        public double Revenue { get; set; }
    }
    public class rating_rate
    {
        public int name { get; set; }
        public int  quantity  { get; set; }
    }

}
