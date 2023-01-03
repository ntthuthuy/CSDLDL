using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace TechLife.Model.VanBanDen
{
    public class VanBanDenRequest 
    {
        public string NgayBanHanh { get; set; }
        public string Keyword { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
