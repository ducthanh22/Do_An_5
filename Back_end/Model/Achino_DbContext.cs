using Back_end.Model;
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
        public DbSet<Categories> Categorie_entities{ get; set; }
        public DbSet<Custommer> Custommer_entities { get; set; }
        public DbSet<Detail_exportbill> Detail_exportbill_entities { get; set; }
        public DbSet<Detail_importbill> Detail_importbill_entities { get; set; }
        public DbSet<Detail_warehouse> Detail_warehouse_entities { get; set; }
        public DbSet<Exportbill> Exportbill_entities { get; set; }
        public DbSet<Importbill> Importbill_entities { get; set; }
        public DbSet<Price> Price_entities { get; set; }
        public DbSet<Produces> Produces_entities { get; set; }
        public DbSet<Products> Products_entities { get; set; }
        public DbSet<Staff> Staff_entities { get; set; }
        public DbSet<Warehouse> Warehouse_entities { get; set; }












        // Các constructor và cấu hình khác
    }
}
