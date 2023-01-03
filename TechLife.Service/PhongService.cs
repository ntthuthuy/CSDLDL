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
    public interface IPhongService
    {
        Task<List<PhongModel>> GetAll();

        Task<PagedResult<PhongModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<PhongModel>> Create(PhongModel request);

        Task<ApiResult<int>> Update(int id, PhongModel request);

        Task<PhongModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
    }

    public class PhongService : IPhongService
    {
        private readonly TLDbContext _context;

        public PhongService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<PhongModel>> Create(PhongModel request)
        {
            var Phong = new Phong()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                LoaiPhongId = request.LoaiPhongId
            };
            _context.Phong.Add(Phong);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = Phong.Id;
                return new ApiSuccessResult<PhongModel>(request);
            }
            return new ApiErrorResult<PhongModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var Phong = await _context.Phong.Where(x => x.Id == id).ToListAsync();
            if (Phong == null || Phong.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var obj = Phong.FirstOrDefault();
            obj.IsDelete = true;

            _context.Phong.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<PhongModel>> GetAll()
        {
            var query = from m in _context.Phong
                        where m.IsDelete == false
                        select new PhongModel
                        {
                            Id = m.Id,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            LoaiPhongId = m.LoaiPhongId
                        };
            return await query.ToListAsync();
        }

        public async Task<PhongModel> GetById(int id)
        {
            var phong = await _context.Phong.Where(x => x.Id == id).ToListAsync();
            if (phong == null || phong.Count() <= 0)
            {
                throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
            }

            var obj = phong.FirstOrDefault();

            return new PhongModel()
            {
                Id = obj.Id,
                LoaiPhongId = obj.LoaiPhongId
            };

        }

        public async Task<PagedResult<PhongModel>> GetPaging(GetPagingRequest request)
        {
            var query = from m in _context.Phong
                        where m.IsDelete == false
                        select new { m };


            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(obj => new PhongModel()
                {
                    Id = obj.m.Id,
                    IsDelete = obj.m.IsDelete,
                    IsStatus = obj.m.IsStatus,
                    LoaiPhongId = obj.m.LoaiPhongId

                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<PhongModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<int>> Update(int id, PhongModel request)
        {
            var Phong = await _context.Phong.Where(x => x.Id == id).ToListAsync();
            if (Phong == null || Phong.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var model = Phong.FirstOrDefault();

            model.LoaiPhongId = request.LoaiPhongId;

            _context.Phong.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }
    }
}