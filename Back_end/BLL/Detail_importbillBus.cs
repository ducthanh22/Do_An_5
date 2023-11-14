using BUS.Interface;
using DAL.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class Detail_importbillBus : GenericBus<Detail_importbill>, IDetail_importbillBus
    {
        public Detail_importbillBus(IDetail_importbillRepository res) : base(res)
        {
        }
    }
}
