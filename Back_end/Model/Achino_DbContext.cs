using Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Model
{
    
    public class Achino_DbContext : IdentityDbContext
    {
        public Achino_DbContext(DbContextOptions<Achino_DbContext> options) : base(options)
        {
        }
        public DbSet<Categories> Categorie{ get; set; }
        public DbSet<Detail_exportbill> Detail_exportbill { get; set; }
        public DbSet<Detail_importbill> Detail_importbill { get; set; }
        public DbSet<Detail_warehouse> Detail_warehouse { get; set; }
        public DbSet<Exportbill> Exportbill { get; set; }
        public DbSet<Importbill> Importbill { get; set; }
        public DbSet<Price> Price { get; set; }
        public DbSet<Produces> Produces { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Order_detail> Order_detail { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<PaymentTransactions> PaymentTransactions { get; set; }

    }
}
