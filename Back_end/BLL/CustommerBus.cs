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
    public class CustommerBus : GenericBus<Custommer>, ICustommerBus
    {
        public CustommerBus(ICustommerRepository res) : base(res)
        {
        }
    }
}
