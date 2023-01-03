using System;
using System.Collections.Generic;
using TechLife.Model;
namespace TechLife.Model.Tour
{
    public class TourVm
    {
        public int Id { get; set; }
        public int LoaiId { get; set; }
        public int CongTyLuHanhId { get; set; }
        public string TenCongTy { get; set; }
        public int SoNgay { get; set; }
        public string MaTour { get; set; }
        public string TenChuyenDi { get; set; }
        public string MoTaChuyenDi { get; set; }
        public string KhoiHanhTu { get; set; }
        public string LichTrinh { get; set; }
        public int HinhThucId { get; set; }
        public string Gia { get; set; }
        public bool IsHangNgay { get; set; }
        public List<HanhTrinhModel> DSHanhTrinh { get; set; }
        public List<FileUploadModel> DSHinhAnh { get; set; }
    }
}