using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model.HoatDongKinhDoanh;

namespace TechLife.Service
{
    public interface IHoatDongKinhDoanhService
    {
        Task<PagedResult<HoatDongKinhDoanhVm>> GetPaging(HoatDongKinhDoanhFormRequest request);
        Task<Result<bool>> Create(HoatDongKinhDoanhCreateRequest request);
        Task<Result<bool>> Update(HoatDongKinhDoanhUpdateRequest request);
        Task<Result<bool>> Delete(HoatDongKinhDoanhDeleteRequest request);
        Task<List<HoatDongKinhDoanhVm>> GetAll();
        Task<HoatDongKinhDoanhVm> GetById(int id);
        Task<Result<bool>> Import(List<ImporFileVm> items, int thang, int nam);
        Task<int> MinYear();
    }

    public class HoatDongKinhDoanhService : IHoatDongKinhDoanhService
    {
        private readonly TLDbContext _context;
        private readonly ILogger<HoatDongKinhDoanhService> _logger;
        private readonly IDanhMucDuLieuThongKeService _danhMucService;

        public HoatDongKinhDoanhService(TLDbContext context
            , ILogger<HoatDongKinhDoanhService> logger
            , IDanhMucDuLieuThongKeService danhMucService)
        {
            _context = context;
            _logger = logger;
            _danhMucService = danhMucService;
        }

        public async Task<Result<bool>> Create(HoatDongKinhDoanhCreateRequest request)
        {
            int? parentId = !string.IsNullOrWhiteSpace(request.ParentId) ? Convert.ToInt32(HashUtil.DecodeID(request.ParentId)) : null;

            var data = new HoatDongKinhDoanh
            {
                Code = request.Code?.Trim(),
                Name = request.Name.Trim(),
                DVT = request.DVT?.Trim(),
                ChinhThucThangTruoc = decimal.Parse(request.ChinhThucThangTruoc),
                UocThangHienTai = decimal.Parse(request.UocThangHienTai),
                DuTinhUocThangSau = decimal.Parse(request.DuTinhUocThangSau),
                LuyKeTuDauNam = decimal.Parse(request.LuyKeTuDauNam),
                ParentId = parentId
            };

            await _context.HoatDongKinhDoanh.AddAsync(data);

            await _context.SaveChangesAsync();

            return new Result<bool>() { IsSuccessed = true, Message = "Thêm thành công" };
        }

        public async Task<Result<bool>> Delete(HoatDongKinhDoanhDeleteRequest request)
        {
            var data = await _context.HoatDongKinhDoanh.Where(x => !x.IsDelete && x.Thang == request.Thang && x.Nam == request.Nam).ToListAsync();

            if (data.Count == 0) return new Result<bool>() { IsSuccessed = false, Message = "Dữ liệu không tồn tại" };

            foreach (var item in data)
            {
                item.IsDelete = true;
            }

            _context.HoatDongKinhDoanh.UpdateRange(data);

            await _context.SaveChangesAsync();

            return new Result<bool>() { IsSuccessed = true, Message = "Xóa thành công" };
        }

        public async Task<List<HoatDongKinhDoanhVm>> GetAll()
        {
            var data = await _context.HoatDongKinhDoanh.Where(x => !x.IsDelete).AsNoTracking().ToListAsync();

            return data.Select(x => new HoatDongKinhDoanhVm
            {
                Id = x.Id,
                ParentId = x.ParentId,
                Code = x.Code,
                Name = x.Name,
                DVT = x.DVT,
                ChinhThucThangTruoc = x.ChinhThucThangTruoc,
                UocThangHienTai = x.UocThangHienTai,
                DuTinhUocThangSau = x.DuTinhUocThangSau,
                LuyKeTuDauNam = x.LuyKeTuDauNam,
                Thang = x.Thang,
                Nam = x.Nam
            }).ToList();
        }

        public async Task<HoatDongKinhDoanhVm> GetById(int id)
        {
            var data = await _context.HoatDongKinhDoanh.FindAsync(id);

            if (data == null || data.IsDelete) return null;

            return new HoatDongKinhDoanhVm
            {
                Id = data.Id,
                ParentId = data.ParentId,
                Code = data.Code,
                Name = data.Name,
                DVT = data.DVT,
                ChinhThucThangTruoc = data.ChinhThucThangTruoc,
                UocThangHienTai = data.UocThangHienTai,
                DuTinhUocThangSau = data.DuTinhUocThangSau,
                LuyKeTuDauNam = data.LuyKeTuDauNam,
                Thang = data.Thang,
                Nam = data.Nam
            };
        }

