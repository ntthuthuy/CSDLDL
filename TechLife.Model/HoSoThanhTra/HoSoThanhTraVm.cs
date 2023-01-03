using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Model.DuLieuDuLich;

namespace TechLife.Model.HoSoThanhTra
{
    public class HoSoThanhTraVm
    {
        public int Id { get; set; }
        public int HoSoId { get; set; }
        public DuLieuDuLichModel HoSo { get; set; }
        public string TruongDoan { get; set; }
        public string NoiDung { get; set; }
        public string KetLuan { get; set; }
        public string KetQuaThanhTra { get; set; }
        public int KetQua { get; set; }
        public DateTime ThoiGian { get; set; }
        public int UserId { get; set; }
        public DateTime NgayTao { get; set; }
        public List<VanBanHoSoThanhTraVm> DSVanBan { get; set; }
    }
    public class VanBanHoSoThanhTraVm
    {
        public int Id { get; set; }
        public string SoHieu { get; set; }
        public string TenVanBan { get; set; }
        public DateTime NgayKy { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
