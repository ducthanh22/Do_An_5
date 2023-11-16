using BUS;
using DAL.Interface;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StaffBus : GenericBus<Staff>, IStaffBus
    {
        public IStaffRepository _res;
        public StaffBus(IStaffRepository res) : base(res)
        {
            _res = res;
        }
        public async Task<BaseQuerieResponse<StaffDto>>Search(string keywork, int page, int pageSize)
        {
            return await _res.Search(keywork, page, pageSize);
        }
    }
}
