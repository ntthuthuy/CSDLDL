using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
    public class QuyMoNhaHangVm
    {
        public int Id { get; set; }
        public int HoSoId { get; set; }
        public string TenGoi { get; set; }
        public int DienTich { get; set; }
        public int SoGhe { get; set; }
        public bool IsDelete { get; set; }  
    }
}
