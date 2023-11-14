using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Back_end.Model;
using Model;

namespace DTO.AutoMapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {

            CreateMap<Categories, CategoriesDto>().ReverseMap();
            CreateMap<Products, ProductsDto>().ReverseMap();
            CreateMap<Custommer, CustommerDto>().ReverseMap();
            CreateMap<Detail_exportbill, Detail_exportbillDto>().ReverseMap();
            CreateMap<Detail_importbill, Detail_importbillDto>().ReverseMap();
            CreateMap<Detail_warehouse, Detail_warehouseDto>().ReverseMap();

            CreateMap<Exportbill, ExportbillDto>().ReverseMap();
            CreateMap<Importbill, ImportbillDto>().ReverseMap();
            CreateMap<Price, PriceDto>().ReverseMap();
            CreateMap<Produces, ProducesDto>().ReverseMap();
            CreateMap<Staff, StaffDto>().ReverseMap();
            CreateMap<Warehouse, WarehouseDto>().ReverseMap();
        }
    }
}
