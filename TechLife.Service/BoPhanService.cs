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
using TechLife.Model.BoPhan;

namespace TechLife.Service
{
    public interface IBoPhanService
    {
        Task<List<BoPhanVm>> GetAll(int linhvucId);

        Task<PagedResult<BoPhanVm>> GetPaging(GetPagingRequest request);

        Task<ApiResult<BoPhanVm>> Create(BoPhanCreateRequest request);

        Task<ApiResult<int>> Update(int id, BoPhanUpdateRequest request);
        Task<ApiResult<bool>> UpdateViTri(int id, int value);

        Task<BoPhanVm> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
        Task<List<BoPhanHoSoModel>> GetAllByHoSo(int hosoId);
    }

    public class BoPhanService : BaseRepository, IBoPhanService
    {
        private readonly TLDbContext _context;
        private readonly ILogService _logService;

        public BoPhanService(TLDbContext context
            , ILogService logService) : base(context)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<ApiResult<BoPhanVm>> Create(BoPhanCreateRequest request)
        {
            try
            {
                var bophan = new BoPhan()
                {
                    MoTa = request.MoTa,
                    TenBoPhan = request.TenBoPhan,
                    LinhVucId = string.Join(",", request.LinhVucId),
                    ViTri = _context.BoPhan.Count()
                };

                _context.BoPhan.Add(bophan);

                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<BoPhanVm>(await GetById(bophan.Id), "Thêm thành công!");
                }
                else
                {
                    return new ApiErrorResult<BoPhanVm>("Thêm không thành công!");
                }

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
                var bophan = await _context.BoPhan.Where(x => x.Id == id).ToListAsync();
                if (bophan == null || bophan.Count() <= 0)
                {
                    throw new TLException($"Không tìm thấy dữ liệu với id = {id}");
                }

                var obj = bophan.FirstOrDefault();
                obj.IsDelete = true;

                _context.BoPhan.Update(obj);

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

        public async Task<List<BoPhanVm>> GetAll(int linhvucId)
        {
            try
            {
                var query = from m in _context.BoPhan

                            where m.IsDelete == false
                            && (linhvucId == 0 || m.LinhVucId.Contains(linhvucId.ToString()))
                            orderby m.ViTri
                            select new { m };


                return await query.Select(v => new BoPhanVm
                {
                    TenBoPhan = v.m.TenBoPhan,
                    MoTa = v.m.MoTa,
                    Id = v.m.Id,
                    ViTri = v.m.ViTri,
                    LinhVucId = v.m.LinhVucId
                }).ToListAsync();

            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }

        }

        public async Task<BoPhanVm> GetById(int id)
        {
            try
            {
                var obj = await base.GetItem<BoPhan>("Id", id);
                if (obj == null)
                {
                    return null;
                }

                var model = new BoPhanVm()
                {
                    MoTa = obj.MoTa,
                    TenBoPhan = obj.TenBoPhan,
                    Id = obj.Id,
                    LinhVucId = obj.LinhVucId,
                    ViTri = obj.ViTri
                };

                return model;
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<PagedResult<BoPhanVm>> GetPaging(GetPagingRequest request)
        {
            try
            {
                var query = from m in _context.BoPhan

                            where m.IsDelete == false
                            orderby m.ViTri
                            select new { m };

                if (!string.IsNullOrEmpty(request.Keyword))
                    query = query.Where(x => x.m.TenBoPhan.Contains(request.Keyword));
                //3. Paging
                int totalRow = query.Count();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new BoPhanVm()
                    {
                        MoTa = x.m.MoTa,
                        TenBoPhan = x.m.TenBoPhan,
                        Id = x.m.Id,
                        ViTri = x.m.ViTri,
                        LinhVucId = x.m.LinhVucId

                    }).ToListAsync();

                //4. Select and projection
                return new PagedResult<BoPhanVm>()
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

        public async Task<ApiResult<int>> Update(int id, BoPhanUpdateRequest request)
        {
            try
            {
                var obj = await base.GetItem<BoPhan>("Id", id);
                if (obj == null)
                {
                    return new ApiErrorResult<int>($"Không tìm thấy dữ liệu tương ứng với id = {id}");
                }

                obj.MoTa = request.MoTa;
                obj.TenBoPhan = request.TenBoPhan;
                obj.LinhVucId = string.Join(",", request.LinhVucId);

                var trans = await base.CreateTransaction();

                var result = await base.UpdateItem<BoPhan>(trans, obj, "Id");

                if (result)
                {
                    result = await base.CommitTransaction(trans);
                }

                if (result)
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
        public async Task<List<BoPhanHoSoModel>> GetAllByHoSo(int hosoId)
        {
            try
            {
                var bophan = await _context.BoPhanHoSo.Where(v => v.HoSoId == hosoId).ToListAsync();

                var listBoPhan = new List<BoPhanHoSoModel>();
                foreach (var m in bophan)
                {
                    listBoPhan.Add(new BoPhanHoSoModel()
                    {
                        BoPhan = await GetById(m.BoPhanId),
                        BoPhanId = m.BoPhanId,
                        HoSoId = hosoId,
                        DonViTinhId = m.DonViTinhId,
                        SoLuong = m.SoLuong
                    });
                }
                return listBoPhan;

            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }

        }

        public async Task<ApiResult<bool>> UpdateViTri(int id, int value)
        {
            var obj = await _context.BoPhan.FindAsync(id);
            if (obj == null)
            {
                return new ApiErrorResult<bool>($"Không tìm thấy bộ phận với id = {id}");
            }

            obj.ViTri = value;
            _context.BoPhan.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<bool>(true, "Cập nhật thành công!");
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công!");
        }
    }
}