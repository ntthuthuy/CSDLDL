using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Model.DuLieuDuLich;

namespace TechLife.Model.Tour
{
    public class DiaDiemTheoNgayVm
    {
        public int Id { get; set; }
        public DuLieuDuLichRpt DiaDiem { get; set; }
        public string ThoiGian { get; set; }
        public string KhoanCach { get; set; }
        public int DiaDiemId { get; set; }
        public int Gio { get; set; }
        public int Phut { get; set; }
    }
}
