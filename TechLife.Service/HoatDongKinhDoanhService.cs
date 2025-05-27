using Microsoft.EntityFrameworkCore;
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
        Task<Result<bool>> Delete(int id);
        Task<List<HoatDongKinhDoanhVm>> GetAll();
        Task<HoatDongKinhDoanhVm> GetById(int id);
    }

    public class HoatDongKinhDoanhService : IHoatDongKinhDoanhService
    {
        private readonly TLDbContext _context;

        public HoatDongKinhDoanhService(TLDbContext context)
        {
            _context = context;
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

        public async Task<Result<bool>> Delete(int id)
        {
            var data = await _context.HoatDongKinhDoanh.FindAsync(id);

            if (data == null || data.IsDelete) return new Result<bool>() { IsSuccessed = false, Message = "Dữ liệu không tồn tại" };

            data.IsDelete = true;

            await _context.SaveChangesAsync();

            return new Result<bool>() { IsSuccessed = true, Message = "Xóa thành công" };
        }

        public async Task<List<HoatDongKinhDoanhVm>> GetAll()
        {
            var data = await _context.HoatDongKinhDoanh.Where(x => !x.IsDelete).ToListAsync();

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
                LuyKeTuDauNam = x.LuyKeTuDauNam
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
                LuyKeTuDauNam = data.LuyKeTuDauNam
            };
        }

        public async Task<PagedResult<HoatDongKinhDoanhVm>> GetPaging(HoatDongKinhDoanhFormRequest request)
        {
            var query = _context.HoatDongKinhDoanh.Where(x => !x.IsDelete);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                request.Search = request.Search.Trim();

                query = query.Where(x => x.Code.Contains(request.Search, StringComparison.OrdinalIgnoreCase) || x.Name.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new HoatDongKinhDoanhVm
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Code = x.Code,
                    Name = x.Name,
                    DVT = x.DVT,
                    ChinhThucThangTruoc = x.ChinhThucThangTruoc,
                    UocThangHienTai = x.UocThangHienTai,
                    DuTinhUocThangSau = x.DuTinhUocThangSau,
                    LuyKeTuDauNam = x.LuyKeTuDauNam
                }).ToListAsync();

            return new PagedResult<HoatDongKinhDoanhVm>
            {
                Items = data,
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
        }

        public async Task<Result<bool>> Update(HoatDongKinhDoanhUpdateRequest request)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(request.Id));
            int? parentId = !string.IsNullOrWhiteSpace(request.ParentId) ? Convert.ToInt32(HashUtil.DecodeID(request.ParentId)) : null;

            var data = await _context.HoatDongKinhDoanh.FindAsync(id);

            if (data == null || data.IsDelete) return new Result<bool>() { IsSuccessed = false, Message = "Dữ liệu không tồn tại" };

            data.Code = request.Code?.Trim();
            data.Name = request.Name.Trim();
            data.ParentId = parentId;
            data.DVT = request.DVT?.Trim();
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
