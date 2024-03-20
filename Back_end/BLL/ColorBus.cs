using BLL.Interface;
using DAL.Interface;
using Model;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ColorBus : GenericBus<Color>, IColorBus
    {
        public IColorRepository _res;
        public ColorBus(IColorRepository res) : base(res)
        {
            _res = res;
        }
      
    }
}
