using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechLife.Common.Enums;
using TechLife.Common;
using TechLife.Data.Entities;
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.HueCIT;
using TechLife.Model;
using System.Linq;
using TechLife.Data;
using TechLife.Service.Common;
using TechLife.Data.Repositories;
using TechLife.Model.HoSoVanBan;
using TechLife.Model.NhaCungCap;

namespace TechLife.Service.HueCIT
{
    public interface IHoSoDongBoService
    {
        #region Hồ sơ
        Task<DuLieuDuLichModel> Add(string langId, DuLieuDuLichModel request);

        Task<DuLieuDuLichModel> Edit(int id, DuLieuDuLichModel request);

        Task<DuLieuDuLichModel> GetHoSoByDongBo(int linhVucId, int? dongBoId);

        Task<DuLieuDuLichModel> GetHoSoByTen(int linhVucId, string Ten);

        #endregion

        #region Danh mục
        Task<int> LoaiHinhDongBo(LoaiHinhDB req);

        #endregion

        #region Hình ảnh
        Task<IEnumerable<FileUploadModel>> GetFileDongBoByHoSoId(int hosoid, string type, int nguondongbo);

        Task<bool> AddFile(FileUploadModel request);
        Task<bool> AddFiles(List<FileUploadModel> request);

        Task<bool> EditFile(int id, FileUploadModel request);

        Task<bool> DeleteFiles(List<int> filesId);

        #endregion
    }
    public class HoSoDongBoService : BaseRepository, IHoSoDongBoService
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

