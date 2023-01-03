using System;
using TechLife.Common.Enums.HueCIT;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class VeDiTich
    {
        public int Id { get; set; }
        public decimal TongTien { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayBan { get; set; }
        public int MaDiaDiem { get; set; }
        public int LoaiKhach { get; set; }
        public int SoVeDon { get; set; }
        public int SoVeTuyen { get; set; }
        public string TenLoaiKhach { get; set; }
        public DateTime NgayDongBo { get; set; }
    }

    public class VeDiTichDiaDiem
    {
        public int Id { get; set; }
        public string DiaDiem { get; set; }
    }

    public class VeDiTichLoai
    {
        public int Id { get; set; }
        public string TenLoai { get; set; }
        public decimal GiaVe { get; set; }
        public int LoaiDoiTuong { get; set; }
        public int LoaiVeId { get; set; }
    }

    public class VeDiTichLoaiTrinhDien
    {
        public int Id { get; set; }
        public string TenLoai { get; set; }
        public decimal GiaVe { get; set; }
        public LoaiKhachVeDiTich Loai { get; set; }
        public string TenDoiTuong { get; set; }
    }

    public class VeDiTichLoaiDongBo
    {
        public string name { get; set; }
        public int ticketTypeID { get; set; }
        public int customerTypeID { get; set; }
        public string customerTypeName { get; set; }
        public decimal price { get; set; }
    }

    public class VeDiTichLoaiRequest
    {
        public int LoaiDoiTuong { get; set; }
        public int LoaiVe { get; set; }
    }

    public class VeDiTichDiaDiemDongBo
    {
        public int Id { get; set; }
        public string title { get; set; }
    }

    public class VeDiTichDongBo
    {
        public int placeID { get; set; }
        public decimal total { get; set; }
        public string placeTitle { get; set; }
        public int soVeDon { get; set; }
        public int soVeTuyen { get; set; }
        public int customerType { get; set; }
        public string customerTypeName { get; set; }
    }

    public class VeDiTichTrinhDien
    {
        public int Id { get; set; }
        public string TenLoaiKhach { get; set; }
        public DateTime NgayBan { get; set; }
        public string DiaDiem { get; set; }
        public int SoLuong { get; set; }
        public decimal TongTien { get; set; }
        public DateTime NgayDongBo { get; set; }
    }
    public class VeDiTichRequest
    {
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public int LoaiKhach { get; set; }
        public int DiaDiem { get; set; }
    }
}
