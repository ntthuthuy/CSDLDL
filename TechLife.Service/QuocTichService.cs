using Microsoft.Data.SqlClient;
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
                return new ApiSuccessResult<QuocTichModel>(request);
            }
            return new ApiErrorResult<QuocTichModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var QuocTich = await _context.QuocTich.Where(x => x.Id == id).ToListAsync();
            if (QuocTich == null || QuocTich.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy!");
            }

            var obj = QuocTich.FirstOrDefault();
            obj.IsDelete = true;

            _context.QuocTich.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
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
            var quoctich = await _context.QuocTich.Where(x => x.Id == id).ToListAsync();
            if (quoctich == null || quoctich.Count() <= 0)
            {
                throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
            }

            var obj = quoctich.FirstOrDefault();

            return new QuocTichModel()
            {
                MoTa = obj.MoTa,
                TenQuocTich = obj.TenQuocTich,
                Id = obj.Id
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
            var QuocTich = await _context.QuocTich.Where(x => x.Id == id).ToListAsync();
            if (QuocTich == null || QuocTich.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy!");
            }

            var model = QuocTich.FirstOrDefault();

            model.MoTa = request.MoTa;
            model.TenQuocTich = request.TenQuocTich;

            _context.QuocTich.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }

    }
}