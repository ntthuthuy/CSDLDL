using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model.BinhLuan;
using TechLife.Service.Common;

namespace TechLife.Service
{
    public interface IBinhLuanService
    {
        Task<PagedResult<BinhLuanVm>> GetAll(string type, int id);

        Task<PagedResult<BinhLuanVm>> GetPaging(string type, int id, GetPagingRequest request);

        Task<ApiResult<BinhLuanVm>> Create(BinhLuanCreateRequest request);

        Task<ApiResult<int>> Update(int id, BinhLuanUpdateRequest request);


        Task<BinhLuanVm> GetById(int id);

        Task<ApiResult<int>> Delete(int id);

    }

    public class BinhLuanService : IBinhLuanService
    {
        private readonly TLDbContext _context;

        public BinhLuanService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<BinhLuanVm>> Create(BinhLuanCreateRequest request)
        {
            var obj = new BinhLuan()
            {
                AvataUrl = request.AvataUrl,
                NgayBinhLuan = DateTime.Now,
                Ten = request.Ten,
                NoiDung = request.NoiDung,
                Type = request.Type,
                HoSoId = request.HoSoId,
            };

            _context.BinhLuan.Add(obj);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<BinhLuanVm>(await GetById(obj.Id));
            }
            return new ApiErrorResult<BinhLuanVm>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var obj = await _context.BinhLuan.FindAsync(id);

            obj.IsDelete = true;

            _context.BinhLuan.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<PagedResult<BinhLuanVm>> GetAll(string type, int id)
        {
            var query = from m in _context.BinhLuan
                        where m.IsDelete == false
                        && m.Type == type && m.HoSoId == id

                        select new BinhLuanVm
                        {
                            Id = m.Id,
                            Ten = m.Ten,
                            AvataUrl = m.AvataUrl,
                            NgayBinhLuan = m.NgayBinhLuan,
                            NoiDung = m.NoiDung
                        };

            return new PagedResult<BinhLuanVm>()
            {
                TotalRecords = query.Count(),
                PageIndex = 1,
                PageSize = 1,
                Items = await query.ToListAsync()
            };

        }

        public async Task<BinhLuanVm> GetById(int id)
        {
            var obj = await _context.BinhLuan.FindAsync(id);

            return new BinhLuanVm()
            {
                Id = obj.Id,
                Ten = obj.Ten,
                AvataUrl = obj.AvataUrl,
                NgayBinhLuan = obj.NgayBinhLuan,
                NoiDung = obj.NoiDung
            };
        }

        public async Task<PagedResult<BinhLuanVm>> GetPaging(string type, int id, GetPagingRequest request)
        {
            var query = from m in _context.BinhLuan
                        where m.IsDelete == false && m.Type == type && m.HoSoId == id
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.Ten.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();


            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(x => new BinhLuanVm()
            {
                Id = x.m.Id,
                Ten = x.m.Ten,
                AvataUrl = x.m.AvataUrl,
                NgayBinhLuan = x.m.NgayBinhLuan,
                NoiDung = x.m.NoiDung
            }).ToListAsync();


            data = data.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();

            //4. Select and projection
            return new PagedResult<BinhLuanVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }


        public async Task<ApiResult<int>> Update(int id, BinhLuanUpdateRequest request)
        {
            var obj = await _context.BinhLuan.FindAsync(id);

            obj.NoiDung = request.NoiDung;

            _context.BinhLuan.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }

    }
}