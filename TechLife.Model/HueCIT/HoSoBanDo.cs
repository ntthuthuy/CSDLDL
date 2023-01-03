using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace TechLife.Model.HueCIT
{
    public class HoSoBanDo
    {
        public int ID { get; set; }
        public int HSID { get; set; }
        public string Ten { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public string Origin { get; set; }
    }
}
