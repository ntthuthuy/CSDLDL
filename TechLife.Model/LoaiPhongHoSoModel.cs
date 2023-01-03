using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model
{
    public class LoaiPhongHoSoModel
    {
        public LoaiPhongModel LoaiPhong { get; set; }
        public List<LoaiGiuongHoSoModel> DSLoaiGiuong { get; set; }
        public int LoaiPhongId { get; set; }
        //public int LoaiGiuongId { get; set; }
        public int HoSoId { get; set; }
        //public int SoPhong { get; set; }
        //public int DienTich { get; set; }
        //public string TenHienThi { get; set; }
        //public decimal GiaPhong { get; set; }
    }
}
