﻿using Back_end.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Produces :Basedb
    {
        public string Name {  get; set; }
        public  string Address {  get; set; }
        public string Email {  get; set; }
    }
}