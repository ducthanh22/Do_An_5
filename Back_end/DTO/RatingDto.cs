using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RatingDto :BasedbDto
    {
        public Guid Id_Order { get; set; }
        public Guid Id_product { get; set; }
        public string Id_customer { get; set; }
        public int Evaluate { get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }
        public string? Username { get; set; }
        public string? Image { get; set; }


    }
    public class CreateRatingDto
    {
       public List<Rating> ListRating { get; set; }
    }
    public class GetRatingByEvaluate: BasedbDto
    {
        public Guid Id_Order { get; set; }
        public Guid Id_product { get; set; }
        public string Product_name { get; set; }
        public string Image { get; set; }

        public string Id_customer { get; set; }
        public int Evaluate { get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }
        public string? Username { get; set; }
    }
}
