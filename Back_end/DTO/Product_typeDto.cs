using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Product_typeDto : BasedbDto
    {
        public string? Name { get; set; }
        public Guid Idcategories { get; set; }

    }
}
