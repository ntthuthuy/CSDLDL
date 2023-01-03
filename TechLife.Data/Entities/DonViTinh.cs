using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class DonViTinh
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
