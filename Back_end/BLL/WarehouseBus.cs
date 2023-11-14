using BUS;
using DAL.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class WarehouseBus : GenericBus<Warehouse>, IWarehouseBus
    {
        public WarehouseBus(IWarehouseRepository res) : base(res)
        {
        }
    }
}
