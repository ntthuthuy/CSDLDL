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
using TechLife.Model.TinTuc;
using TechLife.Service.Common;

namespace TechLife.Service
{
    public interface ITinTucService
    {
        Task<List<TinTucVm>> GetAll(string languageId, int chuyenmucId = -1);

        Task<PagedResult<TinTucVm>> GetRelating(int id, int chuyenmucId = -1);

        Task<PagedResult<TinTucVm>> GetPaging(string languageId, TinTucPagingRequest request);
        Task<PagedResult<TinTucVm>> GetFestivals(string languageId, TinTucPagingRequest request);
        Task<ApiResult<TinTucVm>> Create(TinTucCreateRequest request);

        Task<ApiResult<int>> Update(int id, TinTucUpdateRequest request);

        Task<ApiResult<int>> UpdateStatus(int id);

        Task<TinTucVm> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
        Task<ApiResult<bool>> UpdateView(int id);

        Task<string> SaveFile(IFormFile file);
    }

    public class TinTucService : ITinTucService
    {
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private readonly TLDbContext _context;
        private readonly IStorageService _storageService;

        public TinTucService(TLDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<ApiResult<TinTucVm>> Create(TinTucCreateRequest request)
        {
            var obj = new TinTuc()
            {
                AnhDaiDien = request.AnhDaiDien,
                ChuyenMucId = request.ChuyenMucId,
                IsTinBaiChiaSe = request.IsTinBaiChiaSe,
                HoSoId = request.HoSoId,
                IsTinKhuyenMai = request.IsTinKhuyenMai,
                IsTinTieuDiem = request.IsTinTieuDiem,
                MoTa = request.MoTa,
                MoTaAnh = request.MoTaAnh,
                NgayTao = DateTime.Now,
                NgonNguId = request.NgonNguId,
                NguonNgonNguId = request.NguonNgonNguId,
                NguonTin = request.NguonTin,
                NoiDung = request.NoiDung,
                TacGia = request.TacGia,
                TacQuyen = request.TacQuyen,
                Tag = request.Tag,
                TieuDe = request.TieuDe,
                TuKhoa = request.TuKhoa,
                URL = request.URL,
                NgayDienRa = request.NgayDienRa,
                IsTinLeHoi = request.IsTinLeHoi,
                NgayKetThuc = request.NgayKetThuc

            };
            var data = _context.TinTuc.Add(obj);
            var result = await _context.SaveChangesAsync();
            if (request.listChuyenMucId.Count() > 0)
            {
                int tintucid = obj.Id;
                foreach (var d in request.listChuyenMucId)
                {
                    var inputObj = new TinTucChuyenMuc()
                    {
                        ChuyenMucId = d,
                        TinTucId = tintucid
                    };
                    _context.TinTucChuyenMuc.Add(inputObj);
                }
            }
            result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<TinTucVm>(await GetById(obj.Id), "Thêm bài viết thành công");
            }
            return new ApiErrorResult<TinTucVm>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var obj = await _context.TinTuc.FindAsync(id);

            obj.IsDelete = true;

            _context.TinTuc.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }
        public async Task<ApiResult<bool>> UpdateView(int id)
        {
            var obj = await _context.TinTuc.FindAsync(id);

            obj.LuotXem = obj.LuotXem + 1;

            _context.TinTuc.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi!");
        }

