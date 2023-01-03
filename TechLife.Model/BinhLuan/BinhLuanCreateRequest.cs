using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.BinhLuan
{
    public class BinhLuanCreateRequest
    {
 
        public string Ten { get; set; }
        public string NoiDung { get; set; }
        public string Type { get; set; }
        public string AvataUrl { get; set; }
         public int HoSoId { get; set; }

    }
}
