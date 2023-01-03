using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model;
using TechLife.Model.PhongBan;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TechLife.Service
{
    public interface IPhongBanService
    {
        Task<PagedResult<PhongBanVm>> GetPaging(GetPagingRequest request);

        Task<ApiResult<bool>> CreateSso(List<TechLife.Model.HSCV.PhongBanVm> request);
    }
    public class PhongBanService : IPhongBanService
    {
        private readonly TLDbContext _context;
        private readonly ILogService _logService;
        public PhongBanService(TLDbContext context
            , ILogService logService)
        {
            _context = context;
            _logService = logService;
        }
        public async Task<ApiResult<bool>> CreateSso(List<TechLife.Model.HSCV.PhongBanVm> request)
        {
            if (request != null && request.Count > 0)
            {
                foreach (var item in request)
                {
                    var obj = new PhongBan()
                    {
                        MaDinhDanh = item.UniqueCode,
                        Ten = item.DepartmentName,
                        SoDienThoai = item.OfficePhone
                    };
                    _context.PhongBan.Add(obj);
                }
            }

            int result = await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>(true, "Đã đồng bộ thành công " + result + " phòng ban");
        }

        public async Task<PagedResult<PhongBanVm>> GetPaging(GetPagingRequest request)
        {
            try
            {
                var query = from m in _context.PhongBan
                            where m.IsDelete == false
                            select new { m };

                if (!string.IsNullOrEmpty(request.Keyword))
                    query = query.Where(x => x.m.Ten.Contains(request.Keyword));
                //3. Paging
                int totalRow = query.Count();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new PhongBanVm()
                    {
                        Id = x.m.Id,
                        Ten = x.m.Ten,
                        MaDinhDanh = x.m.MaDinhDanh,
                        SoDienThoai = x.m.SoDienThoai
                    }).ToListAsync();

                //4. Select and projection
                return new PagedResult<PhongBanVm>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }
    }
}
