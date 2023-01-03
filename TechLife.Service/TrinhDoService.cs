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
    public interface ITrinhDoService
    {
        Task<List<TrinhDoModel>> GetAll();

        Task<PagedResult<TrinhDoModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<TrinhDoModel>> Create(TrinhDoModel request);

        Task<ApiResult<int>> Update(int id, TrinhDoModel request);

        Task<TrinhDoModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
        Task<List<TrinhDoHoSoModel>> GetAllByHoSo(int hosoId);
    }

    public class TrinhDoService : ITrinhDoService
    {
        private readonly TLDbContext _context;

        public TrinhDoService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<TrinhDoModel>> Create(TrinhDoModel request)
        {
            var TrinhDo = new TrinhDo()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                TenTrinhDo = request.TenTrinhDo,
            };
            _context.TrinhDo.Add(TrinhDo);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = TrinhDo.Id;
                return new ApiSuccessResult<TrinhDoModel>(request);
            }
            return new ApiErrorResult<TrinhDoModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var TrinhDo = await _context.TrinhDo.Where(x => x.Id == id).ToListAsync();
            if (TrinhDo == null || TrinhDo.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy.");
            }

            var obj = TrinhDo.FirstOrDefault();
            obj.IsDelete = true;

            _context.TrinhDo.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<TrinhDoModel>> GetAll()
        {
            var query = from m in _context.TrinhDo
                        where m.IsDelete == false
                        select new TrinhDoModel
                        {
                            Id = m.Id,
                            TenTrinhDo = m.TenTrinhDo,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            MoTa = m.MoTa
                        };
            return await query.ToListAsync();
        }

        public async Task<TrinhDoModel> GetById(int id)
        {
            var trinhdo = await _context.TrinhDo.Where(x => x.Id == id).ToListAsync();
            if (trinhdo == null || trinhdo.Count() <= 0)
            {
                throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
            }

            var obj = trinhdo.FirstOrDefault();

            return new TrinhDoModel()
            {
                MoTa = obj.MoTa,
                TenTrinhDo = obj.TenTrinhDo,
                Id = obj.Id
            };

        }

        public async Task<PagedResult<TrinhDoModel>> GetPaging(GetPagingRequest request)
        {
            var query = from m in _context.TrinhDo
                        where m.IsDelete == false
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.TenTrinhDo.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new TrinhDoModel()
                {
                    IsDelete = x.m.IsDelete,
                    IsStatus = x.m.IsStatus,
                    MoTa = x.m.MoTa,
                    TenTrinhDo = x.m.TenTrinhDo,
                    Id = x.m.Id
                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<TrinhDoModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<int>> Update(int id, TrinhDoModel request)
        {
            var TrinhDo = await _context.TrinhDo.Where(x => x.Id == id).ToListAsync();
            if (TrinhDo == null || TrinhDo.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy!");
            }

            var model = TrinhDo.FirstOrDefault();

            model.MoTa = request.MoTa;
            model.TenTrinhDo = request.TenTrinhDo;

            _context.TrinhDo.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }
        public async Task<List<TrinhDoHoSoModel>> GetAllByHoSo(int hosoId)
        {
            var bophan = await _context.TrinhDoHoSo.Where(v => v.HoSoId == hosoId).ToListAsync();

            var list = new List<TrinhDoHoSoModel>();
            foreach (var m in bophan)
            {
                list.Add(new TrinhDoHoSoModel()
                {
                    TrinhDo =await GetById(m.TrinhDoId),
                    TrinhDoId = m.TrinhDoId,
                    HoSoId = hosoId,
                    DonViTinhId = m.DonViTinhId,
                    SoLuong = m.SoLuong
                });
            }
            return list;
        }
    }
}