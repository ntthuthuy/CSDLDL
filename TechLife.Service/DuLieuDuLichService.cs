using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Common.Enums.HueCIT;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Data.Extensions;
using TechLife.Data.Repositories;
using TechLife.Model;
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.HoSoVanBan;
using TechLife.Model.NhaCungCap;
using TechLife.Model.TienNghi;
using TechLife.Service.Common;

namespace TechLife.Service
{
    public interface IDuLieuDuLichService
    {
        Task<List<DuLieuDuLichModel>> GetAll(int linhvucId);
        Task<List<DuLieuDuLichModel>> GetAllCSLT(string key = "");

        Task<PagedResult<DuLieuDuLichModel>> GetPaging(string langId, int linhvucId, HoSoFromRequets request);

        Task<ApiResult<DuLieuDuLichModel>> Create(string langId, DuLieuDuLichModel request);

        Task<ApiResult<bool>> UploadImage(int id, ImageUploadRequest request);

        Task<ApiResult<bool>> UploadFile(int id, DocumentUploadRequest request);

        Task<ApiResult<bool>> UploadDoc(int id, HoSoVanBanCreateRequets requests);

        Task<ApiResult<int>> Update(int id, DuLieuDuLichModel request);

        Task<DuLieuDuLichModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);

        Task<ApiResult<int>> DeleteNhaHangLuuTru(int id);

        Task<ApiResult<bool>> ImportLuuTru(List<DuLieuDuLichImport> request);

        Task<ApiResult<bool>> ImportLuHanh(List<DuLieuDuLichImport> request);

        Task<List<DuLieuDuLichTheoDiaBanReportVm>> DuLieuDuLichTheoDiaBan();

        List<DanhMucMoblieVm> GetCategory();

        Task<List<DuLieuDuLichTheoLoaiHinhVrm>> LuuTruTheoLoaiHinh();

        Task<List<DuLieuDuLichTheoLoaiHinhVrm>> KhachSanTheoHangSao();

        Task<List<DuLieuDuLichTheoLoaiHinhVrm>> LuuTruTheoDiaBan();

        Task<List<DuLieuDuLichTheoLoaiHinhVrm>> NhaHangTheoLoaiHinh();

        Task<List<DuLieuDuLichTheoLoaiHinhVrm>> NhaHangTheoDiaBan();

        Task<List<DuLieuDuLichTheoLoaiHinhVrm>> LuHanhTheoLoaiHinh();

        Task<List<DuLieuDuLichTheoLoaiHinhVrm>> MuaSamTheoLoaiHinh();

        Task<List<DuLieuDuLichTheoLoaiHinhVrm>> MuaSamTheoDiaBan();

        Task<List<DuLieuDuLichTheoLoaiHinhVrm>> TourTheoLoaiHinh();

        Task<List<DuLieuDuLichTheoLoaiHinhVrm>> DiemDuLichTheoLoaiHinh();

        Task<List<DuLieuDuLichTheoLoaiHinhVrm>> HDVTheoLoaiThe();

        Task<List<DuLieuDuLichTheoLoaiHinhVrm>> HDVTheoNgonNgu();
        Task<PagedResult<TimKiemDuLieuVrm>> TimKiemDuLieu(GetPagingRequest request, int linhvucId = 0);
        Task<PagedResult<TimKiemDuLieuHDVVrm>> TimKiemDuLieuHDV(DuLieuDuLichSearchRequest request);
        Task<PagedResult<TimKiemDuLieuCSLTVrm>> TimKiemDuLieuCSLT(DuLieuDuLichCSLTSearchRequest request);
        Task<PagedResult<TimKiemDuLieuNhaHangVrm>> TimKiemDuLieuNhaHang(DuLieuDuLichNhaHangSearchRequest request);
        Task<PagedResult<TimKiemDuLieuThanhTraVrm>> TimKiemDuLieuThanhTra(DuLieuDuLichThanhTraSearchRequest request);
        Task<PagedResult<TimKiemDuLieuLuHanhVrm>> TimKiemDuLieuLuHanh(DuLieuDuLichLuHanhSearchRequest request);
        Task<PagedResult<TimKiemDuLieuDiemDuLichVrm>> TimKiemDuLieuDiemDuLich(DuLieuDuLichDiemDuLichSearchRequest request);
        Task<PagedResult<TimKiemDuLieuCoSoMuaSamVrm>> TimKiemDuLieuCoSoMuaSam(DuLieuDuLichCoSoMuaSamSearchRequest request);
        Task<PagedResult<DuLieuDuLichRpt>> DuLieuDuLich(string langId, int loaihinh, RptFromRequets request);

        Task<List<LoaiHinhVm>> LoaiHinh(int linhvucId);

        Task<DuLieuDuLichRpt> DuLieuDuLichById(int linhvucId, int id);
        Task<ApiResult<bool>> Delete_File_Vanban(int id);
        Task<List<DuLieuDuLichExportRequest>> GetAllExportData();
        Task<List<DuLieuDuLichAPI>> GetAll_CSLT();
        Task<List<DuLieuDuLichAPI>> GetAll_NhaHang();
        Task<List<DuLieuDuLichAPI>> GetAll_LuHanh();
        Task<List<DuLieuDuLichAPI>> GetAll_CoSoMuaSam();
        Task<List<DuLieuDuLichAPI>> GetAll_VanChuyen();

        Task<Dictionary<int, int>> DuLieuDuLichEnglish(List<DuLieuDuLichModel> items);