        public HoSoDongBoService(TLDbContext context
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

        #region Hồ sơ
        public async Task<DuLieuDuLichModel> Add(string langId, DuLieuDuLichModel request)
        {
            try
            {
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
                    //HueCIT
                    LoaiDiaDiemAnUong = request.LoaiDiaDiemAnUong,
                    PhucVu = request.PhucVu,
                    ToaDoX = request.ToaDoX,
                    ToaDoY = request.ToaDoY,
                    MaDoanhNghiep = request.MaDoanhNghiep,
                    NguonDongBo = request.NguonDongBo,
                    DongBoID = request.DongBoID,
                    GiaThamKhaoTu = request.GiaThamKhaoTu,
                    GiaThamKhaoDen = request.GiaThamKhaoDen
                };
                _context.HoSo.Add(coSoLuuTru);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    request.Id = coSoLuuTru.Id;

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
                        foreach (var d in request.DSNhaHang)
                        {
                            _context.QuyMoNhaHangLuuTru.Add(new QuyMoNhaHangLuuTru()
                            {
                                DienTich = d.DienTich,
                                TenGoi = d.TenGoi,
                                HoSoId = request.Id,
                                SoGhe = d.SoGhe
                            });
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
                    }

                    await _context.SaveChangesAsync();

                    return request;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DuLieuDuLichModel> Edit(int id, DuLieuDuLichModel request)
        {
            try
            {
                var coSoLuuTru = await _context.HoSo.Where(x => x.Id == id).ToListAsync();
                if (coSoLuuTru == null || coSoLuuTru.Count() <= 0)
                {
                    return null;
                }

                var model = coSoLuuTru.FirstOrDefault();
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
                model.GiaThamKhaoTu = request.GiaThamKhaoTu;
                model.GiaThamKhaoDen = request.GiaThamKhaoDen;
                model.DongBoID = request.DongBoID;
                model.NguonDongBo = request.NguonDongBo;
                model.IsDelete = false;
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
                    foreach (var d in request.DSNhaHang)
                    {
                        _context.QuyMoNhaHangLuuTru.Add(new QuyMoNhaHangLuuTru()
                        {
                            DienTich = d.DienTich,
                            TenGoi = d.TenGoi,
                            HoSoId = request.Id,
                            SoGhe = d.SoGhe
                        });
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
                            obj.FilePath = objRequest.FilePath;
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

                _context.HoSo.Update(model);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    request.Id = model.Id;
                    return request;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DuLieuDuLichModel> GetHoSoByDongBo(int linhVucId, int? dongBoId)
        {
            try
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

                            join loaidisan in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.DiSanVanHoa) on m.LoaiHinhId equals loaidisan.Id into lds
                            from loaidisan in lds.DefaultIfEmpty()

                            join nhacungcap in _context.NhaCungCap on m.NhaCungCapId equals nhacungcap.Id into ncc
                            from nhacungcap in ncc.DefaultIfEmpty()

                            join loaidiadiem in _context.LoaiDiaDiemAnUong.Where(v => v.IsDelete == false) on m.LoaiDiaDiemAnUong equals loaidiadiem.Id into ldd
                            from loaidiadiem in ldd.DefaultIfEmpty()

                            where m.IsDelete == false && m.DongBoID == dongBoId && m.LinhVucKinhDoanhId == linhVucId
                            select new { m, xa, huyen, loaihinh, loainhahang, loaimuasam, loaidiemdulich, loaikhudulich, loaikhuvuichoi, loaithethao, loaicssk, loailuhanh, nhacungcap, loaidiadiem, loaidisan };

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
                    PhuongXa = x.xa.TenDiaPhuong,
                    QuanHuyenId = x.m.QuanHuyenId,
                    QuanHuyen = x.huyen.TenDiaPhuong,
                    TinhThanh = "Thừa Thiên Huế",

                    SoDienThoai = x.m.SoDienThoai,
                    SoDienThoaiNguoiDaiDien = x.m.SoDienThoaiNguoiDaiDien,
                    SoGiayPhep = x.m.SoGiayPhep,
                    SoLanChuyenChu = x.m.SoLanChuyenChu,
                    SoLuongLaoDong = x.m.SoLuongLaoDong,
                    SoNha = x.m.SoNha,
                    SoQuyetDinh = x.m.SoQuyetDinh,
                    SoTang = x.m.SoTang,
                    NhaCungCap = new NhaCungCapVm() { Ten = x.nhacungcap.Ten },
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
                    LoaiHinh = x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CoSoLuuTru ? new LoaiHinhModel() { Id = x.loaihinh.Id, TenLoai = x.loaihinh.TenLoai }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.NhaHang ? new LoaiHinhModel() { Id = x.loainhahang.Id, TenLoai = x.loainhahang.TenDichVu }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiemDuLich ? new LoaiHinhModel() { Id = x.loaidiemdulich.Id, TenLoai = x.loaidiemdulich.Ten }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuDuLich ? new LoaiHinhModel() { Id = x.loaikhudulich.Id, TenLoai = x.loaikhudulich.Ten }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuVuiChoi ? new LoaiHinhModel() { Id = x.loaikhuvuichoi.Id, TenLoai = x.loaikhuvuichoi.Ten }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.TheThao ? new LoaiHinhModel() { Id = x.loaithethao.Id, TenLoai = x.loaithethao.Ten }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CSSK ? new LoaiHinhModel() { Id = x.loaicssk.Id, TenLoai = x.loaicssk.Ten }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.LuHanh ? new LoaiHinhModel() { Id = x.loailuhanh.Id, TenLoai = x.loailuhanh.Ten }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiSanVanHoa ? new LoaiHinhModel() { Id = x.loaidisan.Id, TenLoai = x.loaidisan.Ten }
                        : new LoaiHinhModel() { Id = x.loaimuasam.Id, TenLoai = x.loaimuasam.TenLoai },
                    SoLDGianTiep = x.m.SoLDGianTiep,
                    SoLDNamNgoaiNuoc = x.m.SoLDNamNgoaiNuoc,
                    SoLDNamTrongNuoc = x.m.SoLDNamTrongNuoc,
                    SoLDNuNgoaiNuoc = x.m.SoLDNuNgoaiNuoc,
                    SoLDNuTrongNuoc = x.m.SoLDNuTrongNuoc,
                    SoLDThoiVu = x.m.SoLDThoiVu,
                    SoLDThuongXuyen = x.m.SoLDThuongXuyen,
                    SoLDTrucTiep = x.m.SoLDTrucTiep,
                    GioiThieu = x.m.GioiThieu,
                    Images = _fileUploadService.GetImageByHoSoId(x.m.Id, LoaiFile.hosodulich.ToString()).Result,
                    DocumentFiles = _fileUploadService.GetFileByHoSoId(x.m.Id, LoaiFile.hosodulich.ToString()).Result,
                    DSBoPhan = _boPhanService.GetAllByHoSo(x.m.Id).Result,
                    DSVeDichVu = _dichVuService.GetAllByHoSo(x.m.Id).Result,
                    DSLoaiPhong = _loaiPhongService.GetAllByHoSo(x.m.Id).Result,
                    DSMucDoTTNN = _mucDoThongThaoNgoaiNguService.GetAllByHoSo(x.m.Id).Result,
                    DSNgoaiNgu = _ngoaiNguService.GetAllByHoSo(x.m.Id).Result,
                    DSThucDon = _thucDonService.GetAllByHoSo(x.m.Id).Result,
                    DSTienNghi = _tienNghiService.GetAllByHoSo(x.m.Id).Result,
                    DSTrinhDo = _trinhDoService.GetAllByHoSo(x.m.Id).Result,
                    DSDanhGia = _danhGiaService.GetAll(x.m.Id, TechLife.Common.Enums.LoaiBinhLuan.hosodulich.ToString()).Result,
                    Tours = _tourService.GetAll(x.m.Id).Result,
                    DSNhaHang = _context.QuyMoNhaHangLuuTru.Where(v => v.HoSoId == x.m.Id).Select(v => new QuyMoNhaHangVm()
                    {
                        DienTich = v.DienTich,
                        HoSoId = v.HoSoId,
                        Id = v.Id,
                        SoGhe = v.SoGhe,
                        TenGoi = v.TenGoi
                    }).ToList(),
                    DSVanBan = _context.HoSoVanBan.Where(v => v.HosoId == x.m.Id && v.Loai == LoaiFile.hosodulich.ToString()).Select(x => new HoSoVanBanVm()
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
                    }).ToList(),
                    LoaiDiaDiemAnUong = x.m.LoaiDiaDiemAnUong,
                    LoaiDiaDiem = new TechLife.Model.HueCIT.LoaiDiaDiemAnUong() { Id = x.loaidiadiem.Id, TenLoai = x.loaidiadiem.TenLoai },
                    PhucVu = x.m.PhucVu,
                    ToaDoX = x.m.ToaDoX,
                    ToaDoY = x.m.ToaDoY,
                    MaDoanhNghiep = x.m.MaDoanhNghiep,
                    NguonDongBo = x.m.NguonDongBo
                }).FirstOrDefaultAsync();

                return data;
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }
        
        public async Task<DuLieuDuLichModel> GetHoSoByTen(int linhVucId, string ten)
        {
            try
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

                            join loaidisan in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.DiSanVanHoa) on m.LoaiHinhId equals loaidisan.Id into lds
                            from loaidisan in lds.DefaultIfEmpty()

                            join nhacungcap in _context.NhaCungCap on m.NhaCungCapId equals nhacungcap.Id into ncc
                            from nhacungcap in ncc.DefaultIfEmpty()

                            join loaidiadiem in _context.LoaiDiaDiemAnUong.Where(v => v.IsDelete == false) on m.LoaiDiaDiemAnUong equals loaidiadiem.Id into ldd
                            from loaidiadiem in ldd.DefaultIfEmpty()

                            where m.IsDelete == false && m.Ten == ten && m.LinhVucKinhDoanhId == linhVucId
                            select new { m, xa, huyen, loaihinh, loainhahang, loaimuasam, loaidiemdulich, loaikhudulich, loaikhuvuichoi, loaithethao, loaicssk, loailuhanh, nhacungcap, loaidiadiem, loaidisan };

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
                    PhuongXa = x.xa.TenDiaPhuong,
                    QuanHuyenId = x.m.QuanHuyenId,
                    QuanHuyen = x.huyen.TenDiaPhuong,
                    TinhThanh = "Thừa Thiên Huế",

                    SoDienThoai = x.m.SoDienThoai,
                    SoDienThoaiNguoiDaiDien = x.m.SoDienThoaiNguoiDaiDien,
                    SoGiayPhep = x.m.SoGiayPhep,
                    SoLanChuyenChu = x.m.SoLanChuyenChu,
                    SoLuongLaoDong = x.m.SoLuongLaoDong,
                    SoNha = x.m.SoNha,
                    SoQuyetDinh = x.m.SoQuyetDinh,
                    SoTang = x.m.SoTang,
                    NhaCungCap = new NhaCungCapVm() { Ten = x.nhacungcap.Ten },
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
                    LoaiHinh = x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CoSoLuuTru ? new LoaiHinhModel() { Id = x.loaihinh.Id, TenLoai = x.loaihinh.TenLoai }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.NhaHang ? new LoaiHinhModel() { Id = x.loainhahang.Id, TenLoai = x.loainhahang.TenDichVu }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiemDuLich ? new LoaiHinhModel() { Id = x.loaidiemdulich.Id, TenLoai = x.loaidiemdulich.Ten }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuDuLich ? new LoaiHinhModel() { Id = x.loaikhudulich.Id, TenLoai = x.loaikhudulich.Ten }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.KhuVuiChoi ? new LoaiHinhModel() { Id = x.loaikhuvuichoi.Id, TenLoai = x.loaikhuvuichoi.Ten }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.TheThao ? new LoaiHinhModel() { Id = x.loaithethao.Id, TenLoai = x.loaithethao.Ten }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.CSSK ? new LoaiHinhModel() { Id = x.loaicssk.Id, TenLoai = x.loaicssk.Ten }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.LuHanh ? new LoaiHinhModel() { Id = x.loailuhanh.Id, TenLoai = x.loailuhanh.Ten }
                        : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.DiSanVanHoa ? new LoaiHinhModel() { Id = x.loaidisan.Id, TenLoai = x.loaidisan.Ten }
                        : new LoaiHinhModel() { Id = x.loaimuasam.Id, TenLoai = x.loaimuasam.TenLoai },
                    SoLDGianTiep = x.m.SoLDGianTiep,
                    SoLDNamNgoaiNuoc = x.m.SoLDNamNgoaiNuoc,
                    SoLDNamTrongNuoc = x.m.SoLDNamTrongNuoc,
                    SoLDNuNgoaiNuoc = x.m.SoLDNuNgoaiNuoc,
                    SoLDNuTrongNuoc = x.m.SoLDNuTrongNuoc,
                    SoLDThoiVu = x.m.SoLDThoiVu,
                    SoLDThuongXuyen = x.m.SoLDThuongXuyen,
                    SoLDTrucTiep = x.m.SoLDTrucTiep,
                    GioiThieu = x.m.GioiThieu,
                    Images = _fileUploadService.GetImageByHoSoId(x.m.Id, LoaiFile.hosodulich.ToString()).Result,
                    DocumentFiles = _fileUploadService.GetFileByHoSoId(x.m.Id, LoaiFile.hosodulich.ToString()).Result,
                    DSBoPhan = _boPhanService.GetAllByHoSo(x.m.Id).Result,
                    DSVeDichVu = _dichVuService.GetAllByHoSo(x.m.Id).Result,
                    DSLoaiPhong = _loaiPhongService.GetAllByHoSo(x.m.Id).Result,
                    DSMucDoTTNN = _mucDoThongThaoNgoaiNguService.GetAllByHoSo(x.m.Id).Result,
                    DSNgoaiNgu = _ngoaiNguService.GetAllByHoSo(x.m.Id).Result,
                    DSThucDon = _thucDonService.GetAllByHoSo(x.m.Id).Result,
                    DSTienNghi = _tienNghiService.GetAllByHoSo(x.m.Id).Result,
                    DSTrinhDo = _trinhDoService.GetAllByHoSo(x.m.Id).Result,
                    DSDanhGia = _danhGiaService.GetAll(x.m.Id, TechLife.Common.Enums.LoaiBinhLuan.hosodulich.ToString()).Result,
                    Tours = _tourService.GetAll(x.m.Id).Result,
                    DSNhaHang = _context.QuyMoNhaHangLuuTru.Where(v => v.HoSoId == x.m.Id).Select(v => new QuyMoNhaHangVm()
                    {
                        DienTich = v.DienTich,
                        HoSoId = v.HoSoId,
                        Id = v.Id,
                        SoGhe = v.SoGhe,
                        TenGoi = v.TenGoi
                    }).ToList(),
                    DSVanBan = _context.HoSoVanBan.Where(v => v.HosoId == x.m.Id && v.Loai == LoaiFile.hosodulich.ToString()).Select(x => new HoSoVanBanVm()
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
                    }).ToList(),
                    LoaiDiaDiemAnUong = x.m.LoaiDiaDiemAnUong,
                    LoaiDiaDiem = new TechLife.Model.HueCIT.LoaiDiaDiemAnUong() { Id = x.loaidiadiem.Id, TenLoai = x.loaidiadiem.TenLoai },
                    PhucVu = x.m.PhucVu,
                    ToaDoX = x.m.ToaDoX,
                    ToaDoY = x.m.ToaDoY,
                    MaDoanhNghiep = x.m.MaDoanhNghiep,
                    NguonDongBo = x.m.NguonDongBo
                }).FirstOrDefaultAsync();

                return data;
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        #endregion


        #region Danh mục
        public async Task<int> LoaiHinhDongBo(LoaiHinhDB req)
        {
            int result = 0;
            if (req.LinhVucId == (int)LinhVucKinhDoanh.NhaHang)
            {
                //var danhmuc = await _context.DichVu.Where(v => v.IsDelete == false && v.TenDichVu == req.Ten).FirstOrDefaultAsync();
            }
            else if (req.LinhVucId == (int)LinhVucKinhDoanh.MuaSam)
            {
                var danhmuc = await _context.LoaiDichVu.Where(v => v.IsDelete == false && v.DongBoID == req.DongBoID && v.NguonDongBo == req.NguonDongBo).FirstOrDefaultAsync();
                if (danhmuc != null)
                {
                    result = danhmuc.Id;
                }
            }
            else if (req.LinhVucId == (int)LinhVucKinhDoanh.VanChuyen)
            {
                var danhmuc = await _context.DanhMuc.Where(v => v.LoaiId == req.LinhVucId && v.Ten.ToUpper() == req.Ten.ToUpper()).FirstOrDefaultAsync();
                if (danhmuc != null)
                {
                    result = danhmuc.Id;
                }
            } 
            else if (req.LinhVucId == (int)LinhVucKinhDoanh.DiemDuLich
                || req.LinhVucId == (int)LinhVucKinhDoanh.KhuDuLich
                || req.LinhVucId == (int)LinhVucKinhDoanh.KhuVuiChoi
                || req.LinhVucId == (int)LinhVucKinhDoanh.CSSK
                || req.LinhVucId == (int)LinhVucKinhDoanh.LuHanh
                || req.LinhVucId == (int)LinhVucKinhDoanh.TheThao
                || req.LinhVucId == (int)LinhVucKinhDoanh.DiSanVanHoa)
            {
                var danhmuc = await _context.DanhMuc.Where(v => v.IsDelete == false && v.LoaiId == req.LinhVucId && v.DongBoID == req.DongBoID).FirstOrDefaultAsync();
                if (danhmuc != null)
                {
                    result = danhmuc.Id;
                }
            }
            else
            {
                
            }

            return result;
        }
        #endregion

        #region Hình ảnh
        public async Task<IEnumerable<FileUploadModel>> GetFileDongBoByHoSoId(int hosoid, string type, int nguondongbo)
        {
            var query = from m in _context.FileUploads
                        where m.IsImage && m.Id == hosoid && m.Type == type && m.NguonDongBo == nguondongbo
                        orderby m.FileId descending
                        select new FileUploadModel
                        {
                            Id = m.Id,
                            FileName = m.FileName,
                            IsImage = m.IsImage,
                            FileId = m.FileId,
                            FileUrl = m.FileUrl,
                            IsStatus = m.IsStatus,
                            NgayTao = m.NgayTao,
                            Type = m.Type,
                            //HueCIT
                            FileType = m.FileType == null ? 0 : m.FileType,
                            NguonDongBo = m.NguonDongBo
                        };
            var data = await query.ToListAsync();
            return data;
        }

        public async Task<bool> AddFiles(List<FileUploadModel> request)
        {
            request.ForEach(x =>
            {
                var obj = new TechLife.Data.Entities.FileUpload()
                {
                    FileName = x.FileName,
                    FileUrl = x.FileUrl,
                    Id = x.Id,
                    IsImage = x.IsImage,
                    NgayTao = DateTime.Now,
                    Type = x.Type,
                    IsStatus = true,
                    FileType = (int)x.FileType,
                    NguonDongBo = (int)x.NguonDongBo
                };
                _context.FileUploads.Add(obj);
            });

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> AddFile(FileUploadModel request)
        {

            var obj = new TechLife.Data.Entities.FileUpload()
            {
                FileName = request.FileName,
                FileUrl = request.FileUrl,
                Id = request.Id,
                IsImage = request.IsImage,
                NgayTao = DateTime.Now,
                Type = request.Type,
                IsStatus = true,
                FileType = (int)request.FileType,
                NguonDongBo = (int)request.NguonDongBo
            };

            _context.FileUploads.Add(obj);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> EditFile(int id, FileUploadModel request)
        {
            var m = await _context.FileUploads.Where(x => x.FileId == id).ToListAsync();

            if (m != null)
            {
                var obj = m.FirstOrDefault();
                obj.FileName = request.FileName;
                obj.FileUrl = request.FileUrl;
                obj.Type = request.Type;
                obj.Id = request.Id;
                obj.IsImage = request.IsImage;
                obj.IsStatus = request.IsStatus;
                obj.NgayTao = DateTime.Now;
                obj.FileType = (int)request.FileType;
                obj.NguonDongBo = (int)request.NguonDongBo;

                _context.FileUploads.Update(obj);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> DeleteFiles(List<int> filesId)
        {
            foreach (var item in filesId)
            {
                var obj = await _context.FileUploads.FindAsync(item);
                if (obj == null)
                {
                    return false;
                }
                _context.FileUploads.Remove(obj);
            }

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        public async Task<bool> CheckMaDoanhNghiep(string ma)
        {
            try
            {
                bool flag = false;
                var query = from m in _context.HoSo
                            where m.IsDelete == false && m.MaDoanhNghiep.Equals(ma)
                            select m;

                if (query.Any())
                {
                    flag = true;
                }

                return flag;
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

    }
}
