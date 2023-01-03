using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model;

namespace TechLife.Service
{
    public interface IDonViTinhService
    {
        Task<List<DonViTinhModel>> GetAll();

        Task<PagedResult<DonViTinhModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<DonViTinhModel>> Create(DonViTinhModel request);

        Task<ApiResult<int>> Update(int id, DonViTinhModel request);

        Task<DonViTinhModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
    }

    public class DonViTinhService : IDonViTinhService
    {
        private readonly TLDbContext _context;
        private readonly ILogService _logService;

        public DonViTinhService(TLDbContext context
            , ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<ApiResult<DonViTinhModel>> Create(DonViTinhModel request)
        {
            try
            {
                var donViTinh = new DonViTinh()
                {
                    IsDelete = request.IsDelete,
                    IsStatus = request.IsStatus,
                    Ten = request.Ten,
                };
                _context.DonViTinh.Add(donViTinh);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    request.Id = donViTinh.Id;
                    return new ApiSuccessResult<DonViTinhModel>(request, "Thêm mới thành công!");
                }
                return new ApiErrorResult<DonViTinhModel>("Thêm lỗi!");
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }

        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            try
            {
                var donViTinh = await _context.DonViTinh.Where(x => x.Id == id).ToListAsync();
                if (donViTinh == null || donViTinh.Count() <= 0)
                {
                    throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
                }

                var obj = donViTinh.FirstOrDefault();
                obj.IsDelete = true;

                _context.DonViTinh.Update(obj);

                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<int>(id, "Xóa thành công!");
                }
                return new ApiErrorResult<int>("Xóa lỗi!");
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<List<DonViTinhModel>> GetAll()
        {
            try
            {
                var query = from m in _context.DonViTinh
                            where m.IsDelete == false
                            select new DonViTinhModel
                            {
                                Id = m.Id,
                                Ten = m.Ten,
                                IsDelete = m.IsDelete,
                                IsStatus = m.IsStatus,
                            };
                return await query.ToListAsync();
            }
            catch(Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
          
        }

        public async Task<DonViTinhModel> GetById(int id)
        {
            try
            {
                var donvitrinh = await _context.DonViTinh.Where(x => x.Id == id).ToListAsync();
                if (donvitrinh == null || donvitrinh.Count() <= 0)
                {
                    throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
                }

                var obj = donvitrinh.FirstOrDefault();

                return new DonViTinhModel()
                {
                    Ten = obj.Ten,
                    Id = obj.Id
                };
            }
            catch(Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<PagedResult<DonViTinhModel>> GetPaging(GetPagingRequest request)
        {
            try
            {
                var query = from m in _context.DonViTinh
                            where m.IsDelete == false
                            select new { m };

                if (!string.IsNullOrEmpty(request.Keyword))
                    query = query.Where(x => x.m.Ten.Contains(request.Keyword));
                //3. Paging
                int totalRow = query.Count();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new DonViTinhModel()
                    {
                        IsDelete = x.m.IsDelete,
                        IsStatus = x.m.IsStatus,
                        Ten = x.m.Ten,
                        Id = x.m.Id
                    }).ToListAsync();

                //4. Select and projection
                return new PagedResult<DonViTinhModel>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
            }
            catch(Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<ApiResult<int>> Update(int id, DonViTinhModel request)
        {
            try
            {
                var donViTinh = await _context.DonViTinh.Where(x => x.Id == id).ToListAsync();
                if (donViTinh == null || donViTinh.Count() <= 0)
                {
                    throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
                }

                var model = donViTinh.FirstOrDefault();

                model.Ten = request.Ten;

                _context.DonViTinh.Update(model);

                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<int>(id, "Sửa thành công!");
                }
                return new ApiErrorResult<int>("Sửa lỗi!");
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }
    }
}