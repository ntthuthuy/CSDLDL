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
    public interface ILoaiHinhService
    {
        Task<List<LoaiHinhModel>> GetAll();

        Task<PagedResult<LoaiHinhModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<LoaiHinhModel>> Create(LoaiHinhModel request);

        Task<ApiResult<int>> Update(int id, LoaiHinhModel request);

        Task<LoaiHinhModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
    }

    public class LoaiHinhService : ILoaiHinhService
    {
        private readonly TLDbContext _context;

        public LoaiHinhService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<LoaiHinhModel>> Create(LoaiHinhModel request)
        {
            var LoaiHinh = new LoaiHinh()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                TenLoai = request.TenLoai,
            };
            _context.LoaiHinh.Add(LoaiHinh);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = LoaiHinh.Id;
                return new ApiSuccessResult<LoaiHinhModel>(request);
            }
            return new ApiErrorResult<LoaiHinhModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var LoaiHinh = await _context.LoaiHinh.Where(x => x.Id == id).ToListAsync();
            if (LoaiHinh == null || LoaiHinh.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var obj = LoaiHinh.FirstOrDefault();
            obj.IsDelete = true;

            _context.LoaiHinh.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<LoaiHinhModel>> GetAll()
        {
            var query = from m in _context.LoaiHinh
                        where m.IsDelete == false
                        select new LoaiHinhModel
                        {
                            Id = m.Id,
                            TenLoai = m.TenLoai,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            MoTa = m.MoTa
                        };
            return await query.ToListAsync();
        }

        public async Task<LoaiHinhModel> GetById(int id)
        {
            var loaihinh = await _context.LoaiHinh.Where(x => x.Id == id).ToListAsync();
            if (loaihinh == null || loaihinh.Count() <= 0)
            {
                return null;
            }

            var obj = loaihinh.FirstOrDefault();

            return new LoaiHinhModel()
            {
                MoTa = obj.MoTa,
                TenLoai = obj.TenLoai,
                Id = obj.Id
            };

        }

        public async Task<PagedResult<LoaiHinhModel>> GetPaging(GetPagingRequest request)
        {
            var query = from m in _context.LoaiHinh
                        where m.IsDelete == false
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.TenLoai.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new LoaiHinhModel()
                {
                    IsDelete = x.m.IsDelete,
                    IsStatus = x.m.IsStatus,
                    MoTa = x.m.MoTa,
                    TenLoai = x.m.TenLoai,
                    Id = x.m.Id
                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<LoaiHinhModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<int>> Update(int id, LoaiHinhModel request)
        {
            var LoaiHinh = await _context.LoaiHinh.Where(x => x.Id == id).ToListAsync();
            if (LoaiHinh == null || LoaiHinh.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var model = LoaiHinh.FirstOrDefault();

            model.MoTa = request.MoTa;
            model.TenLoai = request.TenLoai;

            _context.LoaiHinh.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }
    }
}