        //HueCIT
        Task<List<HoSoVanBanVm>> GetListVanBanByHoSo(int hosoId);

    }

    public class DuLieuDuLichService : BaseRepository, IDuLieuDuLichService
    {
        private readonly TLDbContext _context;

        private readonly ILoaiHinhService _loaiHinhService;
        private readonly IBoPhanService _boPhanService;
        private readonly INgoaiNguService _ngoaiNguService;
        private readonly ILoaiPhongService _loaiPhongService;
        private readonly IDichVuService _dichVuService;
        private readonly IMucDoThongThaoNgoaiNguService _mucDoThongThaoNgoaiNguService;
        private readonly ITienNghiService _tienNghiService;
        private readonly ITrinhDoService _trinhDoService;
        private readonly IThucDonService _thucDonService;
        private readonly IFileUploadService _fileUploadService;
        private readonly ILogService _logService;
        private readonly IStorageService _storageService;
        private readonly IDanhGiaService _danhGiaService;
        private readonly ITourService _tourService;
        private readonly IDuLieuDuLichService _duLieuDuLichService;

        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        //HueCIT
        private const string IMG_TYPE = "bmp, png, jpg, jpeg, gif, webp";
        private const string MEDIA_TYPE = "mp4, mov, wmv, avi, flv, mkv, 3gp, mp3, wav";
        private const string DOC_TYPE = "doc, docx, xls, xlsx, pdf";

        public DuLieuDuLichService(TLDbContext context
            , ILoaiHinhService loaiHinhService
            , IBoPhanService boPhanService
            , ILoaiPhongService loaiPhongService
            , IDichVuService dichVuService
            , IMucDoThongThaoNgoaiNguService mucDoThongThaoNgoaiNguService
            , ITienNghiService tienNghiService
            , ITrinhDoService trinhDoService
            , INgoaiNguService ngoaiNguService
            , IFileUploadService fileUploadService
            , IStorageService storageService
            , ILogService logService
            , IDanhGiaService danhGiaService
            , IThucDonService thucDonService
            , ITourService tourService) : base(context)
        {
            _context = context;
            _loaiHinhService = loaiHinhService;
            _boPhanService = boPhanService;
            _ngoaiNguService = ngoaiNguService;
            _loaiPhongService = loaiPhongService;
            _dichVuService = dichVuService;
            _mucDoThongThaoNgoaiNguService = mucDoThongThaoNgoaiNguService;
            _tienNghiService = tienNghiService;
            _trinhDoService = trinhDoService;
            _fileUploadService = fileUploadService;
            _storageService = storageService;
            _thucDonService = thucDonService;
            _logService = logService;
            _danhGiaService = danhGiaService;
            _tourService = tourService;
        }
        public async Task<ApiResult<DuLieuDuLichModel>> Create(string langId, DuLieuDuLichModel request)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.TenNhaCungCap))
                {
                    var ncc = new NhaCungCap()
                    {
                        Ten = request.TenNhaCungCap,
                        IsStatus = true,
                        IsDelete = false
                    };
                    await _context.NhaCungCap.AddAsync(ncc);
                    await _context.SaveChangesAsync();
                    request.NhaCungCapId = ncc.Id;
                }
                var coSoLuuTru = new HoSo()
                {
                    IsDelete = request.IsDelete,
                    IsStatus = request.IsStatus,
                    NgonNguId = langId,
                    Ten = request.Ten,
                    ChucVuNguoiDaiDien = request.ChucVuNguoiDaiDien,
                    CNVSMoiTruong = request.CNVSMoiTruong,
                    DienTichMatBang = request.DienTichMatBang,
                    DienTichMatBangXayDung = request.DienTichMatBangXayDung,
                    DienTichXayDung = request.DienTichXayDung,
                    DoTuoiTBNam = request.DoTuoiTBNam,
                    DoTuoiTBNu = request.DoTuoiTBNu,
                    DuongPho = request.DuongPho,
                    Email = request.Email,
                    Fax = request.Fax,
                    GhiChu = request.GhiChu,
                    GioiTinhNguoiDaiDien = request.GioiTinhNguoiDaiDien,
                    HangSao = request.HangSao,
                    HoTenNguoiDaiDien = request.HoTenNguoiDaiDien,
                    KhamSucKhoeDinhKy = request.KhamSucKhoeDinhKy,
                    LinhVucKinhDoanhId = request.LinhVucKinhDoanhId,
                    LoaiHinhId = request.LoaiHinhId,
                    NgayQuyetDinh = request.NgayQuyetDinh,
                    PhongChayNo = request.PhongChayNo,
                    PhuongXaId = request.PhuongXaId,
                    QuanHuyenId = request.QuanHuyenId,
                    SoDienThoai = request.SoDienThoai,
                    SoDienThoaiNguoiDaiDien = request.SoDienThoaiNguoiDaiDien,
                    SoGiayPhep = request.SoGiayPhep,
                    SoLanChuyenChu = request.SoLanChuyenChu,
                    SoLuongLaoDong = request.SoLuongLaoDong,
                    SoNha = request.SoNha,
                    SoQuyetDinh = request.SoQuyetDinh,
                    SoTang = request.SoTang,
                    NhaCungCapId = request.NhaCungCapId,
                    //TenCongTy = request.TenCongTy,
                    ThoiDiemBatDauKinhDoanh = request.ThoiDiemBatDauKinhDoanh,
                    TinhThanhId = request.TinhThanhId,
                    TongVonDauTuBanDau = request.TongVonDauTuBanDau,
                    TongVonDauTuBoSung = request.TongVonDauTuBoSung,
                    TrangPhucRieng = request.TrangPhucRieng,
                    Website = request.Website,
                    DonViCapPhep = request.DonViCapPhep,
                    MaSoCapPhep = request.MaSoCapPhep,
                    NgayCapPhep = request.NgayCapPhep,
                    GioDongCua = request.GioDongCua,
                    GioMoCua = request.GioMoCua,
                    SoLDGianTiep = request.SoLDGianTiep,
                    SoLDNamNgoaiNuoc = request.SoLDNamNgoaiNuoc,
                    SoLDNamTrongNuoc = request.SoLDNamTrongNuoc,
                    SoLDNuNgoaiNuoc = request.SoLDNuNgoaiNuoc,
                    SoLDNuTrongNuoc = request.SoLDNuTrongNuoc,
                    SoLDThoiVu = request.SoLDThoiVu,
                    SoLDThuongXuyen = request.SoLDThuongXuyen,
                    SoLDTrucTiep = request.SoLDTrucTiep,
                    TenVietTat = request.TenVietTat,
                    ViTriTrenBanDo = request.ViTriTrenBanDo,
                    IsDatChuan = request.IsDatChuan,
                    SoCVDatChuan = request.SoCVDatChuan,
                    NgayCVDatChuan = request.NgayCVDatChuan,
                    GioiThieu = request.GioiThieu,
                    TongSoGiuong = request.TongSoGiuong,
                    TongSoPhong = request.TongSoPhong,
                    GiaThamKhao = !String.IsNullOrEmpty(request.GiaThamKhao) ? request.GiaThamKhao.Replace(",", "") : "0",
                    //HueCIT
                    LoaiDiaDiemAnUong = request.LoaiDiaDiemAnUong,
                    PhucVu = request.PhucVu,
                    ToaDoX = request.ToaDoX,
                    ToaDoY = request.ToaDoY,
                    MaDoanhNghiep = request.MaDoanhNghiep
                };
                _context.HoSo.Add(coSoLuuTru);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    request.Id = coSoLuuTru.Id;

                    if (request.Amenities != null && request.Amenities.Count > 0)
                    {
                        foreach (var item in request.Amenities.Where(x => x.IsSelect))
                        {
                            await _context.Amenities.AddAsync(new Amenities()
                            {
                                AmenityId = item.Id,
                                CompanyId = coSoLuuTru.Id,
                                TypeOfBusinessId = request.TypeOfBusinessId
                            });
                            await _context.SaveChangesAsync();
                        }
                    }
                    if (request.DSDichVu != null && request.DSDichVu.Count() > 0)
                    {
                        foreach (var d in request.DSDichVu)
                        {
                            _context.DichVuHoSo.Add(new DichVuHoSo()
                            {
                                DichVuId = d.DichVu.Id,
                                DonViTinhId = d.DonViTinhId,
                                HoSoId = request.Id,
                                QuyMo = d.QuyMo,
                            });
                        }
                    }
                    if (request.DSBoPhan != null && request.DSBoPhan.Count() > 0)
                    {
                        foreach (var d in request.DSBoPhan)
                        {
                            _context.BoPhanHoSo.Add(new BoPhanHoSo()
                            {
                                BoPhanId = d.BoPhan.Id,
                                HoSoId = request.Id,
                                SoLuong = d.SoLuong,
                                DonViTinhId = d.DonViTinhId
                            });
                        }
                    }
                    if (request.DSLoaiPhong != null && request.DSLoaiPhong.Count() > 0)
                    {
                        foreach (var d in request.DSLoaiPhong)
                        {
                            foreach (var g in d.DSLoaiGiuong)
                            {
                                _context.LoaiPhongHoSo.Add(new LoaiPhongHoSo()
                                {
                                    LoaiPhongId = d.LoaiPhong.Id,
                                    HoSoId = request.Id,
                                    LoaiGiuongId = g.Id,
                                    DienTich = g.DienTich,
                                    GiaPhong = g.GiaPhong,
                                    SoPhong = g.SoPhong,
                                    SoNguoiLon = g.SoNguoiLon,
                                    SoTreEm = g.SoTreEm,
                                    TenHienThi = g.TenHienThi
                                });
                            }
                        }
                    }
                    if (request.DSMucDoTTNN != null && request.DSMucDoTTNN.Count() > 0)
                    {
                        foreach (var d in request.DSMucDoTTNN)
                        {
                            _context.MucDoTTNNHoSo.Add(new MucDoTTNNHoSo()
                            {
                                HoSoId = request.Id,
                                DonViTinhId = d.DonViTinhId,
                                MucDoId = d.MucDoThongThao.Id,
                                SoLuong = d.SoLuong
                            });
                        }
                    }
                    if (request.DSNgoaiNgu != null && request.DSNgoaiNgu.Count() > 0)
                    {
                        foreach (var d in request.DSNgoaiNgu)
                        {
                            _context.NgoaiNguHoSo.Add(new NgoaiNguHoSo()
                            {
                                HoSoId = request.Id,
                                DonViTinhId = d.DonViTinhId,
                                NgoaiNguId = d.NgoaiNgu.Id,
                                SoLuong = d.SoLuong
                            });
                        }
                    }
                    if (request.DSTienNghi != null && request.DSTienNghi.Count() > 0)
                    {
                        foreach (var d in request.DSTienNghi)
                        {
                            _context.TienNghiHoSo.Add(new TienNghiHoSo()
                            {
                                HoSoId = request.Id,
                                TienNghiId = d.TienNghi.Id,
                                SoLuong = d.SoLuong,
                                IsPhuPhi = d.IsPhuPhi,
                                IsSuDung = d.IsSuDung
                            });
                        }
                    }
                    if (request.DSTrinhDo != null && request.DSTrinhDo.Count() > 0)
                    {
                        foreach (var d in request.DSTrinhDo)
                        {
                            _context.TrinhDoHoSo.Add(new TrinhDoHoSo()
                            {
                                HoSoId = request.Id,
                                TrinhDoId = d.TrinhDo.Id,
                                SoLuong = d.SoLuong,
                                DonViTinhId = d.DonViTinhId
                            });
                        }
                    }
                    if (request.DSThucDon != null && request.DSThucDon.Count() > 0)
                    {
                        foreach (var d in request.DSThucDon)
                        {
                            _context.ThucDonHoSo.Add(new ThucDonHoSo()
                            {
                                DonGia = d.DonGia,
                                HosoId = request.Id,
                                MoTa = d.MoTa,
                                TenThucDon = d.TenThucDon
                            });
                        }
                    }
                    if (request.DSVeDichVu != null && request.DSVeDichVu.Count() > 0)
                    {
                        foreach (var d in request.DSVeDichVu)
                        {
                            _context.VeDichVuHoSo.Add(new VeDichVuHoSo()
                            {
                                GiaVe = d.GiaVe,
                                HosoId = request.Id,
                                MoTa = d.MoTa,
                                TenVe = d.TenVe
                            });
                        }
                    }
                    if (request.DSNhaHang != null && request.DSNhaHang.Count() > 0)
                    {
                        foreach (var d in request.DSNhaHang.Where(v => !v.IsDelete && v.TenGoi != null))
                        {
                            _context.QuyMoNhaHangLuuTru.Add(new QuyMoNhaHangLuuTru()
                            {
                                DienTich = d.DienTich,
                                TenGoi = d.TenGoi,
                                HoSoId = request.Id,
                                SoGhe = d.SoGhe
                            });
                            await _context.SaveChangesAsync();
                        }
                    }
                    if (request.DSVanBan != null && request.DSVanBan.Count() > 0)
                    {
                        foreach (var d in request.DSVanBan)
                        {
                            _context.HoSoVanBan.Add(new HoSoVanBan()
                            {
                                NoiCap = d.NoiCap,
                                TenGoi = d.TenGoi,
                                HosoId = request.Id,
                                NgayCap = DateTime.Now,
                                NgayHetHan = DateTime.Now,
                                MaSo = "",
                                FilePath = d.FilePath,
                                FileName = d.FileName,
                                GiayPhepId = d.GiayPhepId,
                                IsStatus = d.IsStatus,
                                Loai = LoaiFile.hosodulich.ToString()
                            });
                        }
                        await _context.SaveChangesAsync();
                    }
                    if (request.DSLoaiPhongGiuong != null && request.DSLoaiPhongGiuong.Count() > 0)
                    {
                        foreach (var d in request.DSLoaiPhongGiuong.Where(v => !v.IsDelete && v.TenGoi != null))
                        {
                            _context.LoaiGiuongPhong.Add(new LoaiGiuongPhong()
                            {
                                Ten = d.TenGoi,
                                LuuTruId = coSoLuuTru.Id,
                                GiaGiuongPhu = Convert.ToDecimal(d.GiaGiuong.Replace(",", "")),
                                GiaPhong = Convert.ToDecimal(d.GiaPhong.Replace(",", "")),
                                SoGiuong = d.SoGiuong,
                            });
                            await _context.SaveChangesAsync();
                        }
                    }
                    return new ApiSuccessResult<DuLieuDuLichModel>(request, "Thêm mới thành công!");
                }
                else
                {
                    return new ApiErrorResult<DuLieuDuLichModel>("Thêm lỗi!");
                }
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<DuLieuDuLichModel>(ex.Message);
            }
        }

        //public async Task<ApiResult<DuLieuDuLichModel>> Create(string langId, DuLieuDuLichModel request)
        //{
        //    try
        //    {
        //        var coSoLuuTru = new HoSo()
        //        {
        //            IsDelete = request.IsDelete,
        //            IsStatus = request.IsStatus,
        //            NgonNguId = langId,
        //            Ten = request.Ten,
        //            ChucVuNguoiDaiDien = request.ChucVuNguoiDaiDien,
        //            CNVSMoiTruong = request.CNVSMoiTruong,
        //            DienTichMatBang = request.DienTichMatBang,
        //            DienTichMatBangXayDung = request.DienTichMatBangXayDung,
        //            DienTichXayDung = request.DienTichXayDung,
        //            DoTuoiTBNam = request.DoTuoiTBNam,
        //            DoTuoiTBNu = request.DoTuoiTBNu,
        //            DuongPho = request.DuongPho,
        //            Email = request.Email,
        //            Fax = request.Fax,
        //            GhiChu = request.GhiChu,
        //            GioiTinhNguoiDaiDien = request.GioiTinhNguoiDaiDien,
        //            HangSao = request.HangSao,
        //            HoTenNguoiDaiDien = request.HoTenNguoiDaiDien,
        //            KhamSucKhoeDinhKy = request.KhamSucKhoeDinhKy,
        //            LinhVucKinhDoanhId = request.LinhVucKinhDoanhId,
        //            LoaiHinhId = request.LoaiHinhId,
        //            NgayQuyetDinh = request.NgayQuyetDinh,
        //            PhongChayNo = request.PhongChayNo,
        //            PhuongXaId = request.PhuongXaId,
        //            QuanHuyenId = request.QuanHuyenId,
        //            SoDienThoai = request.SoDienThoai,
        //            SoDienThoaiNguoiDaiDien = request.SoDienThoaiNguoiDaiDien,
        //            SoGiayPhep = request.SoGiayPhep,
        //            SoLanChuyenChu = request.SoLanChuyenChu,
        //            SoLuongLaoDong = request.SoLuongLaoDong,
        //            SoNha = request.SoNha,
        //            SoQuyetDinh = request.SoQuyetDinh,
        //            SoTang = request.SoTang,
        //            NhaCungCapId = request.NhaCungCapId,
        //            //TenCongTy = request.TenCongTy,
        //            ThoiDiemBatDauKinhDoanh = request.ThoiDiemBatDauKinhDoanh,
        //            TinhThanhId = request.TinhThanhId,
        //            TongVonDauTuBanDau = request.TongVonDauTuBanDau,
        //            TongVonDauTuBoSung = request.TongVonDauTuBoSung,
        //            TrangPhucRieng = request.TrangPhucRieng,
        //            Website = request.Website,
        //            DonViCapPhep = request.DonViCapPhep,
        //            MaSoCapPhep = request.MaSoCapPhep,
        //            NgayCapPhep = request.NgayCapPhep,
        //            GioDongCua = request.GioDongCua,
        //            GioMoCua = request.GioMoCua,
        //            SoLDGianTiep = request.SoLDGianTiep,
        //            SoLDNamNgoaiNuoc = request.SoLDNamNgoaiNuoc,
        //            SoLDNamTrongNuoc = request.SoLDNamTrongNuoc,
        //            SoLDNuNgoaiNuoc = request.SoLDNuNgoaiNuoc,
        //            SoLDNuTrongNuoc = request.SoLDNuTrongNuoc,
        //            SoLDThoiVu = request.SoLDThoiVu,
        //            SoLDThuongXuyen = request.SoLDThuongXuyen,
        //            SoLDTrucTiep = request.SoLDTrucTiep,
        //            TenVietTat = request.TenVietTat,
        //            ViTriTrenBanDo = request.ViTriTrenBanDo,
        //            IsDatChuan = request.IsDatChuan,
        //            SoCVDatChuan = request.SoCVDatChuan,
        //            NgayCVDatChuan = request.NgayCVDatChuan,
        //            GioiThieu = request.GioiThieu,
        //            TongSoGiuong = request.TongSoGiuong,
        //            TongSoPhong = request.TongSoPhong,
        //            //HueCIT
        //            LoaiDiaDiemAnUong = request.LoaiDiaDiemAnUong,
        //            PhucVu = request.PhucVu,
        //            ToaDoX = request.ToaDoX,
        //            ToaDoY = request.ToaDoY,
        //            MaDoanhNghiep = request.MaDoanhNghiep
        //        };
        //        _context.HoSo.Add(coSoLuuTru);
        //        var result = await _context.SaveChangesAsync();
        //        if (result > 0)
        //        {
        //            request.Id = coSoLuuTru.Id;

        //            if (request.DSDichVu != null && request.DSDichVu.Count() > 0)
        //            {
        //                foreach (var d in request.DSDichVu)
        //                {
        //                    _context.DichVuHoSo.Add(new DichVuHoSo()
        //                    {
        //                        DichVuId = d.DichVu.Id,
        //                        DonViTinhId = d.DonViTinhId,
        //                        HoSoId = request.Id,
        //                        QuyMo = d.QuyMo,
        //                    });
        //                }
        //            }
        //            if (request.DSBoPhan != null && request.DSBoPhan.Count() > 0)
        //            {
        //                foreach (var d in request.DSBoPhan)
        //                {
        //                    _context.BoPhanHoSo.Add(new BoPhanHoSo()
        //                    {
        //                        BoPhanId = d.BoPhan.Id,
        //                        HoSoId = request.Id,
        //                        SoLuong = d.SoLuong,
        //                        DonViTinhId = d.DonViTinhId
        //                    });
        //                }
        //            }
        //            if (request.DSLoaiPhong != null && request.DSLoaiPhong.Count() > 0)
        //            {
        //                foreach (var d in request.DSLoaiPhong)
        //                {
        //                    foreach (var g in d.DSLoaiGiuong)
        //                    {
        //                        _context.LoaiPhongHoSo.Add(new LoaiPhongHoSo()
        //                        {
        //                            LoaiPhongId = d.LoaiPhong.Id,
        //                            HoSoId = request.Id,
        //                            LoaiGiuongId = g.Id,
        //                            DienTich = g.DienTich,
        //                            GiaPhong = g.GiaPhong,
        //                            SoPhong = g.SoPhong,
        //                            SoNguoiLon = g.SoNguoiLon,
        //                            SoTreEm = g.SoTreEm,
        //                            TenHienThi = g.TenHienThi
        //                        });
        //                    }
        //                }
        //            }
        //            if (request.DSMucDoTTNN != null && request.DSMucDoTTNN.Count() > 0)
        //            {
        //                foreach (var d in request.DSMucDoTTNN)
        //                {
        //                    _context.MucDoTTNNHoSo.Add(new MucDoTTNNHoSo()
        //                    {
        //                        HoSoId = request.Id,
        //                        DonViTinhId = d.DonViTinhId,
        //                        MucDoId = d.MucDoThongThao.Id,
        //                        SoLuong = d.SoLuong
        //                    });
        //                }
        //            }
        //            if (request.DSNgoaiNgu != null && request.DSNgoaiNgu.Count() > 0)
        //            {
        //                foreach (var d in request.DSNgoaiNgu)
        //                {
        //                    _context.NgoaiNguHoSo.Add(new NgoaiNguHoSo()
        //                    {
        //                        HoSoId = request.Id,
        //                        DonViTinhId = d.DonViTinhId,
        //                        NgoaiNguId = d.NgoaiNgu.Id,
        //                        SoLuong = d.SoLuong
        //                    });
        //                }
        //            }
        //            if (request.DSTienNghi != null && request.DSTienNghi.Count() > 0)
        //            {
        //                foreach (var d in request.DSTienNghi)
        //                {
        //                    _context.TienNghiHoSo.Add(new TienNghiHoSo()
        //                    {
        //                        HoSoId = request.Id,
        //                        TienNghiId = d.TienNghi.Id,
        //                        SoLuong = d.SoLuong,
        //                        IsPhuPhi = d.IsPhuPhi,
        //                        IsSuDung = d.IsSuDung
        //                    });
        //                }
        //            }
        //            if (request.DSTrinhDo != null && request.DSTrinhDo.Count() > 0)
        //            {
        //                foreach (var d in request.DSTrinhDo)
        //                {
        //                    _context.TrinhDoHoSo.Add(new TrinhDoHoSo()
        //                    {
        //                        HoSoId = request.Id,
        //                        TrinhDoId = d.TrinhDo.Id,
        //                        SoLuong = d.SoLuong,
        //                        DonViTinhId = d.DonViTinhId
        //                    });
        //                }
        //            }
        //            if (request.DSThucDon != null && request.DSThucDon.Count() > 0)
        //            {
        //                foreach (var d in request.DSThucDon)
        //                {
        //                    _context.ThucDonHoSo.Add(new ThucDonHoSo()
        //                    {
        //                        DonGia = d.DonGia,
        //                        HosoId = request.Id,
        //                        MoTa = d.MoTa,
        //                        TenThucDon = d.TenThucDon
        //                    });
        //                }
        //            }
        //            if (request.DSVeDichVu != null && request.DSVeDichVu.Count() > 0)
        //            {
        //                foreach (var d in request.DSVeDichVu)
        //                {
        //                    _context.VeDichVuHoSo.Add(new VeDichVuHoSo()
        //                    {
        //                        GiaVe = d.GiaVe,
        //                        HosoId = request.Id,
        //                        MoTa = d.MoTa,
        //                        TenVe = d.TenVe
        //                    });
        //                }
        //            }
        //            if (request.DSNhaHang != null && request.DSNhaHang.Count() > 0)
        //            {
        //                foreach (var d in request.DSNhaHang)
        //                {
        //                    _context.QuyMoNhaHangLuuTru.Add(new QuyMoNhaHangLuuTru()
        //                    {
        //                        DienTich = d.DienTich,
        //                        TenGoi = d.TenGoi,
        //                        HoSoId = request.Id,
        //                        SoGhe = d.SoGhe
        //                    });
        //                }
        //            }
        //            if (request.DSVanBan != null && request.DSVanBan.Count() > 0)
        //            {
        //                foreach (var d in request.DSVanBan)
        //                {
        //                    _context.HoSoVanBan.Add(new HoSoVanBan()
        //                    {
        //                        NoiCap = d.NoiCap,
        //                        TenGoi = d.TenGoi,
        //                        HosoId = request.Id,
        //                        NgayCap = DateTime.Now,
        //                        NgayHetHan = DateTime.Now,
        //                        MaSo = "",
        //                        FilePath = d.FilePath,
        //                        FileName = d.FileName,
        //                        GiayPhepId = d.GiayPhepId,
        //                        IsStatus = d.IsStatus,
        //                        Loai = LoaiFile.hosodulich.ToString()
        //                    });
        //                }
        //            }

        //            result = await _context.SaveChangesAsync();
        //            if (result > 0)
        //            {
        //                return new ApiSuccessResult<DuLieuDuLichModel>(request, "Thêm mới thành công!");
        //            }
        //            else
        //            {
        //                return new ApiErrorResult<DuLieuDuLichModel>("Thêm lỗi!");
        //            }
        //        }
        //        else
        //        {
        //            return new ApiErrorResult<DuLieuDuLichModel>("Thêm lỗi!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiErrorResult<DuLieuDuLichModel>(ex.Message);
        //    }
        //}

        public async Task<ApiResult<int>> Delete(int id)
        {
            try
            {
                var coSoLuuTru = await _context.HoSo.Where(x => x.Id == id).ToListAsync();
                if (coSoLuuTru == null || coSoLuuTru.Count() <= 0)
                {
                    return new ApiErrorResult<int>($"Không tìm thấy dữ liệu tương ứng với id = {id}!");
                }

                var obj = coSoLuuTru.FirstOrDefault();

                obj.IsDelete = true;

                _context.HoSo.Update(obj);

                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<int>(id, "Xóa hồ sơ " + obj.Ten + " thành công!");
                }
                return new ApiErrorResult<int>("Xóa lỗi!");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<int>(ex.Message);
            }
        }
        public async Task<ApiResult<bool>> Delete_File_Vanban(int id)
        {
            try
            {

                var obj = await _context.HoSoVanBan.FindAsync(id);

                obj.FileName = "";
                obj.FilePath = "";

                _context.HoSoVanBan.Update(obj);

                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<bool>(true, "Xóa file văn bản hồ sơ thành công!");
                }
                return new ApiErrorResult<bool>("Xóa file văn bản hồ sơ lỗi!");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }

        public async Task<ApiResult<int>> DeleteNhaHangLuuTru(int id)
        {
            try
            {
                var obj = _context.QuyMoNhaHangLuuTru.Find(id);

                _context.QuyMoNhaHangLuuTru.Remove(obj);

                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<int>(id, "Xóa thành công!");
                }
                return new ApiErrorResult<int>("Xóa lỗi!");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<int>(ex.Message);
            }
        }

        public async Task<List<DuLieuDuLichModel>> GetAll(int linhvucId)
        {
            var query = from m in _context.HoSo
                        where m.IsDelete == false && (linhvucId == 0 || m.LinhVucKinhDoanhId == linhvucId)
                        select new DuLieuDuLichModel
                        {
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            Ten = m.Ten,
                            ChucVuNguoiDaiDien = m.ChucVuNguoiDaiDien,
                            CNVSMoiTruong = m.CNVSMoiTruong,
                            DienTichMatBang = m.DienTichMatBang,
                            DienTichMatBangXayDung = m.DienTichMatBangXayDung,
                            DienTichXayDung = m.DienTichXayDung,
                            DoTuoiTBNam = m.DoTuoiTBNam,
                            DoTuoiTBNu = m.DoTuoiTBNu,
                            DuongPho = m.DuongPho,
                            Email = m.Email,
                            Fax = m.Fax,
                            GhiChu = m.GhiChu,
                            GioiTinhNguoiDaiDien = m.GioiTinhNguoiDaiDien,
                            HangSao = m.HangSao,
                            HoTenNguoiDaiDien = m.HoTenNguoiDaiDien,
                            KhamSucKhoeDinhKy = m.KhamSucKhoeDinhKy,
                            LinhVucKinhDoanhId = m.LinhVucKinhDoanhId,
                            LoaiHinhId = m.LoaiHinhId,
                            NgayQuyetDinh = m.NgayQuyetDinh,
                            PhongChayNo = m.PhongChayNo,
                            PhuongXaId = m.PhuongXaId,
                            QuanHuyenId = m.QuanHuyenId,
                            SoDienThoai = m.SoDienThoai,
                            SoDienThoaiNguoiDaiDien = m.SoDienThoaiNguoiDaiDien,
                            SoGiayPhep = m.SoGiayPhep,
                            SoLanChuyenChu = m.SoLanChuyenChu,
                            SoLuongLaoDong = m.SoLuongLaoDong,
                            SoNha = m.SoNha,
                            SoQuyetDinh = m.SoQuyetDinh,
                            SoTang = m.SoTang,
                            NhaCungCapId = m.NhaCungCapId,
                            //TenCongTy = m.TenCongTy,
                            ThoiDiemBatDauKinhDoanh = m.ThoiDiemBatDauKinhDoanh,
                            TinhThanhId = m.TinhThanhId,
                            TongVonDauTuBanDau = m.TongVonDauTuBanDau,
                            TongVonDauTuBoSung = m.TongVonDauTuBoSung,
                            TrangPhucRieng = m.TrangPhucRieng,
                            Website = m.Website,
                            Id = m.Id,
                            // LoaiHinh = m.LoaiHinh != null ? new LoaiHinhModel() { TenLoai = m.LoaiHinh.TenLoai, Id = m.LoaiHinh.Id } : null,
                            IsDatChuan = m.IsDatChuan,
                            NgayCVDatChuan = m.NgayCVDatChuan,
                            SoCVDatChuan = m.SoCVDatChuan,
                            GioiThieu = m.GioiThieu
                        };
            return await query.ToListAsync();
        }
        public async Task<List<DuLieuDuLichModel>> GetAllCSLT(string key = "")
        {
            var query = from m in _context.HoSo
                        where m.IsDelete == false && (!String.IsNullOrEmpty(key) ? m.Ten.Contains(key) : 1 == 1)
                        select new DuLieuDuLichModel
                        {
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            Ten = m.Ten,
                            ChucVuNguoiDaiDien = m.ChucVuNguoiDaiDien,
                            CNVSMoiTruong = m.CNVSMoiTruong,
                            DienTichMatBang = m.DienTichMatBang,
                            DienTichMatBangXayDung = m.DienTichMatBangXayDung,
                            DienTichXayDung = m.DienTichXayDung,
                            DoTuoiTBNam = m.DoTuoiTBNam,
                            DoTuoiTBNu = m.DoTuoiTBNu,
                            DuongPho = m.DuongPho,
                            Email = m.Email,
                            Fax = m.Fax,
                            GhiChu = m.GhiChu,
                            GioiTinhNguoiDaiDien = m.GioiTinhNguoiDaiDien,
                            HangSao = m.HangSao,
                            HoTenNguoiDaiDien = m.HoTenNguoiDaiDien,
                            KhamSucKhoeDinhKy = m.KhamSucKhoeDinhKy,
                            LinhVucKinhDoanhId = m.LinhVucKinhDoanhId,
                            LoaiHinhId = m.LoaiHinhId,
                            NgayQuyetDinh = m.NgayQuyetDinh,
                            PhongChayNo = m.PhongChayNo,
                            PhuongXaId = m.PhuongXaId,
                            QuanHuyenId = m.QuanHuyenId,
                            SoDienThoai = m.SoDienThoai,
                            SoDienThoaiNguoiDaiDien = m.SoDienThoaiNguoiDaiDien,
                            SoGiayPhep = m.SoGiayPhep,
                            SoLanChuyenChu = m.SoLanChuyenChu,
                            SoLuongLaoDong = m.SoLuongLaoDong,
                            SoNha = m.SoNha,
                            SoQuyetDinh = m.SoQuyetDinh,
                            SoTang = m.SoTang,
                            NhaCungCapId = m.NhaCungCapId,
                            //TenCongTy = m.TenCongTy,
                            ThoiDiemBatDauKinhDoanh = m.ThoiDiemBatDauKinhDoanh,
                            TinhThanhId = m.TinhThanhId,
                            TongVonDauTuBanDau = m.TongVonDauTuBanDau,
                            TongVonDauTuBoSung = m.TongVonDauTuBoSung,
                            TrangPhucRieng = m.TrangPhucRieng,
                            Website = m.Website,
                            Id = m.Id,
                            // LoaiHinh = m.LoaiHinh != null ? new LoaiHinhModel() { TenLoai = m.LoaiHinh.TenLoai, Id = m.LoaiHinh.Id } : null,
                            IsDatChuan = m.IsDatChuan,
                            NgayCVDatChuan = m.NgayCVDatChuan,
                            SoCVDatChuan = m.SoCVDatChuan,
                            GioiThieu = m.GioiThieu
                        };
            return await query.ToListAsync();
        }

        public List<DanhMucMoblieVm> GetCategory()
        {
            var list = Enum.GetValues(typeof(CategoryMobile)).Cast<CategoryMobile>()
               .Select(x => new DanhMucMoblieVm
               {
                   Name = StringEnum.GetStringValue(x),
                   Id = Convert.ToInt32(x),
                   UrlImage = x == CategoryMobile.ThongTinCanBiet ? "/imgs/thong-tin-du-lich.jpg"
                            : x == CategoryMobile.LeHoi ? "/imgs/le-hoi-su-kien.jpg"
                            : x == CategoryMobile.CoSoLuuTru ? "/imgs/khach-san.jpg"
                            : x == CategoryMobile.NhaHang ? "/imgs/am-thuc-hue.jpg"
                            : x == CategoryMobile.DiemDuLich ? "/imgs/diem_tham_quan.jpg"
                            // : x == CategoryMobile.MuaSam ? "/imgs/mua-sam.jpg"
                            : x == CategoryMobile.Tour ? "/imgs/tour-du-lich.jpg"
                            : x == CategoryMobile.VanChuyen ? "/imgs/van-chuyen.jpg" : "",

                   Icon = x == CategoryMobile.ThongTinCanBiet ? "0xe7d9"
                            : x == CategoryMobile.LeHoi ? "0xe630"
                            : x == CategoryMobile.CoSoLuuTru ? "0xf25b"
                            : x == CategoryMobile.NhaHang ? "0xe28b"
                            : x == CategoryMobile.DiemDuLich ? "0xe2b8"
                            // : x == CategoryMobile.MuaSam ? "0xf25b"
                            : x == CategoryMobile.Tour ? "0xf25b" : "0xf25b"
               }).ToList();
            return list;
        }

        public async Task<DuLieuDuLichModel> GetById(int id)
        {
            try
            {

                var query = from m in _context.HoSo
                                //join xa in _context.DiaPhuong on m.PhuongXaId equals xa.Id into dp
                                //from xa in dp.DefaultIfEmpty()
                                //join huyen in _context.DiaPhuong on m.QuanHuyenId equals huyen.Id into dph
                                //from huyen in dph.DefaultIfEmpty()

                                //join loaihinh in _context.LoaiHinh on m.LoaiHinhId equals loaihinh.Id into lh
                                //from loaihinh in lh.DefaultIfEmpty()

                                //join loainhahang in _context.DichVu on m.LoaiHinhId equals loainhahang.Id into lnh
                                //from loainhahang in lnh.DefaultIfEmpty()

                                //join loaimuasam in _context.LoaiDichVu on m.LoaiHinhId equals loaimuasam.Id into lms
                                //from loaimuasam in lms.DefaultIfEmpty()

                                //join loaidiemdulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.DiemDuLich) on m.LoaiHinhId equals loaidiemdulich.Id into lddl
                                //from loaidiemdulich in lddl.DefaultIfEmpty()

                                //join loaikhudulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuDuLich) on m.LoaiHinhId equals loaikhudulich.Id into lkhudl
                                //from loaikhudulich in lkhudl.DefaultIfEmpty()

                                //join loaikhuvuichoi in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuVuiChoi) on m.LoaiHinhId equals loaikhuvuichoi.Id into lkhuvc
                                //from loaikhuvuichoi in lkhuvc.DefaultIfEmpty()

                                //join loaithethao in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.TheThao) on m.LoaiHinhId equals loaithethao.Id into ltt
                                //from loaithethao in ltt.DefaultIfEmpty()

                                //join loaicssk in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.CSSK) on m.LoaiHinhId equals loaicssk.Id into lcssk
                                //from loaicssk in lcssk.DefaultIfEmpty()

                                //join loailuhanh in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.LuHanh) on m.LoaiHinhId equals loailuhanh.Id into lluhanh
                                //from loailuhanh in lluhanh.DefaultIfEmpty()

                                //join nhacungcap in _context.NhaCungCap on m.NhaCungCapId equals nhacungcap.Id into ncc
                                //from nhacungcap in ncc.DefaultIfEmpty()

                            where m.IsDelete == false && m.Id == id
                            select new { m };

                var data = await query.Select(x => new DuLieuDuLichModel()
                {
                    Id = x.m.Id,
                    IsDelete = x.m.IsDelete,
                    IsStatus = x.m.IsStatus,
                    Ten = x.m.Ten,
                    ChucVuNguoiDaiDien = x.m.ChucVuNguoiDaiDien,
                    CNVSMoiTruong = x.m.CNVSMoiTruong,
                    DienTichMatBang = x.m.DienTichMatBang,
                    DienTichMatBangXayDung = x.m.DienTichMatBangXayDung,
                    DienTichXayDung = x.m.DienTichXayDung,
                    DoTuoiTBNam = x.m.DoTuoiTBNam,
                    DoTuoiTBNu = x.m.DoTuoiTBNu,
                    DuongPho = x.m.DuongPho,
                    Email = x.m.Email,
                    Fax = x.m.Fax,
                    GhiChu = x.m.GhiChu,
                    GioiTinhNguoiDaiDien = x.m.GioiTinhNguoiDaiDien,
                    HangSao = x.m.HangSao,
                    CSLTId = x.m.CSLTId,
                    DonViCapPhep = x.m.DonViCapPhep,
                    IsDatChuan = x.m.IsDatChuan,
                    IsNhaHangTrongCSLT = x.m.IsNhaHangTrongCSLT,
                    MaSoCapPhep = x.m.MaSoCapPhep,
                    NgayCapPhep = x.m.NgayCapPhep,
                    NgayCVDatChuan = x.m.NgayCVDatChuan,
                    NgayHetHan = x.m.NgayHetHan,
                    SoCVDatChuan = x.m.SoCVDatChuan,
                    TenVietTat = x.m.TenVietTat,
                    ViTriTrenBanDo = x.m.ViTriTrenBanDo,
                    HoTenNguoiDaiDien = x.m.HoTenNguoiDaiDien,
                    KhamSucKhoeDinhKy = x.m.KhamSucKhoeDinhKy,
                    LinhVucKinhDoanhId = x.m.LinhVucKinhDoanhId,
                    LoaiHinhId = x.m.LoaiHinhId,
                    NgayQuyetDinh = x.m.NgayQuyetDinh,
                    PhongChayNo = x.m.PhongChayNo,
                    PhuongXaId = x.m.PhuongXaId,
                    //PhuongXa = x.xa.TenDiaPhuong,
                    QuanHuyenId = x.m.QuanHuyenId,
                    //QuanHuyen = x.huyen.TenDiaPhuong,
                    TinhThanh = "Thừa Thiên Huế",

                    SoDienThoai = x.m.SoDienThoai,
                    SoDienThoaiNguoiDaiDien = x.m.SoDienThoaiNguoiDaiDien,
                    SoGiayPhep = x.m.SoGiayPhep,
                    SoLanChuyenChu = x.m.SoLanChuyenChu,
                    SoLuongLaoDong = x.m.SoLuongLaoDong,
                    SoNha = x.m.SoNha,
                    SoQuyetDinh = x.m.SoQuyetDinh,
                    SoTang = x.m.SoTang,
                    //NhaCungCap = new NhaCungCapVm() { Ten = x.nhacungcap.Ten },
                    //TenCongTy = x.m.TenCongTy,
                    NhaCungCapId = x.m.NhaCungCapId,
                    ThoiDiemBatDauKinhDoanh = x.m.ThoiDiemBatDauKinhDoanh,
                    TinhThanhId = x.m.TinhThanhId,
                    TongVonDauTuBanDau = x.m.TongVonDauTuBanDau,
                    TongVonDauTuBoSung = x.m.TongVonDauTuBoSung,
                    TongSoPhong = x.m.TongSoPhong,
                    TongSoGiuong = x.m.TongSoGiuong,
                    TrangPhucRieng = x.m.TrangPhucRieng,
                    Website = x.m.Website,
                    GioDongCua = x.m.GioDongCua,
                    GioMoCua = x.m.GioMoCua,
                    //LoaiHinh = x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CoSoLuuTru ? new LoaiHinhModel() { Id = x.loaihinh.Id, TenLoai = x.loaihinh.TenLoai }
                    //    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.NhaHang ? new LoaiHinhModel() { Id = x.loainhahang.Id, TenLoai = x.loainhahang.TenDichVu }
                    //    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiemDuLich ? new LoaiHinhModel() { Id = x.loaidiemdulich.Id, TenLoai = x.loaidiemdulich.Ten }
                    //    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuDuLich ? new LoaiHinhModel() { Id = x.loaikhudulich.Id, TenLoai = x.loaikhudulich.Ten }
                    //    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuVuiChoi ? new LoaiHinhModel() { Id = x.loaikhuvuichoi.Id, TenLoai = x.loaikhuvuichoi.Ten }
                    //    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.TheThao ? new LoaiHinhModel() { Id = x.loaithethao.Id, TenLoai = x.loaithethao.Ten }
                    //    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CSSK ? new LoaiHinhModel() { Id = x.loaicssk.Id, TenLoai = x.loaicssk.Ten }
                    //    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.LuHanh ? new LoaiHinhModel() { Id = x.loailuhanh.Id, TenLoai = x.loailuhanh.Ten }
                    //    : new LoaiHinhModel() { Id = x.loaimuasam.Id, TenLoai = x.loaimuasam.TenLoai },
                    SoLDGianTiep = x.m.SoLDGianTiep,
                    SoLDNamNgoaiNuoc = x.m.SoLDNamNgoaiNuoc,
                    SoLDNamTrongNuoc = x.m.SoLDNamTrongNuoc,
                    SoLDNuNgoaiNuoc = x.m.SoLDNuNgoaiNuoc,
                    SoLDNuTrongNuoc = x.m.SoLDNuTrongNuoc,
                    SoLDThoiVu = x.m.SoLDThoiVu,
                    SoLDThuongXuyen = x.m.SoLDThuongXuyen,
                    SoLDTrucTiep = x.m.SoLDTrucTiep,
                    GioiThieu = x.m.GioiThieu,
                    //Images = _fileUploadService.GetImageByHoSoId(x.m.Id, LoaiFile.hosodulich.ToString()).Result,

                    GiaThamKhao = x.m.GiaThamKhao != null ? Functions.ConvertDecimalVND(Convert.ToDecimal(x.m.GiaThamKhao)) : "0",
                    //HueCIT
                    ToaDoX = x.m.ToaDoX,
                    ToaDoY = x.m.ToaDoY,
                    NgonNguId = x.m.NgonNguId
                }).FirstOrDefaultAsync();

                var item = data;

                item.LoaiHinh = item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CoSoLuuTru ? await _context.LoaiHinh.Where(x => x.Id == item.LoaiHinhId).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.TenLoai }).FirstOrDefaultAsync()
                    : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.NhaHang ? await _context.DichVu.Where(x => x.Id == item.LoaiHinhId).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.TenDichVu }).FirstOrDefaultAsync()
                    : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiemDuLich ? await _context.DanhMuc.Where(x => x.Id == item.LoaiHinhId && x.LoaiId == (int)LinhVucKinhDoanh.DiemDuLich).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.Ten }).FirstOrDefaultAsync()
                    : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuDuLich ? await _context.DanhMuc.Where(x => x.Id == item.LoaiHinhId && x.LoaiId == (int)LinhVucKinhDoanh.KhuDuLich).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.Ten }).FirstOrDefaultAsync()
                    : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuVuiChoi ? await _context.DanhMuc.Where(x => x.Id == item.LoaiHinhId && x.LoaiId == (int)LinhVucKinhDoanh.KhuVuiChoi).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.Ten }).FirstOrDefaultAsync()
                    : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.TheThao ? await _context.DanhMuc.Where(x => x.Id == item.LoaiHinhId && x.LoaiId == (int)LinhVucKinhDoanh.TheThao).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.Ten }).FirstOrDefaultAsync()
                    : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CSSK ? await _context.DanhMuc.Where(x => x.Id == item.LoaiHinhId && x.LoaiId == (int)LinhVucKinhDoanh.CSSK).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.Ten }).FirstOrDefaultAsync()
                    : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.LuHanh ? await _context.DanhMuc.Where(x => x.Id == item.LoaiHinhId && x.LoaiId == (int)LinhVucKinhDoanh.LuHanh).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.Ten }).FirstOrDefaultAsync()
                    : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.VanChuyen ? await _context.DanhMuc.Where(x => x.Id == item.LoaiHinhId && x.LoaiId == (int)LinhVucKinhDoanh.VanChuyen).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.Ten }).FirstOrDefaultAsync()
                    : await _context.LoaiDichVu.Where(x => x.Id == item.LoaiHinhId).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.TenLoai }).FirstOrDefaultAsync();
                item.Images = await _fileUploadService.GetImageByHoSoId(item.Id, LoaiFile.hosodulich.ToString());
                item.NhaCungCap = await _context.NhaCungCap.Where(x => x.Id == item.NhaCungCapId).Select(x => new NhaCungCapVm() { Ten = x.Ten }).FirstOrDefaultAsync();
                item.DocumentFiles = await _fileUploadService.GetFileByHoSoId(item.Id, LoaiFile.hosodulich.ToString());
                item.DSBoPhan = await _boPhanService.GetAllByHoSo(item.Id);
                item.DSVeDichVu = await _dichVuService.GetAllByHoSo(item.Id);
                item.DSLoaiPhong = await _loaiPhongService.GetAllByHoSo(item.Id);
                item.DSMucDoTTNN = await _mucDoThongThaoNgoaiNguService.GetAllByHoSo(item.Id);
                item.DSNgoaiNgu = await _ngoaiNguService.GetAllByHoSo(item.Id);
                item.DSThucDon = await _thucDonService.GetAllByHoSo(item.Id);
                item.DSTienNghi = await _tienNghiService.GetAllByHoSo(item.Id);
                item.DSTrinhDo = await _trinhDoService.GetAllByHoSo(item.Id);
                item.DSDanhGia = await _danhGiaService.GetAll(item.Id, TechLife.Common.Enums.LoaiBinhLuan.hosodulich.ToString());
                item.Tours = await _tourService.GetAll(item.Id);
                item.DSNhaHang = await _context.QuyMoNhaHangLuuTru.OrderBy(x => x.Id).Where(v => v.HoSoId == item.Id).Select(v => new QuyMoNhaHangVm()
                {
                    DienTich = v.DienTich,
                    HoSoId = v.HoSoId,
                    Id = v.Id,
                    SoGhe = v.SoGhe,
                    TenGoi = v.TenGoi
                }).ToListAsync();
                item.DSVanBan = await _context.HoSoVanBan.Where(v => v.HosoId == item.Id && v.Loai == LoaiFile.hosodulich.ToString()).Select(x => new HoSoVanBanVm()
                {
                    FileName = x.FileName,
                    FilePath = x.FilePath,
                    Id = x.Id,
                    MaSo = x.MaSo,
                    NgayCap = x.NgayCap,
                    NgayHetHan = x.NgayHetHan,
                    NoiCap = x.NoiCap,
                    TenGoi = x.TenGoi,
                    GiayPhepId = x.GiayPhepId,
                    IsStatus = x.IsStatus
                }).ToListAsync();
                item.DSLoaiPhongGiuong = await _context.LoaiGiuongPhong.OrderBy(x => x.Id).Where(v => v.LuuTruId == item.Id).Select(v => new TechLife.Model.LoaiPhong.LoaiPhongGiuong()
                {
                    Id = v.Id,
                    GiaGiuong = Functions.ConvertDecimalVND(v.GiaGiuongPhu),
                    GiaPhong = Functions.ConvertDecimalVND(v.GiaPhong),
                    TenGoi = v.Ten,
                    SoGiuong = v.SoGiuong,
                    IsDelete = v.IsDelete

                }).ToListAsync();
                item.Amenities = await _context.Amenities.Where(v => v.CompanyId == item.Id).Select(v => new Model.DuLieuDuLich.AmenityVm()
                {
                    Id = v.AmenityId,
                    Name = _context.TienNghi.Where(x => x.Id == v.AmenityId).Select(v => v.Ten).FirstOrDefault()

                }).ToListAsync();

                return item;
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<PagedResult<DuLieuDuLichModel>> GetPaging(string langId, int linhvucId, HoSoFromRequets request)
        {
            var query = from m in _context.HoSo
                            //join xa in _context.DiaPhuong on m.PhuongXaId equals xa.Id into dp
                            //from xa in dp.DefaultIfEmpty()
                            //join huyen in _context.DiaPhuong on m.QuanHuyenId equals huyen.Id into dph
                            //from huyen in dph.DefaultIfEmpty()

                            //join loaihinh in _context.LoaiHinh on m.LoaiHinhId equals loaihinh.Id into lh
                            //from loaihinh in lh.DefaultIfEmpty()

                            //join loainhahang in _context.DichVu on m.LoaiHinhId equals loainhahang.Id into lnh
                            //from loainhahang in lnh.DefaultIfEmpty()

                            //join loaimuasam in _context.LoaiDichVu on m.LoaiHinhId equals loaimuasam.Id into lms
                            //from loaimuasam in lms.DefaultIfEmpty()

                            //join loaidiemdulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.DiemDuLich) on m.LoaiHinhId equals loaidiemdulich.Id into lddl
                            //from loaidiemdulich in lddl.DefaultIfEmpty()

                            //join loaikhudulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuDuLich) on m.LoaiHinhId equals loaikhudulich.Id into lkhudl
                            //from loaikhudulich in lkhudl.DefaultIfEmpty()

                            //join loaikhuvuichoi in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuVuiChoi) on m.LoaiHinhId equals loaikhuvuichoi.Id into lkhuvc
                            //from loaikhuvuichoi in lkhuvc.DefaultIfEmpty()

                            //join loaithethao in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.TheThao) on m.LoaiHinhId equals loaithethao.Id into ltt
                            //from loaithethao in ltt.DefaultIfEmpty()

                            //join loaicssk in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.CSSK) on m.LoaiHinhId equals loaicssk.Id into lcssk
                            //from loaicssk in lcssk.DefaultIfEmpty()

                            //join loailuhanh in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.LuHanh) on m.LoaiHinhId equals loailuhanh.Id into lluhanh
                            //from loailuhanh in lluhanh.DefaultIfEmpty()

                            //join vanchuyen in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.VanChuyen) on m.LoaiHinhId equals vanchuyen.Id into vc
                            //from vanchuyen in vc.DefaultIfEmpty()

                            //join nhacungcap in _context.NhaCungCap on m.NhaCungCapId equals nhacungcap.Id into ncc
                            //from nhacungcap in ncc.DefaultIfEmpty()

                        orderby m.Ten
                        where m.IsDelete == false && m.NgonNguId == langId
                        && (linhvucId == 0 || m.LinhVucKinhDoanhId == linhvucId)
                        && (request.hangsao == -1 || m.HangSao == request.hangsao)
                        && (request.huyen == -1 || m.QuanHuyenId == request.huyen)
                        && (request.loaihinh == -1 || m.LoaiHinhId == request.loaihinh)
                        && (request.namecslt == -1 || m.Id == request.namecslt)
                        && (request.nameddl == -1 || m.Id == request.nameddl)
                        && (request.nameluhanh == -1 || m.Id == request.nameluhanh)
                        && (request.namenhahang == -1 || m.Id == request.namenhahang)
                        && (request.namecsms == -1 || m.Id == request.namecsms)
                        && (request.nguon == -1 ? true : request.nguon == 0 ? m.NguonDongBo == null : m.NguonDongBo == request.nguon)
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.Ten.Contains(request.Keyword));

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new DuLieuDuLichModel()
                {
                    Id = x.m.Id,
                    IsDelete = x.m.IsDelete,
                    IsStatus = x.m.IsStatus,
                    Ten = x.m.Ten,
                    ChucVuNguoiDaiDien = x.m.ChucVuNguoiDaiDien,
                    CNVSMoiTruong = x.m.CNVSMoiTruong,
                    DienTichMatBang = x.m.DienTichMatBang,
                    DienTichMatBangXayDung = x.m.DienTichMatBangXayDung,
                    DienTichXayDung = x.m.DienTichXayDung,
                    DoTuoiTBNam = x.m.DoTuoiTBNam,
                    DoTuoiTBNu = x.m.DoTuoiTBNu,
                    DuongPho = x.m.DuongPho,
                    Email = x.m.Email,
                    Fax = x.m.Fax,
                    GhiChu = x.m.GhiChu,
                    GioiTinhNguoiDaiDien = x.m.GioiTinhNguoiDaiDien,
                    HangSao = x.m.HangSao,
                    HoTenNguoiDaiDien = x.m.HoTenNguoiDaiDien,
                    KhamSucKhoeDinhKy = x.m.KhamSucKhoeDinhKy,
                    LinhVucKinhDoanhId = x.m.LinhVucKinhDoanhId,
                    LoaiHinhId = x.m.LoaiHinhId,
                    NgayQuyetDinh = x.m.NgayQuyetDinh,
                    PhongChayNo = x.m.PhongChayNo,
                    PhuongXaId = x.m.PhuongXaId,
                    //PhuongXa = x.xa.TenDiaPhuong,
                    QuanHuyenId = x.m.QuanHuyenId,
                    //QuanHuyen = x.huyen.TenDiaPhuong,
                    TinhThanh = "Thừa Thiên Huế",
                    SoDienThoai = x.m.SoDienThoai,
                    SoDienThoaiNguoiDaiDien = x.m.SoDienThoaiNguoiDaiDien,
                    SoGiayPhep = x.m.SoGiayPhep,
                    SoLanChuyenChu = x.m.SoLanChuyenChu,
                    SoLuongLaoDong = x.m.SoLuongLaoDong,
                    SoNha = x.m.SoNha,
                    SoQuyetDinh = x.m.SoQuyetDinh,
                    SoTang = x.m.SoTang,
                    //NhaCungCap = new NhaCungCapVm() { Ten = x.nhacungcap.Ten },
                    //TenCongTy = x.m.TenCongTy,
                    NhaCungCapId = x.m.NhaCungCapId,
                    ThoiDiemBatDauKinhDoanh = x.m.ThoiDiemBatDauKinhDoanh,
                    TinhThanhId = x.m.TinhThanhId,
                    TongVonDauTuBanDau = x.m.TongVonDauTuBanDau,
                    TongVonDauTuBoSung = x.m.TongVonDauTuBoSung,
                    TrangPhucRieng = x.m.TrangPhucRieng,
                    Website = x.m.Website,
                    GioDongCua = x.m.GioDongCua,
                    GioMoCua = x.m.GioMoCua,
                    //LoaiHinh = x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CoSoLuuTru ? new LoaiHinhModel() { Id = x.loaihinh.Id, TenLoai = x.loaihinh.TenLoai }
                    //: x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.NhaHang ? new LoaiHinhModel() { Id = x.loainhahang.Id, TenLoai = x.loainhahang.TenDichVu }
                    //: x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiemDuLich ? new LoaiHinhModel() { Id = x.loaidiemdulich.Id, TenLoai = x.loaidiemdulich.Ten }
                    //: x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuDuLich ? new LoaiHinhModel() { Id = x.loaikhudulich.Id, TenLoai = x.loaikhudulich.Ten }
                    //: x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuVuiChoi ? new LoaiHinhModel() { Id = x.loaikhuvuichoi.Id, TenLoai = x.loaikhuvuichoi.Ten }
                    //: x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.TheThao ? new LoaiHinhModel() { Id = x.loaithethao.Id, TenLoai = x.loaithethao.Ten }
                    //: x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CSSK ? new LoaiHinhModel() { Id = x.loaicssk.Id, TenLoai = x.loaicssk.Ten }
                    //: x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.LuHanh ? new LoaiHinhModel() { Id = x.loailuhanh.Id, TenLoai = x.loailuhanh.Ten }
                    //: x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.VanChuyen ? new LoaiHinhModel() { Id = x.vanchuyen.Id, TenLoai = x.vanchuyen.Ten }
                    //: new LoaiHinhModel() { Id = x.loaimuasam.Id, TenLoai = x.loaimuasam.TenLoai },
                    SoLDGianTiep = x.m.SoLDGianTiep,
                    SoLDNamNgoaiNuoc = x.m.SoLDNamNgoaiNuoc,
                    SoLDNamTrongNuoc = x.m.SoLDNamTrongNuoc,
                    SoLDNuNgoaiNuoc = x.m.SoLDNuNgoaiNuoc,
                    SoLDNuTrongNuoc = x.m.SoLDNuTrongNuoc,
                    SoLDThoiVu = x.m.SoLDThoiVu,
                    SoLDThuongXuyen = x.m.SoLDThuongXuyen,
                    SoLDTrucTiep = x.m.SoLDTrucTiep,
                    GioiThieu = x.m.GioiThieu,
                    ToaDoX = x.m.ToaDoX,
                    ToaDoY = x.m.ToaDoY,
                    //Images = _fileUploadService.GetImageByHoSoId(x.m.Id, LoaiFile.hosodulich.ToString()).Result,
                }).ToListAsync();

            foreach (var item in data)
            {
                item.LoaiHinh = item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CoSoLuuTru ? await _context.LoaiHinh.Where(x => x.Id == item.LoaiHinhId).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.TenLoai }).FirstOrDefaultAsync()
                : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.NhaHang ? await _context.DichVu.Where(x => x.Id == item.LoaiHinhId).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.TenDichVu }).FirstOrDefaultAsync()
                : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiemDuLich ? await _context.DanhMuc.Where(x => x.Id == item.LoaiHinhId && x.LoaiId == (int)LinhVucKinhDoanh.DiemDuLich).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.Ten }).FirstOrDefaultAsync()
                : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuDuLich ? await _context.DanhMuc.Where(x => x.Id == item.LoaiHinhId && x.LoaiId == (int)LinhVucKinhDoanh.KhuDuLich).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.Ten }).FirstOrDefaultAsync()
                : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuVuiChoi ? await _context.DanhMuc.Where(x => x.Id == item.LoaiHinhId && x.LoaiId == (int)LinhVucKinhDoanh.KhuVuiChoi).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.Ten }).FirstOrDefaultAsync()
                : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.TheThao ? await _context.DanhMuc.Where(x => x.Id == item.LoaiHinhId && x.LoaiId == (int)LinhVucKinhDoanh.TheThao).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.Ten }).FirstOrDefaultAsync()
                : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CSSK ? await _context.DanhMuc.Where(x => x.Id == item.LoaiHinhId && x.LoaiId == (int)LinhVucKinhDoanh.CSSK).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.Ten }).FirstOrDefaultAsync()
                : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.LuHanh ? await _context.DanhMuc.Where(x => x.Id == item.LoaiHinhId && x.LoaiId == (int)LinhVucKinhDoanh.LuHanh).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.Ten }).FirstOrDefaultAsync()
                : item.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.VanChuyen ? await _context.DanhMuc.Where(x => x.Id == item.LoaiHinhId && x.LoaiId == (int)LinhVucKinhDoanh.VanChuyen).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.Ten }).FirstOrDefaultAsync()
                : await _context.LoaiDichVu.Where(x => x.Id == item.LoaiHinhId).Select(x => new LoaiHinhModel() { Id = x.Id, TenLoai = x.TenLoai }).FirstOrDefaultAsync();
                item.Images = await _fileUploadService.GetImageByHoSoId(item.Id, LoaiFile.hosodulich.ToString());
                item.NhaCungCap = await _context.NhaCungCap.Where(x => x.Id == item.NhaCungCapId).Select(x => new NhaCungCapVm() { Ten = x.Ten }).FirstOrDefaultAsync();
            }
            ;
            //4. Select and projection
            return new PagedResult<DuLieuDuLichModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<int>> Update(int id, DuLieuDuLichModel request)
        {
            try
            {
                request.GiaThamKhao = request.GiaThamKhao.Replace(",", "");

                var coSoLuuTru = await _context.HoSo.FindAsync(id);
                //if (coSoLuuTru == null || coSoLuuTru.Count() <= 0)
                //{
                //    return new ApiErrorResult<int>($"Không tìm thấy dữ liệu tương ứng với id = {id}!");
                //}

                var model = coSoLuuTru;
                model.Ten = request.Ten;
                model.ChucVuNguoiDaiDien = request.ChucVuNguoiDaiDien;
                model.CNVSMoiTruong = request.CNVSMoiTruong;
                model.DienTichMatBang = request.DienTichMatBang;
                model.DienTichMatBangXayDung = request.DienTichMatBangXayDung;
                model.DienTichXayDung = request.DienTichXayDung;
                model.DoTuoiTBNam = request.DoTuoiTBNam;
                model.DoTuoiTBNu = request.DoTuoiTBNu;
                model.DuongPho = request.DuongPho;
                model.Email = request.Email;
                model.Fax = request.Fax;
                model.GhiChu = request.GhiChu;
                model.GioiTinhNguoiDaiDien = request.GioiTinhNguoiDaiDien;
                model.HangSao = request.HangSao;
                model.HoTenNguoiDaiDien = request.HoTenNguoiDaiDien;
                model.KhamSucKhoeDinhKy = request.KhamSucKhoeDinhKy;
                model.LinhVucKinhDoanhId = request.LinhVucKinhDoanhId;
                model.LoaiHinhId = request.LoaiHinhId;
                model.NgayQuyetDinh = request.NgayQuyetDinh;
                model.NgayHetHan = request.NgayHetHan;
                model.PhongChayNo = request.PhongChayNo;
                model.PhuongXaId = request.PhuongXaId;
                model.QuanHuyenId = request.QuanHuyenId;
                model.SoDienThoai = request.SoDienThoai;
                model.SoDienThoaiNguoiDaiDien = request.SoDienThoaiNguoiDaiDien;
                model.SoGiayPhep = request.SoGiayPhep;
                model.SoLanChuyenChu = request.SoLanChuyenChu;
                model.SoLuongLaoDong = request.SoLuongLaoDong;
                model.SoNha = request.SoNha;
                model.SoQuyetDinh = request.SoQuyetDinh;
                model.SoTang = request.SoTang;
                model.NhaCungCapId = request.NhaCungCapId;
                //model.TenCongTy = request.TenCongTy;
                model.ThoiDiemBatDauKinhDoanh = request.ThoiDiemBatDauKinhDoanh;
                model.TinhThanhId = request.TinhThanhId;
                model.TongVonDauTuBanDau = request.TongVonDauTuBanDau;
                model.TongVonDauTuBoSung = request.TongVonDauTuBoSung;
                model.TrangPhucRieng = request.TrangPhucRieng;
                model.Website = request.Website;
                model.DonViCapPhep = request.DonViCapPhep;
                model.MaSoCapPhep = request.MaSoCapPhep;
                model.NgayCapPhep = request.NgayCapPhep;
                model.GioDongCua = request.GioDongCua;
                model.GioMoCua = request.GioMoCua;
                model.SoLDGianTiep = request.SoLDGianTiep;
                model.SoLDNamNgoaiNuoc = request.SoLDNamNgoaiNuoc;
                model.SoLDNamTrongNuoc = request.SoLDNamTrongNuoc;
                model.SoLDNuNgoaiNuoc = request.SoLDNuNgoaiNuoc;
                model.SoLDNuTrongNuoc = request.SoLDNuTrongNuoc;
                model.SoLDThoiVu = request.SoLDThoiVu;
                model.SoLDThuongXuyen = request.SoLDThuongXuyen;
                model.SoLDTrucTiep = request.SoLDTrucTiep;
                model.TenVietTat = request.TenVietTat;
                model.ViTriTrenBanDo = request.ViTriTrenBanDo;
                model.IsDatChuan = request.IsDatChuan;
                model.SoCVDatChuan = request.SoCVDatChuan;
                model.NgayCVDatChuan = request.NgayCVDatChuan;
                model.IsNhaHangTrongCSLT = request.IsNhaHangTrongCSLT;
                model.CSLTId = request.CSLTId;
                model.GioiThieu = request.GioiThieu;
                model.TongSoGiuong = request.TongSoGiuong;
                model.TongSoPhong = request.TongSoPhong;
                //HueCIT
                model.LoaiDiaDiemAnUong = request.LoaiDiaDiemAnUong;
                model.PhucVu = request.PhucVu;
                model.ToaDoX = request.ToaDoX;
                model.ToaDoY = request.ToaDoY;
                model.MaDoanhNghiep = request.MaDoanhNghiep;
                model.GiaThamKhao = decimal.TryParse(request.GiaThamKhao, out _) ? request.GiaThamKhao : "0";
                if (request.DSDichVu != null && request.DSDichVu.Count() > 0)
                {
                    var dichvu = _context.DichVuHoSo.Where(v => v.HoSoId == model.Id);
                    foreach (var d in dichvu)
                    {
                        _context.DichVuHoSo.Remove(d);
                    }

                    foreach (var d in request.DSDichVu)
                    {
                        _context.DichVuHoSo.Add(new DichVuHoSo()
                        {
                            DichVuId = d.DichVu.Id,
                            DonViTinhId = d.DonViTinhId,
                            HoSoId = request.Id,
                            QuyMo = d.QuyMo
                        });
                    }
                }
                if (request.DSBoPhan != null && request.DSBoPhan.Count() > 0)
                {
                    var bophan = _context.BoPhanHoSo.Where(v => v.HoSoId == model.Id);
                    foreach (var d in bophan)
                    {
                        _context.BoPhanHoSo.Remove(d);
                    }
                    foreach (var d in request.DSBoPhan)
                    {
                        _context.BoPhanHoSo.Add(new BoPhanHoSo()
                        {
                            BoPhanId = d.BoPhan.Id,
                            HoSoId = request.Id,
                            SoLuong = d.SoLuong,
                            DonViTinhId = d.DonViTinhId
                        });
                    }
                }
                if (request.DSLoaiPhong != null && request.DSLoaiPhong.Count() > 0)
                {
                    var loaiphong = _context.LoaiPhongHoSo.Where(v => v.HoSoId == model.Id);
                    foreach (var d in loaiphong)
                    {
                        _context.LoaiPhongHoSo.Remove(d);
                    }
                    foreach (var d in request.DSLoaiPhong)
                    {
                        foreach (var g in d.DSLoaiGiuong)
                        {
                            _context.LoaiPhongHoSo.Add(new LoaiPhongHoSo()
                            {
                                LoaiPhongId = d.LoaiPhong.Id,
                                HoSoId = request.Id,
                                LoaiGiuongId = g.Id,
                                DienTich = g.DienTich,
                                GiaPhong = g.GiaPhong,
                                SoPhong = g.SoPhong,
                                SoNguoiLon = g.SoNguoiLon,
                                SoTreEm = g.SoTreEm,
                                TenHienThi = g.TenHienThi
                            });
                        }
                    }
                }
                if (request.DSMucDoTTNN != null && request.DSMucDoTTNN.Count() > 0)
                {
                    var mucdo = _context.MucDoTTNNHoSo.Where(v => v.HoSoId == model.Id);
                    foreach (var d in mucdo)
                    {
                        _context.MucDoTTNNHoSo.Remove(d);
                    }

                    foreach (var d in request.DSMucDoTTNN)
                    {
                        _context.MucDoTTNNHoSo.Add(new MucDoTTNNHoSo()
                        {
                            HoSoId = request.Id,
                            DonViTinhId = d.DonViTinhId,
                            MucDoId = d.MucDoThongThao.Id,
                            SoLuong = d.SoLuong
                        });
                    }
                }
                if (request.DSNgoaiNgu != null && request.DSNgoaiNgu.Count() > 0)
                {
                    var ngoaingu = _context.NgoaiNguHoSo.Where(v => v.HoSoId == model.Id);
                    foreach (var d in ngoaingu)
                    {
                        _context.NgoaiNguHoSo.Remove(d);
                    }

                    foreach (var d in request.DSNgoaiNgu)
                    {
                        _context.NgoaiNguHoSo.Add(new NgoaiNguHoSo()
                        {
                            HoSoId = request.Id,
                            DonViTinhId = d.DonViTinhId,
                            NgoaiNguId = d.NgoaiNgu.Id,
                            SoLuong = d.SoLuong
                        });
                    }
                }
                if (request.DSTienNghi != null && request.DSTienNghi.Count() > 0)
                {
                    var tiennghi = _context.TienNghiHoSo.Where(v => v.HoSoId == model.Id);
                    foreach (var d in tiennghi)
                    {
                        _context.TienNghiHoSo.Remove(d);
                    }
                    foreach (var d in request.DSTienNghi)
                    {
                        _context.TienNghiHoSo.Add(new TienNghiHoSo()
                        {
                            HoSoId = request.Id,
                            TienNghiId = d.TienNghi.Id,
                            SoLuong = d.SoLuong,
                            IsPhuPhi = d.IsPhuPhi,
                            IsSuDung = d.IsSuDung
                        });
                    }
                }
                if (request.DSTrinhDo != null && request.DSTrinhDo.Count() > 0)
                {
                    var trinhdo = _context.TrinhDoHoSo.Where(v => v.HoSoId == model.Id);
                    foreach (var d in trinhdo)
                    {
                        _context.TrinhDoHoSo.Remove(d);
                    }
                    foreach (var d in request.DSTrinhDo)
                    {
                        _context.TrinhDoHoSo.Add(new TrinhDoHoSo()
                        {
                            HoSoId = request.Id,
                            TrinhDoId = d.TrinhDo.Id,
                            SoLuong = d.SoLuong,
                            DonViTinhId = d.DonViTinhId
                        });
                    }
                }
                if (request.DSThucDon != null && request.DSThucDon.Count() > 0)
                {
                    var trinhdo = _context.ThucDonHoSo.Where(v => v.HosoId == model.Id);
                    foreach (var d in trinhdo)
                    {
                        _context.ThucDonHoSo.Remove(d);
                    }
                    foreach (var d in request.DSThucDon)
                    {
                        _context.ThucDonHoSo.Add(new ThucDonHoSo()
                        {
                            DonGia = d.DonGia,
                            HosoId = model.Id,
                            MoTa = d.MoTa,
                            TenThucDon = d.TenThucDon
                        });
                    }
                }
                if (request.DSVeDichVu != null && request.DSVeDichVu.Count() > 0)
                {
                    var dichvu = _context.VeDichVuHoSo.Where(v => v.HosoId == model.Id);
                    foreach (var d in dichvu)
                    {
                        _context.VeDichVuHoSo.Remove(d);
                    }
                    foreach (var d in request.DSVeDichVu)
                    {
                        _context.VeDichVuHoSo.Add(new VeDichVuHoSo()
                        {
                            GiaVe = d.GiaVe,
                            HosoId = request.Id,
                            MoTa = d.MoTa,
                            TenVe = d.TenVe
                        });
                    }
                }
                if (request.DSNhaHang != null && request.DSNhaHang.Count() > 0)
                {
                    var dichvu = _context.QuyMoNhaHangLuuTru.Where(v => v.HoSoId == model.Id);
                    foreach (var d in dichvu)
                    {
                        _context.QuyMoNhaHangLuuTru.Remove(d);
                    }
                    foreach (var d in request.DSNhaHang.Where(v => !v.IsDelete && v.TenGoi != null))
                    {
                        _context.QuyMoNhaHangLuuTru.Add(new QuyMoNhaHangLuuTru()
                        {
                            DienTich = d.DienTich,
                            TenGoi = d.TenGoi,
                            HoSoId = request.Id,
                            SoGhe = d.SoGhe
                        });
                        await _context.SaveChangesAsync();
                    }
                }
                if (request.DSVanBan != null && request.DSVanBan.Count() > 0)
                {
                    var dichvu = _context.GiayPhep.Where(v => v.LinhVucId.Contains(model.LinhVucKinhDoanhId.ToString())).ToList();

                    foreach (var d in dichvu)
                    {
                        var lstVanBan = _context.HoSoVanBan.Where(v => v.HosoId == request.Id && v.GiayPhepId == d.Id && v.Loai == LoaiFile.hosodulich.ToString()).ToList();
                        if (lstVanBan != null && lstVanBan.Count > 0)
                        {
                            var obj = lstVanBan.FirstOrDefault();
                            var value = request.DSVanBan.Where(v => v.GiayPhepId == obj.GiayPhepId).ToList();
                            var objRequest = value.FirstOrDefault();
                            obj.FileName = objRequest.FileName;
                            obj.FilePath = objRequest.FilePath != null ? objRequest.FilePath : obj.FilePath;
                            obj.NoiCap = objRequest.NoiCap;
                            obj.TenGoi = objRequest.TenGoi;
                            obj.IsStatus = objRequest.IsStatus;
                            _context.HoSoVanBan.Update(obj);
                        }
                        else
                        {
                            var objRequest = request.DSVanBan.Single(v => v.GiayPhepId.Equals(d.Id));
                            var obj = new HoSoVanBan()
                            {
                                FileName = objRequest.FileName,
                                FilePath = objRequest.FilePath,
                                NoiCap = objRequest.NoiCap,
                                TenGoi = objRequest.TenGoi,
                                GiayPhepId = d.Id,
                                NgayCap = DateTime.Now,
                                NgayHetHan = DateTime.Now,
                                HosoId = request.Id,
                                IsDelete = false,
                                IsStatus = objRequest.IsStatus,
                                MaSo = "",
                                Loai = LoaiFile.hosodulich.ToString()
                            };
                            _context.HoSoVanBan.Add(obj);
                        }
                    }
                }
                var loaiGiuongPhong = _context.LoaiGiuongPhong.Where(x => x.LuuTruId == id);
                _context.LoaiGiuongPhong.RemoveRange(loaiGiuongPhong);
                if (request.DSLoaiPhongGiuong != null && request.DSLoaiPhongGiuong.Count() > 0)
                {
                    foreach (var d in request.DSLoaiPhongGiuong.Where(v => !v.IsDelete))
                    {
                        _context.LoaiGiuongPhong.Add(new LoaiGiuongPhong()
                        {
                            Ten = d.TenGoi,
                            LuuTruId = id,
                            GiaGiuongPhu = Convert.ToDecimal(d.GiaGiuong.Replace(",", "")),
                            GiaPhong = Convert.ToDecimal(d.GiaPhong.Replace(",", "")),
                            SoGiuong = d.SoGiuong,

                        });
                        await _context.SaveChangesAsync();
                    }
                }
                var amenities = _context.Amenities.Where(x => x.CompanyId == model.Id);
                _context.Amenities.RemoveRange(amenities);
                if (request.Amenities != null && request.Amenities.Where(x => x.IsSelect).Count() > 0)
                {
                    foreach (var item in request.Amenities.Where(x => x.IsSelect))
                    {
                        await _context.Amenities.AddAsync(new Amenities()
                        {
                            AmenityId = item.Id,
                            CompanyId = model.Id,
                        });
                        await _context.SaveChangesAsync();
                    }

                }
                _context.HoSo.Update(model);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<int>(id, "Cập nhật dữ liệu thành công!");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<int>("Sửa lỗi!");
            }
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
        //HueCIT
        private int CheckFileType(string name)
        {
            var FileExtension = name.Substring(name.LastIndexOf('.') + 1).ToLower();
            int ft = (int)FileType.Other;
            if (IMG_TYPE.Contains(FileExtension))
            {
                ft = (int)FileType.Img;
            }
            else if (MEDIA_TYPE.Contains(FileExtension))
            {
                ft = (int)FileType.Media;
            }
            else if (DOC_TYPE.Contains(FileExtension))
            {
                ft = (int)FileType.Doc;
            }

            return ft;
        }

        public async Task<ApiResult<bool>> UploadImage(int id, ImageUploadRequest request)
        {
            try
            {
                if (request.Images != null)
                {
                    foreach (var d in request.Images)
                    {


                        var image = new FileUpload()
                        {
                            FileName = d.FileName,
                            FileUrl = await this.SaveFile(d),
                            IsImage = true,
                            IsStatus = true,
                            Type = LoaiFile.hosodulich.ToString(),
                            Id = id,
                            NgayTao = DateTime.Now,
                            //HueCIT
                            FileType = CheckFileType(d.FileName)
                        };
                        _context.FileUploads.Add(image);
                    }
                }
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<bool>(true, "Upload hình ảnh thành công");
                }
                else
                {
                    return new ApiErrorResult<bool>("Upload hình ảnh lỗi");
                }
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }

        public async Task<ApiResult<bool>> UploadFile(int id, DocumentUploadRequest request)
        {
            try
            {
                if (request.DocumentFiles != null)
                {
                    foreach (var d in request.DocumentFiles)
                    {
                        var image = new FileUpload()
                        {
                            FileName = d.FileName,
                            FileUrl = await this.SaveFile(d),
                            IsImage = false,
                            IsStatus = true,
                            Type = LoaiFile.hosodulich.ToString(),
                            Id = id,
                            //HueCIT
                            FileType = CheckFileType(d.FileName)
                        };
                        _context.FileUploads.Add(image);
                    }
                }
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<bool>();
                }
                else
                {
                    return new ApiErrorResult<bool>("Upload file lỗi");
                }
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }

        public async Task<List<DuLieuDuLichTheoDiaBanReportVm>> DuLieuDuLichTheoDiaBan()
        {
            try
            {
                prams = new Dictionary<string, object>();

                var result = await base.GetAllByProc<DuLieuDuLichTheoDiaBanReportVm>("", prams);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ApiResult<bool>> UploadDoc(int id, HoSoVanBanCreateRequets requests)
        {
            var doc = new HoSoVanBan()
            {
                FileName = requests.File.FileName,
                FilePath = await this.SaveFile(requests.File),
                HosoId = id,
                MaSo = requests.MaSo,
                NgayCap = requests.NgayCap,
                NgayHetHan = requests.NgayHetHan,
                NoiCap = requests.NoiCap,
                TenGoi = requests.TenGoi
            };
            _context.HoSoVanBan.Add(doc);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<bool>();
            }
            else
            {
                return new ApiErrorResult<bool>("Upload file lỗi");
            }
        }

        private List<HoSoVanBanVm> GetListVanBanByHoSoId(int hosoId)
        {
            var data = _context.HoSoVanBan.Where(v => v.HosoId == hosoId)
                .Select(x => new HoSoVanBanVm()
                {
                    FileName = x.FileName,
                    FilePath = x.FilePath,
                    Id = x.Id,
                    MaSo = x.MaSo,
                    NgayCap = x.NgayCap,
                    NgayHetHan = x.NgayHetHan,
                    NoiCap = x.NoiCap,
                    TenGoi = x.TenGoi
                }).ToList();

            return data;
        }

        public async Task<List<DuLieuDuLichTheoLoaiHinhVrm>> LuuTruTheoLoaiHinh()
        {
            prams = new Dictionary<string, object>();
            var result = await base.GetAllByProc<DuLieuDuLichTheoLoaiHinhVrm>("sp_CoSoLuuTruTheoLoaiHinh", prams);

            return result;
        }

        public async Task<List<DuLieuDuLichTheoLoaiHinhVrm>> KhachSanTheoHangSao()
        {
            prams = new Dictionary<string, object>();
            var result = await base.GetAllByProc<DuLieuDuLichTheoLoaiHinhVrm>("sp_CoSoLuuTruTheoHangSao", prams);

            return result;
        }

        public async Task<List<DuLieuDuLichTheoLoaiHinhVrm>> LuuTruTheoDiaBan()
        {
            prams = new Dictionary<string, object>();
            var result = await base.GetAllByProc<DuLieuDuLichTheoLoaiHinhVrm>("sp_CoSoLuuTruTheoHuyen", prams);

            return result;
        }

        public async Task<List<DuLieuDuLichTheoLoaiHinhVrm>> NhaHangTheoLoaiHinh()
        {
            prams = new Dictionary<string, object>();
            var result = await base.GetAllByProc<DuLieuDuLichTheoLoaiHinhVrm>("sp_NhaHangTheoLoai", prams);

            return result;
        }

        public async Task<List<DuLieuDuLichTheoLoaiHinhVrm>> NhaHangTheoDiaBan()
        {
            prams = new Dictionary<string, object>();
            var result = await base.GetAllByProc<DuLieuDuLichTheoLoaiHinhVrm>("sp_NhaHangTheoHuyen", prams);

            return result;
        }

        public async Task<List<DuLieuDuLichTheoLoaiHinhVrm>> LuHanhTheoLoaiHinh()
        {
            prams = new Dictionary<string, object>();
            var result = await base.GetAllByProc<DuLieuDuLichTheoLoaiHinhVrm>("sp_LuHanhTheoLoai", prams);

            return result;
        }

        public async Task<List<DuLieuDuLichTheoLoaiHinhVrm>> MuaSamTheoLoaiHinh()
        {
            prams = new Dictionary<string, object>();
            var result = await base.GetAllByProc<DuLieuDuLichTheoLoaiHinhVrm>("sp_MuaSamTheoLoai", prams);

            return result;
        }

        public async Task<List<DuLieuDuLichTheoLoaiHinhVrm>> MuaSamTheoDiaBan()
        {
            prams = new Dictionary<string, object>();
            var result = await base.GetAllByProc<DuLieuDuLichTheoLoaiHinhVrm>("sp_MuaSamTheoHuyen", prams);

            return result;
        }

        public async Task<List<DuLieuDuLichTheoLoaiHinhVrm>> TourTheoLoaiHinh()
        {
            prams = new Dictionary<string, object>();
            var result = await base.GetAllByProc<DuLieuDuLichTheoLoaiHinhVrm>("sp_TourTheoLoai", prams);

            return result;
        }

        public async Task<List<DuLieuDuLichTheoLoaiHinhVrm>> DiemDuLichTheoLoaiHinh()
        {
            prams = new Dictionary<string, object>();
            var result = await base.GetAllByProc<DuLieuDuLichTheoLoaiHinhVrm>("sp_DiemDuLichTheoLoai", prams);

            return result;
        }

        public async Task<List<DuLieuDuLichTheoLoaiHinhVrm>> HDVTheoLoaiThe()
        {
            prams = new Dictionary<string, object>();
            var result = await base.GetAllByProc<DuLieuDuLichTheoLoaiHinhVrm>("sp_HuongDanVienTheoLoaiThe", prams);

            return result;
        }

        public async Task<List<DuLieuDuLichTheoLoaiHinhVrm>> HDVTheoNgonNgu()
        {
            prams = new Dictionary<string, object>();

            var result = await base.GetAllByProc<DuLieuDuLichTheoLoaiHinhVrm>("sp_HuongDanVienTheoNgonNgu", prams);

            var query = await _context.RawProcedure<List<DuLieuDuLichTheoLoaiHinhVrm>>("sp_HuongDanVienTheoNgonNgu", prams);
            return result;
        }

        public async Task<PagedResult<TimKiemDuLieuVrm>> TimKiemDuLieu(GetPagingRequest request, int linhvucId = 0)

        {

            var loaihinh = _context.LoaiHinh.Where(s => !s.IsDelete);
            var query = from m in _context.HoSo
                        join xa in _context.DiaPhuong on m.PhuongXaId equals xa.Id into dp
                        from xa in dp.DefaultIfEmpty()
                        join huyen in _context.DiaPhuong on m.QuanHuyenId equals huyen.Id into dph
                        from huyen in dph.DefaultIfEmpty()

                        join loai in _context.LoaiHinh on m.LoaiHinhId equals loai.Id into lh
                        from loai in lh.DefaultIfEmpty()

                        join loainhahang in _context.DichVu on m.LoaiHinhId equals loainhahang.Id into lnh
                        from loainhahang in lnh.DefaultIfEmpty()

                        join loaimuasam in _context.LoaiDichVu on m.LoaiHinhId equals loaimuasam.Id into lms
                        from loaimuasam in lms.DefaultIfEmpty()

                        join loaidiemdulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.DiemDuLich) on m.LoaiHinhId equals loaidiemdulich.Id into lddl
                        from loaidiemdulich in lddl.DefaultIfEmpty()

                        join loaikhudulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuDuLich) on m.LoaiHinhId equals loaikhudulich.Id into lkhudl
                        from loaikhudulich in lkhudl.DefaultIfEmpty()

                        join loaikhuvuichoi in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuVuiChoi) on m.LoaiHinhId equals loaikhuvuichoi.Id into lkhuvc
                        from loaikhuvuichoi in lkhuvc.DefaultIfEmpty()

                        join loaithethao in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.TheThao) on m.LoaiHinhId equals loaithethao.Id into ltt
                        from loaithethao in ltt.DefaultIfEmpty()

                        join loaicssk in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.CSSK) on m.LoaiHinhId equals loaicssk.Id into lcssk
                        from loaicssk in lcssk.DefaultIfEmpty()

                        join loailuhanh in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.LuHanh) on m.LoaiHinhId equals loailuhanh.Id into lluhanh
                        from loailuhanh in lluhanh.DefaultIfEmpty()

                        join vanchuyen in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.VanChuyen) on m.LoaiHinhId equals vanchuyen.Id into vc
                        from vanchuyen in vc.DefaultIfEmpty()

                        join nhacungcap in _context.NhaCungCap on m.NhaCungCapId equals nhacungcap.Id into ncc
                        from nhacungcap in ncc.DefaultIfEmpty()
                        orderby m.Ten
                        where m.IsDelete == false
                        && (linhvucId == 0 || m.LinhVucKinhDoanhId == linhvucId)
                        select new { m, xa, huyen, loai, loainhahang, loaimuasam, loaidiemdulich, loaikhudulich, loaikhuvuichoi, loaithethao, loaicssk, loailuhanh, vanchuyen, nhacungcap };
            //3. Paging
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.Ten.Contains(request.Keyword));
            int totalRow = await query.CountAsync();
            var data_paging = await query.Skip((request.PageIndex - 1) * request.PageSize)
                 .Take(request.PageSize)
                 .Select(x => new TimKiemDuLieuVrm()
                 {
                     Id = x.m.Id,
                     DiaChi = x.m.DuongPho,
                     Email = x.m.Email,
                     LinhVucKinhDoanhId = x.m.LinhVucKinhDoanhId,
                     LoaiHinh = x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CoSoLuuTru ? new LoaiHinhModel() { Id = x.loai.Id, TenLoai = x.loai.TenLoai }
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.NhaHang ? new LoaiHinhModel() { Id = x.loainhahang.Id, TenLoai = x.loainhahang.TenDichVu }
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiemDuLich ? new LoaiHinhModel() { Id = x.loaidiemdulich.Id, TenLoai = x.loaidiemdulich.Ten }
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuDuLich ? new LoaiHinhModel() { Id = x.loaikhudulich.Id, TenLoai = x.loaikhudulich.Ten }
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuVuiChoi ? new LoaiHinhModel() { Id = x.loaikhuvuichoi.Id, TenLoai = x.loaikhuvuichoi.Ten }
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.TheThao ? new LoaiHinhModel() { Id = x.loaithethao.Id, TenLoai = x.loaithethao.Ten }
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CSSK ? new LoaiHinhModel() { Id = x.loaicssk.Id, TenLoai = x.loaicssk.Ten }
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.LuHanh ? new LoaiHinhModel() { Id = x.loailuhanh.Id, TenLoai = x.loailuhanh.Ten }
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.VanChuyen ? new LoaiHinhModel() { Id = x.vanchuyen.Id, TenLoai = x.vanchuyen.Ten }
                    : new LoaiHinhModel() { Id = x.loaimuasam.Id, TenLoai = x.loaimuasam.TenLoai },
                     //LinhVuc_LoaiHinh = x.m.LinhVucKinhDoanhId + "_" + x.m.LoaiHinhId,
                     SoDienThoai = x.m.SoDienThoai,
                     Ten = x.m.Ten,
                     ChucVuNguoiDaiDien = x.m.ChucVuNguoiDaiDien,
                     SDTNguoiDaiDien = x.m.SoDienThoaiNguoiDaiDien,
                     HoTenNguoiDaiDien = x.m.HoTenNguoiDaiDien,
                     HangSao = x.m.HangSao.ToString(),
                     SoGiayPhep = x.m.SoGiayPhep,
                     SoQuyetDinh = x.m.SoQuyetDinh,
                     LoaiHinhName = loaihinh.Where(s => s.Id == x.m.LoaiHinhId).FirstOrDefault().TenLoai,

                 }).OrderBy(v => v.Ten).ThenBy(v => v.LinhVucKinhDoanhId).ToListAsync();

            return new PagedResult<TimKiemDuLieuVrm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data_paging
            };
        }
        public async Task<PagedResult<TimKiemDuLieuHDVVrm>> TimKiemDuLieuHDV(DuLieuDuLichSearchRequest request)
        {
            var prams = new Dictionary<string, object>();

            if (!String.IsNullOrEmpty(request.Keyword))
                prams.Add("Key", request.Keyword);

            prams.Add("NgonNguId", request.NgonNguId);
            prams.Add("LoaiTheId", request.LoaiTheId);
            var data = await _context.RawProcedure<TimKiemDuLieuHDVVrm>("Sp_TimKiemDuLieuHDV", prams);

            if (request.TinhTrang == "Thẻ hết hạn")
            {
                data = data.Where(V => V.NgayHetHan <= DateTime.Now).ToList();
            }
            else if (request.TinhTrang == "Thẻ còn hạn")
            {
                data = data.Where(V => V.NgayHetHan > DateTime.Now).ToList();
            }
            else
            {
                request.TinhTrang = "Chưa cấp thẻ";
            }
            int totalRow = data.Count();

            data = data.Select(x => new TimKiemDuLieuHDVVrm()
            {
                NgonNgu = x.NgonNgu,
                DiaChi = x.DiaChi,
                HoVaTen = x.HoVaTen,
                LoaiThe = x.LoaiThe,
                NgayCapThe = x.NgayCapThe,
                NgayHetHan = x.NgayHetHan,
                SoDienThoai = x.SoDienThoai,
                Id = x.Id
            }).OrderBy(v => v.HoVaTen).ToList();

            //4. Select and projection
            var pagedResult = new PagedResult<TimKiemDuLieuHDVVrm>()
            {
                TotalRecords = totalRow,
                //PageIndex = request.PageIndex,
                //PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }
        public async Task<PagedResult<TimKiemDuLieuCSLTVrm>> TimKiemDuLieuCSLT(DuLieuDuLichCSLTSearchRequest request)
        {
            var prams = new Dictionary<string, object>();

            if (!String.IsNullOrEmpty(request.Keyword))
                prams.Add("Key", request.Keyword);

            prams.Add("LoaiHinhId", request.LoaiHinhId);
            prams.Add("HangSao", request.HangSao);

            var data = await _context.RawProcedure<TimKiemDuLieuCSLTVrm>("Sp_TimKiemDuLieuCSLT", prams);

            int totalRow = data.Count();

            data = data.Select(x => new TimKiemDuLieuCSLTVrm()
            {
                Ten = x.Ten,
                ChucVuNguoiDaiDien = x.ChucVuNguoiDaiDien,
                DuongPho = x.DuongPho,
                DienTichMatBang = x.DienTichMatBang,
                HangSao = x.HangSao,
                NgayCongNhanDatChuan = x.NgayCongNhanDatChuan,
                SoDienThoaiNguoiDaiDien = x.SoDienThoaiNguoiDaiDien,
                HoTenNguoiDaiDien = x.HoTenNguoiDaiDien,
                NgayHetHan = x.NgayHetHan,
                NgayQuyetDinh = x.NgayQuyetDinh,
                SoDienThoai = x.SoDienThoai,
                Id = x.Id,
                LoaiHinhId = x.LoaiHinhId
            }).OrderBy(v => v.Ten).ToList();

            //4. Select and projection
            var pagedResult = new PagedResult<TimKiemDuLieuCSLTVrm>()
            {
                TotalRecords = totalRow,
                //PageIndex = request.PageIndex,
                //PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }
        public async Task<PagedResult<TimKiemDuLieuNhaHangVrm>> TimKiemDuLieuNhaHang(DuLieuDuLichNhaHangSearchRequest request)
        {
            var prams = new Dictionary<string, object>();

            if (!String.IsNullOrEmpty(request.Keyword))
                prams.Add("Key", request.Keyword);
            if (request.LoaiId != 0)
            {
                prams.Add("LoaiId", request.LoaiId);
            }

            var data = await _context.RawProcedure<TimKiemDuLieuNhaHangVrm>("sp_TimKiemNhaHangTheoLoai", prams);

            int totalRow = data.Count();

            data = data.Select(x => new TimKiemDuLieuNhaHangVrm()
            {
                Ten = x.Ten,
                LoaiId = x.LoaiId,
                NgayCVDatChuan = x.NgayCVDatChuan,
                TenDichVu = x.TenDichVu,
                ThoiDiemBatDauKinhDoanh = x.ThoiDiemBatDauKinhDoanh,
                DuongPho = x.DuongPho,
                SoDienThoai = x.SoDienThoai,
                DienTichMatBang = x.DienTichMatBang,
                Id = x.Id
            }).OrderBy(v => v.Ten).ToList();

            //4. Select and projection
            var pagedResult = new PagedResult<TimKiemDuLieuNhaHangVrm>()
            {
                TotalRecords = totalRow,
                //PageIndex = request.PageIndex,
                //PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }
        public async Task<PagedResult<TimKiemDuLieuThanhTraVrm>> TimKiemDuLieuThanhTra(DuLieuDuLichThanhTraSearchRequest request)
        {
            var prams = new Dictionary<string, object>();

            if (!String.IsNullOrEmpty(request.Keyword))
                prams.Add("Key", request.Keyword);
            prams.Add("NgayTao", request.NgayTao);

            var data = await _context.RawProcedure<TimKiemDuLieuThanhTraVrm>("Sp_TimKiemDuLieuThanhTra", prams);

            int totalRow = data.Count();

            data = data.Select(x => new TimKiemDuLieuThanhTraVrm()
            {
                Id = x.Id,
                KetQua = x.KetQua,
                NgayTao = x.NgayTao,
                NoiDung = x.NoiDung,
                ThoiGian = x.ThoiGian,
                TruongDoan = x.TruongDoan,
            }).OrderBy(v => v.TruongDoan).ToList();

            //4. Select and projection
            var pagedResult = new PagedResult<TimKiemDuLieuThanhTraVrm>()
            {
                TotalRecords = totalRow,
                //PageIndex = request.PageIndex,
                //PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }
        public async Task<PagedResult<TimKiemDuLieuLuHanhVrm>> TimKiemDuLieuLuHanh(DuLieuDuLichLuHanhSearchRequest request)
        {
            var prams = new Dictionary<string, object>();

            if (!String.IsNullOrEmpty(request.Keyword))
                prams.Add("Key", request.Keyword);
            if (request.LoaiHinhId != 0)
            {
                prams.Add("LoaiHinhId", request.LoaiHinhId);
            }

            var data = await _context.RawProcedure<TimKiemDuLieuLuHanhVrm>("Sp_TimKiemDuLieuLuHanh", prams);

            int totalRow = data.Count();

            data = data.Select(x => new TimKiemDuLieuLuHanhVrm()
            {
                Id = x.Id,
                DuongPho = x.DuongPho,
                DienTichMatbang = x.DienTichMatbang,
                LoaiHinhId = x.LoaiHinhId,
                SoDienThoai = x.SoDienThoai,
                SoLuongLaoDong = x.SoLuongLaoDong,
                Ten = x.Ten,
                ThoiDiembatDauKinhDoanh = x.ThoiDiembatDauKinhDoanh,
                TenDanhMuc = x.TenDanhMuc

            }).OrderBy(v => v.Ten).ToList();
            //4. Select and projection
            var pagedResult = new PagedResult<TimKiemDuLieuLuHanhVrm>()
            {
                TotalRecords = totalRow,
                //PageIndex = request.PageIndex,
                //PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }
        public async Task<PagedResult<TimKiemDuLieuDiemDuLichVrm>> TimKiemDuLieuDiemDuLich(DuLieuDuLichDiemDuLichSearchRequest request)
        {

            var prams = new Dictionary<string, object>();

            if (!String.IsNullOrEmpty(request.Keyword))
                prams.Add("Key", request.Keyword);
            if (request.LoaiHinhId != 0)
            {
                prams.Add("LoaiHinhId", request.LoaiHinhId);
            }


            var data = await _context.RawProcedure<TimKiemDuLieuDiemDuLichVrm>("Sp_TimKiemDuLieuDiemDuLich", prams);

            int totalRow = data.Count();

            data = data.Select(x => new TimKiemDuLieuDiemDuLichVrm()
            {
                Id = x.Id,
                Ten = x.Ten,
                DiaChi = x.DiaChi,
                DienTichMatBang = x.DienTichMatBang,
                TenDanhMuc = x.TenDanhMuc,
                SoDienThoai = x.SoDienThoai,
                LoaiHinhId = x.LoaiHinhId,
                SoGiayPhep = x.SoGiayPhep,
                ThoiDiemBatDauKinhDoanh = x.ThoiDiemBatDauKinhDoanh

            }).OrderBy(v => v.Ten).ToList();

            //4. Select and projection
            var pagedResult = new PagedResult<TimKiemDuLieuDiemDuLichVrm>()
            {
                TotalRecords = totalRow,
                //PageIndex = request.PageIndex,
                //PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }
        public async Task<PagedResult<TimKiemDuLieuCoSoMuaSamVrm>> TimKiemDuLieuCoSoMuaSam(DuLieuDuLichCoSoMuaSamSearchRequest request)
        {

            var prams = new Dictionary<string, object>();

            if (!String.IsNullOrEmpty(request.Keyword))
                prams.Add("Key", request.Keyword);
            if (request.LoaiHinhId != 0)
            {
                prams.Add("LoaiHinhId", request.LoaiHinhId);
            }


            var data = await _context.RawProcedure<TimKiemDuLieuCoSoMuaSamVrm>("Sp_TimKiemDuLieuCoSoMuaSam", prams);

            int totalRow = data.Count();

            data = data.Select(x => new TimKiemDuLieuCoSoMuaSamVrm()
            {
                Id = x.Id,
                Ten = x.Ten,
                DiaChi = x.DiaChi,
                DienTichMatBang = x.DienTichMatBang,
                LoaiHinhId = x.LoaiHinhId,
                SoDienThoai = x.SoDienThoai,
                TenLoaiHinhKinhDoanh = x.TenLoaiHinhKinhDoanh,
                SoGiayPhep = x.SoGiayPhep,
                ThoiDiemBatDauKinhDoanh = x.ThoiDiemBatDauKinhDoanh

            }).OrderBy(v => v.Ten).ToList();

            //4. Select and projection
            var pagedResult = new PagedResult<TimKiemDuLieuCoSoMuaSamVrm>()
            {
                TotalRecords = totalRow,
                //PageIndex = request.PageIndex,
                //PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }
        public async Task<PagedResult<DuLieuDuLichRpt>> DuLieuDuLich(string langId, int linhvucId, RptFromRequets request)
        {
            if (linhvucId == (int)LinhVucKinhDoanh.VSCC)
            {
                var query = from m in _context.DiemVeSinh
                            select new { m };

                int totalRow = await query.CountAsync();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new DuLieuDuLichRpt()
                    {
                        Id = x.m.Id,
                        Ten = x.m.Ten,
                        ViTriTrenBanDo = x.m.ViTri
                    }).ToListAsync();

                return new PagedResult<DuLieuDuLichRpt>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
            }
            else if (linhvucId == (int)LinhVucKinhDoanh.Tour)
            {
                var query = from m in _context.Tours
                            where !m.IsDelete
                            select new { m };

                int totalRow = await query.CountAsync();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new DuLieuDuLichRpt()
                    {
                        Id = x.m.Id,
                        Ten = x.m.TenChuyenDi,
                        Avata = _context.FileUploads.Where(v => v.IsImage && v.IsStatus && v.Id == x.m.Id && v.Type == LoaiFile.tour.ToString()).Select(v => new ImageVm()
                        {
                            Id = v.FileId,
                            Name = v.FileName,
                            Url = v.FileUrl
                        }).FirstOrDefault()
                    }).ToListAsync();

                return new PagedResult<DuLieuDuLichRpt>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
            }
            else if (linhvucId == (int)LinhVucKinhDoanh.HuongDanVien)
            {
                var query = from m in _context.HuongDanVien
                            where m.NgayHetHan >= DateTime.Now
                            select new { m };

                int totalRow = await query.CountAsync();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new DuLieuDuLichRpt()
                    {
                        Id = x.m.Id,
                        Ten = x.m.HoVaTen,
                        SoDienThoai = x.m.SoDienThoai,
                        DiaChi = x.m.DiaChi,
                        Avata = _context.FileUploads.Where(v => v.IsImage && v.Id == x.m.Id && v.Type == LoaiFile.hosohuongdanvien.ToString()).Select(v => new ImageVm()
                        {
                            Name = v.FileName,
                            Id = v.FileId,
                            Url = v.FileUrl
                        }).FirstOrDefault()
                    }).ToListAsync();

                return new PagedResult<DuLieuDuLichRpt>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
            }
            else
            {
                int[] hangsao = !String.IsNullOrEmpty(request.hangsao) ? Array.ConvertAll(request.hangsao.Split(','), int.Parse) : null;
                int[] loaihinh = !String.IsNullOrEmpty(request.loaihinh) ? Array.ConvertAll(request.loaihinh.Split(','), int.Parse) : null;
                int[] tiennghi = !String.IsNullOrEmpty(request.tiennghi) ? Array.ConvertAll(request.tiennghi.Split(','), int.Parse) : null;
                int[] diaphuong = !String.IsNullOrEmpty(request.diaphuong) ? Array.ConvertAll(request.diaphuong.Split(','), int.Parse) : null;

                var query = from m in _context.HoSo
                            join xa in _context.DiaPhuong on m.PhuongXaId equals xa.Id into dp
                            from xa in dp.DefaultIfEmpty()
                            join huyen in _context.DiaPhuong on m.QuanHuyenId equals huyen.Id into dph
                            from huyen in dph.DefaultIfEmpty()

                            join loai in _context.LoaiHinh on m.LoaiHinhId equals loai.Id into lh
                            from loai in lh.DefaultIfEmpty()

                            join loainhahang in _context.DichVu on m.LoaiHinhId equals loainhahang.Id into lnh
                            from loainhahang in lnh.DefaultIfEmpty()

                            join loaimuasam in _context.LoaiDichVu on m.LoaiHinhId equals loaimuasam.Id into lms
                            from loaimuasam in lms.DefaultIfEmpty()

                            join loaidiemdulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.DiemDuLich) on m.LoaiHinhId equals loaidiemdulich.Id into lddl
                            from loaidiemdulich in lddl.DefaultIfEmpty()

                            join loaikhudulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuDuLich) on m.LoaiHinhId equals loaikhudulich.Id into lkhudl
                            from loaikhudulich in lkhudl.DefaultIfEmpty()

                            join loaikhuvuichoi in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuVuiChoi) on m.LoaiHinhId equals loaikhuvuichoi.Id into lkhuvc
                            from loaikhuvuichoi in lkhuvc.DefaultIfEmpty()

                            join loaithethao in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.TheThao) on m.LoaiHinhId equals loaithethao.Id into ltt
                            from loaithethao in ltt.DefaultIfEmpty()

                            join loaicssk in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.CSSK) on m.LoaiHinhId equals loaicssk.Id into lcssk
                            from loaicssk in lcssk.DefaultIfEmpty()

                            join loailuhanh in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.LuHanh) on m.LoaiHinhId equals loailuhanh.Id into lluhanh
                            from loailuhanh in lluhanh.DefaultIfEmpty()

                                // join tn in _context.TienNghiHoSo on m.Id equals tn.HoSoId into tiennghihoso
                                // from tn in tiennghihoso.DefaultIfEmpty()

                            orderby m.HangSao descending, m.LoaiHinhId descending, m.Ten
                            where m.IsDelete == false && m.NgonNguId == langId
                            && (request.datchuan == -1 || m.IsDatChuan == (request.datchuan == 1 ? true : false))
                            && m.LinhVucKinhDoanhId == linhvucId
                            && (hangsao != null && hangsao.Length > 0 ? hangsao.Contains(m.HangSao) : 1 == 1)
                            && (diaphuong != null && diaphuong.Length > 0 ? diaphuong.Contains(m.QuanHuyenId) : 1 == 1)
                            && (loaihinh != null && loaihinh.Length > 0 ?
                                  linhvucId == (int)LinhVucKinhDoanh.CoSoLuuTru ? loaihinh.Contains(loai.Id)
                                : linhvucId == (int)LinhVucKinhDoanh.NhaHang ? loaihinh.Contains(loainhahang.Id)
                                : linhvucId == (int)LinhVucKinhDoanh.DiemDuLich ? loaihinh.Contains(loaidiemdulich.Id)
                                : linhvucId == (int)LinhVucKinhDoanh.KhuDuLich ? loaihinh.Contains(loaikhudulich.Id)
                                : linhvucId == (int)LinhVucKinhDoanh.KhuVuiChoi ? loaihinh.Contains(loaikhuvuichoi.Id)
                                : linhvucId == (int)LinhVucKinhDoanh.TheThao ? loaihinh.Contains(loaithethao.Id)
                                : linhvucId == (int)LinhVucKinhDoanh.CSSK ? loaihinh.Contains(loaicssk.Id)
                                : linhvucId == (int)LinhVucKinhDoanh.LuHanh ? loaihinh.Contains(loailuhanh.Id)
                                : linhvucId == (int)LinhVucKinhDoanh.MuaSam ? loaihinh.Contains(loaimuasam.Id)
                                : 1 == 1 : 1 == 1)

                            // && (tiennghi != null && tiennghi.Length > 0 ? tiennghi.Contains(tn.TienNghiId) && tn.IsSuDung == true : 1 == 1)

                            select new { m, xa, huyen, loai, loainhahang, loaimuasam, loaidiemdulich, loaikhudulich, loaikhuvuichoi, loaithethao, loaicssk, loailuhanh };

                //3. Paging

                if (!string.IsNullOrEmpty(request.Keyword))
                    query = query.Where(x => x.m.Ten.Contains(request.Keyword));

                int totalRow = await query.CountAsync();

                var data_paging = await query.Skip((request.PageIndex - 1) * request.PageSize)
                     .Take(request.PageSize)
                     .Select(x => new DuLieuDuLichRpt()
                     {
                         Id = x.m.Id,
                         DuongPho = x.m.DuongPho,
                         LoaiHinhId = x.m.LoaiHinhId,
                         LoaiHinh = x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CoSoLuuTru ? x.loai.TenLoai
                            : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.NhaHang ? x.loainhahang.TenDichVu
                            : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiemDuLich ? x.loaidiemdulich.Ten
                            : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuDuLich ? x.loaikhudulich.Ten
                            : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuVuiChoi ? x.loaikhuvuichoi.Ten
                            : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.TheThao ? x.loaithethao.Ten
                            : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CSSK ? x.loaicssk.Ten
                            : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.LuHanh ? x.loailuhanh.Ten
                            : x.loaimuasam.TenLoai,
                         Email = x.m.Email,
                         Fax = x.m.Fax,
                         HangSao = x.m.HangSao,
                         QuanHuyen = x.huyen.TenDiaPhuong,
                         SoDienThoai = x.m.SoDienThoai,
                         SoNha = x.m.SoNha,
                         Ten = x.m.Ten,
                         Website = x.m.Website,
                         PhuongXa = x.xa.TenDiaPhuong,
                         GioDongCua = x.m.GioDongCua,
                         GioMoCua = x.m.GioMoCua,
                         MoTa = x.m.GhiChu,
                         GioiThieu = x.m.GioiThieu,
                         LoiKhuyen = "",
                         ViTriTrenBanDo = x.m.ViTriTrenBanDo,
                         GiaThamKhao = "0",
                         IsDatChuan = x.m.IsDatChuan,
                         DiaChi = Functions.GetFullDiaPhuong(x.m.SoNha, x.m.DuongPho, x.xa.TenDiaPhuong, x.huyen.TenDiaPhuong, ""),
                         Avata = _context.FileUploads.Where(v => v.IsImage && v.Id == x.m.Id && v.IsStatus).Select(v =>
                         new ImageVm
                         {
                             Id = v.FileId,
                             Name = v.FileName,
                             Url = v.FileUrl
                         }).FirstOrDefault()
                     }).ToListAsync();

                return new PagedResult<DuLieuDuLichRpt>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data_paging
                };
            }
        }

        public async Task<List<LoaiHinhVm>> LoaiHinh(int linhvucId)
        {
            if (linhvucId == (int)LinhVucKinhDoanh.NhaHang)
            {
                return await _context.DichVu.Where(v => v.IsDelete == false).Select(v => new LoaiHinhVm()
                {
                    Id = v.Id,
                    Ten = v.TenDichVu,
                    LinhVucId = linhvucId
                }).ToListAsync();
            }
            else if (linhvucId == (int)LinhVucKinhDoanh.MuaSam)
            {
                return await _context.LoaiDichVu.Where(v => !v.IsDelete).Select(v => new LoaiHinhVm()
                {
                    Id = v.Id,
                    Ten = v.TenLoai,
                    LinhVucId = linhvucId
                }).ToListAsync();
            }
            else if (linhvucId == (int)LinhVucKinhDoanh.DiemDuLich
                || linhvucId == (int)LinhVucKinhDoanh.KhuDuLich
                || linhvucId == (int)LinhVucKinhDoanh.KhuVuiChoi
                || linhvucId == (int)LinhVucKinhDoanh.CSSK
                || linhvucId == (int)LinhVucKinhDoanh.LuHanh
                || linhvucId == (int)LinhVucKinhDoanh.LuHanh
                || linhvucId == (int)LinhVucKinhDoanh.TheThao)
            {
                return await _context.DanhMuc.Where(v => !v.IsDelete && v.LoaiId == linhvucId).Select(v => new LoaiHinhVm()
                {
                    Id = v.Id,
                    Ten = v.Ten,
                    LinhVucId = linhvucId
                }).ToListAsync();
            }
            else
            {
                return await _context.LoaiHinh.Where(v => !v.IsDelete).Select(v => new LoaiHinhVm()
                {
                    Id = v.Id,
                    Ten = v.TenLoai,
                    LinhVucId = linhvucId
                }).ToListAsync();
            }
        }

        public async Task<DuLieuDuLichRpt> DuLieuDuLichById(int linhvucId, int id)
        {
            try
            {
                if (linhvucId == (int)LinhVucKinhDoanh.VSCC)
                {
                    var query = from m in _context.DiemVeSinh
                                where m.Id == id
                                select new { m };

                    var data = await query.Distinct()
                        .Select(x => new DuLieuDuLichRpt()
                        {
                            Id = x.m.Id,
                            Ten = x.m.Ten,
                            ViTriTrenBanDo = x.m.ViTri
                        }).ToListAsync();

                    return data.FirstOrDefault();
                }
                else if (linhvucId == (int)LinhVucKinhDoanh.Tour)
                {
                    var query = from m in _context.Tours
                                where !m.IsDelete && m.Id == id
                                select new { m };

                    var data = await query.Distinct()
                        .Select(x => new DuLieuDuLichRpt()
                        {
                            Id = x.m.Id,
                            Ten = x.m.TenChuyenDi,
                            Avata = _context.FileUploads.Where(v => v.IsImage && v.IsStatus && v.Id == x.m.Id && v.Type == LinhVucKinhDoanh.Tour.ToString()).Select(v => new ImageVm()
                            {
                                Id = v.FileId,
                                Name = v.FileName,
                                Url = v.FileUrl,
                            }).FirstOrDefault(),
                            GioiThieu = x.m.MoTaChuyenDi
                        }).ToListAsync();

                    return data.FirstOrDefault();
                }
                else if (linhvucId == (int)LinhVucKinhDoanh.HuongDanVien)
                {
                    var query = from m in _context.HuongDanVien
                                where m.Id == id
                                select new { m };

                    var data = await query.Distinct()
                        .Select(x => new DuLieuDuLichRpt()
                        {
                            Id = x.m.Id,
                            Ten = x.m.HoVaTen,
                            Avata = _context.FileUploads.Where(v => v.IsImage && v.Id == x.m.Id && v.Type == LoaiFile.hosohuongdanvien.ToString()).Select(v => new ImageVm()
                            {
                                Name = v.FileName,
                                Id = v.FileId,
                                Url = v.FileUrl
                            }).FirstOrDefault()
                        }).ToListAsync();

                    return data.FirstOrDefault();
                }
                else
                {
                    var query = from m in _context.HoSo
                                join xa in _context.DiaPhuong on m.PhuongXaId equals xa.Id into dp
                                from xa in dp.DefaultIfEmpty()
                                join huyen in _context.DiaPhuong on m.QuanHuyenId equals huyen.Id into dph
                                from huyen in dph.DefaultIfEmpty()

                                join loaihinh in _context.LoaiHinh on m.LoaiHinhId equals loaihinh.Id into lh
                                from loaihinh in lh.DefaultIfEmpty()

                                join loainhahang in _context.DichVu on m.LoaiHinhId equals loainhahang.Id into lnh
                                from loainhahang in lnh.DefaultIfEmpty()

                                join loaimuasam in _context.LoaiDichVu on m.LoaiHinhId equals loaimuasam.Id into lms
                                from loaimuasam in lms.DefaultIfEmpty()

                                join loaidiemdulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.DiemDuLich) on m.LoaiHinhId equals loaidiemdulich.Id into lddl
                                from loaidiemdulich in lddl.DefaultIfEmpty()

                                join loaikhudulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuDuLich) on m.LoaiHinhId equals loaikhudulich.Id into lkhudl
                                from loaikhudulich in lkhudl.DefaultIfEmpty()

                                join loaikhuvuichoi in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuVuiChoi) on m.LoaiHinhId equals loaikhuvuichoi.Id into lkhuvc
                                from loaikhuvuichoi in lkhuvc.DefaultIfEmpty()

                                join loaithethao in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.TheThao) on m.LoaiHinhId equals loaithethao.Id into ltt
                                from loaithethao in ltt.DefaultIfEmpty()

                                join loaicssk in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.CSSK) on m.LoaiHinhId equals loaicssk.Id into lcssk
                                from loaicssk in lcssk.DefaultIfEmpty()

                                join loailuhanh in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.LuHanh) on m.LoaiHinhId equals loailuhanh.Id into lluhanh
                                from loailuhanh in lluhanh.DefaultIfEmpty()

                                join loaivanchuyen in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.VanChuyen) on m.LoaiHinhId equals loaivanchuyen.Id into vanchuyen
                                from loaivanchuyen in vanchuyen.DefaultIfEmpty()

                                join nhacungcap in _context.NhaCungCap on m.NhaCungCapId equals nhacungcap.Id into ncc
                                from nhacungcap in ncc.DefaultIfEmpty()

                                where m.IsDelete == false && m.Id == id
                                select new { m, xa, huyen, loaihinh, loainhahang, loaimuasam, loaidiemdulich, loaikhudulich, loaikhuvuichoi, loaithethao, loaicssk, loailuhanh, nhacungcap, loaivanchuyen };

                    var data = await query.Select(x => new DuLieuDuLichRpt()
                    {
                        Id = x.m.Id,
                        DuongPho = x.m.DuongPho,
                        Email = x.m.Email,
                        Fax = x.m.Fax,
                        HangSao = x.m.HangSao,
                        QuanHuyen = x.huyen.TenDiaPhuong,
                        SoDienThoai = x.m.SoDienThoai,
                        SoNha = x.m.SoNha,
                        Ten = x.m.Ten,
                        Website = x.m.Website,
                        PhuongXa = x.xa.TenDiaPhuong,
                        GioDongCua = x.m.GioDongCua,
                        GioMoCua = x.m.GioMoCua,
                        MoTa = x.m.GhiChu,
                        GioiThieu = x.m.GioiThieu,
                        LoiKhuyen = "",
                        ViTriTrenBanDo = x.m.ViTriTrenBanDo,
                        GiaThamKhao = "0",
                        DiaChi = Functions.GetFullDiaPhuong(x.m.SoNha, x.m.DuongPho, x.xa.TenDiaPhuong, x.huyen.TenDiaPhuong, "Thừa Thiên Huế"),
                        Images = _fileUploadService.GetImageHoSo(x.m.Id).Result,
                        Avata = _context.FileUploads.Where(v => v.IsImage && v.Id == x.m.Id && v.IsStatus).Select(v =>
                               new ImageVm
                               {
                                   Id = v.FileId,
                                   Name = v.FileName,
                                   Url = v.FileUrl
                               }).FirstOrDefault(),
                        IsDatChuan = x.m.IsDatChuan,
                        TienNghi = _context.TienNghiHoSo.Where(v => v.HoSoId == x.m.Id && v.IsSuDung).Select(v => new TienNghiVm
                        {
                            Id = v.TienNghiId,
                            Ten = v.TienNghi.Ten
                        }).ToList(),
                        LoaiHinhId = x.m.LoaiHinhId,
                        ChucVuNguoiDaiDien = x.m.ChucVuNguoiDaiDien,
                        NguoiDaiDien = x.m.HoTenNguoiDaiDien,
                        SDTNguoiDaiDien = x.m.SoDienThoaiNguoiDaiDien,
                        TenVietTat = x.m.TenVietTat,
                        LoaiHinh = x.m.LinhVucKinhDoanhId == 1 ? x.loaihinh.TenLoai
                                   : x.m.LinhVucKinhDoanhId == 2 ? x.loailuhanh.Ten
                                   : x.m.LinhVucKinhDoanhId == 3 ? x.loaimuasam.TenLoai
                                   : x.m.LinhVucKinhDoanhId == 4 ? x.loainhahang.TenDichVu
                                   : x.m.LinhVucKinhDoanhId == 12 ? x.loaivanchuyen.Ten : ""
                    }).FirstOrDefaultAsync();
                    return data;
                }
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<ApiResult<bool>> ImportLuuTru(List<DuLieuDuLichImport> request)
        {
            try
            {
                if (request != null && request.Count > 0)
                {
                    foreach (var obj in request)
                    {
                        var coSoLuuTru = new HoSo()
                        {
                            Ten = obj.Ten,
                            DuongPho = obj.DiaChi,
                            Fax = obj.SoDienThoai,
                            HangSao = obj.Sao,
                            LoaiHinhId = obj.LoaiHinh,
                            QuanHuyenId = obj.Huyen,
                            SoDienThoai = obj.SoDienThoai,
                            TongSoGiuong = obj.SoGiuong,
                            TongSoPhong = obj.SoPhong,
                            IsDatChuan = false,
                            PhuongXaId = 0,
                            GioDongCua = "0",
                            GioMoCua = "0",

                            LinhVucKinhDoanhId = (int)LinhVucKinhDoanh.CoSoLuuTru,
                            NgonNguId = obj.LangId
                        };
                        _context.HoSo.Add(coSoLuuTru);
                    }
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        return new ApiSuccessResult<bool>(true, "Import dữ liệu thành công!");
                    }
                    else
                        return new ApiErrorResult<bool>("Import dữ liệu không thành công");
                }
                else
                    return new ApiSuccessResult<bool>(true, "Không có dữ liệu trong file upload");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }

        public async Task<ApiResult<bool>> ImportLuHanh(List<DuLieuDuLichImport> request)
        {
            try
            {
                if (request != null && request.Count > 0)
                {
                    foreach (var obj in request)
                    {
                        var lienheArr = !String.IsNullOrEmpty(obj.LienHe) ? obj.LienHe.Split(',') : null;

                        var coSoLuuTru = new HoSo()
                        {
                            Ten = obj.Ten,
                            DuongPho = obj.DiaChi,
                            Fax = obj.SoDienThoai,
                            HangSao = obj.Sao,
                            LoaiHinhId = obj.LoaiHinh,
                            QuanHuyenId = obj.Huyen,
                            SoDienThoai = obj.SoDienThoai,
                            TongSoGiuong = obj.SoGiuong,
                            TongSoPhong = obj.SoPhong,
                            IsDatChuan = false,
                            PhuongXaId = 0,
                            GioDongCua = "0",
                            GioMoCua = "0",
                            Email = obj.Email,
                            HoTenNguoiDaiDien = lienheArr != null && lienheArr.Length > 1 ? lienheArr[0] : "",
                            SoDienThoaiNguoiDaiDien = lienheArr != null && lienheArr.Length > 2 ? lienheArr[1] : "",
                            LinhVucKinhDoanhId = (int)LinhVucKinhDoanh.LuHanh,
                            NgonNguId = obj.LangId
                        };
                        _context.HoSo.Add(coSoLuuTru);
                    }
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        return new ApiSuccessResult<bool>(true, "Import dữ liệu thành công!");
                    }
                    else
                        return new ApiErrorResult<bool>("Import dữ liệu không thành công");
                }
                else
                    return new ApiSuccessResult<bool>(true, "Không có dữ liệu trong file upload");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }
        public async Task<List<DuLieuDuLichExportRequest>> GetAllExportData()
        {
            var query = _context.HoSo.Where(x => !x.IsDelete);
            var loaihinh = _context.LoaiHinh.Where(s => !s.IsDelete);
            var diachi = _context.DiaPhuong.Where(s => !s.IsDelete);
            var data = await query
                .Select(x => new DuLieuDuLichExportRequest()
                {
                    Ten = x.Ten,
                    Loai = loaihinh.Where(s => s.Id == x.LoaiHinhId).FirstOrDefault().TenLoai,
                    SoDienThoai = x.SoDienThoai,
                    SoGiayPhep = x.SoGiayPhep,
                    SoQuyetDinh = x.SoQuyetDinh,
                    DiaChi = x.DuongPho,
                    Email = x.Email,
                    HangSao = x.HangSao.ToString(),
                    ChucVuNguoiDaiDien = x.ChucVuNguoiDaiDien,
                    HoTenNguoiDaiDien = x.HoTenNguoiDaiDien,
                    SDTNguoiDaiDien = x.SoDienThoaiNguoiDaiDien
                }).ToListAsync();

            return data;
        }
        public async Task<List<DuLieuDuLichAPI>> GetAll_CSLT()
        {
            //var prams = new Dictionary<string, object>();
            //var sql = "select HoSo.Id,HoSo.Email, HoSo.Ten, HoSo.SoDienThoai ,SoNha +','+ DuongPho +','+ Xa.TenDiaPhuong + ',' + Quan.TenDiaPhuong as DiaChi, LoaiHinh.TenLoai as LoaiHinh from HoSo " +
            //                        "left join LoaiHinh on LoaiHinhId = LoaiHinh.Id " +
            //                        "left join DiaPhuong Xa on Xa.Id = HoSo.PhuongXaId " +
            //                        "left join DiaPhuong Quan on Quan.Id = HoSo.QuanHuyenId " +
            //                        "where HoSo.IsDelete = 0 and HoSo.LinhVucKinhDoanhId = 1";

            //var query = await _context.RawQuery<DuLieuDuLichAPI>(sql, prams);

            var query = from m in _context.HoSo
                        join xa in _context.DiaPhuong on m.PhuongXaId equals xa.Id into dp
                        from xa in dp.DefaultIfEmpty()
                        join huyen in _context.DiaPhuong on m.QuanHuyenId equals huyen.Id into dph
                        from huyen in dph.DefaultIfEmpty()

                        join loaihinh in _context.LoaiHinh on m.LoaiHinhId equals loaihinh.Id into lh
                        from loaihinh in lh.DefaultIfEmpty()

                        join loainhahang in _context.DichVu on m.LoaiHinhId equals loainhahang.Id into lnh
                        from loainhahang in lnh.DefaultIfEmpty()

                        join loaimuasam in _context.LoaiDichVu on m.LoaiHinhId equals loaimuasam.Id into lms
                        from loaimuasam in lms.DefaultIfEmpty()

                        join loaidiemdulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.DiemDuLich) on m.LoaiHinhId equals loaidiemdulich.Id into lddl
                        from loaidiemdulich in lddl.DefaultIfEmpty()

                        join loaikhudulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuDuLich) on m.LoaiHinhId equals loaikhudulich.Id into lkhudl
                        from loaikhudulich in lkhudl.DefaultIfEmpty()

                        join loaikhuvuichoi in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuVuiChoi) on m.LoaiHinhId equals loaikhuvuichoi.Id into lkhuvc
                        from loaikhuvuichoi in lkhuvc.DefaultIfEmpty()

                        join loaithethao in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.TheThao) on m.LoaiHinhId equals loaithethao.Id into ltt
                        from loaithethao in ltt.DefaultIfEmpty()

                        join loaicssk in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.CSSK) on m.LoaiHinhId equals loaicssk.Id into lcssk
                        from loaicssk in lcssk.DefaultIfEmpty()

                        join loailuhanh in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.LuHanh) on m.LoaiHinhId equals loailuhanh.Id into lluhanh
                        from loailuhanh in lluhanh.DefaultIfEmpty()

                        join nhacungcap in _context.NhaCungCap on m.NhaCungCapId equals nhacungcap.Id into ncc
                        from nhacungcap in ncc.DefaultIfEmpty()

                        where m.IsDelete == false && m.LinhVucKinhDoanhId == 1
                        select new { m, xa, huyen, loaihinh, loainhahang, loaimuasam, loaidiemdulich, loaikhudulich, loaikhuvuichoi, loaithethao, loaicssk, loailuhanh, nhacungcap };

            var data = await query.Select(x => new DuLieuDuLichAPI()
            {
                Id = x.m.Id,
                Ten = x.m.Ten,
                ChucVuNguoiDaiDien = x.m.ChucVuNguoiDaiDien,
                DuongPho = x.m.DuongPho,
                Email = x.m.Email,
                Fax = x.m.Fax,
                HangSao = x.m.HangSao,
                IsDatChuan = x.m.IsDatChuan,
                TenVietTat = x.m.TenVietTat,
                ViTriTrenBanDo = x.m.ViTriTrenBanDo,
                LoaiHinhId = x.m.LoaiHinhId,
                PhuongXa = x.xa.TenDiaPhuong,
                QuanHuyen = x.huyen.TenDiaPhuong,
                TinhThanh = "Thừa Thiên Huế",
                SoDienThoai = x.m.SoDienThoai,
                SoNha = x.m.SoNha,
                Website = x.m.Website,
                GioDongCua = x.m.GioDongCua,
                GioMoCua = x.m.GioMoCua,
                LoaiHinh = x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CoSoLuuTru ? x.loaihinh.TenLoai
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.NhaHang ? x.loainhahang.TenDichVu
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiemDuLich ? x.loaidiemdulich.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuDuLich ? x.loaikhudulich.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuVuiChoi ? x.loaikhuvuichoi.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.TheThao ? x.loaithethao.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CSSK ? x.loaicssk.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.LuHanh ? x.loailuhanh.Ten
                    : x.loaimuasam.TenLoai,
                GioiThieu = x.m.GioiThieu,
                Images = _fileUploadService.GetImageByHoSoId(x.m.Id, LoaiFile.hosodulich.ToString()).Result,
                TienNghi = _tienNghiService.GetAllByHoSoUsing(x.m.Id).Result,
                Avatar = _context.FileUploads.Where(v => v.IsStatus && v.Type == LoaiFile.hosodulich.ToString() && v.Id == x.m.Id && v.IsImage).Select(v => v.FileUrl).First(),
                DiaChi = Functions.GetFullDiaPhuong(x.m.SoNha, x.m.DuongPho, x.xa.TenDiaPhuong, x.huyen.TenDiaPhuong, "Thừa Thiên Huế"),
            }).ToListAsync();

            return data;

        }
        public async Task<List<DuLieuDuLichAPI>> GetAll_NhaHang()
        {
            var query = from m in _context.HoSo
                        join xa in _context.DiaPhuong on m.PhuongXaId equals xa.Id into dp
                        from xa in dp.DefaultIfEmpty()
                        join huyen in _context.DiaPhuong on m.QuanHuyenId equals huyen.Id into dph
                        from huyen in dph.DefaultIfEmpty()

                        join loaihinh in _context.LoaiHinh on m.LoaiHinhId equals loaihinh.Id into lh
                        from loaihinh in lh.DefaultIfEmpty()

                        join loainhahang in _context.DichVu on m.LoaiHinhId equals loainhahang.Id into lnh
                        from loainhahang in lnh.DefaultIfEmpty()

                        join loaimuasam in _context.LoaiDichVu on m.LoaiHinhId equals loaimuasam.Id into lms
                        from loaimuasam in lms.DefaultIfEmpty()

                        join loaidiemdulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.DiemDuLich) on m.LoaiHinhId equals loaidiemdulich.Id into lddl
                        from loaidiemdulich in lddl.DefaultIfEmpty()

                        join loaikhudulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuDuLich) on m.LoaiHinhId equals loaikhudulich.Id into lkhudl
                        from loaikhudulich in lkhudl.DefaultIfEmpty()

                        join loaikhuvuichoi in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuVuiChoi) on m.LoaiHinhId equals loaikhuvuichoi.Id into lkhuvc
                        from loaikhuvuichoi in lkhuvc.DefaultIfEmpty()

                        join loaithethao in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.TheThao) on m.LoaiHinhId equals loaithethao.Id into ltt
                        from loaithethao in ltt.DefaultIfEmpty()

                        join loaicssk in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.CSSK) on m.LoaiHinhId equals loaicssk.Id into lcssk
                        from loaicssk in lcssk.DefaultIfEmpty()

                        join loailuhanh in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.LuHanh) on m.LoaiHinhId equals loailuhanh.Id into lluhanh
                        from loailuhanh in lluhanh.DefaultIfEmpty()

                        join nhacungcap in _context.NhaCungCap on m.NhaCungCapId equals nhacungcap.Id into ncc
                        from nhacungcap in ncc.DefaultIfEmpty()

                        where m.IsDelete == false && m.LinhVucKinhDoanhId == 4
                        select new { m, xa, huyen, loaihinh, loainhahang, loaimuasam, loaidiemdulich, loaikhudulich, loaikhuvuichoi, loaithethao, loaicssk, loailuhanh, nhacungcap };

            var data = await query.Select(x => new DuLieuDuLichAPI()
            {
                Id = x.m.Id,
                Ten = x.m.Ten,
                ChucVuNguoiDaiDien = x.m.ChucVuNguoiDaiDien,
                DuongPho = x.m.DuongPho,
                Email = x.m.Email,
                Fax = x.m.Fax,
                HangSao = x.m.HangSao,
                IsDatChuan = x.m.IsDatChuan,
                TenVietTat = x.m.TenVietTat,
                ViTriTrenBanDo = x.m.ViTriTrenBanDo,
                LoaiHinhId = x.m.LoaiHinhId,
                PhuongXa = x.xa.TenDiaPhuong,
                QuanHuyen = x.huyen.TenDiaPhuong,
                TinhThanh = "Thừa Thiên Huế",
                SoDienThoai = x.m.SoDienThoai,
                SoNha = x.m.SoNha,
                Website = x.m.Website,
                GioDongCua = x.m.GioDongCua,
                GioMoCua = x.m.GioMoCua,
                LoaiHinh = x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CoSoLuuTru ? x.loaihinh.TenLoai
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.NhaHang ? x.loainhahang.TenDichVu
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiemDuLich ? x.loaidiemdulich.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuDuLich ? x.loaikhudulich.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuVuiChoi ? x.loaikhuvuichoi.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.TheThao ? x.loaithethao.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CSSK ? x.loaicssk.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.LuHanh ? x.loailuhanh.Ten
                    : x.loaimuasam.TenLoai,
                GioiThieu = x.m.GioiThieu,
                DiaChi = Functions.GetFullDiaPhuong(x.m.SoNha, x.m.DuongPho, x.xa.TenDiaPhuong, x.huyen.TenDiaPhuong, "Thừa Thiên Huế"),
                Images = _fileUploadService.GetImageByHoSoId(x.m.Id, LoaiFile.hosodulich.ToString()).Result,
                TienNghi = _tienNghiService.GetAllByHoSoUsing(x.m.Id).Result,
                Avatar = _context.FileUploads.Where(v => v.IsStatus && v.Type == LoaiFile.hosodulich.ToString() && v.Id == x.m.Id && v.IsImage).Select(v => v.FileUrl).First()

            }).ToListAsync();

            return data;

        }
        public async Task<List<DuLieuDuLichAPI>> GetAll_CoSoMuaSam()
        {
            var query = from m in _context.HoSo
                        join xa in _context.DiaPhuong on m.PhuongXaId equals xa.Id into dp
                        from xa in dp.DefaultIfEmpty()
                        join huyen in _context.DiaPhuong on m.QuanHuyenId equals huyen.Id into dph
                        from huyen in dph.DefaultIfEmpty()

                        join loaihinh in _context.LoaiHinh on m.LoaiHinhId equals loaihinh.Id into lh
                        from loaihinh in lh.DefaultIfEmpty()

                        join loainhahang in _context.DichVu on m.LoaiHinhId equals loainhahang.Id into lnh
                        from loainhahang in lnh.DefaultIfEmpty()

                        join loaimuasam in _context.LoaiDichVu on m.LoaiHinhId equals loaimuasam.Id into lms
                        from loaimuasam in lms.DefaultIfEmpty()

                        join loaidiemdulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.DiemDuLich) on m.LoaiHinhId equals loaidiemdulich.Id into lddl
                        from loaidiemdulich in lddl.DefaultIfEmpty()

                        join loaikhudulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuDuLich) on m.LoaiHinhId equals loaikhudulich.Id into lkhudl
                        from loaikhudulich in lkhudl.DefaultIfEmpty()

                        join loaikhuvuichoi in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuVuiChoi) on m.LoaiHinhId equals loaikhuvuichoi.Id into lkhuvc
                        from loaikhuvuichoi in lkhuvc.DefaultIfEmpty()

                        join loaithethao in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.TheThao) on m.LoaiHinhId equals loaithethao.Id into ltt
                        from loaithethao in ltt.DefaultIfEmpty()

                        join loaicssk in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.CSSK) on m.LoaiHinhId equals loaicssk.Id into lcssk
                        from loaicssk in lcssk.DefaultIfEmpty()

                        join loailuhanh in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.LuHanh) on m.LoaiHinhId equals loailuhanh.Id into lluhanh
                        from loailuhanh in lluhanh.DefaultIfEmpty()

                        join nhacungcap in _context.NhaCungCap on m.NhaCungCapId equals nhacungcap.Id into ncc
                        from nhacungcap in ncc.DefaultIfEmpty()

                        where m.IsDelete == false && m.LinhVucKinhDoanhId == 3
                        select new { m, xa, huyen, loaihinh, loainhahang, loaimuasam, loaidiemdulich, loaikhudulich, loaikhuvuichoi, loaithethao, loaicssk, loailuhanh, nhacungcap };

            var data = await query.Select(x => new DuLieuDuLichAPI()
            {
                Id = x.m.Id,
                Ten = x.m.Ten,
                ChucVuNguoiDaiDien = x.m.ChucVuNguoiDaiDien,
                DuongPho = x.m.DuongPho,
                Email = x.m.Email,
                Fax = x.m.Fax,
                HangSao = x.m.HangSao,
                IsDatChuan = x.m.IsDatChuan,
                TenVietTat = x.m.TenVietTat,
                ViTriTrenBanDo = x.m.ViTriTrenBanDo,
                LoaiHinhId = x.m.LoaiHinhId,
                PhuongXa = x.xa.TenDiaPhuong,
                QuanHuyen = x.huyen.TenDiaPhuong,
                TinhThanh = "Thừa Thiên Huế",
                SoDienThoai = x.m.SoDienThoai,
                SoNha = x.m.SoNha,
                Website = x.m.Website,
                GioDongCua = x.m.GioDongCua,
                GioMoCua = x.m.GioMoCua,
                LoaiHinh = x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CoSoLuuTru ? x.loaihinh.TenLoai
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.NhaHang ? x.loainhahang.TenDichVu
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiemDuLich ? x.loaidiemdulich.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuDuLich ? x.loaikhudulich.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuVuiChoi ? x.loaikhuvuichoi.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.TheThao ? x.loaithethao.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CSSK ? x.loaicssk.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.LuHanh ? x.loailuhanh.Ten
                    : x.loaimuasam.TenLoai,
                GioiThieu = x.m.GioiThieu,
                DiaChi = Functions.GetFullDiaPhuong(x.m.SoNha, x.m.DuongPho, x.xa.TenDiaPhuong, x.huyen.TenDiaPhuong, "Thừa Thiên Huế"),
                Images = _fileUploadService.GetImageByHoSoId(x.m.Id, LoaiFile.hosodulich.ToString()).Result,
                TienNghi = _tienNghiService.GetAllByHoSoUsing(x.m.Id).Result,
                Avatar = _context.FileUploads.Where(v => v.IsStatus && v.Type == LoaiFile.hosodulich.ToString() && v.Id == x.m.Id && v.IsImage).Select(v => v.FileUrl).First()


            }).ToListAsync();

            return data;

        }
        public async Task<List<DuLieuDuLichAPI>> GetAll_LuHanh()
        {
            var query = from m in _context.HoSo
                        join xa in _context.DiaPhuong on m.PhuongXaId equals xa.Id into dp
                        from xa in dp.DefaultIfEmpty()
                        join huyen in _context.DiaPhuong on m.QuanHuyenId equals huyen.Id into dph
                        from huyen in dph.DefaultIfEmpty()

                        join loaihinh in _context.LoaiHinh on m.LoaiHinhId equals loaihinh.Id into lh
                        from loaihinh in lh.DefaultIfEmpty()

                        join loainhahang in _context.DichVu on m.LoaiHinhId equals loainhahang.Id into lnh
                        from loainhahang in lnh.DefaultIfEmpty()

                        join loaimuasam in _context.LoaiDichVu on m.LoaiHinhId equals loaimuasam.Id into lms
                        from loaimuasam in lms.DefaultIfEmpty()

                        join loaidiemdulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.DiemDuLich) on m.LoaiHinhId equals loaidiemdulich.Id into lddl
                        from loaidiemdulich in lddl.DefaultIfEmpty()

                        join loaikhudulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuDuLich) on m.LoaiHinhId equals loaikhudulich.Id into lkhudl
                        from loaikhudulich in lkhudl.DefaultIfEmpty()

                        join loaikhuvuichoi in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuVuiChoi) on m.LoaiHinhId equals loaikhuvuichoi.Id into lkhuvc
                        from loaikhuvuichoi in lkhuvc.DefaultIfEmpty()

                        join loaithethao in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.TheThao) on m.LoaiHinhId equals loaithethao.Id into ltt
                        from loaithethao in ltt.DefaultIfEmpty()

                        join loaicssk in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.CSSK) on m.LoaiHinhId equals loaicssk.Id into lcssk
                        from loaicssk in lcssk.DefaultIfEmpty()

                        join loailuhanh in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.LuHanh) on m.LoaiHinhId equals loailuhanh.Id into lluhanh
                        from loailuhanh in lluhanh.DefaultIfEmpty()

                        join nhacungcap in _context.NhaCungCap on m.NhaCungCapId equals nhacungcap.Id into ncc
                        from nhacungcap in ncc.DefaultIfEmpty()

                        where m.IsDelete == false && m.LinhVucKinhDoanhId == 2
                        select new { m, xa, huyen, loaihinh, loainhahang, loaimuasam, loaidiemdulich, loaikhudulich, loaikhuvuichoi, loaithethao, loaicssk, loailuhanh, nhacungcap };

            var data = await query.Select(x => new DuLieuDuLichAPI()
            {
                Id = x.m.Id,
                Ten = x.m.Ten,
                ChucVuNguoiDaiDien = x.m.ChucVuNguoiDaiDien,
                DuongPho = x.m.DuongPho,
                Email = x.m.Email,
                Fax = x.m.Fax,
                HangSao = x.m.HangSao,
                IsDatChuan = x.m.IsDatChuan,
                TenVietTat = x.m.TenVietTat,
                ViTriTrenBanDo = x.m.ViTriTrenBanDo,
                LoaiHinhId = x.m.LoaiHinhId,
                PhuongXa = x.xa.TenDiaPhuong,
                QuanHuyen = x.huyen.TenDiaPhuong,
                TinhThanh = "Thừa Thiên Huế",
                SoDienThoai = x.m.SoDienThoai,
                SoNha = x.m.SoNha,
                Website = x.m.Website,
                GioDongCua = x.m.GioDongCua,
                GioMoCua = x.m.GioMoCua,
                DiaChi = Functions.GetFullDiaPhuong(x.m.SoNha, x.m.DuongPho, x.xa.TenDiaPhuong, x.huyen.TenDiaPhuong, "Thừa Thiên Huế"),
                LoaiHinh = x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CoSoLuuTru ? x.loaihinh.TenLoai
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.NhaHang ? x.loainhahang.TenDichVu
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiemDuLich ? x.loaidiemdulich.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuDuLich ? x.loaikhudulich.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuVuiChoi ? x.loaikhuvuichoi.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.TheThao ? x.loaithethao.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CSSK ? x.loaicssk.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.LuHanh ? x.loailuhanh.Ten
                    : x.loaimuasam.TenLoai,
                GioiThieu = x.m.GioiThieu,
                Images = _fileUploadService.GetImageByHoSoId(x.m.Id, LoaiFile.hosodulich.ToString()).Result,
                TienNghi = _tienNghiService.GetAllByHoSoUsing(x.m.Id).Result,
                Avatar = _context.FileUploads.Where(v => v.IsStatus && v.Type == LoaiFile.hosodulich.ToString() && v.Id == x.m.Id && v.IsImage).Select(v => v.FileUrl).First()

            }).ToListAsync();

            return data;

        }
        public async Task<List<DuLieuDuLichAPI>> GetAll_VanChuyen()
        {
            var query = from m in _context.HoSo
                        join xa in _context.DiaPhuong on m.PhuongXaId equals xa.Id into dp
                        from xa in dp.DefaultIfEmpty()
                        join huyen in _context.DiaPhuong on m.QuanHuyenId equals huyen.Id into dph
                        from huyen in dph.DefaultIfEmpty()

                        join loaihinh in _context.LoaiHinh on m.LoaiHinhId equals loaihinh.Id into lh
                        from loaihinh in lh.DefaultIfEmpty()

                        join loainhahang in _context.DichVu on m.LoaiHinhId equals loainhahang.Id into lnh
                        from loainhahang in lnh.DefaultIfEmpty()

                        join loaimuasam in _context.LoaiDichVu on m.LoaiHinhId equals loaimuasam.Id into lms
                        from loaimuasam in lms.DefaultIfEmpty()

                        join loaidiemdulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.DiemDuLich) on m.LoaiHinhId equals loaidiemdulich.Id into lddl
                        from loaidiemdulich in lddl.DefaultIfEmpty()

                        join loaikhudulich in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuDuLich) on m.LoaiHinhId equals loaikhudulich.Id into lkhudl
                        from loaikhudulich in lkhudl.DefaultIfEmpty()

                        join loaikhuvuichoi in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.KhuVuiChoi) on m.LoaiHinhId equals loaikhuvuichoi.Id into lkhuvc
                        from loaikhuvuichoi in lkhuvc.DefaultIfEmpty()

                        join loaithethao in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.TheThao) on m.LoaiHinhId equals loaithethao.Id into ltt
                        from loaithethao in ltt.DefaultIfEmpty()

                        join loaicssk in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.CSSK) on m.LoaiHinhId equals loaicssk.Id into lcssk
                        from loaicssk in lcssk.DefaultIfEmpty()

                        join loailuhanh in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.LuHanh) on m.LoaiHinhId equals loailuhanh.Id into lluhanh
                        from loailuhanh in lluhanh.DefaultIfEmpty()

                        join nhacungcap in _context.NhaCungCap on m.NhaCungCapId equals nhacungcap.Id into ncc
                        from nhacungcap in ncc.DefaultIfEmpty()

                        where m.IsDelete == false && m.LinhVucKinhDoanhId == 12
                        select new { m, xa, huyen, loaihinh, loainhahang, loaimuasam, loaidiemdulich, loaikhudulich, loaikhuvuichoi, loaithethao, loaicssk, loailuhanh, nhacungcap };

            var data = await query.Select(x => new DuLieuDuLichAPI()
            {
                Id = x.m.Id,
                Ten = x.m.Ten,
                ChucVuNguoiDaiDien = x.m.ChucVuNguoiDaiDien,
                DuongPho = x.m.DuongPho,
                Email = x.m.Email,
                Fax = x.m.Fax,
                HangSao = x.m.HangSao,
                IsDatChuan = x.m.IsDatChuan,
                TenVietTat = x.m.TenVietTat,
                ViTriTrenBanDo = x.m.ViTriTrenBanDo,
                LoaiHinhId = x.m.LoaiHinhId,
                PhuongXa = x.xa.TenDiaPhuong,
                QuanHuyen = x.huyen.TenDiaPhuong,
                TinhThanh = "Thừa Thiên Huế",
                SoDienThoai = x.m.SoDienThoai,
                SoNha = x.m.SoNha,
                Website = x.m.Website,
                GioDongCua = x.m.GioDongCua,
                GioMoCua = x.m.GioMoCua,
                LoaiHinh = x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CoSoLuuTru ? x.loaihinh.TenLoai
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.NhaHang ? x.loainhahang.TenDichVu
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiemDuLich ? x.loaidiemdulich.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuDuLich ? x.loaikhudulich.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuVuiChoi ? x.loaikhuvuichoi.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.TheThao ? x.loaithethao.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CSSK ? x.loaicssk.Ten
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.LuHanh ? x.loailuhanh.Ten
                    : x.loaimuasam.TenLoai,
                GioiThieu = x.m.GioiThieu,
                DiaChi = Functions.GetFullDiaPhuong(x.m.SoNha, x.m.DuongPho, x.xa.TenDiaPhuong, x.huyen.TenDiaPhuong, "Thừa Thiên Huế"),
                Images = _fileUploadService.GetImageByHoSoId(x.m.Id, LoaiFile.hosodulich.ToString()).Result,
                TienNghi = _tienNghiService.GetAllByHoSoUsing(x.m.Id).Result,
                Avatar = _context.FileUploads.Where(v => v.IsStatus && v.Type == LoaiFile.hosodulich.ToString() && v.Id == x.m.Id && v.IsImage).Select(v => v.FileUrl).First()

            }).ToListAsync();

            return data;
        }

        //HueCIT
        public async Task<List<HoSoVanBanVm>> GetListVanBanByHoSo(int hosoId)
        {
            var data = _context.HoSoVanBan.Where(v => v.HosoId == hosoId)
                .Select(x => new HoSoVanBanVm()
                {
                    FileName = x.FileName,
                    FilePath = x.FilePath,
                    Id = x.Id,
                    GiayPhepId = x.GiayPhepId,
                }).ToList();

            return data;
        }

        public async Task<Dictionary<int, int>> DuLieuDuLichEnglish(List<DuLieuDuLichModel> items)
        {
            var data = await _context.HoSo
                .AsNoTracking()
                .Where(x => !x.IsDelete && x.NgonNguId.ToLower() == "en")
                .ToListAsync();

            var result = from d in data
                         join m in items on d.SoDienThoai equals m.SoDienThoai
                         select new { Key = m.Id, Value = d.Id };

            return result.ToDictionary(x => x.Key, x => x.Value);
        }
    }

}

