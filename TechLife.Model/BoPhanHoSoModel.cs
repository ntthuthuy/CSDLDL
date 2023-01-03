using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Model.BoPhan;

namespace TechLife.Model
{
    public class BoPhanHoSoModel
    {
        public BoPhanVm BoPhan { get; set; }
        public int BoPhanId { get; set; }
        public int HoSoId { get; set; }
        public int SoLuong { get; set; }
        public int DonViTinhId { get; set; }
    }
}
