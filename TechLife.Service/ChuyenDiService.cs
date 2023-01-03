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
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model.ChuyenDi;
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.Tour;
using TechLife.Service.Common;
namespace TechLife.Service
{
    public interface IChuyenDiService
    {
        Task<PagedResult<ChuyenDiVm>> GetAll(string ma);

        Task<ApiResult<ChuyenDiVm>> Create(ChuyenDiCreateRequest request);

        Task<ApiResult<PagedResult<HanhTrinhTheoNgayVm>>> AddItem(int id, HanhTrinhChuyenDiUpdateRequest request);
        Task<ApiResult<PagedResult<HanhTrinhTheoNgayVm>>> EditItem(List<HanhTrinhChuyenDiUpdateRequest> requests);
        Task<ApiResult<bool>> DeleteItem(int id);
        Task<ApiResult<bool>> DeleteDate(int chuyendiId, int ngay);
        Task<ChuyenDiVm> GetById(int id);
        Task<PagedResult<HanhTrinhTheoNgayVm>> GetListNgayHanhTrinh(int id);

        Task<ApiResult<bool>> Delete(int id);
        Task<PagedResult<HanhTrinhTheoNgayVm>> GetHanhTrinhTheoNgay(int id, int ngay);
        Task<PagedResult<HanhTrinhTheoNgayVm>> GetListHanhTrinh(int id);
    }
    public class ChuyenDiService : IChuyenDiService
    {
        private readonly TLDbContext _context;
        private readonly IFileUploadService _fileUploadService;

        public ChuyenDiService(TLDbContext context
        , IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }

        public async Task<ApiResult<ChuyenDiVm>> Create(ChuyenDiCreateRequest request)
        {
            var data = new ChuyenDi()
            {
                Gia = request.Gia,
                MoTa = request.MoTa,
                SoNgay = request.SoNgay,
                SoNguoi = request.SoNguoi,
                TenChuyenDi = request.TenChuyenDi,
                NgayTao = DateTime.Now,
                MaThietBi = request.MaThietBi,
                UserName = request.UserName,
                TourId = request.TourId
            };
            await _context.ChuyenDi.AddAsync(data);
            var result = await _context.SaveChangesAsync();
            if (request.DSHanhTrinh != null && request.DSHanhTrinh.Count > 0)
            {
                foreach (var hanhtrinh in request.DSHanhTrinh)
                {
                    var resultObj = new HanhTrinhChuyenDi()
                    {
                        DiaDiemId = hanhtrinh.DiaDiemId,
                        Gio = hanhtrinh.Gio,
                        Ngay = hanhtrinh.Ngay,
                        MoTa = hanhtrinh.MoTa,
                        Phut = hanhtrinh.Phut,
                        ChuyenDiId = data.Id
                    };
                    await _context.HanhTrinhChuyenDi.AddAsync(resultObj);
                }
                result = await _context.SaveChangesAsync();
            }

            if (result > 0)
                return new ApiSuccessResult<ChuyenDiVm>(await GetById(data.Id), "Thêm chuyến đi thành công");
            else return new ApiErrorResult<ChuyenDiVm>("Thêm không thành công");
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var data = await _context.ChuyenDi.FindAsync(id);
            if (data == null)
                return new ApiErrorResult<bool>("Không tìm chuyến đi cần xóa");

            data.IsDelete = true;
            _context.ChuyenDi.Update(data);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return new ApiSuccessResult<bool>(true, "Xóa chuyến đi thành công");
            else return new ApiErrorResult<bool>("Xóa không thành công");
        }

