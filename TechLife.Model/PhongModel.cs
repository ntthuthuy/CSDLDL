using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Model.DuLieuDuLich;

namespace TechLife.Model
{
    public class PhongModel
    {
        public int Id { get; set; }
        public string MaPhong { get; set; }
        public int SoGiuong { get; set; }
        public int LoaiPhongId { get; set; }
        public LoaiPhongModel LoaiPhong { get; set; }
        public int DichVuId { get; set; }
        public DichVuHoSoModel DichVuPhong { get; set; }
        public int CoSoLuTruId { get; set; }
        public DuLieuDuLichModel CoSoLuuTru { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
