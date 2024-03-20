using Model;
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
        Task<BaseQuerieResponse<GetProductsDto>> Search(string keywork, int page, int pageSize);
        Task<UpLoadFile> UploadFile(UpLoadFile img);
        Task<IQueryable<GetProductsDto>>GetByIds(Guid ids);

        Task<List<GetProductsDto>> Getalls();
        Task<ProductsDto> Creates(ProductsDto entity);
        Task<ProductsDto> Updates(ProductsDto entity);

    }
}
