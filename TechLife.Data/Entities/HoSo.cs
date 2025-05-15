using System;
using System.Collections.Generic;

namespace TechLife.Data.Entities
{
    public class HoSo
    {
        public int Id { get; set; }
        //public string TenCongTy { get; set; }
        public string Ten { get; set; }
        public string TenVietTat { get; set; }
        public string ViTriTrenBanDo { get; set; }
        public int NhaCungCapId { get; set; }


        /// <summary>
        /// Enum LinhVucKinhDoanh
        /// </summary>
        public int LinhVucKinhDoanhId { get; set; }
        public int HangSao { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public int LoaiHinhId { get; set; }

        public bool IsDatChuan { get; set; }
        public string SoCVDatChuan { get; set; }
        public DateTime? NgayCVDatChuan { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public string MaSoCapPhep { get; set; }
        public string DonViCapPhep { get; set; }
        public DateTime? NgayCapPhep { get; set; }

        public decimal TongVonDauTuBanDau { get; set; }
        public decimal TongVonDauTuBoSung { get; set; }
        public decimal DienTichMatBang { get; set; }
        public decimal DienTichMatBangXayDung { get; set; }
        public decimal DienTichXayDung { get; set; }

        public int SoTang { get; set; }
        public int TongSoPhong { get; set; }
        public int TongSoGiuong { get; set; }
        public string SoGiayPhep { get; set; }
        public string GioMoCua { get; set; }
        public string GioDongCua { get; set; }
        public DateTime? ThoiDiemBatDauKinhDoanh { get; set; }

        public int SoLanChuyenChu { get; set; }

        // Địa chỉ
        public string SoNha { get; set; }
        public string DuongPho { get; set; }
        public int PhuongXaId { get; set; }
        public int QuanHuyenId { get; set; }
        public int TinhThanhId { get; set; }
        // Liên hệ
        public string SoDienThoai { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        //Thông tin người đại diện
        public string HoTenNguoiDaiDien { get; set; }
        public string ChucVuNguoiDaiDien { get; set; }
        public int GioiTinhNguoiDaiDien { get; set; }
        public string SoDienThoaiNguoiDaiDien { get; set; }


        // Số lượng lao động

        public int SoLuongLaoDong { get; set; }
        public int DoTuoiTBNam { get; set; }
        public int DoTuoiTBNu { get; set; }

        public bool KhamSucKhoeDinhKy { get; set; }
        public bool TrangPhucRieng { get; set; }
        public bool PhongChayNo { get; set; }
        public bool CNVSMoiTruong { get; set; }

        public bool IsNhaHangTrongCSLT { get; set; }
        public int CSLTId { get; set; }

        // Số lượng lao động


        public int SoLDNamTrongNuoc { get; set; }
        public int SoLDNamNgoaiNuoc { get; set; }
        public int SoLDNuTrongNuoc { get; set; }
        public int SoLDNuNgoaiNuoc { get; set; }

        public int SoLDTrucTiep { get; set; }
        public int SoLDGianTiep { get; set; }
        public int SoLDThuongXuyen { get; set; }
        public int SoLDThoiVu { get; set; }

        // Thông tin khác
        public string GioiThieu { get; set; }
        public string GhiChu { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public string NgonNguId { get; set; }
        public decimal? GiaThamKhao { get; set; }

        //HueCIT
        public int? LoaiDiaDiemAnUong { get; set; }
        public int? PhucVu { get; set; }
        public double? ToaDoX { get; set; }
        public double? ToaDoY { get; set; }
        public string MaDoanhNghiep { get; set; }
        public int? NguonDongBo { get; set; }
        public int? DongBoID { get; set; }
        public string GiaThamKhaoTu { get; set; }
        public string GiaThamKhaoDen { get; set; }

        // Lịch sử cập nhật
        public virtual List<LichSuCapNhat> LichSuCapNhat { get; set; }
    }
}
