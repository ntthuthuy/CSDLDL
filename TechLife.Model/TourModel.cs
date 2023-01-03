using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Model.Tour;
namespace TechLife.Model
{
    public class TourModel
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
        public decimal Gia { get; set; }
        public bool IsHangNgay { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public List<HanhTrinhModel> DSHanhTrinh { get; set; }
        public List<FileUploadModel> DSHinhAnh { get; set; }
    }
}
