using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class DiemGiaoDich
    {
        public int ID { get; set; }
        [Required]
        public string TenDiaDiem { get; set; }
        public int Loai { get; set; }
        public string DienThoai { get; set; }
        public string GioPhucVu { get; set; }
        public string DiaChi { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public int DiemGiaoDichID { get; set; }
        public int PhuongXaId { get; set; }
        public int QuanHuyenId { get; set; }
    }

    public class DiemGiaoDichTrinhDien
    {
        public int ID { get; set; }
        public string TenDiaDiem { get; set; }
        public int Loai { get; set; }
        public string TenLoai { get; set; }
        public string DienThoai { get; set; }
        public string GioPhucVu { get; set; }
        public string DiaChi { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public int PhuongXaId { get; set; }
        public int QuanHuyenId { get; set; }
        public string TenPhuongXa { get; set; }
        public string TenQuanHuyen { get; set; }
    }

    public class DiemGiaoDichRequest
    {
        public int Loai { get; set; }
        public int DiaDiem { get; set; }
        public int QuanHuyenId { get; set; }
        public int NguonDongBo { get; set; } = -1;
    }

    public class DiemGiaoDichSearch
    {
        public int ID { get; set; }
        public string DiaDiem { get; set; }
    }

    public class DiemGiaoDichDongBo
    {
        public int id { get; set; }
        public string tendoituong { get; set; }
        public string nhomlh { get; set; }
        public string loaihinh { get; set; }
        public string diachi { get; set; }
        public string kinhdo { get; set; }
        public string vido { get; set; }
        public string anh { get; set; }
    }

    public class DanhSachDiemGiaoDichDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<DiemGiaoDichDongBo> data { get; set; }
        public int totalrow { get; set; }
    }
}
