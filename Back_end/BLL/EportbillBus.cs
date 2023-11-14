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
    public class EportbillBus : GenericBus<Exportbill>, IExportbillBus
    {
        public EportbillBus(IExportbillRepository res) : base(res)
        {
        }
    }
}
