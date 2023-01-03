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
    public interface INganhDaoTaoService
    {
        Task<List<NganhDaoTaoModel>> GetAll();

        Task<PagedResult<NganhDaoTaoModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<NganhDaoTaoModel>> Create(NganhDaoTaoModel request);

        Task<ApiResult<int>> Update(int id, NganhDaoTaoModel request);

        Task<NganhDaoTaoModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
    }

    public class NganhDaoTaoService : INganhDaoTaoService
    {
        private readonly TLDbContext _context;

        public NganhDaoTaoService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<NganhDaoTaoModel>> Create(NganhDaoTaoModel request)
        {
            var NganhDaoTao = new NganhDaoTao()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                TenNganhDaoTao = request.TenNganhDaoTao,
            };
            _context.NganhDaoTao.Add(NganhDaoTao);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = NganhDaoTao.Id;
                return new ApiSuccessResult<NganhDaoTaoModel>(request);
            }
            return new ApiErrorResult<NganhDaoTaoModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var NganhDaoTao = await _context.NganhDaoTao.Where(x => x.Id == id).ToListAsync();
            if (NganhDaoTao == null || NganhDaoTao.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var obj = NganhDaoTao.FirstOrDefault();
            obj.IsDelete = true;

            _context.NganhDaoTao.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<NganhDaoTaoModel>> GetAll()
        {
            var query = from m in _context.NganhDaoTao
                        where m.IsDelete == false
                        select new NganhDaoTaoModel
                        {
                            Id = m.Id,
                            TenNganhDaoTao = m.TenNganhDaoTao,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            MoTa = m.MoTa

                        };
            return await query.ToListAsync();
        }

        public async Task<NganhDaoTaoModel> GetById(int id)
        {
            var nganh = await _context.NganhDaoTao.Where(x => x.Id == id).ToListAsync();
            if (nganh == null || nganh.Count() <= 0)
            {
                throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
            }

            var obj = nganh.FirstOrDefault();

            return new NganhDaoTaoModel()
            {
                MoTa = obj.MoTa,
                TenNganhDaoTao = obj.TenNganhDaoTao,
                Id = obj.Id
            };

        }

        public async Task<PagedResult<NganhDaoTaoModel>> GetPaging(GetPagingRequest request)
        {
            var query = from m in _context.NganhDaoTao
                        where m.IsDelete == false
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.TenNganhDaoTao.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new NganhDaoTaoModel()
                {
                    IsDelete = x.m.IsDelete,
                    IsStatus = x.m.IsStatus,
                    MoTa = x.m.MoTa,
                    TenNganhDaoTao = x.m.TenNganhDaoTao,
                    Id = x.m.Id
                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<NganhDaoTaoModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<int>> Update(int id, NganhDaoTaoModel request)
        {
            var NganhDaoTao = await _context.NganhDaoTao.Where(x => x.Id == id).ToListAsync();
            if (NganhDaoTao == null || NganhDaoTao.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var model = NganhDaoTao.FirstOrDefault();

            model.MoTa = request.MoTa;
            model.TenNganhDaoTao = request.TenNganhDaoTao;

            _context.NganhDaoTao.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }
    }
}