        public async Task<List<TinTucVm>> GetAll(string languageId, int chuyenmucId = -1)
        {
            var query = from m in _context.TinTuc
                        where m.IsDelete == false
                        && m.NgonNguId == languageId
                        && (chuyenmucId == -1 || m.ChuyenMucId == chuyenmucId)
                        select new TinTucVm
                        {
                            AnhDaiDien = m.AnhDaiDien,
                            ChuyenMucId = m.ChuyenMucId,
                            IsTinBaiChiaSe = m.IsTinBaiChiaSe,
                            HoSoId = m.HoSoId,
                            Id = m.Id,
                            IsTinKhuyenMai = m.IsTinKhuyenMai,
                            IsTinTieuDiem = m.IsTinTieuDiem,
                            MoTa = m.MoTa,
                            MoTaAnh = m.MoTaAnh,
                            NgayCongBo = m.NgayCongBo,
                            NgayKetThuc = m.NgayKetThuc,
                            NgayTao = m.NgayTao,
                            NgonNguId = m.NgonNguId,
                            NguoiDangId = m.NguoiDangId,
                            NguoiDuyetId = m.NguoiDuyetId,
                            NguonNgonNguId = m.NguonNgonNguId,
                            NguonTin = m.NguonTin,
                            NoiDung = m.NoiDung,
                            TacGia = m.TacGia,
                            TacQuyen = m.TacQuyen,
                            Tag = m.Tag,
                            TieuDe = m.TieuDe,
                            TrangThai = m.TrangThai,
                            TuKhoa = m.TuKhoa,
                            URL = m.URL
                        };
            return await query.ToListAsync();
        }

        public async Task<PagedResult<ChuyenMucVm>> GetChuyenMucFestivals(string languageId)
        {

            var query = from m in _context.TinTuc
                        join c in _context.ChuyenMuc on m.ChuyenMucId equals c.Id

                        where m.IsDelete == false && m.IsTinLeHoi
                        && m.NgonNguId == languageId

                        select new { m, c };

            var resultList = query.Select(v => new ChuyenMucVm()
            {
                Ten = v.c.Ten,
                Id = v.c.Id
            }).Distinct();
            return new PagedResult<ChuyenMucVm>()
            {
                TotalRecords = query.Count(),
                PageIndex = 1,
                PageSize = 1,
                Items = await resultList.ToListAsync()
            };
        }
        public async Task<PagedResult<TinTucVm>> GetFestivals(string languageId, TinTucPagingRequest request)
        {
            var query = from m in _context.TinTuc
                        where m.IsDelete == false && m.IsTinLeHoi
                        && m.NgonNguId == languageId

                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))

                query = query.Where(x => x.m.TieuDe.Contains(request.Keyword));

