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
using TechLife.Model;
using TechLife.Service.Common;
using TechLife.Model.Tour;
using TechLife.Model.DuLieuDuLich;

namespace TechLife.Service
{
    public interface ITourService
    {
        Task<List<TourModel>> GetAll(int id);

        Task<PagedResult<TourVm>> GetPaging(TourRequets request);

        Task<TourVm> GetById(int id);

        Task<ApiResult<TourModel>> Create(TourModel request);

        Task<ApiResult<HanhTrinhModel>> AddItem(HanhTrinhModel request);

        Task<HanhTrinhModel> GetItemById(int id);

        Task<ApiResult<int>> Update(int id, TourVm request);

        Task<ApiResult<int>> UpdateItem(int id, HanhTrinhModel request);

        Task<ApiResult<int>> Delete(int id);

        Task<ApiResult<bool>> UploadImage(int id, ImageUploadRequest request);
        Task<PagedResult<HanhTrinhTheoNgayVm>> GetListNgayHanhTrinhByTour(int id);
    }

    public class TourService : ITourService
    {
        private readonly TLDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IFileUploadService _fileUploadService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public TourService(TLDbContext context
            , IFileUploadService fileUploadService
            , IStorageService storageService)

        {
            _context = context;
            _fileUploadService = fileUploadService;
            _storageService = storageService;
        }

