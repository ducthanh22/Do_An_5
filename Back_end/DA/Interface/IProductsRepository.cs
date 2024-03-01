﻿using Model;
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
        //Task<UpLoadFile> UploadFile(UpLoadFile product);
        Task<IQueryable<ProductsDto>>GetByIds(Guid ids);

        Task<List<ProductsDto>> Getalls();
        Task<ProductsDto> Creates(ProductsDto entity);
        Task<ProductsDto> Updates(ProductsDto entity);

    }
}
