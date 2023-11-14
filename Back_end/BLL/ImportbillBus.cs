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
    public class ImportbillBus : GenericBus<Importbill>, IImportbillBus
    {
        public ImportbillBus(IImportbillRepository res) : base(res)
        {
        }
    }
}
