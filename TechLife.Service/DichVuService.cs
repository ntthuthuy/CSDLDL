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
    public interface IDichVuService
    {
        Task<List<DichVuModel>> GetAll();

        Task<PagedResult<DichVuModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<DichVuModel>> Create(DichVuModel request);

        Task<ApiResult<int>> Update(int id, DichVuModel request);

        Task<DichVuModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);

        Task<List<VeDichVuHoSoModel>> GetAllByHoSo(int hosoId);
    }

    public class DichVuService : IDichVuService
    {
        private readonly TLDbContext _context;
        private readonly ILogService _logService;
        public DichVuService(TLDbContext context
            , ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<ApiResult<DichVuModel>> Create(DichVuModel request)
        {
            try
            {
                var dichVu = new DichVu()
                {
                    IsDelete = request.IsDelete,
                    IsStatus = request.IsStatus,
                    MoTa = request.MoTa,
                    TenDichVu = request.TenDichVu,
                    DVT = request.DVT,
                    LoaiId = request.LoaiId,
                    SucChua = request.SucChua,
                };
                _context.DichVu.Add(dichVu);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    request.Id = dichVu.Id;
                    return new ApiSuccessResult<DichVuModel>(request, "Thêm mới thành công!");
                }
                return new ApiErrorResult<DichVuModel>("Thêm lỗi!");
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
                var dichVu = await _context.DichVu.Where(x => x.Id == id).ToListAsync();
                if (dichVu == null || dichVu.Count() <= 0)
                {
                    throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
                }

                var obj = dichVu.FirstOrDefault();

                obj.IsDelete = true;

                _context.DichVu.Update(obj);

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

        public async Task<List<DichVuModel>> GetAll()
        {
            try
            {
                var query = from m in _context.DichVu
                            where m.IsDelete == false
                            select new DichVuModel
                            {
                                Id = m.Id,
                                TenDichVu = m.TenDichVu,
                                IsDelete = m.IsDelete,
                                IsStatus = m.IsStatus,
                                MoTa = m.MoTa,
                                SucChua = m.SucChua,
                                LoaiId = m.LoaiId,
                                DVT = m.DVT
                            };

                return await query.ToListAsync();
            }
            catch(Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
            
        }

        public async Task<DichVuModel> GetById(int id)
        {
            try
            {
                var dichVu = await _context.DichVu.Where(x => x.Id == id).ToListAsync();
                if (dichVu == null || dichVu.Count() <= 0)
                {
                    throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
                }

                var obj = dichVu.FirstOrDefault();

                return new DichVuModel()
                {
                    MoTa = obj.MoTa,
                    TenDichVu = obj.TenDichVu,
                    DVT = obj.DVT,
                    LoaiId = obj.LoaiId,
                    SucChua = obj.SucChua,
                    Id = obj.Id
                };
            }
            catch(Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<PagedResult<DichVuModel>> GetPaging(GetPagingRequest request)
        {
            try
            {
                var query = from m in _context.DichVu
                            where m.IsDelete == false
                            select new { m };

                if (!string.IsNullOrEmpty(request.Keyword))
                    query = query.Where(x => x.m.TenDichVu.Contains(request.Keyword));
                //3. Paging
                int totalRow = query.Count();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new DichVuModel()
                    {
                        IsDelete = x.m.IsDelete,
                        IsStatus = x.m.IsStatus,
                        MoTa = x.m.MoTa,
                        TenDichVu = x.m.TenDichVu,
                        Id = x.m.Id,
                        SucChua = x.m.SucChua,
                        LoaiId = x.m.LoaiId,
                        DVT = x.m.DVT
                    }).ToListAsync();

                //4. Select and projection
                return new PagedResult<DichVuModel>()
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

        public async Task<ApiResult<int>> Update(int id, DichVuModel request)
        {
            try
            {
                var dichVu = await _context.DichVu.Where(x => x.Id == id).ToListAsync();
                if (dichVu == null || dichVu.Count() <= 0)
                {
                    throw new TLException($"Không tìm thấy bảng ghi nào tương ứng với id = {id}");
                }

                var model = dichVu.FirstOrDefault();

                model.MoTa = request.MoTa;
                model.TenDichVu = request.TenDichVu;
                model.DVT = request.DVT;
                model.SucChua = request.SucChua;

                _context.DichVu.Update(model);

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

        public async Task<List<VeDichVuHoSoModel>> GetAllByHoSo(int hosoId)
        {
            try
            {
                var thucDon = await _context.VeDichVuHoSo.Where(v => v.HosoId == hosoId).ToListAsync();

                var listThucDon = new List<VeDichVuHoSoModel>();
                foreach (var m in thucDon)
                {
                    listThucDon.Add(new VeDichVuHoSoModel()
                    {
                        MoTa = m.MoTa,
                        GiaVe = m.GiaVe,
                        HosoId = m.HosoId,
                        TenVe = m.TenVe,
                        Id = m.Id
                    });
                }
                return listThucDon;
            }
            catch(Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
            
        }
    }
}