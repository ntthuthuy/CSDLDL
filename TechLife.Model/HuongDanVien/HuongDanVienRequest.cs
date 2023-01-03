using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.HuongDanVien
{
    public class HuongDanVienRequest
    {
        public HuongDanVienModel HuongDanVien { get; set; }
        public ImageUploadRequest Images { get; set; }
    }
}
