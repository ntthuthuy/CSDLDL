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
    public interface ILoaiDichVuService
    {
        Task<List<LoaiDichVuModel>> GetAll(string ngonNguId = SystemConstants.DefaultLanguage);

        Task<PagedResult<LoaiDichVuModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<LoaiDichVuModel>> Create(LoaiDichVuModel request);

        Task<ApiResult<int>> Update(int id, LoaiDichVuModel request);

        Task<LoaiDichVuModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
    }

    public class LoaiDichVuService : ILoaiDichVuService
    {
        private readonly TLDbContext _context;

        public LoaiDichVuService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<LoaiDichVuModel>> Create(LoaiDichVuModel request)
        {
            if (!await _context.NgonNgu.AnyAsync(x => x.Id == request.NgonNguId)) return new ApiErrorResult<LoaiDichVuModel>("Ngôn ngữ không hợp lệ");

            var loaiDichVu = new LoaiDichVu()
            {
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                TenLoai = request.TenLoai,
                // HueCIT
                DongBoID = request.DongBoID,
                NguonDongBo = request.NguonDongBo,
                NgonNguId = request.NgonNguId
            };
            await _context.LoaiDichVu.AddAsync(loaiDichVu);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = loaiDichVu.Id;
                return new ApiSuccessResult<LoaiDichVuModel>(request);
            }
            return new ApiErrorResult<LoaiDichVuModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var LoaiDichVu = await _context.LoaiDichVu.Where(x => x.Id == id).ToListAsync();
            if (LoaiDichVu == null || LoaiDichVu.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var obj = LoaiDichVu.FirstOrDefault();
            obj.IsDelete = true;

            _context.LoaiDichVu.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<LoaiDichVuModel>> GetAll(string ngonNguId)
        {
            var query = from m in _context.LoaiDichVu
                        where m.IsDelete == false && m.NgonNguId == ngonNguId
                        select new LoaiDichVuModel
                        {
                            Id = m.Id,
                            TenLoai = m.TenLoai,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            MoTa = m.MoTa,
                            DongBoID = m.DongBoID,
                            NguonDongBo = m.NguonDongBo,
                            NgonNguId = m.NgonNguId
                        };
            return await query.ToListAsync();
        }

        public async Task<LoaiDichVuModel> GetById(int id)
        {
            var loaiDichVu = await _context.LoaiDichVu.Where(x => x.Id == id).ToListAsync();
            if (loaiDichVu == null || loaiDichVu.Count() <= 0)
            {
                throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
            }

            var obj = loaiDichVu.FirstOrDefault();

            return new LoaiDichVuModel()
            {
                MoTa = obj.MoTa,
                TenLoai = obj.TenLoai,
                Id = obj.Id,
                NgonNguId = obj.NgonNguId
            };

        }

        public async Task<PagedResult<LoaiDichVuModel>> GetPaging(GetPagingRequest request)
        {
            var query = from m in _context.LoaiDichVu
                        where m.IsDelete == false
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.TenLoai.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new LoaiDichVuModel()
                {
                    IsDelete = x.m.IsDelete,
                    IsStatus = x.m.IsStatus,
                    MoTa = x.m.MoTa,
                    TenLoai = x.m.TenLoai,
                    Id = x.m.Id
                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<LoaiDichVuModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<int>> Update(int id, LoaiDichVuModel request)
        {
            if (!await _context.NgonNgu.AnyAsync(x => x.Id == request.NgonNguId)) return new ApiErrorResult<int>("Ngôn ngữ không hợp lệ");
            var LoaiDichVu = await _context.LoaiDichVu.Where(x => x.Id == id).ToListAsync();
            if (LoaiDichVu == null || LoaiDichVu.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var model = LoaiDichVu.FirstOrDefault();

            model.MoTa = request.MoTa;
            model.TenLoai = request.TenLoai;
            model.NgonNguId = request.NgonNguId;

            _context.LoaiDichVu.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }
    }
}