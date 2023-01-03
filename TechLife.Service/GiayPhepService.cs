using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model.GiayPhepChungChi;

namespace TechLife.Service
{
    public interface IGiayPhepService
    {
        Task<PagedResult<GiayPhepVm>> GetPaging(int loaihinhId, GetPagingRequest request);

        Task<ApiResult<GiayPhepVm>> Create(GiayPhepCreateRequest request);

        Task<ApiResult<bool>> Update(int id, GiayPhepUpdateRequest request);

        Task<GiayPhepVm> GetById(int id);

        Task<List<GiayPhepVm>> GetAll(int loaihinhId);

        Task<ApiResult<bool>> Delete(int id);
    }

    public class GiayPhepService : IGiayPhepService
    {
        private readonly TLDbContext _context;

        public GiayPhepService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<GiayPhepVm>> Create(GiayPhepCreateRequest request)
        {
            var giayphep = new GiayPhep()
            {
                Ten = request.Ten,
                LinhVucId = string.Join(",", request.LoaiHinhId)
            };
            _context.GiayPhep.Add(giayphep);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<GiayPhepVm>(await GetById(giayphep.Id), "Thêm thành công");
            }
            return new ApiErrorResult<GiayPhepVm>("Thêm không thành công!");
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var query = await _context.GiayPhep.FindAsync(id);
            if (query == null)
            {
                return new ApiErrorResult<bool>($"Không tìm thấy giấy phép với id {id}!");
            }
            _context.Remove(query);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<bool>(true, $"Xóa thành công!");
            }
            return new ApiErrorResult<bool>($"Xóa không thành công!");
        }

        public async Task<List<GiayPhepVm>> GetAll(int loaihinhId)
        {
            var query = from m in _context.GiayPhep
                        where (loaihinhId == -1 || m.LinhVucId.Contains(loaihinhId.ToString()))
                        select new { m };

            return await query
                   .Select(x => new GiayPhepVm()
                   {
                       Ten = x.m.Ten,
                       Id = x.m.Id,
                       LinhVucId = x.m.LinhVucId
                   }).ToListAsync();
        }

        public async Task<GiayPhepVm> GetById(int id)
        {
            var query = await _context.GiayPhep.FindAsync(id);
            if (query == null)
            {
                return null;
            }
            return new GiayPhepVm()
            {
                Id = query.Id,
                Ten = query.Ten,
                LinhVucId = query.LinhVucId
            };
        }

        public async Task<PagedResult<GiayPhepVm>> GetPaging(int loaihinhId, GetPagingRequest request)
        {
            var query = from m in _context.GiayPhep
                        where (loaihinhId == -1 || m.LinhVucId.Contains(loaihinhId.ToString()))
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.Ten.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new GiayPhepVm()
                {
                    Ten = x.m.Ten,
                    Id = x.m.Id,
                    LinhVucId = x.m.LinhVucId
                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<GiayPhepVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<bool>> Update(int id, GiayPhepUpdateRequest request)
        {
            var giayphep = await _context.GiayPhep.FindAsync(id);
            if (giayphep == null)
            {
                return new ApiErrorResult<bool>($"Lỗi! Không tìm thấy với id :{id} ");
            }

            giayphep.Ten = request.Ten;
            giayphep.LinhVucId = string.Join(",", request.LoaiHinhId);

            _context.GiayPhep.Update(giayphep);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<bool>(true, "Cập nhật thành công!");
            }
            return new ApiErrorResult<bool>("Sửa lỗi!");
        }
    }
}