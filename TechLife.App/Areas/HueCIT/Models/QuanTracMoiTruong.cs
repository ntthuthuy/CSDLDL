using System;
using System.Collections.Generic;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class QuanTracMoiTruong
    {
        public int Id { get; set; }
        public float? X { get; set; }
        public float? Y { get; set; }
        public string Node { get; set; }
        public string TenNode { get; set; }
        public string TenThongSo { get; set; }
        public double GiaTri { get; set; }
        public string DonViTinh { get; set; }
        public DateTime? ThoiDiem { get; set; }
        public int TrangThai { get; set; }
        public string _id { get; set; }
    }

    public class QuanTracMoiTruongTrinhDien
    {
        public int Id { get; set; }
        public string _id { get; set; }
        public DateTime ThoiDiem { get; set; }
        public string TenThongSo { get; set; }
        public double? GiaTri { get; set; }
        public string Node { get; set; }
        public string TenNode { get; set; }
        public int? TrangThai { get; set; }
        public string TenTrangThai { get; set; }
    }

    public class DanhSachQuanTracMoiTruong
    {
        public DateTime ThoiDiem { get; set; }
        public QuanTracMoiTruongTrinhDien? AQI { get; set; }
        public QuanTracMoiTruongTrinhDien? CO2 { get; set; }
        public QuanTracMoiTruongTrinhDien? HUM { get; set; }
        public QuanTracMoiTruongTrinhDien? PM01 { get; set; }
        public QuanTracMoiTruongTrinhDien? PM10 { get; set; }
        public QuanTracMoiTruongTrinhDien? PM25 { get; set; }
        public QuanTracMoiTruongTrinhDien? TEMP { get; set; }
        public QuanTracMoiTruongTrinhDien? TVOC { get; set; }
    }

    public class QuanTracMoiTruongDongBo
    {
        public int Id { get; set; }
        public string _id { get; set; }
        public string time { get; set; }
        public string node { get; set; }
        public string deviceId { get; set; }
        public string index { get; set; }
        public string originalUnit { get; set; }
        public double originalValue { get; set; }
        public bool anomaly { get; set; }
        public List<object> forecast { get; set; }
        public double value { get; set; }
        public string unit { get; set; }
        public int status { get; set; }
        public double value_vn { get; set; }
        public int status_vn { get; set; }
    }
    public class QuanTracMoiTruongRequest
    {
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public string DiaDiem { get; set; }
    }

    public class QuanTracMoiTruongTrangThai
    {
        public string Id { get; set; }
        public string TenTrangThai { get; set; }
    }

    public class QuanTracMoiTruongFilter
    {
        public string Id { get; set; }
        public string TenThongSo { get; set; }
        public string Node { get; set; }
    }
}
