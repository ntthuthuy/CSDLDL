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
    public interface ILoaiHinhLaoDongService
    {
        Task<List<LoaiHinhLaoDongModel>> GetAll();

        Task<PagedResult<LoaiHinhLaoDongModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<LoaiHinhLaoDongModel>> Create(LoaiHinhLaoDongModel request);

        Task<ApiResult<int>> Update(int id, LoaiHinhLaoDongModel request);

        Task<LoaiHinhLaoDongModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
    }

    public class LoaiHinhLaoDongService : ILoaiHinhLaoDongService
    {
        private readonly TLDbContext _context;

        public LoaiHinhLaoDongService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<LoaiHinhLaoDongModel>> Create(LoaiHinhLaoDongModel request)
        {
            var LoaiHinhLaoDong = new LoaiHinhLaoDong()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                TenLoaiHinh = request.TenLoaiHinh,
            };
            _context.LoaiHinhLaoDong.Add(LoaiHinhLaoDong);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = LoaiHinhLaoDong.Id;
                return new ApiSuccessResult<LoaiHinhLaoDongModel>(request);
            }
            return new ApiErrorResult<LoaiHinhLaoDongModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var LoaiHinhLaoDong = await _context.LoaiHinhLaoDong.Where(x => x.Id == id).ToListAsync();
            if (LoaiHinhLaoDong == null || LoaiHinhLaoDong.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var obj = LoaiHinhLaoDong.FirstOrDefault();
            obj.IsDelete = true;

            _context.LoaiHinhLaoDong.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<LoaiHinhLaoDongModel>> GetAll()
        {
            var query = from m in _context.LoaiHinhLaoDong
                        where m.IsDelete == false
                        select new LoaiHinhLaoDongModel
                        {
                            Id = m.Id,
                            TenLoaiHinh = m.TenLoaiHinh,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            MoTa = m.MoTa

                        };
            return await query.ToListAsync();
        }

        public async Task<LoaiHinhLaoDongModel> GetById(int id)
        {
            var loaihinh = await _context.LoaiHinhLaoDong.Where(x => x.Id == id).ToListAsync();
            if (loaihinh == null || loaihinh.Count() <= 0)
            {
                throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
            }

            var obj = loaihinh.FirstOrDefault();

            return new LoaiHinhLaoDongModel()
            {
                MoTa = obj.MoTa,
                TenLoaiHinh = obj.TenLoaiHinh,
                Id = obj.Id
            };

        }

        public async Task<PagedResult<LoaiHinhLaoDongModel>> GetPaging(GetPagingRequest request)
        {
            var query = from m in _context.LoaiHinhLaoDong
                        where m.IsDelete == false
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.TenLoaiHinh.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new LoaiHinhLaoDongModel()
                {
                    IsDelete = x.m.IsDelete,
                    IsStatus = x.m.IsStatus,
                    MoTa = x.m.MoTa,
                    TenLoaiHinh = x.m.TenLoaiHinh,
                    Id = x.m.Id
                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<LoaiHinhLaoDongModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<int>> Update(int id, LoaiHinhLaoDongModel request)
        {
            var loaihinh = await _context.LoaiHinhLaoDong.Where(x => x.Id == id).ToListAsync();
            if (loaihinh == null || loaihinh.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var model = loaihinh.FirstOrDefault();

            model.MoTa = request.MoTa;
            model.TenLoaiHinh = request.TenLoaiHinh;

            _context.LoaiHinhLaoDong.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }
    }
}