
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Products : Basedb
    {
        public string Name { get; set; }
        public int Idcategories {  get; set; }
        public int Idproduces { get; set;}
        public int Idcolor { get; set;}
        public string Describe {  get; set; }
        public string Image { get; set; }
    }
}