        public async Task<ApiResult<HanhTrinhModel>> AddItem(HanhTrinhModel request)
        {
            var hanhtrinh = new HanhTrinh()
            {
                Gio = request.Gio,
                Id = request.Id,
                IsStatus = true,
                TourId = request.TourId,
                Mota = request.Mota,
                Ngay = request.Ngay,
                NoiDenId = request.NoiDenId,
                Phut = request.Phut,
                ThoiGian = request.ThoiGian
            };
            _context.HanhTrinh.Add(hanhtrinh);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = hanhtrinh.Id;
                return new ApiSuccessResult<HanhTrinhModel>(request);
            }
            return new ApiErrorResult<HanhTrinhModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<TourModel>> Create(TourModel request)
        {
            var tour = new Tour()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTaChuyenDi = request.MoTaChuyenDi,
                TenChuyenDi = request.TenChuyenDi,
                CongTyLuHanhId = request.CongTyLuHanhId,
                Gia = request.Gia,
                LoaiId = request.LoaiId,
                SoNgay = request.SoNgay,
                MaTour = request.MaTour,
                LichTrinh = request.LichTrinh,
                KhoiHanhTu = request.KhoiHanhTu,
                IsHangNgay = request.IsHangNgay,
                HinhThucId = request.HinhThucId
            };
            _context.Tours.Add(tour);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = tour.Id;
                return new ApiSuccessResult<TourModel>(request);
            }
            return new ApiErrorResult<TourModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null)
            {
                return new ApiErrorResult<int>("Không tìm thấy dữ liệu!");
            }

            tour.IsDelete = true;

            _context.Tours.Update(tour);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id, "Xóa thành công!");
            }
            return new ApiErrorResult<int>("Xóa không thành công!");
        }

        public async Task<List<TourModel>> GetAll(int id)
        {
            var query = from m in _context.Tours
                        where m.IsDelete == false && m.CongTyLuHanhId == id
                        select new TourModel
                        {
                            Id = m.Id,
                            CongTyLuHanhId = m.CongTyLuHanhId,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            Gia = m.Gia,
                            LoaiId = m.LoaiId,
                            TenChuyenDi = m.TenChuyenDi,
                            MoTaChuyenDi = m.MoTaChuyenDi,
                            SoNgay = m.SoNgay,
                            HinhThucId = m.HinhThucId,
                            IsHangNgay = m.IsHangNgay,
                            KhoiHanhTu = m.KhoiHanhTu,
                            LichTrinh = m.LichTrinh,
                            MaTour = m.MaTour,
                            DSHanhTrinh = m.DSHanhTrinh.Select(v => new HanhTrinhModel()
                            {
                                Gio = v.Gio,
                                Id = v.Id,
                                IsStatus = v.IsStatus,
                                Mota = v.Mota,
                                Ngay = v.Ngay,
                                NoiDenId = v.NoiDenId,
                                Phut = v.Phut,
                                ThoiGian = v.ThoiGian,
                                TourId = v.TourId,
                                DiaDiem = v.HoSo.Ten
                            }).ToList(),
                            DSHinhAnh = _fileUploadService.GetImageByHoSoId(m.Id, LoaiFile.tour.ToString()).Result
                        };
            return await query.ToListAsync();
        }

        public async Task<TourVm> GetById(int id)
        {
            var obj = await _context.Tours.FindAsync(id);
            if (obj != null)
            {
                var model = new TourVm();
                model.Id = obj.Id;
                model.CongTyLuHanhId = obj.CongTyLuHanhId;
                model.TenCongTy = _context.HoSo.Where(v => v.Id == obj.CongTyLuHanhId).Count() > 0 ? _context.HoSo.Where(v => v.Id == obj.CongTyLuHanhId).FirstOrDefault().Ten : "";

                model.Gia = obj.Gia.ToString();
                model.LoaiId = obj.LoaiId;
                model.TenChuyenDi = obj.TenChuyenDi;
                model.MoTaChuyenDi = obj.MoTaChuyenDi;
                model.SoNgay = obj.SoNgay;
                model.HinhThucId = obj.HinhThucId;
                model.IsHangNgay = obj.IsHangNgay;
                model.KhoiHanhTu = obj.KhoiHanhTu;
                model.LichTrinh = obj.LichTrinh;
                model.MaTour = obj.MaTour;
                model.DSHinhAnh = await _fileUploadService.GetImageByHoSoId(obj.Id, LoaiFile.tour.ToString());
                model.DSHanhTrinh = await GetListHanhTrinhByTour(obj.Id);

                return model;
            }
            return null;
        }

        private async Task<List<HanhTrinhModel>> GetListHanhTrinhByTour(int id)
        {
            var query = from m in _context.HanhTrinh
                        where m.TourId == id
                        select new HanhTrinhModel
                        {
                            Gio = m.Gio,
                            Id = m.Id,
                            IsStatus = m.IsStatus,
                            Mota = m.Mota,
                            Ngay = m.Ngay,
                            NoiDenId = m.NoiDenId,
                            Phut = m.Phut,
                            ThoiGian = m.ThoiGian,
                            TourId = m.TourId,
                            DiaDiem = m.HoSo.Ten,
                            DSHinhAnh = _context.FileUploads.Where(v => v.Id == m.Id && v.Type == LoaiFile.hanhtrinhtour.ToString() && v.IsImage).Select(v => new FileUploadModel()
                            {
                                FileName = v.FileName,
                                FileUrl = v.FileUrl,
                                FileId = v.FileId
                            }).ToList()
                        };

            return await query.ToListAsync();
        }
        public async Task<PagedResult<HanhTrinhTheoNgayVm>> GetListNgayHanhTrinhByTour(int id)
        {
            var query = from m in _context.HanhTrinh
                        where m.TourId == id
                        group m by m.Ngay into h
                        select new
                        {
                            h.Key
                        };

            var lstNgay = await query.ToListAsync();

            var data = new List<HanhTrinhTheoNgayVm>();
            foreach (var d in lstNgay)
            {
                data.Add(new HanhTrinhTheoNgayVm()
                {
                    Ngay = d.Key,
                    DSDiaDiem = _context.HanhTrinh.Where(v => v.Ngay == d.Key && v.TourId == id).Select(v => new DiaDiemTheoNgayVm()
                    {
                        DiaDiemId = v.NoiDenId,
                        KhoanCach = "",
                        ThoiGian = v.Gio.ToString(),
                        DiaDiem = _context.HoSo.Where(x => x.Id == v.NoiDenId).Select(x => new DuLieuDuLichRpt()
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
                            DiaChi = Functions.GetFullDiaPhuong(x.SoNha, x.DuongPho, _context.DiaPhuong.Where(v => v.Id == x.PhuongXaId).Select(v => v.TenDiaPhuong).FirstOrDefault(), _context.DiaPhuong.Where(v => v.Id == x.QuanHuyenId).Select(v => v.TenDiaPhuong).FirstOrDefault(), "Thừa Thiên Huế"),
                            Images = _fileUploadService.GetImageHoSo(x.Id).Result,
                            Avata = _context.FileUploads.Where(v => v.IsImage && v.Id == x.Id && v.IsStatus).Select(v =>
                                   new ImageVm
                                   {
                                       Id = v.FileId,
                                       Name = v.FileName,
                                       Url = v.FileUrl
                                   }).FirstOrDefault(),
                            IsDatChuan = x.IsDatChuan

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
        public async Task<HanhTrinhModel> GetItemById(int id)
        {
            var query = from m in _context.HanhTrinh
                        where m.Id == id
                        select new HanhTrinhModel
                        {
                            Gio = m.Gio,
                            Id = m.Id,
                            IsStatus = m.IsStatus,
                            Mota = m.Mota,
                            Ngay = m.Ngay,
                            NoiDenId = m.NoiDenId,
                            Phut = m.Phut,
                            ThoiGian = m.ThoiGian,
                            TourId = m.TourId,
                            Tour = new TourVm()
                            {
                                CongTyLuHanhId = m.Tour.CongTyLuHanhId,
                                Gia = m.Tour.Gia != null ? Functions.ConvertDecimalToVnd(m.Tour.Gia) : "0 vnđ",
                                DSHanhTrinh = m.Tour.DSHanhTrinh.Select(v => new HanhTrinhModel()
                                {
                                    Gio = v.Gio,
                                    Id = v.Id,
                                    IsStatus = v.IsStatus,
                                    Mota = v.Mota,
                                    Ngay = v.Ngay,
                                    NoiDenId = v.NoiDenId,
                                    Phut = v.Phut,
                                    ThoiGian = v.ThoiGian,
                                    TourId = v.TourId,
                                    DiaDiem = _context.HoSo.Where(x => x.Id == v.NoiDenId).Select(x => x.Ten).FirstOrDefault()

                                }).ToList(),
                                TenChuyenDi = m.Tour.TenChuyenDi,
                                Id = m.Tour.Id,
                                MoTaChuyenDi = m.Tour.MoTaChuyenDi,
                                SoNgay = m.Tour.SoNgay,

                            },
                            DiaDiem = m.HoSo.Ten,
                            DSHinhAnh = _context.FileUploads.Where(v => v.IsImage && v.Type == LoaiFile.hanhtrinhtour.ToString() && v.Id == m.Id).Select(v => new FileUploadModel()
                            {
                                FileUrl = v.FileUrl,
                                FileName = v.FileName,
                                FileId = v.Id
                            }).ToList()
                        };

            if (query == null || query.Count() <= 0)
            {
                throw new TLException($"Không tìm thấy dữ liệu với id: {id}");
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<PagedResult<TourVm>> GetPaging(TourRequets request)
        {
            try
            {

                var query = from m in _context.Tours
                            where m.IsDelete == false
                            && (request.luhanh == -1 || m.CongTyLuHanhId == request.luhanh)
                            orderby m.Id descending
                            select new { m };
               

                if (!string.IsNullOrEmpty(request.gia) && Functions.IsNumeric(request.gia))
                {
                    query = query.Where(x => x.m.Gia == Convert.ToDecimal(request.gia));
                }
                if (!string.IsNullOrEmpty(request.tungay) && !string.IsNullOrEmpty(request.denngay))
                {
                    query = query.Where(x => x.m.SoNgay == Functions.TongSoNgay(request.tungay, request.denngay));
                }
                if (!string.IsNullOrEmpty(request.loaihinh))
                {
                    int[] loaihinh = !String.IsNullOrEmpty(request.loaihinh) ? Array.ConvertAll(request.loaihinh.Split(','), int.Parse) : null;

                    query = query.Where(x => loaihinh.Contains(x.m.LoaiId));
                }
                if (!string.IsNullOrEmpty(request.Keyword))
                    query = query.Where(x => x.m.TenChuyenDi.Contains(request.Keyword));
                //3. Paging
                int totalRow = query.Count();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new TourVm()
                    {

                        MoTaChuyenDi = x.m.MoTaChuyenDi,
                        TenChuyenDi = x.m.TenChuyenDi,
                        SoNgay = x.m.SoNgay,
                        LoaiId = x.m.LoaiId,
                        CongTyLuHanhId = x.m.CongTyLuHanhId,
                        Gia = x.m.Gia != null ? Functions.ConvertDecimalToVnd(x.m.Gia) : "0 vnđ",
                        Id = x.m.Id,
                        HinhThucId = x.m.HinhThucId,
                        IsHangNgay = x.m.IsHangNgay,
                        KhoiHanhTu = x.m.KhoiHanhTu,
                        LichTrinh = x.m.LichTrinh,
                        MaTour = x.m.MaTour,
                        DSHanhTrinh = _context.HanhTrinh.Where(v => v.TourId == x.m.Id).Select(v => new HanhTrinhModel()
                        {
                            NoiDenId = v.NoiDenId,
                            Gio = v.Gio,
                            Mota = v.Mota,
                            Ngay = v.Ngay,
                            Phut = v.Phut,
                            ThoiGian = v.ThoiGian,
                        }).ToList(),
                        DSHinhAnh = _fileUploadService.GetImageByHoSoId(x.m.Id, LoaiFile.tour.ToString()).Result
                    }).ToListAsync();

                //4. Select and projection
                return new PagedResult<TourVm>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ApiResult<int>> Update(int id, TourVm request)
        {
            var query = from m in _context.Tours
                        where m.Id == id
                        select m;
            if (query == null || query.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy dữ liệu!");
            }

            var model = await query.FirstOrDefaultAsync();

            model.MoTaChuyenDi = request.MoTaChuyenDi;
            model.TenChuyenDi = request.TenChuyenDi;
            model.SoNgay = request.SoNgay;
            model.LoaiId = request.LoaiId;
            model.CongTyLuHanhId = request.CongTyLuHanhId;
            model.MaTour = request.MaTour;
            model.LichTrinh = request.LichTrinh;
            model.KhoiHanhTu = request.KhoiHanhTu;
            model.IsHangNgay = request.IsHangNgay;
            model.HinhThucId = request.HinhThucId;
            model.Gia = Convert.ToDecimal(request.Gia);
            _context.Tours.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }

        public async Task<ApiResult<int>> UpdateItem(int id, HanhTrinhModel request)
        {
            var query = from m in _context.HanhTrinh
                        where m.Id == id
                        select m;
            if (query == null || query.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy dữ liệu!");
            }

            var model = await query.FirstOrDefaultAsync();

            model.Mota = request.Mota;
            model.NoiDenId = request.NoiDenId;
            model.Phut = request.Phut;
            model.Ngay = request.Ngay;
            model.ThoiGian = request.ThoiGian;
            model.Gio = request.Gio;

            _context.HanhTrinh.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
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
                            Type = LoaiFile.tour.ToString(),
                            Id = id
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

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }
}