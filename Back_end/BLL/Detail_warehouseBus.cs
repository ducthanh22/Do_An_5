using BUS;
using BUS.Interface;
using DAL.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Detail_warehouseBus : GenericBus<Detail_warehouse>, IDetail_warehouseBus
    {
        public Detail_warehouseBus(IDetail_warehouseRepository res) : base(res)
        {
        }
    }
}
