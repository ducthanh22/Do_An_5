using DTO;
using Microsoft.AspNetCore.Http;

namespace DTO
{
    public class ProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Idcategories { get; set; }
        public int Idproduces { get; set; }
        public string Describe { get; set; }
        public string NameColor { get; set; }

        public int Idproduct { get; set; }
        public int Price_product { get; set; }
        public string NameSize { get; set; }

        public DateTime Startday { get; set; } = DateTime.Now;
        public DateTime Endday { get; set; } = DateTime.Now;



    }
    public class UpLoadFile
    {
        public int Id { get; set; }
        public IFormFile Img { get; set; }
        public string? Image { get; set; }

    }
   
}
