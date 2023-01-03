using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.Tour
{
    class TourRequest
    {
        public HanhTrinhModel HuongDanVien { get; set; }
        public ImageUploadRequest Images { get; set; }
    }
}
