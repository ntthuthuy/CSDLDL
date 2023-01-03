using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TechLife.Model.HoSoVanBan;

namespace TechLife.Model.HuongDanVien
{
    public class HuongDanVienModel
    {
        public int Id { get; set; }
        public string HoVaTen { get; set; }
        public bool GioiTinh { get; set; }
        public string CMND { get; set; }

        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }

        [DataType(DataType.Date)]
        public DateTime NgayCapCMND { get; set; }
        public string NoiCapCMND { get; set; }
        public string NoiCapThe { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string HoKhau { get; set; }
        public string SoTheHDV { get; set; }
        public int CongTyLuHanhId { get; set; }
        public int LoaiTheId { get; set; }
        public string LoaiThe { get; set; }
        public List<int> NgonNguId { get; set; }
        public List<string> NgonNgu { get; set; }
        public List<int> LoaiHinhId { get; set; }
        public List<string> LoaiHinh { get; set; }

        [DataType(DataType.Date)]
        public DateTime NgayCapThe { get; set; }

        [DataType(DataType.Date)]
        public DateTime NgayHetHan { get; set; }
        public List<QuaTrinhHoatDongModel> DSQuaTrinhHD { get; set; }
        public List<FileUploadModel> Images { get; set; }
        public List<HoSoVanBanVm> DSVanBan { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public string TinhTrang { get; set; }
    }
}
