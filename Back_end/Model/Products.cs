namespace Back_end.Model
{
    public class Products : Basedb
    {
        public string Name { get; set; }
        public int Idcategories {  get; set; }
        public int Idproduces { get; set;}
        public string Describe {  get; set; }
        public string Image { get; set; }
    }
}
