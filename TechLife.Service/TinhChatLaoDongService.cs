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
    public interface ITinhChatLaoDongService
    {
        Task<List<TinhChatLaoDongModel>> GetAll();

        Task<PagedResult<TinhChatLaoDongModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<TinhChatLaoDongModel>> Create(TinhChatLaoDongModel request);

        Task<ApiResult<int>> Update(int id, TinhChatLaoDongModel request);

        Task<TinhChatLaoDongModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
    }

    public class TinhChatLaoDongService : ITinhChatLaoDongService
    {
        private readonly TLDbContext _context;

        public TinhChatLaoDongService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<TinhChatLaoDongModel>> Create(TinhChatLaoDongModel request)
        {
            var TinhChatLaoDong = new TinhChatLaoDong()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                Ten = request.Ten,
            };
            _context.TinhChatLaoDong.Add(TinhChatLaoDong);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = TinhChatLaoDong.Id;
                return new ApiSuccessResult<TinhChatLaoDongModel>(request);
            }
            return new ApiErrorResult<TinhChatLaoDongModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var TinhChatLaoDong = await _context.TinhChatLaoDong.Where(x => x.Id == id).ToListAsync();
            if (TinhChatLaoDong == null || TinhChatLaoDong.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy.");
            }

            var obj = TinhChatLaoDong.FirstOrDefault();
            obj.IsDelete = true;

            _context.TinhChatLaoDong.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<TinhChatLaoDongModel>> GetAll()
        {
            var query = from m in _context.TinhChatLaoDong
                        where m.IsDelete == false
                        select new TinhChatLaoDongModel
                        {
                            Id = m.Id,
                            Ten = m.Ten,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            MoTa = m.MoTa
                        };
            return await query.ToListAsync();
        }

        public async Task<TinhChatLaoDongModel> GetById(int id)
        {
            var tinhchat = await _context.TinhChatLaoDong.Where(x => x.Id == id).ToListAsync();
            if (tinhchat == null || tinhchat.Count() <= 0)
            {
                throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
            }

            var obj = tinhchat.FirstOrDefault();

            return new TinhChatLaoDongModel()
            {
                MoTa = obj.MoTa,
                Ten = obj.Ten,
                Id = obj.Id
            };

        }

        public async Task<PagedResult<TinhChatLaoDongModel>> GetPaging(GetPagingRequest request)
        {
            var query = from m in _context.TinhChatLaoDong
                        where m.IsDelete == false
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.Ten.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new TinhChatLaoDongModel()
                {
                    IsDelete = x.m.IsDelete,
                    IsStatus = x.m.IsStatus,
                    MoTa = x.m.MoTa,
                    Ten = x.m.Ten,
                    Id = x.m.Id
                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<TinhChatLaoDongModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<int>> Update(int id, TinhChatLaoDongModel request)
        {
            var TinhChatLaoDong = await _context.TinhChatLaoDong.Where(x => x.Id == id).ToListAsync();
            if (TinhChatLaoDong == null || TinhChatLaoDong.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy!");
            }

            var model = TinhChatLaoDong.FirstOrDefault();

            model.MoTa = request.MoTa;
            model.Ten = request.Ten;

            _context.TinhChatLaoDong.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }
    }
}