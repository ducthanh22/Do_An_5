namespace Back_end.Model
{
    public class ProductsDto : BasedbDto
    {
        public string Name { get; set; }
        public int Idcategories {  get; set; }
        public int Idproduces { get; set;}
        public string Describe {  get; set; }
        public string Image { get; set; }
    }
}
