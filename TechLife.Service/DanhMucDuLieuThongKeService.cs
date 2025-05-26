using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model.DanhMucDuLieuThongKe;

namespace TechLife.Service
{
    public interface IDanhMucDuLieuThongKeService
    {
        Task<PagedResult<DanhMucDuLieuThongKeVm>> GetPaging(DanhMucDuLieuThongKeFromRequets requets);
        Task<Result<bool>> Create(DanhMucDuLieuThongKeCreateRequest request);
        Task<Result<bool>> Update(DanhMucDuLieuThongKeUpdateRequest request);
        Task<Result<bool>> Delete(int id);
        Task<List<DanhMucDuLieuThongKeVm>> GetAll();
        Task<DanhMucDuLieuThongKeVm> GetById(int id);
    }

    public class DanhMucDuLieuThongKeService : IDanhMucDuLieuThongKeService
    {
        private readonly TLDbContext _context;

        public DanhMucDuLieuThongKeService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Create(DanhMucDuLieuThongKeCreateRequest request)
        {
            int? parentId = !string.IsNullOrEmpty(request.ParentId) ? Convert.ToInt32(HashUtil.DecodeID(request.ParentId)) : null;

            var data = new DanhMucDuLieuThongKe
            {
                Code = request.Code?.Trim(),
                Name = request.Name?.Trim(),
                DVT = request.DVT?.Trim(),
                ParentId = parentId,
                IsDelete = false
            };

            await _context.DanhMucDuLieuThongKe.AddAsync(data);

            await _context.SaveChangesAsync();

            return new Result<bool>() { IsSuccessed = true, Message = "Thêm danh mục thành công" };
        }

        public async Task<Result<bool>> Delete(int id)
        {
            var data = await _context.DanhMucDuLieuThongKe.FindAsync(id);

            if (data == null || data.IsDelete) return new Result<bool>() { IsSuccessed = false, Message = "Dữ liệu không tồn tại" };

            data.IsDelete = true;

            await _context.SaveChangesAsync();

            return new Result<bool>() { IsSuccessed = true, Message = "Xóa danh mục thành công" };
        }

        public async Task<List<DanhMucDuLieuThongKeVm>> GetAll()
        {
            var data = await _context.DanhMucDuLieuThongKe.Where(x => !x.IsDelete).ToListAsync();
            return data.Select(x => new DanhMucDuLieuThongKeVm
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                DVT = x.DVT,
                ParentId = x.ParentId,
            }).ToList();
        }

        public async Task<DanhMucDuLieuThongKeVm> GetById(int id)
        {
            var data = await _context.DanhMucDuLieuThongKe.FindAsync(id);

            if (data == null || data.IsDelete) return null;

            return new DanhMucDuLieuThongKeVm
            {
                Id = id,
                Code = data.Code,
                Name = data.Name,
                DVT = data.DVT,
                ParentId = data.ParentId,
            };
        }

        public async Task<PagedResult<DanhMucDuLieuThongKeVm>> GetPaging(DanhMucDuLieuThongKeFromRequets requets)
        {
            var query = _context.DanhMucDuLieuThongKe.Where(x => !x.IsDelete);

            if (!string.IsNullOrWhiteSpace(requets.Search))
            {
                requets.Search = requets.Search.Trim();

                query = query.Where(x => requets.Search.Contains(x.Name, StringComparison.OrdinalIgnoreCase) || requets.Search.Contains(x.Code, StringComparison.OrdinalIgnoreCase));
            }

            int totalRow = await query.CountAsync();

            var data = await query
            .Skip((requets.PageIndex - 1) * requets.PageSize)
            .Take(requets.PageSize)
            .Select(x => new DanhMucDuLieuThongKeVm
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                DVT = x.DVT,
                ParentId = x.ParentId,
            }).ToListAsync();

            return new PagedResult<DanhMucDuLieuThongKeVm>
            {
                TotalRecords = totalRow,
                Items = data,
                PageIndex = requets.PageIndex,
                PageSize = requets.PageSize,
            };
        }

        public async Task<Result<bool>> Update(DanhMucDuLieuThongKeUpdateRequest request)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(request.Id));

            int? parentId = string.IsNullOrWhiteSpace(request.ParentId) ? null : Convert.ToInt32(HashUtil.DecodeID(request.ParentId));

            var data = await _context.DanhMucDuLieuThongKe.FindAsync(id);

            if (data == null || data.IsDelete) return new Result<bool>() { IsSuccessed = false, Message = "Dữ liệu không tồn tại" };

            data.Code = request.Code?.Trim();
            data.Name = request.Name.Trim();
            data.ParentId = parentId;
            data.DVT = request.DVT?.Trim();

            _context.DanhMucDuLieuThongKe.Update(data);

            await _context.SaveChangesAsync();

            return new Result<bool>() { IsSuccessed = true, Message = "Cập nhật thành công" };
        }
    }
}
