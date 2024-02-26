using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ImportbillDto:BasedbDto
    {
        public int Price { get; set; }
        public int Status { get; set; }
        public int IdStaff { get; set; }
    }
    public class CreateImportbillDto : BasedbDto
    {
        public int Price { get; set; }
        public int Status { get; set; }
        public int IdStaff { get; set; }
        public List<Detail_importbill> Detail_importbill { get;}
    }
}
