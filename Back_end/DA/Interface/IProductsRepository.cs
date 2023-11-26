using Back_end.Model;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IProductsRepository :IGenericRepository<Products>
    {
        Task<BaseQuerieResponse<ProductsDto>> Search(string keywork, int page, int pageSize);
        Task<UpLoadFile> UploadFile(UpLoadFile product);

    }
}