        public async Task<PagedResult<ChuyenDiVm>> GetAll(string ma)
        {

            var data = from chuyendi in _context.ChuyenDi
                       where !chuyendi.IsDelete && (chuyendi.MaThietBi == ma || chuyendi.UserName == ma)
                       select new ChuyenDiVm()
                       {
                           TenChuyenDi = chuyendi.TenChuyenDi,
                           Gia = chuyendi.Gia,
                           NgayTao = chuyendi.NgayTao,
                           SoNgay = chuyendi.SoNgay,
                           SoNguoi = chuyendi.SoNguoi,
                           Id = chuyendi.Id
                       };

            return new PagedResult<ChuyenDiVm>()
            {
                Items = await data.ToListAsync(),
                PageIndex = 1,
                PageSize = 1,
                TotalRecords = data.Count()
            };

        }
        public async Task<PagedResult<HanhTrinhTheoNgayVm>> GetListNgayHanhTrinh(int id)
        {
            var query = from m in _context.HanhTrinhChuyenDi
                        where m.ChuyenDiId == id
                        group m by m.Ngay into h
                        select new
                        {
                            h.Key
                        };

            var lstNgay = await query.ToListAsync();

            var data = new List<HanhTrinhTheoNgayVm>();
            int maxDay = 0;
            foreach (var d in lstNgay)
            {
                maxDay = d.Key;
                data.Add(new HanhTrinhTheoNgayVm()
                {
                    Ngay = d.Key,
                    DSDiaDiem = _context.HanhTrinhChuyenDi.Where(v => v.Ngay == d.Key && v.ChuyenDiId == id).OrderBy(n => n.Gio).ThenBy(n => n.Phut).Select(v => new DiaDiemTheoNgayVm()
                    {
                        DiaDiemId = v.DiaDiemId,
                        KhoanCach = "",
                        Gio = v.Gio,
                        Phut = v.Phut,
                        ThoiGian = Functions.ConvertTimeVn(v.Gio, v.Phut),
                        Id = v.Id,
                        DiaDiem = _context.HoSo.Where(x => x.Id == v.DiaDiemId).Select(x => new DuLieuDuLichRpt()
                        {
                            Id = x.Id,
                            DuongPho = x.DuongPho,
                            Email = x.Email,
                            Fax = x.Fax,
                            HangSao = x.HangSao,
                            SoDienThoai = x.SoDienThoai,
                            SoNha = x.SoNha,
                            Ten = x.Ten,
                            Website = x.Website,
                            GioDongCua = x.GioDongCua,
                            GioMoCua = x.GioMoCua,
                            MoTa = x.GhiChu,
                            GioiThieu = x.GioiThieu,
                            LoiKhuyen = "",
                            ViTriTrenBanDo = x.ViTriTrenBanDo,
                            GiaThamKhao = "0",
                            PhuongXa = _context.DiaPhuong.Where(v => v.Id == x.PhuongXaId).Select(v => v.TenDiaPhuong).FirstOrDefault(),
                            QuanHuyen = _context.DiaPhuong.Where(v => v.Id == x.QuanHuyenId).Select(v => v.TenDiaPhuong).FirstOrDefault(),

                            DiaChi = Functions.GetFullDiaPhuong(x.SoNha, x.DuongPho, _context.DiaPhuong.Where(v => v.Id == x.PhuongXaId).Select(v => v.TenDiaPhuong).FirstOrDefault(), _context.DiaPhuong.Where(v => v.Id == x.QuanHuyenId).Select(v => v.TenDiaPhuong).FirstOrDefault(), ""),

                            UrlAvata = _context.FileUploads.Where(n => n.IsImage && n.Id == x.Id && n.IsStatus && n.Type == LoaiFile.hosodulich.ToString()).Select(v => v.FileUrl).FirstOrDefault(),
                            IsDatChuan = x.IsDatChuan,


                        }).FirstOrDefault()
                    }).ToList()
                });
            }

            data.Add(new HanhTrinhTheoNgayVm()
            {
                Ngay = maxDay + 1,
                DSDiaDiem = _context.HanhTrinhChuyenDi.Where(v => v.Ngay == (maxDay + 1) && v.ChuyenDiId == id).OrderBy(n => n.Gio).ThenBy(n => n.Phut).Select(v => new DiaDiemTheoNgayVm()
                {
                    DiaDiemId = v.DiaDiemId,
                    KhoanCach = "",
                    Gio = v.Gio,
                    Phut = v.Phut,
                    ThoiGian = Functions.ConvertTimeVn(v.Gio, v.Phut),
                    Id = v.Id,
                    DiaDiem = _context.HoSo.Where(x => x.Id == v.DiaDiemId).Select(x => new DuLieuDuLichRpt()
                    {
                        Id = x.Id,
                        DuongPho = x.DuongPho,
                        Email = x.Email,
                        Fax = x.Fax,
                        HangSao = x.HangSao,
                        SoDienThoai = x.SoDienThoai,
                        SoNha = x.SoNha,
                        Ten = x.Ten,
                        Website = x.Website,
                        GioDongCua = x.GioDongCua,
                        GioMoCua = x.GioMoCua,
                        MoTa = x.GhiChu,
                        GioiThieu = x.GioiThieu,
                        LoiKhuyen = "",
                        ViTriTrenBanDo = x.ViTriTrenBanDo,
                        GiaThamKhao = "0",
                        PhuongXa = _context.DiaPhuong.Where(v => v.Id == x.PhuongXaId).Select(v => v.TenDiaPhuong).FirstOrDefault(),
                        QuanHuyen = _context.DiaPhuong.Where(v => v.Id == x.QuanHuyenId).Select(v => v.TenDiaPhuong).FirstOrDefault(),

                        DiaChi = Functions.GetFullDiaPhuong(x.SoNha, x.DuongPho, _context.DiaPhuong.Where(v => v.Id == x.PhuongXaId).Select(v => v.TenDiaPhuong).FirstOrDefault(), _context.DiaPhuong.Where(v => v.Id == x.QuanHuyenId).Select(v => v.TenDiaPhuong).FirstOrDefault(), ""),

                        UrlAvata = _context.FileUploads.Where(n => n.IsImage && n.Id == x.Id && n.IsStatus && n.Type == LoaiFile.hosodulich.ToString()).Select(v => v.FileUrl).FirstOrDefault(),
                        IsDatChuan = x.IsDatChuan,


                    }).FirstOrDefault()
                }).ToList()
            });
            var resultObj = new PagedResult<HanhTrinhTheoNgayVm>()
            {
                Items = data,
                PageIndex = 1,
                PageSize = 1,
                TotalRecords = data.Count,

            };

            return resultObj;
        }
        public async Task<PagedResult<HanhTrinhTheoNgayVm>> GetListHanhTrinh(int id)
        {
            var query = from m in _context.HanhTrinhChuyenDi
                        where m.ChuyenDiId == id
                        group m by m.Ngay into h
                        select new
                        {
                            h.Key
                        };

            var lstNgay = await query.OrderBy(v=>v.Key).ToListAsync();

            var data = new List<HanhTrinhTheoNgayVm>();
            int maxDay = 0;
            foreach (var d in lstNgay)
            {
                maxDay = d.Key;
                data.Add(new HanhTrinhTheoNgayVm()
                {
                    Ngay = d.Key,
                    DSDiaDiem = _context.HanhTrinhChuyenDi.Where(v => v.Ngay == d.Key && v.ChuyenDiId == id).OrderBy(n => n.Gio).ThenBy(n => n.Phut).Select(v => new DiaDiemTheoNgayVm()
                    {
                        DiaDiemId = v.DiaDiemId,
                        KhoanCach = "",
                        Gio = v.Gio,
                        Phut = v.Phut,
                        ThoiGian = Functions.ConvertTimeVn(v.Gio, v.Phut),
                        Id = v.Id,
                        DiaDiem = _context.HoSo.Where(x => x.Id == v.DiaDiemId).Select(x => new DuLieuDuLichRpt()
                        {
                            Id = x.Id,
                            DuongPho = x.DuongPho,
                            Email = x.Email,
                            Fax = x.Fax,
                            HangSao = x.HangSao,
                            SoDienThoai = x.SoDienThoai,
                            SoNha = x.SoNha,
                            Ten = x.Ten,
                            Website = x.Website,
                            GioDongCua = x.GioDongCua,
                            GioMoCua = x.GioMoCua,
                            MoTa = x.GhiChu,
                            GioiThieu = x.GioiThieu,
                            LoiKhuyen = "",
                            ViTriTrenBanDo = x.ViTriTrenBanDo,
                            GiaThamKhao = "0",
                            PhuongXa = _context.DiaPhuong.Where(v => v.Id == x.PhuongXaId).Select(v => v.TenDiaPhuong).FirstOrDefault(),
                            QuanHuyen = _context.DiaPhuong.Where(v => v.Id == x.QuanHuyenId).Select(v => v.TenDiaPhuong).FirstOrDefault(),

                            DiaChi = Functions.GetFullDiaPhuong(x.SoNha, x.DuongPho, _context.DiaPhuong.Where(v => v.Id == x.PhuongXaId).Select(v => v.TenDiaPhuong).FirstOrDefault(), _context.DiaPhuong.Where(v => v.Id == x.QuanHuyenId).Select(v => v.TenDiaPhuong).FirstOrDefault(), ""),

                            UrlAvata = _context.FileUploads.Where(n => n.IsImage && n.Id == x.Id && n.IsStatus && n.Type == LoaiFile.hosodulich.ToString()).Select(v => v.FileUrl).FirstOrDefault(),
                            IsDatChuan = x.IsDatChuan,


                        }).FirstOrDefault()
                    }).ToList()
                });
            }

            var resultObj = new PagedResult<HanhTrinhTheoNgayVm>()
            {
                Items = data,
                PageIndex = 1,
                PageSize = 1,
                TotalRecords = data.Count,

            };

            return resultObj;
        }
        public async Task<PagedResult<HanhTrinhTheoNgayVm>> GetHanhTrinhTheoNgay(int id, int ngay)
        {

            var data = new List<HanhTrinhTheoNgayVm>();
            data.Add(new HanhTrinhTheoNgayVm()
            {
                Ngay = ngay,
                DSDiaDiem = await _context.HanhTrinhChuyenDi.Where(v => v.Ngay == ngay && v.ChuyenDiId == id).OrderBy(n => n.Gio).ThenBy(n => n.Phut).Select(v => new DiaDiemTheoNgayVm()
                {
                    DiaDiemId = v.DiaDiemId,
                    KhoanCach = "",
                    Gio = v.Gio,
                    Phut = v.Phut,
                    ThoiGian = Functions.ConvertTimeVn(v.Gio, v.Phut),
                    Id = v.Id,
                    DiaDiem = _context.HoSo.Where(x => x.Id == v.DiaDiemId).Select(x => new DuLieuDuLichRpt()
                    {
                        Id = x.Id,
                        DuongPho = x.DuongPho,
                        Email = x.Email,
                        Fax = x.Fax,
                        HangSao = x.HangSao,
                        SoDienThoai = x.SoDienThoai,
                        SoNha = x.SoNha,
                        Ten = x.Ten,
                        Website = x.Website,
                        GioDongCua = x.GioDongCua,
                        GioMoCua = x.GioMoCua,
                        MoTa = x.GhiChu,
                        GioiThieu = x.GioiThieu,
                        LoiKhuyen = "",
                        ViTriTrenBanDo = x.ViTriTrenBanDo,
                        GiaThamKhao = "0",
                        PhuongXa = _context.DiaPhuong.Where(v => v.Id == x.PhuongXaId).Select(v => v.TenDiaPhuong).FirstOrDefault(),
                        QuanHuyen = _context.DiaPhuong.Where(v => v.Id == x.QuanHuyenId).Select(v => v.TenDiaPhuong).FirstOrDefault(),

                        DiaChi = Functions.GetFullDiaPhuong(x.SoNha, x.DuongPho, _context.DiaPhuong.Where(v => v.Id == x.PhuongXaId).Select(v => v.TenDiaPhuong).FirstOrDefault(), _context.DiaPhuong.Where(v => v.Id == x.QuanHuyenId).Select(v => v.TenDiaPhuong).FirstOrDefault(), ""),

                        UrlAvata = _context.FileUploads.Where(n => n.IsImage && n.Id == x.Id && n.IsStatus && n.Type == LoaiFile.hosodulich.ToString()).Select(v => v.FileUrl).FirstOrDefault(),
                        IsDatChuan = x.IsDatChuan,


                    }).FirstOrDefault()
                }).ToListAsync()
            });
            var resultObj = new PagedResult<HanhTrinhTheoNgayVm>()
            {
                Items = data,
                PageIndex = 1,
                PageSize = 1,
                TotalRecords = data.Count,

            };

            return resultObj;
        }
        public async Task<ChuyenDiVm> GetById(int id)
        {
            var data = await _context.ChuyenDi.FindAsync(id);
            if (data != null)
            {
                var resultObj = new ChuyenDiVm()
                {
                    Gia = data.Gia,
                    Id = data.Id,
                    MoTa = data.MoTa,
                    SoNgay = data.SoNgay,
                    SoNguoi = data.SoNguoi,
                    TenChuyenDi = data.TenChuyenDi,
                    NgayTao = data.NgayTao,
                    Avata = _context.FileUploads.Where(v => v.IsImage && v.Id == data.TourId && v.IsStatus && v.Type == TechLife.Common.Enums.LoaiFile.tour.ToString()).Select(v => v.FileUrl).FirstOrDefault(),
                };
                return resultObj;
            }
            else return null;
        }