            if (!String.IsNullOrEmpty(request.TuNgay))
            {
                query = query.Where(v => v.m.NgayDienRa >= Functions.ConvertDateToSql(request.TuNgay));
            }
            if (!String.IsNullOrEmpty(request.DenNgay))
            {
                query = query.Where(v => v.m.NgayDienRa <= Functions.ConvertDateToSql(request.DenNgay));
            }
            List<TinTucVm> q = new List<TinTucVm>();
            if (!string.IsNullOrEmpty(request.Loai))
            {
                if (request.Loai == "view")
                {
                    q = query.Select(x => new TinTucVm()
                    {
                        AnhDaiDien = x.m.AnhDaiDien,
                        ChuyenMucId = x.m.ChuyenMucId,
                        IsTinBaiChiaSe = x.m.IsTinBaiChiaSe,
                        HoSoId = x.m.HoSoId,
                        Id = x.m.Id,
                        IsTinKhuyenMai = x.m.IsTinKhuyenMai,
                        IsTinTieuDiem = x.m.IsTinTieuDiem,
                        MoTa = x.m.MoTa,
                        MoTaAnh = x.m.MoTaAnh,
                        NgayCongBo = x.m.NgayCongBo,
                        NgayKetThuc = x.m.NgayKetThuc,
                        NgayTao = x.m.NgayTao,
                        NgonNguId = x.m.NgonNguId,
                        NguoiDangId = x.m.NguoiDangId,
                        NguoiDuyetId = x.m.NguoiDuyetId,
                        NguonNgonNguId = x.m.NguonNgonNguId,
                        NguonTin = x.m.NguonTin,
                        NoiDung = x.m.NoiDung,
                        TacGia = x.m.TacGia,
                        TacQuyen = x.m.TacQuyen,
                        Tag = x.m.Tag,
                        TieuDe = x.m.TieuDe,
                        TrangThai = x.m.TrangThai,
                        TuKhoa = x.m.TuKhoa,
                        URL = x.m.URL,
                        IsTinLeHoi = x.m.IsTinLeHoi,
                        IsStatus = x.m.IsStatus,
                        NgayDienRa = x.m.NgayDienRa,
                        LuotXem = x.m.LuotXem,
                        TongDanhGia = _context.DanhGia.Where(a => a.HoSoId == x.m.Id && a.Loai == LoaiBinhLuan.baiviet.ToString()).Select(v => v.SoSao).Count()
                    }).Distinct().OrderByDescending(v => v.LuotXem).ToList();
                }
                else if (request.Loai == "top")
                {
                    query = query.Where(v => v.m.IsTinTieuDiem);
                    q = query.Select(x => new TinTucVm()
                    {
                        AnhDaiDien = x.m.AnhDaiDien,
                        ChuyenMucId = x.m.ChuyenMucId,
                        IsTinBaiChiaSe = x.m.IsTinBaiChiaSe,
                        HoSoId = x.m.HoSoId,
                        Id = x.m.Id,
                        IsTinKhuyenMai = x.m.IsTinKhuyenMai,
                        IsTinTieuDiem = x.m.IsTinTieuDiem,
                        MoTa = x.m.MoTa,
                        MoTaAnh = x.m.MoTaAnh,
                        NgayCongBo = x.m.NgayCongBo,
                        NgayKetThuc = x.m.NgayKetThuc,
                        NgayTao = x.m.NgayTao,
                        NgonNguId = x.m.NgonNguId,
                        NguoiDangId = x.m.NguoiDangId,
                        NguoiDuyetId = x.m.NguoiDuyetId,
                        NguonNgonNguId = x.m.NguonNgonNguId,
                        NguonTin = x.m.NguonTin,
                        NoiDung = x.m.NoiDung,
                        TacGia = x.m.TacGia,
                        TacQuyen = x.m.TacQuyen,
                        Tag = x.m.Tag,
                        TieuDe = x.m.TieuDe,
                        TrangThai = x.m.TrangThai,
                        TuKhoa = x.m.TuKhoa,
                        URL = x.m.URL,
                        IsTinLeHoi = x.m.IsTinLeHoi,
                        IsStatus = x.m.IsStatus,
                        NgayDienRa = x.m.NgayDienRa,
                        LuotXem = x.m.LuotXem,
                        TongDanhGia = _context.DanhGia.Where(a => a.HoSoId == x.m.Id && a.Loai == LoaiBinhLuan.baiviet.ToString()).Select(v => v.SoSao).Count()
                    }).Distinct().OrderByDescending(v => v.NgayDienRa).ToList();
                }
                else if (request.Loai == "feedback")
                {
                    q = query.Select(v => new TinTucVm()
                    {
                        AnhDaiDien = v.m.AnhDaiDien,
                        ChuyenMucId = v.m.ChuyenMucId,
                        IsTinBaiChiaSe = v.m.IsTinBaiChiaSe,
                        HoSoId = v.m.HoSoId,
                        Id = v.m.Id,
                        IsTinKhuyenMai = v.m.IsTinKhuyenMai,
                        IsTinTieuDiem = v.m.IsTinTieuDiem,
                        MoTa = v.m.MoTa,
                        MoTaAnh = v.m.MoTaAnh,
                        NgayCongBo = v.m.NgayCongBo,
                        NgayKetThuc = v.m.NgayKetThuc,
                        NgayTao = v.m.NgayTao,
                        NgonNguId = v.m.NgonNguId,
                        NguoiDangId = v.m.NguoiDangId,
                        NguoiDuyetId = v.m.NguoiDuyetId,
                        NguonNgonNguId = v.m.NguonNgonNguId,
                        NguonTin = v.m.NguonTin,
                        NoiDung = v.m.NoiDung,
                        TacGia = v.m.TacGia,
                        TacQuyen = v.m.TacQuyen,
                        Tag = v.m.Tag,
                        TieuDe = v.m.TieuDe,
                        TrangThai = v.m.TrangThai,
                        TuKhoa = v.m.TuKhoa,
                        URL = v.m.URL,
                        IsTinLeHoi = v.m.IsTinLeHoi,
                        IsStatus = v.m.IsStatus,
                        NgayDienRa = v.m.NgayDienRa,
                        LuotXem = v.m.LuotXem,
                        TongDanhGia = _context.DanhGia.Where(a => a.HoSoId == v.m.Id && a.Loai == LoaiBinhLuan.baiviet.ToString()).Select(v => v.SoSao).Count()
                    }).Distinct().OrderByDescending(v => v.TongDanhGia).ToList();
                }
                else
                {
                    q = query.Select(x => new TinTucVm()
                    {
                        AnhDaiDien = x.m.AnhDaiDien,
                        ChuyenMucId = x.m.ChuyenMucId,
                        IsTinBaiChiaSe = x.m.IsTinBaiChiaSe,
                        HoSoId = x.m.HoSoId,
                        Id = x.m.Id,
                        IsTinKhuyenMai = x.m.IsTinKhuyenMai,
                        IsTinTieuDiem = x.m.IsTinTieuDiem,
                        MoTa = x.m.MoTa,
                        MoTaAnh = x.m.MoTaAnh,
                        NgayCongBo = x.m.NgayCongBo,
                        NgayKetThuc = x.m.NgayKetThuc,
                        NgayTao = x.m.NgayTao,
                        NgonNguId = x.m.NgonNguId,
                        NguoiDangId = x.m.NguoiDangId,
                        NguoiDuyetId = x.m.NguoiDuyetId,
                        NguonNgonNguId = x.m.NguonNgonNguId,
                        NguonTin = x.m.NguonTin,
                        NoiDung = x.m.NoiDung,
                        TacGia = x.m.TacGia,
                        TacQuyen = x.m.TacQuyen,
                        Tag = x.m.Tag,
                        TieuDe = x.m.TieuDe,
                        TrangThai = x.m.TrangThai,
                        TuKhoa = x.m.TuKhoa,
                        URL = x.m.URL,
                        IsTinLeHoi = x.m.IsTinLeHoi,
                        IsStatus = x.m.IsStatus,
                        NgayDienRa = x.m.NgayDienRa,
                        LuotXem = x.m.LuotXem,
                        TongDanhGia = _context.DanhGia.Where(a => a.HoSoId == x.m.Id && a.Loai == LoaiBinhLuan.baiviet.ToString()).Select(v => v.SoSao).Count()
                    }).Distinct().OrderByDescending(v => v.NgayTao).ToList();
                }
            }

            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new TinTucVm()
                {
                    AnhDaiDien = x.m.AnhDaiDien,
                    ChuyenMucId = x.m.ChuyenMucId,
                    IsTinBaiChiaSe = x.m.IsTinBaiChiaSe,
                    HoSoId = x.m.HoSoId,
                    Id = x.m.Id,
                    IsTinKhuyenMai = x.m.IsTinKhuyenMai,
                    IsTinTieuDiem = x.m.IsTinTieuDiem,
                    MoTa = x.m.MoTa,
                    MoTaAnh = x.m.MoTaAnh,
                    NgayCongBo = x.m.NgayCongBo,
                    NgayKetThuc = x.m.NgayKetThuc,
                    NgayTao = x.m.NgayTao,
                    NgonNguId = x.m.NgonNguId,
                    NguoiDangId = x.m.NguoiDangId,
                    NguoiDuyetId = x.m.NguoiDuyetId,
                    NguonNgonNguId = x.m.NguonNgonNguId,
                    NguonTin = x.m.NguonTin,
                    NoiDung = x.m.NoiDung,
                    TacGia = x.m.TacGia,
                    TacQuyen = x.m.TacQuyen,
                    Tag = x.m.Tag,
                    TieuDe = x.m.TieuDe,
                    TrangThai = x.m.TrangThai,
                    TuKhoa = x.m.TuKhoa,
                    URL = x.m.URL,
                    IsTinLeHoi = x.m.IsTinLeHoi,
                    NgayDienRa = x.m.NgayDienRa

                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<TinTucVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<TinTucVm> GetById(int id)
        {
            var obj = await _context.TinTuc.FindAsync(id);

            return new TinTucVm()
            {
                AnhDaiDien = obj.AnhDaiDien,
                ChuyenMucId = obj.ChuyenMucId,
                IsTinBaiChiaSe = obj.IsTinBaiChiaSe,
                HoSoId = obj.HoSoId,
                Id = obj.Id,
                IsTinKhuyenMai = obj.IsTinKhuyenMai,
                IsTinTieuDiem = obj.IsTinTieuDiem,
                MoTa = obj.MoTa,
                MoTaAnh = obj.MoTaAnh,
                NgayCongBo = obj.NgayCongBo,
                NgayKetThuc = obj.NgayKetThuc,
                NgayTao = obj.NgayTao,
                NgonNguId = obj.NgonNguId,
                NguoiDangId = obj.NguoiDangId,
                NguoiDuyetId = obj.NguoiDuyetId,
                NguonNgonNguId = obj.NguonNgonNguId,
                NguonTin = obj.NguonTin,
                NoiDung = obj.NoiDung,
                TacGia = obj.TacGia,
                TacQuyen = obj.TacQuyen,
                Tag = obj.Tag,
                TieuDe = obj.TieuDe,
                TrangThai = obj.TrangThai,
                TuKhoa = obj.TuKhoa,
                URL = obj.URL,
                NgayDienRa = obj.NgayDienRa,
                IsStatus = obj.IsStatus,
                IsTinLeHoi = obj.IsTinLeHoi,
                ThuTuHienThi = obj.ThuTu,
                DSChuyenMucKhac = _context.TinTucChuyenMuc.Where(v => v.TinTucId == obj.Id).Select(x => new ChuyenMucVm
                {
                    Id = x.ChuyenMucId
                }).ToList()
            };
        }

        public async Task<PagedResult<TinTucVm>> GetPaging(string languageId, TinTucPagingRequest request)
        {
            var query = from m in _context.TinTuc
                        orderby m.NgayTao descending
                        where m.IsDelete == false

                        && m.NgonNguId == languageId
                        && (request.ChuyenMucId == -1 || m.ChuyenMucId == request.ChuyenMucId)
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.TieuDe.Contains(request.Keyword) || x.m.TuKhoa.Contains(request.Keyword));

            List<TinTucVm> q = new List<TinTucVm>();
            if (!string.IsNullOrEmpty(request.Loai))
            {
                if (request.Loai == "view")
                {
                    q = query.Select(x => new TinTucVm()
                    {
                        AnhDaiDien = x.m.AnhDaiDien,
                        ChuyenMucId = x.m.ChuyenMucId,
                        IsTinBaiChiaSe = x.m.IsTinBaiChiaSe,
                        HoSoId = x.m.HoSoId,
                        Id = x.m.Id,
                        IsTinKhuyenMai = x.m.IsTinKhuyenMai,
                        IsTinTieuDiem = x.m.IsTinTieuDiem,
                        MoTa = x.m.MoTa,
                        MoTaAnh = x.m.MoTaAnh,
                        NgayCongBo = x.m.NgayCongBo,
                        NgayKetThuc = x.m.NgayKetThuc,
                        NgayTao = x.m.NgayTao,
                        NgonNguId = x.m.NgonNguId,
                        NguoiDangId = x.m.NguoiDangId,
                        NguoiDuyetId = x.m.NguoiDuyetId,
                        NguonNgonNguId = x.m.NguonNgonNguId,
                        NguonTin = x.m.NguonTin,
                        NoiDung = x.m.NoiDung,
                        TacGia = x.m.TacGia,
                        TacQuyen = x.m.TacQuyen,
                        Tag = x.m.Tag,
                        TieuDe = x.m.TieuDe,
                        TrangThai = x.m.TrangThai,
                        TuKhoa = x.m.TuKhoa,
                        URL = x.m.URL,
                        IsTinLeHoi = x.m.IsTinLeHoi,
                        IsStatus = x.m.IsStatus,
                        NgayDienRa = x.m.NgayDienRa,
                        LuotXem = x.m.LuotXem,
                        TongDanhGia = _context.DanhGia.Where(a => a.HoSoId == x.m.Id && a.Loai == LoaiBinhLuan.baiviet.ToString()).Select(v => v.SoSao).Count()
                    }).Distinct().OrderByDescending(v => v.LuotXem).ToList();
                }
                else if (request.Loai == "top")
                {
                    query = query.Where(v => v.m.IsTinTieuDiem);
                    q = query.Select(x => new TinTucVm()
                    {
                        AnhDaiDien = x.m.AnhDaiDien,
                        ChuyenMucId = x.m.ChuyenMucId,
                        IsTinBaiChiaSe = x.m.IsTinBaiChiaSe,
                        HoSoId = x.m.HoSoId,
                        Id = x.m.Id,
                        IsTinKhuyenMai = x.m.IsTinKhuyenMai,
                        IsTinTieuDiem = x.m.IsTinTieuDiem,
                        MoTa = x.m.MoTa,
                        MoTaAnh = x.m.MoTaAnh,
                        NgayCongBo = x.m.NgayCongBo,
                        NgayKetThuc = x.m.NgayKetThuc,
                        NgayTao = x.m.NgayTao,
                        NgonNguId = x.m.NgonNguId,
                        NguoiDangId = x.m.NguoiDangId,
                        NguoiDuyetId = x.m.NguoiDuyetId,
                        NguonNgonNguId = x.m.NguonNgonNguId,
                        NguonTin = x.m.NguonTin,
                        NoiDung = x.m.NoiDung,
                        TacGia = x.m.TacGia,
                        TacQuyen = x.m.TacQuyen,
                        Tag = x.m.Tag,
                        TieuDe = x.m.TieuDe,
                        TrangThai = x.m.TrangThai,
                        TuKhoa = x.m.TuKhoa,
                        URL = x.m.URL,
                        IsTinLeHoi = x.m.IsTinLeHoi,
                        IsStatus = x.m.IsStatus,
                        NgayDienRa = x.m.NgayDienRa,
                        LuotXem = x.m.LuotXem,
                        TongDanhGia = _context.DanhGia.Where(a => a.HoSoId == x.m.Id && a.Loai == LoaiBinhLuan.baiviet.ToString()).Select(v => v.SoSao).Count()
                    }).Distinct().ToList();
                }
                else if (request.Loai == "feedback")
                {
                    q = query.Select(v => new TinTucVm()
                    {
                        AnhDaiDien = v.m.AnhDaiDien,
                        ChuyenMucId = v.m.ChuyenMucId,
                        IsTinBaiChiaSe = v.m.IsTinBaiChiaSe,
                        HoSoId = v.m.HoSoId,
                        Id = v.m.Id,
                        IsTinKhuyenMai = v.m.IsTinKhuyenMai,
                        IsTinTieuDiem = v.m.IsTinTieuDiem,
                        MoTa = v.m.MoTa,
                        MoTaAnh = v.m.MoTaAnh,
                        NgayCongBo = v.m.NgayCongBo,
                        NgayKetThuc = v.m.NgayKetThuc,
                        NgayTao = v.m.NgayTao,
                        NgonNguId = v.m.NgonNguId,
                        NguoiDangId = v.m.NguoiDangId,
                        NguoiDuyetId = v.m.NguoiDuyetId,
                        NguonNgonNguId = v.m.NguonNgonNguId,
                        NguonTin = v.m.NguonTin,
                        NoiDung = v.m.NoiDung,
                        TacGia = v.m.TacGia,
                        TacQuyen = v.m.TacQuyen,
                        Tag = v.m.Tag,
                        TieuDe = v.m.TieuDe,
                        TrangThai = v.m.TrangThai,
                        TuKhoa = v.m.TuKhoa,
                        URL = v.m.URL,
                        IsTinLeHoi = v.m.IsTinLeHoi,
                        IsStatus = v.m.IsStatus,
                        NgayDienRa = v.m.NgayDienRa,
                        LuotXem = v.m.LuotXem,
                        TongDanhGia = _context.DanhGia.Where(a => a.HoSoId == v.m.Id && a.Loai == LoaiBinhLuan.baiviet.ToString()).Select(v => v.SoSao).Count()
                    }).Distinct().OrderByDescending(v => v.TongDanhGia).ToList();
                }
                else
                {
                    q = query.Select(x => new TinTucVm()
                    {
                        AnhDaiDien = x.m.AnhDaiDien,
                        ChuyenMucId = x.m.ChuyenMucId,
                        IsTinBaiChiaSe = x.m.IsTinBaiChiaSe,
                        HoSoId = x.m.HoSoId,
                        Id = x.m.Id,
                        IsTinKhuyenMai = x.m.IsTinKhuyenMai,
                        IsTinTieuDiem = x.m.IsTinTieuDiem,
                        MoTa = x.m.MoTa,
                        MoTaAnh = x.m.MoTaAnh,
                        NgayCongBo = x.m.NgayCongBo,
                        NgayKetThuc = x.m.NgayKetThuc,
                        NgayTao = x.m.NgayTao,
                        NgonNguId = x.m.NgonNguId,
                        NguoiDangId = x.m.NguoiDangId,
                        NguoiDuyetId = x.m.NguoiDuyetId,
                        NguonNgonNguId = x.m.NguonNgonNguId,
                        NguonTin = x.m.NguonTin,
                        NoiDung = x.m.NoiDung,
                        TacGia = x.m.TacGia,
                        TacQuyen = x.m.TacQuyen,
                        Tag = x.m.Tag,
                        TieuDe = x.m.TieuDe,
                        TrangThai = x.m.TrangThai,
                        TuKhoa = x.m.TuKhoa,
                        URL = x.m.URL,
                        IsTinLeHoi = x.m.IsTinLeHoi,
                        IsStatus = x.m.IsStatus,
                        NgayDienRa = x.m.NgayDienRa,
                        LuotXem = x.m.LuotXem,
                        TongDanhGia = _context.DanhGia.Where(a => a.HoSoId == x.m.Id && a.Loai == LoaiBinhLuan.baiviet.ToString()).Select(v => v.SoSao).Count()
                    }).Distinct().OrderByDescending(v => v.NgayTao).ToList();
                }
            }


            //3. Paging
            int totalRow = q.Count();


            var data = q.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList();

            //4. Select and projection
            return new PagedResult<TinTucVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<int>> Update(int id, TinTucUpdateRequest request)
        {
            var data = await _context.TinTuc.FindAsync(id);

            data.AnhDaiDien = !String.IsNullOrEmpty(request.AnhDaiDien) ? request.AnhDaiDien : data.AnhDaiDien;
            data.ChuyenMucId = request.ChuyenMucId;
            data.IsTinBaiChiaSe = request.IsTinBaiChiaSe;
            data.HoSoId = request.HoSoId;
            data.IsTinKhuyenMai = request.IsTinKhuyenMai;
            data.IsTinTieuDiem = request.IsTinTieuDiem;
            data.MoTa = request.MoTa;
            data.MoTaAnh = request.MoTaAnh;
            data.NgayTao = DateTime.Now;
            data.NgonNguId = request.NgonNguId;
            data.NguonNgonNguId = request.NguonNgonNguId;
            data.NguonTin = request.NguonTin;
            data.NoiDung = request.NoiDung;
            data.TacGia = request.TacGia;
            data.TacQuyen = request.TacQuyen;
            data.Tag = request.Tag;
            data.TieuDe = request.TieuDe;
            data.TuKhoa = request.TuKhoa;
            data.URL = request.URL;
            data.NgayDienRa = request.NgayDienRa;
            data.IsTinLeHoi = request.IsTinLeHoi;
            data.NgayKetThuc = request.NgayKetThuc;

            _context.TinTuc.Update(data);
            if (request.listChuyenMucId != null && request.listChuyenMucId.Count() > 0)
            {
                var list = _context.TinTucChuyenMuc.Where(v => v.TinTucId == id);
                _context.RemoveRange(list);

                foreach (var d in request.listChuyenMucId)
                {
                    var inputObj = new TinTucChuyenMuc()
                    {
                        ChuyenMucId = d,
                        TinTucId = id
                    };
                    _context.TinTucChuyenMuc.Add(inputObj);
                }
            }
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id, "Cập nhật bài viết thành công!");
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<ApiResult<int>> UpdateStatus(int id)
        {
            var obj = await _context.ChuyenMuc.FindAsync(id);
            if (obj.IsHienThiMenu)
            {
                obj.IsHienThiMenu = false;
            }
            else
            {
                obj.IsHienThiMenu = true;
            }
            _context.ChuyenMuc.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id, "Cập nhật thành công!");
            }
            return new ApiErrorResult<int>("Cập nhật lỗi!");
        }

        public async Task<PagedResult<TinTucVm>> GetRelating(int id, int chuyenmucId = -1)
        {
            var query = from m in _context.TinTuc
                        where m.IsDelete == false && m.Id != id
                        && (chuyenmucId == -1 || m.ChuyenMucId == chuyenmucId)
                        select new TinTucVm
                        {
                            AnhDaiDien = m.AnhDaiDien,
                            ChuyenMucId = m.ChuyenMucId,
                            IsTinBaiChiaSe = m.IsTinBaiChiaSe,
                            HoSoId = m.HoSoId,
                            Id = m.Id,
                            IsTinKhuyenMai = m.IsTinKhuyenMai,
                            IsTinTieuDiem = m.IsTinTieuDiem,
                            MoTa = m.MoTa,
                            MoTaAnh = m.MoTaAnh,
                            NgayCongBo = m.NgayCongBo,
                            NgayDienRa = m.NgayDienRa,
                            NgayKetThuc = m.NgayKetThuc,
                            NgayTao = m.NgayTao,
                            NgonNguId = m.NgonNguId,
                            NguoiDangId = m.NguoiDangId,
                            NguoiDuyetId = m.NguoiDuyetId,
                            NguonNgonNguId = m.NguonNgonNguId,
                            NguonTin = m.NguonTin,
                            NoiDung = m.NoiDung,
                            TacGia = m.TacGia,
                            TacQuyen = m.TacQuyen,
                            Tag = m.Tag,
                            TieuDe = m.TieuDe,
                            TrangThai = m.TrangThai,
                            TuKhoa = m.TuKhoa,
                            URL = m.URL
                        };

            return new PagedResult<TinTucVm>()
            {
                TotalRecords = query.Count(),
                PageIndex = 1,
                PageSize = query.Count(),
                Items = await query.ToListAsync(),
            };

        }
    }
}