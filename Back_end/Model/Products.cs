﻿
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
        public Guid Idcategories {  get; set; }
        public Guid Idproduces { get; set;}
        public string Describe {  get; set; }
        public string Image { get; set; }
        public Guid Idcolor { get; set; }
        public Guid Idsize { get; set; }

    }
}
