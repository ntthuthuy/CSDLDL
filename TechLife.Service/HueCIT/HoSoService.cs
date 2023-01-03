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
using TechLife.Data.Repositories;
using TechLife.Model;
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.HoSoVanBan;
using TechLife.Model.HueCIT;
using TechLife.Model.NhaCungCap;
using TechLife.Service.Common;

namespace TechLife.Service.HueCIT
{
    public interface IHoSoService
    {
        Task<IEnumerable<DuLieuDuLichModel>> GetsHoSo(string langId, int linhvucId, FilterRequest request);
        Task<PagedResult<DuLieuDuLichModel>> GetsHoSoPaging(string langId, int linhvucId, FilterRequestPaging request);
        Task<IEnumerable<HoSoDropdownList>> GetsHoSoForFilter(string langId, int linhvucId, FilterRequest request);

        Task<DuLieuDuLichModel> GetHoSo(int id);

        Task<HoSo> EditBanDo(int id, HoSoBanDo request);

        Task<bool> CheckMaDoanhNghiep(string ma);
        Task<ApiResult<bool>> UploadImage(int id, ImageUploadExtRequest request);
    }

    public class HoSoService : BaseRepository, IHoSoService
    {

        private readonly TLDbContext _context;

        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private const string IMG_TYPE = "bmp, png, jpg, jpeg, gif, webp";
        private const string MEDIA_TYPE = "mp4, mov, wmv, avi, flv, mkv, 3gp, mp3, wav";
        private const string DOC_TYPE = "doc, docx, xls, xlsx, pdf";

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

        public HoSoService(TLDbContext context
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

        public async Task<IEnumerable<DuLieuDuLichModel>> GetsHoSo(string langId, int linhvucId, FilterRequest request)
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

                        join vanchuyen in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.VanChuyen) on m.LoaiHinhId equals vanchuyen.Id into vc
                        from vanchuyen in vc.DefaultIfEmpty()

                        join loaidisan in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.DiSanVanHoa) on m.LoaiHinhId equals loaidisan.Id into lds
                        from loaidisan in lds.DefaultIfEmpty()

                        join nhacungcap in _context.NhaCungCap on m.NhaCungCapId equals nhacungcap.Id into ncc
                        from nhacungcap in ncc.DefaultIfEmpty()

