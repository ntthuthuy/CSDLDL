using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class HuongDanVien
    {
        public int Id { get; set; }
        public string HoVaTen { get; set; }
        public bool GioiTinh { get; set; }
        public string CMND { get; set; }
        public DateTime NgaySinh { get; set; }
        public DateTime NgayCapCMND { get; set; }
        public string NoiCapCMND { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string HoKhau { get; set; }
        public string SoTheHDV { get; set; }
        public int LoaiTheId { get; set; }
        public int CongTyLuHanhId { get; set; }
        public DateTime NgayCapThe { get; set; }
        public string NoiCapThe { get; set; }
        public DateTime NgayHetHan { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }


        public List<QuaTrinhHoatDong> DSQuaTrinhHoatDong { get; set; }
    }
}
