using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Model.TienNghi;

namespace TechLife.Model.DuLieuDuLich
{
   public class DuLieuDuLichAPI
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string TenVietTat { get; set; }
        public int HangSao { get; set; }
        public int LoaiHinhId { get; set; }
        public string LoaiHinh { get; set; }
        public string NguoiDaiDien { get; set; }
        public string ChucVuNguoiDaiDien { get; set; }
        public string SDTNguoiDaiDien { get; set; }
        public string GioMoCua { get; set; }
        public string GioDongCua { get; set; }
        public string SoNha { get; set; }
        public string DuongPho { get; set; }
        public string PhuongXa { get; set; }
        public string QuanHuyen { get; set; }
        public string TinhThanh { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string ViTriTrenBanDo { get; set; }
        public string GioiThieu { get; set; }
        public string MoTa { get; set; }
        public string LoiKhuyen { get; set; }
        public string GiaThamKhao { get; set; }
        public bool IsDatChuan { get; set; }
        public string Avatar { get; set; }
        public List<FileUploadModel> Images { get; set; }
        public List<TienNghiHoSoModel> TienNghi { get; set; }
    }
}
