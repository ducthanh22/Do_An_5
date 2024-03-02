using Model;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IProductsBus : IGenericBUS<Products>
    {
        Task<BaseQuerieResponse<ProductsDto>> Search(string keywork, int page, int pageSize);
        //Task<UpLoadFile> UploadFile(UpLoadFile product);
        Task<IQueryable<ProductsDto>> GetByIds(Guid ids);
        Task<List<ProductsDto>> Getalls();
        Task<ProductsDto> Creates(ProductsDto entity);
        Task<ProductsDto> Updates(ProductsDto entity);






    }
}
