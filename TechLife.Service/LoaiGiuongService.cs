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
    public interface ILoaiGiuongService
    {
        Task<List<LoaiGiuongModel>> GetAll();

        Task<PagedResult<LoaiGiuongModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<LoaiGiuongModel>> Create(LoaiGiuongModel request);

        Task<ApiResult<int>> Update(int id, LoaiGiuongModel request);

        Task<LoaiGiuongModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
    }

    public class LoaiGiuongService : ILoaiGiuongService
    {
        private readonly TLDbContext _context;

        public LoaiGiuongService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<LoaiGiuongModel>> Create(LoaiGiuongModel request)
        {
            var LoaiGiuong = new LoaiGiuong()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                Ten = request.Ten,
            };
            _context.LoaiGiuong.Add(LoaiGiuong);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = LoaiGiuong.Id;
                return new ApiSuccessResult<LoaiGiuongModel>(request);
            }
            return new ApiErrorResult<LoaiGiuongModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var LoaiGiuong = await _context.LoaiGiuong.Where(x => x.Id == id).ToListAsync();
            if (LoaiGiuong == null || LoaiGiuong.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy dữ liệu!");
            }

            var obj = LoaiGiuong.FirstOrDefault();
            obj.IsDelete = true;

            _context.LoaiGiuong.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<LoaiGiuongModel>> GetAll()
        {
            var query = from m in _context.LoaiGiuong
                        where m.IsDelete == false
                        select new LoaiGiuongModel
                        {
                            Id = m.Id,
                            Ten = m.Ten,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            MoTa = m.MoTa
                        };
            return await query.ToListAsync();
        }

        public async Task<LoaiGiuongModel> GetById(int id)
        {
            var LoaiGiuong = await _context.LoaiGiuong.Where(x => x.Id == id).ToListAsync();
            if (LoaiGiuong == null || LoaiGiuong.Count() <= 0)
            {
                throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
            }

            var obj = LoaiGiuong.FirstOrDefault();

           return new LoaiGiuongModel()
            {
                MoTa = obj.MoTa,
                Ten = obj.Ten,
                Id = obj.Id
            };

        }

        public async Task<PagedResult<LoaiGiuongModel>> GetPaging(GetPagingRequest request)
        {
            var query = from m in _context.LoaiGiuong
                        where m.IsDelete == false
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.Ten.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new LoaiGiuongModel()
                {
                    IsDelete = x.m.IsDelete,
                    IsStatus = x.m.IsStatus,
                    MoTa = x.m.MoTa,
                    Ten = x.m.Ten,
                    Id = x.m.Id
                }).ToListAsync();

            //4. Select and projection
           return new PagedResult<LoaiGiuongModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<int>> Update(int id, LoaiGiuongModel request)
        {
            var LoaiGiuong = await _context.LoaiGiuong.Where(x => x.Id == id).ToListAsync();
            if (LoaiGiuong == null || LoaiGiuong.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy dữ liệu!");
            }

            var model = LoaiGiuong.FirstOrDefault();

            model.MoTa = request.MoTa;
            model.Ten = request.Ten;

            _context.LoaiGiuong.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi");
        }
    }
}