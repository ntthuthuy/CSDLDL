using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class ChuyenMuc
    {
        public int Id { get; set; }
        public string Ten { set; get; }
        public string NgonNguId { set; get; }
        public string MoTa { set; get; }
        public string TieuDe { set; get; }
        public string TuKhoa { set; get; }
        public string UrlImage { get; set; }
        public string IconMobile { get; set; }
        public string IconWeb { get; set; }
        public int ParentId { get; set; }
        public int ThuTuHienThi { get; set; }
        public bool IsHienThiMenu { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
