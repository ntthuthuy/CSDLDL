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
    public interface IDiemVeSinhService
    {
        Task<List<DiemVeSinhModel>> GetAll();

        Task<PagedResult<DiemVeSinhModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<DiemVeSinhModel>> Create(DiemVeSinhModel request);

        Task<ApiResult<int>> Update(int id, DiemVeSinhModel request);

        Task<DiemVeSinhModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
    }

    public class DiemVeSinhService : IDiemVeSinhService
    {
        private readonly TLDbContext _context;
        private readonly ILogService _logService;


        public DiemVeSinhService(TLDbContext context
            , ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<ApiResult<DiemVeSinhModel>> Create(DiemVeSinhModel request)
        {
            try
            {
                var DiemVeSinh = new DiemVeSinh()
                {
                    IsDelete = request.IsDelete,
                    IsStatus = request.IsStatus,
                    MoTa = request.MoTa,
                    ViTri = request.ViTri,
                    Ten = request.Ten,
                    NguonDongBo = 0
                };
                _context.DiemVeSinh.Add(DiemVeSinh);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    request.Id = DiemVeSinh.Id;
                    return new ApiSuccessResult<DiemVeSinhModel>(request, "Thêm thành công");
                }
                return new ApiErrorResult<DiemVeSinhModel>("Thêm lỗi");
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
                var DiemVeSinh = await _context.DiemVeSinh.Where(x => x.Id == id).ToListAsync();
                if (DiemVeSinh == null || DiemVeSinh.Count() <= 0)
                {
                    throw new TLException($"Không tìm thấy dữ liệu với id = {id}");
                }

                var obj = DiemVeSinh.FirstOrDefault();
                obj.IsDelete = true;

                _context.DiemVeSinh.Update(obj);

                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<int>(id, "Xóa thành công");
                }
                return new ApiErrorResult<int>("Không xóa được bộ phận");
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<List<DiemVeSinhModel>> GetAll()
        {
            try
            {
                var query = from m in _context.DiemVeSinh
                            where m.IsDelete == false
                            select new DiemVeSinhModel
                            {
                                Id = m.Id,
                                ViTri = m.ViTri,
                                IsDelete = m.IsDelete,
                                IsStatus = m.IsStatus,
                                MoTa = m.MoTa ,
                                Ten =m.Ten
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }

        }

        public async Task<DiemVeSinhModel> GetById(int id)
        {
            try
            {
                var DiemVeSinh = await _context.DiemVeSinh.Where(x => x.Id == id).ToListAsync();
                if (DiemVeSinh == null || DiemVeSinh.Count() <= 0)
                {
                    throw new TLException($"Không tìm thấy dữ liệu với id = {id}");
                }

                var obj = DiemVeSinh.FirstOrDefault();

                var model = new DiemVeSinhModel()
                {
                    MoTa = obj.MoTa,
                    ViTri = obj.ViTri,
                    Id = obj.Id,
                    Ten =obj.Ten,
                    //HueCIT
                    X = obj.X,
                    Y = obj.Y,
                };

                return model;
            }
            catch(Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<PagedResult<DiemVeSinhModel>> GetPaging(GetPagingRequest request)
        {
            try
            {
                var query = from m in _context.DiemVeSinh
                            where m.IsDelete == false
                            select new { m };

                if (!string.IsNullOrEmpty(request.Keyword))
                    query = query.Where(x => x.m.ViTri.Contains(request.Keyword));
                // HUECIT
                // NGUỒN ĐỒNG BỘ
                if (request.NguonDongBo >= 0)
                {
                    query = query.Where(x => x.m.NguonDongBo == request.NguonDongBo);
                }

                //3. Paging
                int totalRow = query.Count();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new DiemVeSinhModel()
                    {
                        IsDelete = x.m.IsDelete,
                        IsStatus = x.m.IsStatus,
                        MoTa = x.m.MoTa,
                        ViTri = x.m.ViTri,
                        Ten = x.m.Ten,
                        Id = x.m.Id,
                        X = x.m.X,
                        Y = x.m.Y,
                        NguonDongBo = x.m.NguonDongBo
                    }).ToListAsync();

                //4. Select and projection
                return new PagedResult<DiemVeSinhModel>()
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

        public async Task<ApiResult<int>> Update(int id, DiemVeSinhModel request)
        {
            try
            {
                var DiemVeSinh = await _context.DiemVeSinh.Where(x => x.Id == id).ToListAsync();
                if (DiemVeSinh == null || DiemVeSinh.Count() <= 0)
                {
                    throw new TLException($"Không tìm thấy dữ liệu với id = {id}");
                }

                var model = DiemVeSinh.FirstOrDefault();

                model.MoTa = request.MoTa;
                model.ViTri = request.ViTri;
                model.Ten = request.Ten;

                _context.DiemVeSinh.Update(model);

                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<int>(id, "Sửa thành công");
                }
                return new ApiErrorResult<int>("Không sửa được bộ phận");
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }

        }
        
    }
}