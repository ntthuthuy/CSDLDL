using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class PhanAnhHienTruongHinhAnh
    {
        public int Id { get; set; }
        public int MaPhanAnh { get; set; }
        public string HinhAnh { get; set; }
        public string HinhAnhThumb { get; set; }
        public bool IsKetQua { get; set; }
    }
}
