using DAL.Interface;
using System;
using System.Collections.Generic;
using Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DTO;

namespace BLL
{
    public class SizeBus : GenericBus<Size>,ISizeBus
    {
        public ISizeRepository _res;
        public SizeBus(ISizeRepository res) : base(res)
        {
            _res = res;
        }
        public async Task<List<SizeDto>> Getbyidproduct(Guid id)
        {
            return await _res.Getbyidproduct(id);
        }
    }
}
