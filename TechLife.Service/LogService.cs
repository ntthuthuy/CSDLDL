using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model;

namespace TechLife.Service
{
    public interface ILogService
    {
        Task<PagedResult<LogModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<bool>> Create(string message, string trace);
    }
    public class LogService : ILogService
    {
        protected readonly TLDbContext _context;
        public LogService(TLDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> Create(string message, string trace)
        {
            try
            {
                var log = new Log()
                {
                    Message = message,
                    StackTrace = trace,
                };
                _context.Logs.Add(log);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiSuccessResult<bool>(true, "Thêm thành công");
                }
                return new ApiErrorResult<bool>("Lỗi!");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>("Đã có lỗi xãy ra trong quá trình xử lý");
            }
        }

        public async Task<PagedResult<LogModel>> GetPaging(GetPagingRequest request)
        {
            var query = from m in _context.Logs
                        select new { m };

            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new LogModel()
                {
                    StackTrace = x.m.StackTrace,
                    Message = x.m.Message,
                    Time = x.m.Time,
                    UserName = x.m.UserName,
                    Id = x.m.Id
                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<LogModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }
    }
}