        public async Task<ApiResult<PagedResult<HanhTrinhTheoNgayVm>>> AddItem(int id, HanhTrinhChuyenDiUpdateRequest request)
        {
            var obj = new HanhTrinhChuyenDi()
            {
                MoTa = request.MoTa,
                Ngay = request.Ngay,
                Gio = request.Gio,
                Phut = request.Phut,
                ChuyenDiId = id,
                DiaDiemId = request.DiaDiemId
            };
            await _context.HanhTrinhChuyenDi.AddAsync(obj);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiSuccessResult<PagedResult<HanhTrinhTheoNgayVm>>(null, "Đã thêm địa điểm vào chuyến đi thành công");
            }

            else return new ApiErrorResult<PagedResult<HanhTrinhTheoNgayVm>>("Thêm địa điểm không thành công");
        }
        public async Task<ApiResult<PagedResult<HanhTrinhTheoNgayVm>>> EditItem(List<HanhTrinhChuyenDiUpdateRequest> requests)
        {

            int ngay = 0;
            int chuyendi = 0;
            if (requests != null && requests.Count > 0)
            {

                foreach (var request in requests)
                {

                    var obj = await _context.HanhTrinhChuyenDi.FindAsync(request.Id);
                    if (obj != null)
                    {
                        obj.MoTa = request.MoTa;
                        obj.Ngay = request.Ngay;
                        obj.Gio = request.Gio;
                        obj.Phut = request.Phut;
                        obj.DiaDiemId = request.DiaDiemId;
                        ngay = obj.Ngay;
                        chuyendi = obj.ChuyenDiId;
                        _context.HanhTrinhChuyenDi.Update(obj);
                    }

                }
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                var data = new List<HanhTrinhTheoNgayVm>();
                data.Add(new HanhTrinhTheoNgayVm()
                {
                    Ngay = ngay,
                    DSDiaDiem = _context.HanhTrinhChuyenDi.Where(v => v.Ngay == ngay && v.ChuyenDiId == chuyendi).OrderBy(n => n.Gio).ThenBy(n => n.Phut).Select(v => new DiaDiemTheoNgayVm()
                    {
                        DiaDiemId = v.DiaDiemId,
                        KhoanCach = "",
                        Gio = v.Gio,
                        Phut = v.Phut,
                        ThoiGian = Functions.ConvertTimeVn(v.Gio, v.Phut),
                        Id = v.Id,
                        DiaDiem = _context.HoSo.Where(x => x.Id == v.DiaDiemId).Select(x => new DuLieuDuLichRpt()
                        {
                            Id = x.Id,
                            DuongPho = x.DuongPho,
                            Email = x.Email,
                            Fax = x.Fax,
                            HangSao = x.HangSao,
                            SoDienThoai = x.SoDienThoai,
                            SoNha = x.SoNha,
                            Ten = x.Ten,
                            Website = x.Website,
                            GioDongCua = x.GioDongCua,
                            GioMoCua = x.GioMoCua,
                            MoTa = x.GhiChu,
                            GioiThieu = x.GioiThieu,
                            LoiKhuyen = "",
                            ViTriTrenBanDo = x.ViTriTrenBanDo,
                            GiaThamKhao = "0",
                            PhuongXa = _context.DiaPhuong.Where(v => v.Id == x.PhuongXaId).Select(v => v.TenDiaPhuong).FirstOrDefault(),
                            QuanHuyen = _context.DiaPhuong.Where(v => v.Id == x.QuanHuyenId).Select(v => v.TenDiaPhuong).FirstOrDefault(),

                            DiaChi = Functions.GetFullDiaPhuong(x.SoNha, x.DuongPho, _context.DiaPhuong.Where(v => v.Id == x.PhuongXaId).Select(v => v.TenDiaPhuong).FirstOrDefault(), _context.DiaPhuong.Where(v => v.Id == x.QuanHuyenId).Select(v => v.TenDiaPhuong).FirstOrDefault(), ""),

                            UrlAvata = _context.FileUploads.Where(n => n.IsImage && n.Id == x.Id && n.IsStatus && n.Type == LoaiFile.hosodulich.ToString()).Select(v => v.FileUrl).FirstOrDefault(),
                            IsDatChuan = x.IsDatChuan,


                        }).FirstOrDefault()
                    }).ToList()
                });
                var resultObj = new PagedResult<HanhTrinhTheoNgayVm>()
                {
                    Items = data,
                    PageIndex = 1,
                    PageSize = 1,
                    TotalRecords = data.Count,

                };
                return new ApiSuccessResult<PagedResult<HanhTrinhTheoNgayVm>>(resultObj, "Đã sửa địa điểm thành công");
            }

            else return new ApiErrorResult<PagedResult<HanhTrinhTheoNgayVm>>("Sửa địa điểm không thành công");
        }
        public async Task<ApiResult<bool>> DeleteItem(int id)
        {

            var obj = await _context.HanhTrinhChuyenDi.FindAsync(id);

            if (obj == null) return new ApiErrorResult<bool>("Không tìm thấy địa điểm cần xóa");

            _context.HanhTrinhChuyenDi.Remove(obj);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return new ApiSuccessResult<bool>(true, "Đã xóa địa điểm thành công");
            else return new ApiErrorResult<bool>("Xóa địa điểm không thành công");
        }
        public async Task<ApiResult<bool>> DeleteDate(int chuyendiId, int ngay)
        {

            var obj = _context.HanhTrinhChuyenDi.Where(x => x.Ngay == ngay && x.ChuyenDiId == chuyendiId);

            if (obj == null) return new ApiErrorResult<bool>("Không tìm dữ liệu cần xóa");

            _context.HanhTrinhChuyenDi.RemoveRange(obj);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return new ApiSuccessResult<bool>(true, "Đã xóa hành trình trong ngày thành công");
            else return new ApiErrorResult<bool>("Xóa địa hành trình trong ngày không thành công");
        }

    }
}