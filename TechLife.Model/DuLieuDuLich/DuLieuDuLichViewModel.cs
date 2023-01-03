using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
    public class DuLieuDuLichViewModel
    {
        public int Id { get; set; }
        public string TenCongTy { get; set; }
        public string Ten { get; set; }
        public int HangSao { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime NgayQuyetDinh { get; set; }
        public int LoaiHinhId { get; set; }
        public int LinhVucKinhDoanhId { get; set; }
        public int GioMoCua { get; set; }
        public int GioDongCua { get; set; }
        public LoaiHinhModel LoaiHinh { get; set; }
        public decimal TongVonDauTuBanDau { get; set; }
        public decimal TongVonDauTuBoSung { get; set; }
        public decimal DienTichMatBang { get; set; }
        public decimal DienTichMatBangXayDung { get; set; }
        public decimal DienTichXayDung { get; set; }
        public int SoTang { get; set; }
        public string SoGiayPhep { get; set; }
        public DateTime ThoiDiemBatDauKinhDoanh { get; set; }

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

        public string ViTriTrenBanDo { get; set; }
        public string GhiChu { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }

        public List<LoaiPhongHoSoModel> DSLoaiPhong { get; set; }
        public List<DichVuHoSoModel> DSDichVu { get; set; }
        public List<NgoaiNguHoSoModel> DSNgoaiNgu { get; set; }
        public List<MucDoTTNNHoSoModel> DSMucDoTTNN { get; set; }
        public List<TrinhDoHoSoModel> DSTrinhDo { get; set; }
        public List<BoPhanHoSoModel> DSBoPhan { get; set; }
        public List<TienNghiHoSoModel> DSTienNghi { get; set; }

        
    }
}
