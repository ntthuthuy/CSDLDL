using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechLife.Model.HoSoVanBan;
using TechLife.Model.HueCIT;
using TechLife.Model.LoaiPhong;
using TechLife.Model.NhaCungCap;

namespace TechLife.Model.DuLieuDuLich
{
    public class DuLieuDuLichModel
    {
        public int Id { get; set; }

        // [Range(1, Int32.MaxValue, ErrorMessage = "Vui lòng chọn nhà cung cấp")]
        public string TenNhaCungCap { get; set; }
        public int NhaCungCapId { get; set; } = 0;
        public NhaCungCapVm NhaCungCap { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên hồ sơ")]
        public string Ten { get; set; }

        public string TenVietTat { get; set; }
        public int HangSao { get; set; }
        public string XepHangSao { get; set; }
        public string SoQuyetDinh { get; set; }

        [DataType(DataType.Date)]
        public DateTime? NgayQuyetDinh { get; set; }
        [DataType(DataType.Date)]
        public DateTime? NgayHetHan { get; set; }
        public bool IsDatChuan { get; set; }
        public string SoCVDatChuan { get; set; }

        [DataType(DataType.Date)]
        public DateTime? NgayCVDatChuan { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Vui lòng chọn loại")]
        public int LoaiHinhId { get; set; }

        public string MaSoCapPhep { get; set; }
        public string DonViCapPhep { get; set; }

        [DataType(DataType.Date)]
        public DateTime? NgayCapPhep { get; set; }

        public FileUploadModel DocumentFiles { get; set; }
        public int LinhVucKinhDoanhId { get; set; }
        public string GioMoCua { get; set; }
        public string GioDongCua { get; set; }
        public LoaiHinhModel LoaiHinh { get; set; }
        public decimal TongVonDauTuBanDau { get; set; }
        public decimal TongVonDauTuBoSung { get; set; }
        public decimal DienTichMatBang { get; set; }
        public decimal DienTichMatBangXayDung { get; set; }
        public decimal DienTichXayDung { get; set; }
        public int SoTang { get; set; }
        public int TongSoPhong { get; set; }
        public int TongSoGiuong { get; set; }
        public string SoGiayPhep { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ThoiDiemBatDauKinhDoanh { get; set; }

        public int SoLanChuyenChu { get; set; }

        // Địa chỉ
        public string SoNha { get; set; }

        public string DuongPho { get; set; }

        public int PhuongXaId { get; set; }

        public string PhuongXa { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Vui lòng chọn quận, huyện, thị xã")]
        public int QuanHuyenId { get; set; }
        public string QuanHuyen { get; set; }
        public int TinhThanhId { get; set; }
        public string TinhThanh { get; set; }
        public string DiaChi { get; set; }

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

        public int SoLDNamTrongNuoc { get; set; }
        public int SoLDNamNgoaiNuoc { get; set; }
        public int SoLDNuTrongNuoc { get; set; }
        public int SoLDNuNgoaiNuoc { get; set; }

        public int SoLDTrucTiep { get; set; }
        public int SoLDGianTiep { get; set; }
        public int SoLDThuongXuyen { get; set; }
        public int SoLDThoiVu { get; set; }

        public bool KhamSucKhoeDinhKy { get; set; }
        public bool TrangPhucRieng { get; set; }
        public bool PhongChayNo { get; set; }
        public bool CNVSMoiTruong { get; set; }

        // Thông tin khách
        public bool IsNhaHangTrongCSLT { get; set; }
        public int CSLTId { get; set; }
        public string ViTriTrenBanDo { get; set; }
        public string GioiThieu { get; set; }
        public string GhiChu { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }

        public List<LoaiPhongHoSoModel> DSLoaiPhong { get; set; }
        public List<ThucDonHoSoModel> DSThucDon { get; set; }
        public List<VeDichVuHoSoModel> DSVeDichVu { get; set; }
        public List<DichVuHoSoModel> DSDichVu { get; set; }
        public List<NgoaiNguHoSoModel> DSNgoaiNgu { get; set; }
        public List<MucDoTTNNHoSoModel> DSMucDoTTNN { get; set; }
        public List<TrinhDoHoSoModel> DSTrinhDo { get; set; }
        public List<BoPhanHoSoModel> DSBoPhan { get; set; }
        public List<TienNghiHoSoModel> DSTienNghi { get; set; }
        public List<DanhGiaModel> DSDanhGia { get; set; }
        public List<QuyMoNhaHangVm> DSNhaHang { get; set; }
        public List<FileUploadModel> Images { get; set; }
        public List<HoSoVanBanVm> DSVanBan { get; set; }
        public List<TourModel> Tours { get; set; }
        public List<LoaiPhongGiuong> DSLoaiPhongGiuong { get; set; }
        public List<AmenityVm> Amenities { get; set; }
        public int TypeOfBusinessId { get; set; }
        public List<int> AmenitieId { get; set; }
        public string GiaThamKhao { get; set; }
        //HueCIT
        public int? LoaiDiaDiemAnUong { get; set; }
        public LoaiDiaDiemAnUong LoaiDiaDiem { get; set; }
        public int? PhucVu { get; set; }
        public double? ToaDoX { get; set; }
        public double? ToaDoY { get; set; }
        public string MaDoanhNghiep { get; set; }
        public int? NguonDongBo { get; set; }
        public int? DongBoID { get; set; }
        public string GiaThamKhaoTu { get; set; }
        public string GiaThamKhaoDen { get; set; }
        public int? MaSoThue { get; set; }

        public string NgonNguId { get; set; }
    }
}