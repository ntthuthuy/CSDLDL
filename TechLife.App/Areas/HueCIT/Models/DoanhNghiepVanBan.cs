using System;
using System.Collections.Generic;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class DoanhNghiepVanBan
    {
        public int Id { get; set; }
        public int MaDoanhNghiep { get; set; }
        public int MaLoai { get; set; }
        public string SoKyHieu { get; set; }
        public string TrichYeu { get; set; }
        public DateTime? NgayHieuLuc { get; set; }
        public DateTime? NgayHetHieuLuc { get; set; }
        public string TepKemTheo { get; set; }
        public string TenGiayPhep { get; set; }
        public string MaSoDoanhNghiep { get; set; }
    }

    public class DoanhNghiepVanBanTrinhDien
    {
        public int Id { get; set; }
        public string MaDoanhNghiep { get; set; }
        public string SoKyHieu { get; set; }
        public string TenGiayPhep { get; set; }
    }
    public class DoanhNghiepVanBanDongBo
    {
        public int loaiGiayPhepID { get; set; }
        public string maDoanhNghiep { get; set; }
        public string soKyHieu { get; set; }
        public string ngayHieuLuc { get; set; }
        public string ngayHetHieuLuc { get; set; }
        public string tenGiayPhep { get; set; }
        public string fileDinhKem { get; set; }
    }
    public class DanhSachDoanhNghiepVanBanDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public string description { get; set; }
        public List<DoanhNghiepVanBanDongBo> data { get; set; }
        public string optionValues { get; set; }
    }
}
