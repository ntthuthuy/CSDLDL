using System;
using System.Collections.Generic;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class PhanAnhHienTruong
    {
        public int Id { get; set; }
        public int PhanAnhId { get; set; }
        public string TieuDe { get; set; }
        public int MaLinhVuc { get; set; }
        public string NoiDung { get; set; }
        public int? ObjectId { get; set; }
        public int? MaDiaDiem { get; set; }
        public int? MaLoaiDuLieu { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string DiaChiSuKien { get; set; }
        public string NguoiGui { get; set; }
        public DateTime? NgayGui { get; set; }
        public string MaCoQuanXuLy { get; set; }
        public string TenCoQuan { get; set; }
        public string YKienXuLy { get; set; }
        public DateTime? NgayXuLy { get; set; }
        public bool LoaiXuLy { get; set; }
        public DateTime NgayTao { get; set; }
    }

    public class PhanAnhHienTruongModel
    {
        public int PhanAnhID { get; set; }
        public int TaiKhoan { get; set; }
        public int ChuyenMucID { get; set; }
        public string TenChuyenMuc { get; set; }
        public string TieuDe { get; set; }
        public string NoiDungPhanAnh { get; set; }
        public DateTime NgayPhanAnh { get; set; }
        public double KinhDo { get; set; }
        public double ViDo { get; set; }
        public string NoiDungTraLoi { get; set; }
        public string FileTraLoi { get; set; }
        public string DiaChiSuKien { get; set; }
        public DateTime NgayTraLoi { get; set; }
        public List<FileDinhKem> DanhSachFileDinhKem { get; set; }
        public List<FileDinhKem> DanhSachFileDinhKem_Kq { get; set; }
        public string DonViThuLy { get; set; }
        public string TrangThai { get; set; }
        public DateTime DenNgay { get; set; }
        public bool CongKhai { get; set; }
        public string DanhGia { get; set; }
        public bool LoaiXuLy { get; set; }
    }

    #region Đồng bộ dữ liệu phản ánh hiện trường theo chuyên mục
    public class PhanAnhHienTruongDongBo
    {
        public int PhanAnhID { get; set; }
        public string TaiKhoan { get; set; }
        public int? ChuyenMucID { get; set; }
        public string TenChuyenMu { get; set; }
        public string TieuDe { get; set; }
        public string NoiDungPhanAnh { get; set; }
        public string NgayPhanAnh { get; set; }
        public double? KinhDo { get; set; }
        public double? ViDo { get; set; }
        public string NoiDungTraLoi { get; set; }
        public string FileTraLoi { get; set; }
        public string DiaChiSuKien { get; set; }
        public string NgayTraLoi { get; set; }
        public List<FileDinhKem> DanhSachFileDinhKem { get; set; }
        public List<FileDinhKem> DanhSachFileDinhKem_Kq { get; set; }
        public string DonViThuLy { get; set; }
        public string TrangThai { get; set; }
        public string DenNgay { get; set; }
        public bool CongKhai { get; set; }
        public string DanhGia { get; set; }
    }

    public class DanhSachPhanAnhHienTruongDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<PhanAnhHienTruongDongBo> data { get; set; }
        public int totalrow { get; set; }
    }
    #endregion

    #region Đồng bộ dữ liệu phản ánh hiện trường theo đơn vị
    public class PhanAnhHienTruongTheoDonVi
    {
        public int PhanAnhID { get; set; }
        public int? ChuyenMucID { get; set; }
        public string TenChuyenMuc { get; set; }
        public string TieuDe { get; set; }
        public string NoiDungPhanAnh { get; set; }
        public string NgayPhanAnh { get; set; }
        public double? KinhDo { get; set; }
        public double? ViDo { get; set; }
        public string NoiDungTraLoi { get; set; }
        public string DiaChiSuKien { get; set; }
        public string NgayTraLoi { get; set; }
        public List<FileDinhKem> DanhSachFileDinhKem { get; set; }
        public List<FileDinhKem> DanhSachFileDinhKem_Kq { get; set; }
        public string NgayCapNhat { get; set; }
    }

    public class DSPhanAnhHienTruongTheoDonVi
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<PhanAnhHienTruongTheoDonVi> data { get; set; }
        public int totalrow { get; set; }
    }
    #endregion

    public class FileDinhKem
    {
        public string FileName { get; set; }
        public string FileName_Thumb { get; set; }
        public bool? OpenFile { get; set; }
    }

    public class PhanAnhHienTruongGetData
    {
        public int PhanAnhID { get; set; }
        public string TaiKhoan { get; set; }
        public int ChuyenMucID { get; set; }
        public string TenChuyenMuc { get; set; }
        public string TieuDe { get; set; }
        public string NoiDungPhanAnh { get; set; }
        public string NgayPhanAnh { get; set; }
        public string KinhDo { get; set; }
        public string ViDo { get; set; }
        public string NoiDungTraLoi { get; set; }
        public string FileTraLoi { get; set; }
        public string DiaChiSuKien { get; set; }
        public string NgayTraLoi { get; set; }
        public List<FileDinhKem> DanhSachFileDinhKem { get; set; }
        public List<FileDinhKem> DanhSachFileDinhKem_Kq { get; set; }
        public string DonViThuLy { get; set; }
        public string TrangThai { get; set; }
        public string DenNgay { get; set; }
        public bool CongKhai { get; set; }
        public string DanhGia { get; set; }
        public bool LoaiXuLy { get; set; }
    }
    public class PhanAnhHienTruongResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public PhanAnhHienTruongGetData data { get; set; }
    }


    public class PhanAnhHienTruongGetDataDetail
    {
        public int PhanAnhID { get; set; }
        public int NguonID { get; set; }
        public int ChuyenMucID { get; set; }
        public string TenChuyenMuc { get; set; }
        public string TieuDe { get; set; }
        public string NoiDungPhanAnh { get; set; }
        public string NgayPhanAnh { get; set; }
        public string KinhDo { get; set; }
        public string ViDo { get; set; }
        public string NoiDungTraLoi { get; set; }
        public string DiaChiSuKien { get; set; }
        public string NgayTraLoi { get; set; }
        public string DonViThuLy { get; set; }
        public string DenNgay { get; set; }
        public string DanhGia { get; set; }
        public bool CongKhai { get; set; }
    }
    public class PhanAnhHienTruongRequest
    {
        public int LoaiXuLy { get; set; }
        public string MaDinhDanh { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public int Page { get; set; }
        public int Perpage { get; set; }
    }

    public class PhanAnhHienTruongDetail
    {
        public int code { get; set; }
        public string message { get; set; }
        public PhanAnhHienTruongGetDataDetail data { get; set; }
        public int totalrow { get; set; }
    }

    public class PhanAnhHienTruongTrinhDien
    {
        public int Id { get; set; }
        public int PhanAnhID { get; set; }
        public int MaLinhVuc { get; set; }
        public string LinhVuc { get; set; }
        public DateTime NgayGui { get; set; }
        public DateTime? NgayXuLy { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string YKienXuLy { get; set; }
        public int LoaiXuLy { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
    }

    public class FileDinhKemTrinhDien
    {
        public string Filename { get; set; }
        public bool IsKetQua { get; set; }
    }

    public class PhanAnhHienTruongTrinhDienMod
    {
        public PhanAnhHienTruongTrinhDien PhanAnhHienTruongTrinhDien { get; set; }
        public List<FileDinhKemTrinhDien> FileDinhKemTrinhDien { get; set; }
    }

    public class PhanAnhHienTruongTrinhDienRequest
    {
        public string Keywork { get; set; }
        public int LinhVuc { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public int LoaiXuLy { get; set; }
    }

    public class PhanAnhHienTruongEditRequest
    {
        public int ID { get; set; }
        public int MaDiaDiem { get; set; }
        public int MaLoaiDuLieu { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class PhanAnhHienTruongHttpContent
    {
        public string LoaiXuLy { get; set; }
        public string MaDinhDanh { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public string Page { get; set; }
        public string Perpage { get; set; }
    }

}
