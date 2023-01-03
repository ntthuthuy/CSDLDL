using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Data.Repositories;
using TechLife.Model;
using TechLife.Model.Tracking;

namespace TechLife.Service
{
    public interface ITrackingService
    {

        Task<PagedResult<TrackingVm>> GetPaging(GetPagingRequest request);

        Task<ApiResult<bool>> Create(TrackingCreateRequets request);
    }

    public class TrackingService : BaseRepository, ITrackingService
    {
        private readonly TLDbContext _context;
        private readonly ILogService _logService;

        public TrackingService(TLDbContext context
            , ILogService logService) : base(context)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<ApiResult<bool>> Create(TrackingCreateRequets request)
        {
            try
            {
                var tracking = new Tracking()
                {
                    Action = request.Action,
                    UserName = request.UserName,
                    Time = request.Time,
                    //FullName = request.FullName,
                };

                var trans = await _context.Database.BeginTransactionAsync();

                int id = await base.InsertItem<Tracking>(trans, tracking);
                if (id != 0)
                {
                    var result = await base.CommitTransaction(trans);
                    if (result)
                    {
                        return new ApiSuccessResult<bool>(true, "Thêm thành công");
                    }
                    return new ApiErrorResult<bool>("Thêm lỗi");
                }
                else
                {
                    return new ApiErrorResult<bool>("Thêm lỗi!");
                }

            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<PagedResult<TrackingVm>> GetPaging(GetPagingRequest request)
        {
            try
            {
                var query = from m in _context.Trackings
                            join u in _context.Users on m.UserName equals u.UserName into user
                            from u in user.DefaultIfEmpty()
                            orderby m.Time descending
                            select new { m, u };

                if (!string.IsNullOrEmpty(request.Keyword))
                    query = query.Where(x => x.m.UserName.Contains(request.Keyword) || x.m.Action.Contains(request.Keyword));
                //3. Paging
                int totalRow = query.Count();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new TrackingVm()
                    {
                        Id = x.m.Id,
                        Action = x.m.Action,
                        FullName = x.u.FullName,
                        Time = x.m.Time,
                        UserName = x.m.UserName
                    }).ToListAsync();

                //4. Select and projection
                return new PagedResult<TrackingVm>()
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
