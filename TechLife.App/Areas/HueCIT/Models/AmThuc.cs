using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class AmThuc
    {
        public int ID { get; set; }
        [Required]
        public string TenMon { get; set; }
        public int? MaLoai { get; set; }
        public int Kieu { get; set; }
        public bool ThucUong { get; set; }
        public string MoTa { get; set; }
        public List<FileUpload> Files { get; set; }
        public int AmThucID { get; set; }
        public string CachLam { get; set; }
        public string ThanhPhan { get; set; }
        public string KhuyenNghiKhiDung { get; set; }
        public int? NguonDongBo { get; set; }
    }

    public class AmThucTrinhDien
    {
        public int ID { get; set; }
        public string TenMon { get; set; }
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
        public KieuMon Kieu { get; set; }
        public string KieuMonAn { get; set; }
        public bool ThucUong { get; set; }
        public string MoTa { get; set; }
        public List<FileUpload> Files { get; set; }
        public string CachLam { get; set; }
        public string ThanhPhan { get; set; }
        public string KhuyenNghiKhiDung { get; set; }
    }

    public class AmThucRequest
    {
        public int Loai { get; set; }
        public int AmThuc { get; set; }
        public int NguonDongBo { get; set; } = -1;
    }

    public class AmThucRequestMod
    {
        public AmThuc DuLieuAmThuc { get; set; }
        public FilesUploadRequest Files { get; set; }
    }

    public class AmThucSearch
    {
        public int ID { get; set; }
        public string TenMon { get; set; }
    }

    public class AmThucDongBo
    {
        public int id { get; set; }
        public string truong001 { get; set; }
        public string truong002 { get; set; }
        public string truong003 { get; set; }
        public string truong004 { get; set; }
        public string truong005 { get; set; }
        public string truong006 { get; set; }
        public string truong007 { get; set; }
        public string truong008 { get; set; }
    }

    public class DanhSachAmThucDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<AmThucDongBo> data { get; set; }
        public int totalrow { get; set; }
    }
}
