using Microsoft.AspNetCore.Http;

namespace Model
{
    public class ProductsDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Idcategories {  get; set; }
        public int Idproduces { get; set;}
        public string Describe {  get; set; }
        public string NameColor { get; set; }
        public int Price_product { get; set; }


    }
    public class UpLoadFile
    {
        public int Id { get; set; }
        public IFormFile Img { get; set; }
        public string? Image { get; set; }

    }
   
}
