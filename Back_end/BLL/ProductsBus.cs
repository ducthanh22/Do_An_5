using Back_end.Model;
using BUS.Interface;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ProductsBus : GenericBus<Products>, IProductsBus
    {
        public ProductsBus(IProductsRepository res) : base(res)
        {
        }
    }
}
