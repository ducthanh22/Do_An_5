using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SizeDto : BasedbDto
    {
        public Guid Idproduct { get; set; }

        public string NameSize { get; set; }
    }
}