        public async Task<PagedResult<HoatDongKinhDoanhVm>> GetPaging(HoatDongKinhDoanhFormRequest request)
        {
            var query = _context.HoatDongKinhDoanh.Where(x => !x.IsDelete && x.Thang == request.Thang && x.Nam == request.Nam);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                request.Search = request.Search.Trim();

                query = query.Where(x => x.Code.Contains(request.Search, StringComparison.OrdinalIgnoreCase) || x.Name.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
            }

            var hierarchy = await _danhMucService.GetHierarchy();

            var data = await query.AsNoTracking().ToListAsync();

            var result = new List<HoatDongKinhDoanhVm>();

            foreach (var c in hierarchy)
            {
                var item = data.FirstOrDefault(x => x.DanhMucId == c.Id);

                result.Add(new()
                {
                    Id = item?.Id ?? 0,
                    Name = c.Name,
                    Code = c.Code,
                    DVT = c.DVT,
                    ChinhThucThangTruoc = item?.ChinhThucThangTruoc ?? 0,
                    UocThangHienTai = item?.UocThangHienTai ?? 0,
                    LuyKeTuDauNam = item?.LuyKeTuDauNam ?? 0,
                    DuTinhUocThangSau = item?.DuTinhUocThangSau ?? 0,
                    Level = c.Level
                });
            }

            int totalRow = result.Count;

            return new PagedResult<HoatDongKinhDoanhVm>
            {
                Items = result.Skip((request.PageIndex) - 1 * request.PageSize).Take(request.PageSize).ToList(),
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
        }

        public async Task<Result<bool>> Import(List<ImporFileVm> items, int thang, int nam)
        {
            try
            {
                var hierarchy = await _danhMucService.GetHierarchy();

                if (hierarchy.Count != items.Count) return new Result<bool>() { IsSuccessed = false, Message = "Import thất bại" };

                var newEntities = new List<HoatDongKinhDoanh>();

                int i = 0;

                var existsData = await _context.HoatDongKinhDoanh.Where(x => !x.IsDelete && x.Thang == thang).ToListAsync();

                foreach (var item in items)
                {
                    if (existsData.Count > 0)
                    {
                        var entity = existsData[i];

                        entity.ChinhThucThangTruoc = decimal.Parse(item.ChinhThucThangTruoc);
                        entity.UocThangHienTai = decimal.Parse(item.UocThangHienTai);
                        entity.LuyKeTuDauNam = decimal.Parse(item.LuyKeTuDauNam);
                        entity.DuTinhUocThangSau = decimal.Parse(item.DuTinhUocThangSau);
                    }
                    else
                    {
                        newEntities.Add(new HoatDongKinhDoanh
                        {
                            Code = hierarchy[i].Code,
                            Name = hierarchy[i].Name,
                            DVT = hierarchy[i].DVT,
                            ChinhThucThangTruoc = decimal.Parse(item.ChinhThucThangTruoc),
                            UocThangHienTai = decimal.Parse(item.UocThangHienTai),
                            LuyKeTuDauNam = decimal.Parse(item.LuyKeTuDauNam),
                            DuTinhUocThangSau = decimal.Parse(item.DuTinhUocThangSau),
                            DanhMucId = hierarchy[i].Id,
                            Thang = thang,
                            Nam = nam,
                            IsDelete = false
                        });
                    }

                    i++;
                }

                if (newEntities.Count > 0) await _context.HoatDongKinhDoanh.AddRangeAsync(newEntities);

                if (existsData.Count > 0) _context.HoatDongKinhDoanh.UpdateRange(existsData);

                await _context.SaveChangesAsync();

                return new Result<bool>() { IsSuccessed = true, Message = "Import thành công" };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new Result<bool>() { IsSuccessed = false, Message = "Import thất bại" };
            }
        }

        public async Task<int> MinYear()
        {
            var year = await _context.HoatDongKinhDoanh.Where(x => !x.IsDelete).MinAsync(x => (int?)x.Nam) ?? DateTime.Now.Year;

            return year;
        }

        public async Task<Result<bool>> Update(HoatDongKinhDoanhUpdateRequest request)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(request.Id));

            var data = await _context.HoatDongKinhDoanh.FindAsync(id);

            if (data == null || data.IsDelete) return new Result<bool>() { IsSuccessed = false, Message = "Dữ liệu không tồn tại" };

            data.ChinhThucThangTruoc = decimal.Parse(request.ChinhThucThangTruoc);
            data.UocThangHienTai = decimal.Parse(request.UocThangHienTai);
            data.DuTinhUocThangSau = decimal.Parse(request.DuTinhUocThangSau);
            data.LuyKeTuDauNam = decimal.Parse(request.LuyKeTuDauNam);

            _context.HoatDongKinhDoanh.Update(data);

            await _context.SaveChangesAsync();

            return new Result<bool>() { IsSuccessed = true, Message = "Cập nhật thành công" };
        }
    }
}
