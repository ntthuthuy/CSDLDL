using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class DoanhNghiep
    {
        public int Id { get; set; }
        public string MaSoDoanhNghiep { get; set; }
        public string TenDoanhNghiep { get; set; }
        public string NguoiDaiDien { get; set; }
        public DateTime? NgayThanhLap { get; set; }
        public string DiaChi { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public string? DienThoai { get; set; }
        public string HopThu { get; set; }
        public string TrangChu { get; set; }
        public int MaLoaiHinh { get; set; }
        public int MaNganhNgheChinh { get; set; }
        public int MaTrangThai { get; set; }
        public int? IDPhuongXa { get; set; }
        public int? IDQuanHuyen { get; set; }
        public int? NguonDongBo { get; set; }
        public string? DongBoID { get; set; }
    }

    public class DoanhNghiepTrinhDien
    {
        public int Id { get; set; }
        public string TenLoaiHinh { get; set; }
        public string MaSoDoanhNghiep { get; set; }
        public string TenDoanhNghiep { get; set; }
        public string TenTrangThai { get; set; }
        public string TenClassCSS { get; set; }
        public string MaTrangThai { get; set; }
        public string NguoiDaiDien { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Fax { get; set; }
        public string HopThu { get; set; }
        public string TrangChu { get; set; }
        public string NganhNgheChinhId { get; set; }
        public string TenNganhNgheChinh { get; set; }
        public List<DoanhNghiepVanBanTrinhDien> DSVanBan { get; set; }
        public int IDPhuongXa { get; set; }
        public int IDQuanHuyen { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public string DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }

    public class DataDoanhNghiep
    {
        public List<DoanhNghiepDongBo> data { get; set; }
        public int totalRow { get; set; }
    }

    public class DoanhNghiepDongBo
    {
        public int totalRow { get; set; }
        public string maDoanhNghiep { get; set; }
        public string tenDoanhNghiep { get; set; }
        public string nguoiDaiDien { get; set; }
        public string ngayCap { get; set; }
        public string diaChi { get; set; }
        public string dienThoai { get; set; }
        public string email { get; set; }
        public string web { get; set; }
        public int loaiHinhID { get; set; }
        public string maNganhNgheChinh { get; set; }
        public int trangThaiID { get; set; }
        public string tenNganhNghe { get; set; }
        public string dienGiai { get; set; }
        public double? lat { get; set; }
        public double? Long { get; set; }
        public int? maPhuongXa { get; set; }
        public string tenPhuongXa { get; set; }
        public int? maQuanHuyen { get; set; }
        public string tenQuanHuyen { get; set; }
        public int totalRows { get; set; }
    }

    public class DanhSachDoanhNghiepDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public string description { get; set; }
        public DataDoanhNghiep data { get; set; }
        public string optionValues { get; set; }
    }



    public class DoanhNghiepRequest
    {
        public string tukhoa { get; set; }
        public int loai { get; set; }
        public int diachi { get; set; }
        public int nganhnghe { get; set; }
        public int nguondongbo { get; set; }
        public int huyen { get; set; }
        public int trangthai { get; set; }
    }
}
