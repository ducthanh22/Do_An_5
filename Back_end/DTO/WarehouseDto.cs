﻿using Back_end.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WarehouseDto :BasedbDto
    {
        public string Name {  get; set; }
        public string Address {  get; set; }
    }
}