using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class LeHoi
    {
        public Guid ID { get; set; }
        [Required]
        public string TenLeHoi { get; set; }
        public int Loai { get; set; }
        public int Cap { get; set; }
        public string NoiDung { get; set; }
        public DateTime? BatDau { get; set; }
        public DateTime? KetThuc { get; set; }
        public string DiaDiem { get; set; }
        public float? X { get; set; }
        public float? Y { get; set; }
        public List<FileUpload> Files { get; set; }
        public int? LeHoiID { get; set; }
        public int? NguonDongBo { get; set; }
        public int PhuongXaId { get; set; }
        public int QuanHuyenId { get; set; }
    }

    public class LeHoiTrinhDien
    {
        public Guid ID { get; set; }
        public string TenLeHoi { get; set; }
        public int Loai { get; set; }
        public CapQuanLyLeHoi Cap { get; set; }
        public string LoaiLeHoi { get; set; }
        public string CapQuanLy { get; set; }
        public string NoiDung { get; set; }
        public DateTime BatDau { get; set; }
        public DateTime KetThuc { get; set; }
        public string DiaDiem { get; set; }
        public float? X { get; set; }
        public float? Y { get; set; }
        public List<FileUpload> Files { get; set; }
        public int LeHoiID { get; set; }
        public int PhuongXaId { get; set; }
        public int QuanHuyenId { get; set; }
        public string TenPhuongXa { get; set; }
        public string TenQuanHuyen { get; set; }
    }

    public class LeHoiRequest
    {
        public int Loai { get; set; }
        public int Cap { get; set; }
        public string DiaDiem { get; set; }
        public string BatDau { get; set; }
        public string KetThuc { get; set; }
        public int? NguonDongBo { get; set; }
        public int QuanHuyenId { get; set; }
    }

    public class LeHoiRequestMod
    {
        public LeHoi DuLieuLeHoi { get; set; }
        public FilesUploadRequest Files { get; set; }
    }

    public class LeHoiDongBo
    {
        public int id { get; set; }
        public string tendoituong { get; set; }
        public string nhomloaihinh { get; set; }
        public string loaihinh { get; set; }
        public string diachi { get; set; }
        public string kinhdo { get; set; }
        public string vido { get; set; }
        public string mota { get; set; }
        public string giathamkhaotu { get; set; }
        public string giathamkhaoden { get; set; }
        public string mocua { get; set; }
        public string dongcua { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string anh { get; set; }
        public string groupid { get; set; }
    }

    public class DanhSachLeHoiDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<LeHoiDongBo> data { get; set; }
        public int totalrow { get; set; }
    }
}
