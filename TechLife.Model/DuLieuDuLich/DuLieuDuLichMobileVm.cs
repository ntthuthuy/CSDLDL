using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
    public class DuLieuDuLichMobileVm
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int HangSao { get; set; }
        public int GioMoCua { get; set; }
        public int GioDongCua { get; set; }
        public string SoNha { get; set; }
        public string DuongPho { get; set; }
        public string PhuongXa { get; set; }
        public string QuanHuyen { get; set; }
        public string TinhThanh { get; set; }
        public string SoDienThoai { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string ViTriTrenBanDo { get; set; }
        public string GioiThieu { get; set; }
        public FileUploadModel Images { get; set; }
    }
}
