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
    public interface IQuocTichService
    {
        Task<List<QuocTichModel>> GetAll();

        Task<PagedResult<QuocTichModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<QuocTichModel>> Create(QuocTichModel request);

        Task<ApiResult<int>> Update(int id, QuocTichModel request);

        Task<QuocTichModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
    }

    public class QuocTichService : IQuocTichService
    {
        private readonly TLDbContext _context;

        public QuocTichService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<QuocTichModel>> Create(QuocTichModel request)
        {
            var QuocTich = new QuocTich()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                TenQuocTich = request.TenQuocTich,
            };
            _context.QuocTich.Add(QuocTich);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = QuocTich.Id;
                return new ApiSuccessResult<QuocTichModel>(request, "Thêm thành công");
            }
            return new ApiErrorResult<QuocTichModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var data = await _context.QuocTich.FindAsync(id);

            if (data == null || data.IsDelete) return new ApiErrorResult<int>("Dữ liệu không tồn tại");

            data.IsDelete = true;

            _context.QuocTich.Update(data);

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<int>(id, "Xóa thành công");
        }

        public async Task<List<QuocTichModel>> GetAll()
        {
            var query = from m in _context.QuocTich
                        where m.IsDelete == false
                        select new QuocTichModel
                        {
                            Id = m.Id,
                            TenQuocTich = m.TenQuocTich,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            MoTa = m.MoTa
                        };
            return await query.ToListAsync();
        }

        public async Task<QuocTichModel> GetById(int id)
        {
            var data = await _context.QuocTich.FindAsync(id);

            if (data == null || data.IsDelete) return null;

            return new QuocTichModel()
            {
                MoTa = data.MoTa,
                TenQuocTich = data.TenQuocTich,
                Id = data.Id
            };

        }

        public async Task<PagedResult<QuocTichModel>> GetPaging(GetPagingRequest request)
        {
            var query = from m in _context.QuocTich
                        where m.IsDelete == false
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.TenQuocTich.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new QuocTichModel()
                {
                    IsDelete = x.m.IsDelete,
                    IsStatus = x.m.IsStatus,
                    MoTa = x.m.MoTa,
                    TenQuocTich = x.m.TenQuocTich,
                    Id = x.m.Id
                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<QuocTichModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<int>> Update(int id, QuocTichModel request)
        {
            var data = await _context.QuocTich.FindAsync(id);

            if (data == null || data.IsDelete) return new ApiErrorResult<int>("Dữ liệu không tồn tại");

            data.MoTa = request.MoTa;
            data.TenQuocTich = request.TenQuocTich;

            _context.QuocTich.Update(data);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id, "Cập nhật thành công");
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }

    }
}