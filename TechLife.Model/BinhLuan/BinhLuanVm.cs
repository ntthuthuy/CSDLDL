using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.BinhLuan
{
    public class BinhLuanVm
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayBinhLuan { get; set; }
        public string AvataUrl { get; set; }

    }
}
