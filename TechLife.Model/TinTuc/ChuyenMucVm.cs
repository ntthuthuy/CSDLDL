using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.TinTuc
{
    public class ChuyenMucVm
    {
        public int Id { get; set; }
        public string Ten { set; get; }
        public string MoTa { set; get; }
        public string TieuDe { set; get; }
        public string TuKhoa { set; get; }
        public string UrlImage { get; set; }
        public string IconMoblie { get; set; }
        public string IconWeb { get; set; }
        public int ParentId { get; set; }
        public int ThuTuHienThi { get; set; }
        public string ChuyenMucCha { get; set; }
        public bool IsHienThiMenu { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
