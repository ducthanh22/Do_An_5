using DTO;
using Microsoft.AspNetCore.Http;

namespace DTO
{
    public class ProductsDto:BasedbDto
    {
      
        public string? Name { get; set; }
        public Guid Idcategories { get; set; }
        public Guid Idproduces { get; set; }
        public string? Describe { get; set; }
        public string? NameColor { get; set; }
        public int? Price_product { get; set; }
        public string? NameSize { get; set; }
        public string? Image { get; set; }


        

    }
    
   
}
