using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class NganhDaoTao
    {
        public int Id { get; set; }
        public string TenNganhDaoTao { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
