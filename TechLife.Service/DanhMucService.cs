using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model;

namespace TechLife.Service
{
    public interface IDanhMucService
    {
        Task<List<DanhMucModel>> GetAll(int loaiId);

        Task<PagedResult<DanhMucModel>> GetPaging(int loaiId,GetPagingRequest request);

        Task<ApiResult<DanhMucModel>> Create(DanhMucModel request);

        Task<ApiResult<int>> Update(int id, DanhMucModel request);

        Task<DanhMucModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
    }

    public class DanhMucService : IDanhMucService
    {
        private readonly TLDbContext _context;

        public DanhMucService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<DanhMucModel>> Create(DanhMucModel request)
        {
            var DanhMuc = new DanhMuc()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                Ten = request.Ten,
                LoaiId = request.LoaiId
            };
            _context.DanhMuc.Add(DanhMuc);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = DanhMuc.Id;
                return new ApiSuccessResult<DanhMucModel>(request);
            }
            return new ApiErrorResult<DanhMucModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var DanhMuc = await _context.DanhMuc.Where(x => x.Id == id).ToListAsync();
            if (DanhMuc == null || DanhMuc.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var obj = DanhMuc.FirstOrDefault();
            obj.IsDelete = true;

            _context.DanhMuc.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<DanhMucModel>> GetAll(int loaiId)
        {
            var query = from m in _context.DanhMuc
                        where m.IsDelete == false && m.LoaiId == loaiId
                        select new DanhMucModel
                        {
                            Id = m.Id,
                            Ten = m.Ten,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            LoaiId = m.LoaiId,
                            MoTa = m.MoTa,
                            DongBoID = m.DongBoID,
                            NguonDongBo = m.NguonDongBo
                        };
            return await query.ToListAsync();
        }

        public async Task<DanhMucModel> GetById(int id)
        {
            var DanhMuc = await _context.DanhMuc.Where(x => x.Id == id).ToListAsync();
            if (DanhMuc == null || DanhMuc.Count() <= 0)
            {
                return null;
            }

            var obj = DanhMuc.FirstOrDefault();

            return new DanhMucModel()
            {
                MoTa = obj.MoTa,
                Ten = obj.Ten,
                Id = obj.Id,
                LoaiId = obj.LoaiId,
                DongBoID = obj.DongBoID,
                NguonDongBo = obj.NguonDongBo,
            };

        }

        public async Task<PagedResult<DanhMucModel>> GetPaging(int loaiId,GetPagingRequest request)
        {
            var query = from m in _context.DanhMuc
                        where m.IsDelete == false && m.LoaiId == loaiId
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.Ten.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new DanhMucModel()
                {
                    IsDelete = x.m.IsDelete,
                    IsStatus = x.m.IsStatus,
                    MoTa = x.m.MoTa,
                    Ten = x.m.Ten,
                    Id = x.m.Id
                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<DanhMucModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<int>> Update(int id, DanhMucModel request)
        {
            var DanhMuc = await _context.DanhMuc.Where(x => x.Id == id).ToListAsync();
            if (DanhMuc == null || DanhMuc.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var model = DanhMuc.FirstOrDefault();

            model.MoTa = request.MoTa;
            model.Ten = request.Ten;

            _context.DanhMuc.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }
    }
}