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
using TechLife.Model.ThietBi;
using TechLife.Service.Common;
namespace TechLife.Service
{
    public interface IThietBiService
    {
        Task<ApiResult<bool>> Create(ThietBiCreateRequest request);
        Task<ApiResult<int>> Count();

    }
    public class ThietBiService : IThietBiService
    {
        private readonly TLDbContext _context;
        public ThietBiService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<int>> Count()
        {
            var data = await _context.ThietBi.ToListAsync();

            return new ApiSuccessResult<int>(data.Count());
        }

        public async Task<ApiResult<bool>> Create(ThietBiCreateRequest request)
        {

            var dataObj = _context.ThietBi.Where(v => v.MaThietBi == request.MaThietBi);
            if (dataObj.Count() > 0)
            {
                var firtsObj = await dataObj.FirstOrDefaultAsync();
                firtsObj.NgaySuDungMoiNhat = DateTime.Now;
                firtsObj.UserName = request.UserName;
                firtsObj.LoaiThietBi = request.LoaiThietBi;
                _context.ThietBi.Update(firtsObj);
            }
            else
            {
                var obj = new ThietBi()
                {
                    MaThietBi = request.MaThietBi,
                    UserName = request.UserName,
                    LoaiThietBi = request.LoaiThietBi,
                    NgaySuDungMoiNhat = DateTime.Now,
            };
                await _context.ThietBi.AddAsync(obj);
            }

            var resultObj = await _context.SaveChangesAsync();
            if (resultObj > 0)
                return new ApiSuccessResult<bool>(true, "Thêm thiết bị thành công");
            else return new ApiErrorResult<bool>("Thêm thiết bị không thành công");
        }
    }
}