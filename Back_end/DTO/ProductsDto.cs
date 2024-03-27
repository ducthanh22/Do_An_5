using DTO;
using Microsoft.AspNetCore.Http;

namespace DTO
{
    public class ProductsDto:BasedbDto
    {
        public string Name { get; set; }
        public Guid Idcategories { get; set; }
        public Guid Idproduces { get; set; }
        public string Describe { get; set; }
        public string Image { get; set; }
        public Guid Idcolor { get; set; }
        public int? Price_product { get; set; }
        public List<SizeDto> ListSize { get; set; }
    }
    public class GetProductsDto : BasedbDto
    {
        public string Name { get; set; }
        public Guid Idcategories { get; set; }
        public string Namecategory { get; set; }
        public string NameProduces { get; set; }

        public Guid Idproduces { get; set; }
        public string Describe { get; set; }
        public string Image { get; set; }
        public string namecolor { get; set; }
       
        public int? Price_product { get; set; }
        public Guid Idcolor { get; set; }
        public List<SizeDto> ListSize { get; set; }

    }
    public class UpLoadFile
    {
        public Guid Id { get; set; }
        public IFormFile Img { get; set; }
        public string? Image { get; set; }

    }

}
