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
    public interface IDiaPhuongService
    {
        Task<List<DiaPhuongModel>> GetAll();

        Task<List<DiaPhuongModel>> GetAllByParent(int id);

        Task<PagedResult<DiaPhuongModel>> GetPaging(GetPagingRequest request);

        Task<DiaPhuongModel> GetById(int id);

        Task<ApiResult<DiaPhuongModel>> Create(DiaPhuongModel request);

        Task<ApiResult<int>> Update(int id, DiaPhuongModel request);

        Task<ApiResult<int>> Delete(int id);
    }

    public class DiaPhuongService : IDiaPhuongService
    {
        private readonly TLDbContext _context;
        private readonly ILogService _logService;

        public DiaPhuongService(TLDbContext context,
            ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<ApiResult<DiaPhuongModel>> Create(DiaPhuongModel request)
        {
            try
            {
                var diaPhuong = new DiaPhuong()
                {
                    IsDelete = request.IsDelete,
                    IsStatus = request.IsStatus,
                    MoTa = request.MoTa,
                    ParentId = request.ParentId,
                    TenDiaPhuong = request.TenDiaPhuong
                };
                _context.DiaPhuong.Add(diaPhuong);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    request.Id = diaPhuong.Id;
                    return new ApiSuccessResult<DiaPhuongModel>(request, "Thêm thành công");
                }
                return new ApiErrorResult<DiaPhuongModel>("Thêm lỗi!");
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
                var diaPhuong = await _context.DiaPhuong.Where(x => x.Id == id).ToListAsync();
                if (diaPhuong == null || diaPhuong.Count() <= 0)
                {
                    return new ApiErrorResult<int>($"Không tìm thấy dữ liệu tương ứng với id ={id}");
                }

                var obj = diaPhuong.FirstOrDefault();
                obj.IsDelete = true;

                _context.DiaPhuong.Update(obj);

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

        public async Task<List<DiaPhuongModel>> GetAll()
        {
            try
            {
                var query = from m in _context.DiaPhuong
                            where m.IsDelete == false
                            select new DiaPhuongModel
                            {
                                Id = m.Id,
                                TenDiaPhuong = m.TenDiaPhuong,
                                IsDelete = m.IsDelete,
                                IsStatus = m.IsStatus,
                                ParentId = m.ParentId,
                                MoTa = m.MoTa
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<List<DiaPhuongModel>> GetAllByParent(int id)
        {
            try
            {
                var query = from m in _context.DiaPhuong
                            where m.ParentId == id && m.IsDelete == false
                            select new DiaPhuongModel
                            {
                                Id = m.Id,
                                TenDiaPhuong = m.TenDiaPhuong,
                                IsDelete = m.IsDelete,
                                IsStatus = m.IsStatus,
                                ParentId = m.ParentId,
                                MoTa = m.MoTa
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<DiaPhuongModel> GetById(int id)
        {
            try
            {
                var diaPhuong = await _context.DiaPhuong.Where(x => x.Id == id).ToListAsync();
                if (diaPhuong == null || diaPhuong.Count() <= 0)
                {
                    throw new TLException($"Không tìm thấy địa phương với id = {id}");
                }

                var obj = diaPhuong.FirstOrDefault();

                return new DiaPhuongModel()
                {
                    MoTa = obj.MoTa,
                    TenDiaPhuong = obj.TenDiaPhuong,
                    Id = obj.Id,
                    ParentId = obj.ParentId
                };
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<PagedResult<DiaPhuongModel>> GetPaging(GetPagingRequest request)
        {
            try
            {
                var query = from m in _context.DiaPhuong
                            where m.IsDelete == false
                            select new { m };

                if (!string.IsNullOrEmpty(request.Keyword))
                    query = query.Where(x => x.m.TenDiaPhuong.Contains(request.Keyword));
                //3. Paging
                int totalRow = query.Count();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new DiaPhuongModel()
                    {
                        IsDelete = x.m.IsDelete,
                        IsStatus = x.m.IsStatus,
                        MoTa = x.m.MoTa,
                        TenDiaPhuong = x.m.TenDiaPhuong,
                        ParentId = x.m.ParentId,
                        Id = x.m.Id
                    }).ToListAsync();

                //4. Select and projection
                return new PagedResult<DiaPhuongModel>()
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

        public async Task<ApiResult<int>> Update(int id, DiaPhuongModel request)
        {
            try
            {
                var diaPhuong = await _context.DiaPhuong.Where(x => x.Id == id).ToListAsync();
                if (diaPhuong == null || diaPhuong.Count() <= 0)
                {
                    return new ApiErrorResult<int>($"Không tìm thấy dữ liệu tương ứng với id ={id}");
                }

                var model = diaPhuong.FirstOrDefault();

                model.MoTa = request.MoTa;
                model.TenDiaPhuong = request.TenDiaPhuong;
                model.ParentId = request.ParentId;
                _context.DiaPhuong.Update(model);

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