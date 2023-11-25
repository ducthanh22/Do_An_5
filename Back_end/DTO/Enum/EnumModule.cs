using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Enum
{
    public static class EnumModule
    {
        public enum Module 
        {
            [Description("Dashboard")]
            Dashboard = 1,
            [Description("Categories management ")]
            QlDm = 2,
            [Description("Product management")]
            QlDv = 3,
            [Description("Import bill management")]
            QlHdn = 4,
            [Description("Export bill management")]
            QlHdb = 5,
            [Description("Order management")]
            QlDh = 6,
            [Description("Price management")]
            QlG = 7,
            [Description("Customer management")]
            QlKh = 8,
            [Description("Staff management")]
            QlNv = 9,
        }
       
    }
}
