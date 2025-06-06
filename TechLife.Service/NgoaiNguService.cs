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
    public interface INgoaiNguService
    {

        Task<PagedResult<NgoaiNguModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<NgoaiNguModel>> Create(NgoaiNguModel request);

        Task<ApiResult<int>> Update(int id, NgoaiNguModel request);

        Task<NgoaiNguModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);

        Task<List<NgoaiNguHoSoModel>> GetAllByHoSo(int hosoId);
        Task<List<NgoaiNguModel>> GetAll(string ngonNguId = SystemConstants.DefaultLanguage);
    }

    public class NgoaiNguService : INgoaiNguService
    {
        private readonly TLDbContext _context;

        public NgoaiNguService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<NgoaiNguModel>> Create(NgoaiNguModel request)
        {
            if (!await _context.NgonNgu.AnyAsync(x => x.Id == request.NgonNguId)) return new ApiErrorResult<NgoaiNguModel>("Ngôn ngữ không hợp lệ");
            var NgoaiNgu = new NgoaiNgu()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                TenNgoaiNgu = request.TenNgoaiNgu,
                NgonNguId = request.NgonNguId
            };
            _context.NgoaiNgu.Add(NgoaiNgu);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = NgoaiNgu.Id;
                return new ApiSuccessResult<NgoaiNguModel>(request);
            }
            return new ApiErrorResult<NgoaiNguModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var NgoaiNgu = await _context.NgoaiNgu.Where(x => x.Id == id).ToListAsync();
            if (NgoaiNgu == null || NgoaiNgu.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var obj = NgoaiNgu.FirstOrDefault();
            obj.IsDelete = true;

            _context.NgoaiNgu.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<NgoaiNguModel>> GetAll(string ngonNguId)
        {
            var query = from m in _context.NgoaiNgu
                        where m.IsDelete == false && m.NgonNguId == ngonNguId
                        select new NgoaiNguModel
                        {
                            Id = m.Id,
                            TenNgoaiNgu = m.TenNgoaiNgu,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            MoTa = m.MoTa
                        };
            return await query.ToListAsync();
        }

        public async Task<NgoaiNguModel> GetById(int id)
        {
            var ngoaingu = await _context.NgoaiNgu.Where(x => x.Id == id).ToListAsync();
            if (ngoaingu == null || ngoaingu.Count() <= 0)
            {
                throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
            }

            var obj = ngoaingu.FirstOrDefault();

            return new NgoaiNguModel()
            {
                MoTa = obj.MoTa,
                TenNgoaiNgu = obj.TenNgoaiNgu,
                Id = obj.Id,
                NgonNguId = obj.NgonNguId
            };

        }

        public async Task<PagedResult<NgoaiNguModel>> GetPaging(GetPagingRequest request)
        {
            var query = from m in _context.NgoaiNgu
                        where m.IsDelete == false
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.TenNgoaiNgu.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new NgoaiNguModel()
                {
                    IsDelete = x.m.IsDelete,
                    IsStatus = x.m.IsStatus,
                    MoTa = x.m.MoTa,
                    TenNgoaiNgu = x.m.TenNgoaiNgu,
                    Id = x.m.Id
                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<NgoaiNguModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<int>> Update(int id, NgoaiNguModel request)
        {
            var NgoaiNgu = await _context.NgoaiNgu.Where(x => x.Id == id).ToListAsync();
            if (NgoaiNgu == null || NgoaiNgu.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            if (!await _context.NgonNgu.AnyAsync(x => x.Id == request.NgonNguId)) return new ApiErrorResult<int>("Ngôn ngữ không hợp lệ");

            var model = NgoaiNgu.FirstOrDefault();

            model.MoTa = request.MoTa;
            model.TenNgoaiNgu = request.TenNgoaiNgu;
            model.NgonNguId = request.NgonNguId;

            _context.NgoaiNgu.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }

        public async Task<List<NgoaiNguHoSoModel>> GetAllByHoSo(int hosoId)
        {
            var bophan = await _context.NgoaiNguHoSo.Where(v => v.HoSoId == hosoId).ToListAsync();

            var list = new List<NgoaiNguHoSoModel>();
            foreach (var m in bophan)
            {
                list.Add(new NgoaiNguHoSoModel()
                {
                    NgoaiNgu = await GetById(m.NgoaiNguId),
                    NgoaiNguId = m.NgoaiNguId,
                    HoSoId = hosoId,
                    DonViTinhId = m.DonViTinhId,
                    SoLuong = m.SoLuong
                });
            }
            return list;
        }
    }
}