                        join loaidiadiem in _context.LoaiDiaDiemAnUong.Where(v => v.IsDelete == false) on m.LoaiDiaDiemAnUong equals loaidiadiem.Id into ldd
                        from loaidiadiem in ldd.DefaultIfEmpty()

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
                        && (request.phucvu == -1 || m.PhucVu == 2 || m.PhucVu == request.phucvu)
                        && (request.loaidiadiem == -1 || m.LoaiDiaDiemAnUong == request.loaidiadiem)
                        && (request.nguondongbo == -1 ? true : request.nguondongbo == 0 ? m.NguonDongBo == null : m.NguonDongBo == request.nguondongbo)
                        && (request.dongboId == -1 || m.DongBoID == request.dongboId)
                        select new { m, xa, huyen, loaihinh, nhacungcap, loainhahang, loaimuasam, loaidiemdulich, loaikhudulich, loaikhuvuichoi, loaithethao, loaicssk, loailuhanh, vanchuyen, loaidiadiem, loaidisan };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.Ten.Contains(request.Keyword));

            int totalRow = await query.CountAsync();

            var data = await query
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
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.VanChuyen ? new LoaiHinhModel() { Id = x.vanchuyen.Id, TenLoai = x.vanchuyen.Ten }
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
                LoaiDiaDiemAnUong = x.m.LoaiDiaDiemAnUong,
                LoaiDiaDiem = new TechLife.Model.HueCIT.LoaiDiaDiemAnUong() { Id = x.loaidiadiem.Id, TenLoai = x.loaidiadiem.TenLoai },
                PhucVu = x.m.PhucVu,
                ToaDoX = x.m.ToaDoX,
                ToaDoY = x.m.ToaDoY,
                MaDoanhNghiep = x.m.MaDoanhNghiep,
                NguonDongBo = x.m.NguonDongBo,
                DongBoID = x.m.DongBoID,
                GiaThamKhaoTu = x.m.GiaThamKhaoTu,
                GiaThamKhaoDen = x.m.GiaThamKhaoDen
            }).ToListAsync();

            //4. Select and projection
            return data;
        }

        public async Task<PagedResult<DuLieuDuLichModel>> GetsHoSoPaging(string langId, int linhvucId, FilterRequestPaging request)
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

                        join vanchuyen in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.VanChuyen) on m.LoaiHinhId equals vanchuyen.Id into vc
                        from vanchuyen in vc.DefaultIfEmpty()

                        join loaidisan in _context.DanhMuc.Where(v => v.LoaiId == (int)LinhVucKinhDoanh.DiSanVanHoa) on m.LoaiHinhId equals loaidisan.Id into lds
                        from loaidisan in lds.DefaultIfEmpty()

                        join nhacungcap in _context.NhaCungCap on m.NhaCungCapId equals nhacungcap.Id into ncc
                        from nhacungcap in ncc.DefaultIfEmpty()

                        join loaidiadiem in _context.LoaiDiaDiemAnUong.Where(v => v.IsDelete == false) on m.LoaiDiaDiemAnUong equals loaidiadiem.Id into ldd
                        from loaidiadiem in ldd.DefaultIfEmpty()

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
                        && (request.phucvu == -1 || m.PhucVu == 2 || m.PhucVu == request.phucvu)
                        && (request.loaidiadiem == -1 || m.LoaiDiaDiemAnUong == request.loaidiadiem)
                        && (request.nguondongbo == -1 ? true : request.nguondongbo == 0 ? m.NguonDongBo == null : m.NguonDongBo == request.nguondongbo)
                        && (request.dongboId == -1 || m.DongBoID == request.dongboId)
                        select new { m, xa, huyen, loaihinh, nhacungcap, loainhahang, loaimuasam, loaidiemdulich, loaikhudulich, loaikhuvuichoi, loaithethao, loaicssk, loailuhanh, vanchuyen, loaidiadiem, loaidisan };

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
                    : x.m.LinhVucKinhDoanhId == (int)LinhVucKinhDoanh.VanChuyen ? new LoaiHinhModel() { Id = x.vanchuyen.Id, TenLoai = x.vanchuyen.Ten }
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
                    LoaiDiaDiemAnUong = x.m.LoaiDiaDiemAnUong,
                    LoaiDiaDiem = new TechLife.Model.HueCIT.LoaiDiaDiemAnUong() { Id = x.loaidiadiem.Id, TenLoai = x.loaidiadiem.TenLoai },
                    PhucVu = x.m.PhucVu,
                    ToaDoX = x.m.ToaDoX,
                    ToaDoY = x.m.ToaDoY,
                    MaDoanhNghiep = x.m.MaDoanhNghiep,
                    NguonDongBo = x.m.NguonDongBo,
                    DongBoID = x.m.DongBoID,
                    GiaThamKhaoTu = x.m.GiaThamKhaoTu,
                    GiaThamKhaoDen = x.m.GiaThamKhaoDen
                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<DuLieuDuLichModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<IEnumerable<HoSoDropdownList>> GetsHoSoForFilter(string langId, int linhvucId, FilterRequest request)
        {
            var query = from m in _context.HoSo
                        orderby m.Ten
                        where m.IsDelete == false && m.NgonNguId == langId
                        && (linhvucId == 0 || m.LinhVucKinhDoanhId == linhvucId)
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.Ten.Contains(request.Keyword));

            var data = await query.Select(x => new HoSoDropdownList()
            {
                Id = x.m.Id,
                Ten = x.m.Ten,
            }).ToListAsync();

            return data;
        }

        public async Task<DuLieuDuLichModel> GetHoSo(int id)
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

                            where m.IsDelete == false && m.Id == id
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

        public async Task<HoSo> EditBanDo(int id, HoSoBanDo request)
        {
            try
            {
                var coSoLuuTru = await _context.HoSo.Where(x => x.Id == id).ToListAsync();
                if (coSoLuuTru == null || coSoLuuTru.Count() <= 0)
                {
                    return null;
                }

                var model = coSoLuuTru.FirstOrDefault();
                model.ToaDoX = request.X;
                model.ToaDoY = request.Y;

                _context.HoSo.Update(model);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    HoSo res = _context.HoSo.FirstOrDefault(x => x.Id == id);

                    return res;
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

        public async Task<ApiResult<bool>> UploadImage(int id, ImageUploadExtRequest request)
        {
            try
            {
                if (request.Images != null)
                {
                    foreach (var d in request.Images)
                    {
                        var image = new TechLife.Data.Entities.FileUpload()
                        {
                            FileName = d.Images.FileName,
                            FileUrl = await this.SaveFile(d.Images),
                            IsImage = true,
                            IsStatus = true,
                            Type = LoaiFile.hosodulich.ToString(),
                            Id = id,
                            NgayTao = DateTime.Now,
                            //HueCIT
                            FileType = CheckFileType(d.Images.FileName),
                            MoTa = d.MoTa
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

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

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
    }
}

