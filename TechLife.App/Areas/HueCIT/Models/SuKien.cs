using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class SuKien
    {
        public int ID { get; set; }
        public int? MaChuDe { get; set; }
        [Required]
        public string TieuDe { get; set; }
        [Required]
        public string NoiDung { get; set; }
        public DateTime BatDau { get; set; }
        public DateTime KetThuc { get; set; }
        [Required]
        public string DiaDiem { get; set; }
        public float? X { get; set; }
        public float? Y { get; set; }
        public bool TrangThai { get; set; }
        public int PhuongXaId { get; set; }
        public int QuanHuyenId { get; set; }
        public List<FileUpload> Files { get; set; }
    }

    public class SuKienTrinhDien
    {
        public int ID { get; set; }
        public int? MaChuDe { get; set; }
        public string TenChuDe { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime BatDau { get; set; }
        public DateTime KetThuc { get; set; }
        public string DiaDiem { get; set; }
        public float? X { get; set; }
        public float? Y { get; set; }
        public bool TrangThai { get; set; }
        public int PhuongXaId { get; set; }
        public int QuanHuyenId { get; set; }
        public string TenPhuongXa { get; set; }
        public string TenQuanHuyen { get; set; }
        public List<FileUpload> Files { get; set; }
    }

    public class SuKienRequest
    {
        public int MaChuDe { get; set; }
        public string DiaDiem { get; set; }
        public string BatDau { get; set; }
        public string KetThuc { get; set; }
        public int QuanHuyenId { get; set; }
    }

    public class SuKienRequestMod
    {
        public SuKien DuLieuSuKien { get; set; }
        public FilesUploadRequest Files { get; set; }
    }
}
