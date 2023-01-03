using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.TienNghi
{
    public class TienNghiVm
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public int ViTri { get; set; }
        public int DonViTinhId { get; set; }
        public string LinhVucId { get; set; }
    }
}
