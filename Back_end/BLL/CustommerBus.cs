using Back_end.Model;
using BUS.Interface;
using DAL.Interface;
using DTO;
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
        public ICustommerRepository _res;
        public CustommerBus(ICustommerRepository res) : base(res)
        {
            _res = res;
        }
        public async Task<BaseQuerieResponse<CustommerDto>> Search(string keywork, int page, int pageSize)
        {
            return await _res.Search(keywork, page, pageSize);
        }
    }
}
