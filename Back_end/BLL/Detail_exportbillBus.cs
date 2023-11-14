using BUS.Interface;
using DAL;
using DAL.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class Detail_exportbillBus : GenericBus<Detail_exportbill>, IDetail_exportbillBus
    {
        public Detail_exportbillBus(IDetail_exportbillRepository res) : base(res)
        {
        }
    }
}
