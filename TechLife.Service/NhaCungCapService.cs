using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Data.Repositories;
using TechLife.Model.NhaCungCap;

namespace TechLife.Service
{
    public interface INhaCungCapService
    {
        Task<List<NhaCungCapVm>> GetAll();

        Task<PagedResult<NhaCungCapVm>> GetPaging(GetPagingRequest request);

        Task<ApiResult<NhaCungCapVm>> Create(NhaCungCapCreateRequets request);

        Task<ApiResult<int>> Update(int id, NhaCungCapUpdateRequets request);

        Task<NhaCungCapVm> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
    }

    public class NhaCungCapService : BaseRepository, INhaCungCapService
    {
        private readonly TLDbContext _context;
        private readonly ILogService _logService;
        public NhaCungCapService(TLDbContext context
            , ILogService logService) : base(context)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<ApiResult<NhaCungCapVm>> Create(NhaCungCapCreateRequets request)
        {
            try
            {
                var nhacungcap = new NhaCungCap()
                {
                    ChucVu = request.ChucVu,
                    EmailDoanhNghiep = request.EmailDoanhNghiep,
                    EmailNguoiDaiDien = request.EmailNguoiDaiDien,
                    MaSoDoanhNghiep = request.MaSoDoanhNghiep,
                    MoTa = request.MoTa,
                    NgayDangKy = request.NgayDangKy,
                    SDTDoanhNghiep = request.SDTDoanhNghiep,
                    SDTNguoiDaiDien = request.SDTNguoiDaiDien,
                    Ten = request.Ten,
                    TenNguoiDaiDien = request.TenNguoiDaiDien,
                    IsDelete = false,
                    IsStatus = true
                };

                var trans = await _context.Database.BeginTransactionAsync();

                int id = await base.InsertItem<NhaCungCap>(trans, nhacungcap);
                if (id != 0)
                {
                    var result = await base.CommitTransaction(trans);
                    if (result)
                    {
                        return new ApiSuccessResult<NhaCungCapVm>(await GetById(id), "Thêm thành công");
                    }
                    return new ApiErrorResult<NhaCungCapVm>("Thêm lỗi");
                }
                else
                {
                    return new ApiErrorResult<NhaCungCapVm>("Thêm lỗi!");
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
                var nhacungcap = await _context.NhaCungCap.FindAsync(id);
                if (nhacungcap == null)
                {
                    throw new TLException($"Không tìm thấy dữ liệu với id = {id}");
                }

                nhacungcap.IsDelete = true;

                _context.NhaCungCap.Update(nhacungcap);

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

        public async Task<List<NhaCungCapVm>> GetAll()
        {
            try
            {
                prams = new Dictionary<string, object>();
                prams.Add("IsDelete", false);

                var list = await base.GetAllByQueryTable<NhaCungCap>(prams);

                return list.Select(v => new NhaCungCapVm
                {
                    Id = v.Id,
                    ChucVu = v.ChucVu,
                    EmailDoanhNghiep = v.EmailDoanhNghiep,
                    EmailNguoiDaiDien = v.EmailNguoiDaiDien,
                    MaSoDoanhNghiep = v.MaSoDoanhNghiep,
                    MoTa = v.MoTa,
                    NgayDangKy = v.NgayDangKy,
                    SDTDoanhNghiep = v.SDTDoanhNghiep,
                    SDTNguoiDaiDien = v.SDTNguoiDaiDien,
                    Ten = v.Ten,
                    TenNguoiDaiDien = v.TenNguoiDaiDien,
                    IsStatus = true
                }).ToList();

            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }

        }

        public async Task<NhaCungCapVm> GetById(int id)
        {
            try
            {
                var obj = await base.GetItem<NhaCungCap>("Id", id);
                if (obj == null)
                {
                    return null;
                }

                var model = new NhaCungCapVm()
                {
                    Id = obj.Id,
                    ChucVu = obj.ChucVu,
                    EmailDoanhNghiep = obj.EmailDoanhNghiep,
                    EmailNguoiDaiDien = obj.EmailNguoiDaiDien,
                    MaSoDoanhNghiep = obj.MaSoDoanhNghiep,
                    MoTa = obj.MoTa,
                    NgayDangKy = obj.NgayDangKy,
                    SDTDoanhNghiep = obj.SDTDoanhNghiep,
                    SDTNguoiDaiDien = obj.SDTNguoiDaiDien,
                    Ten = obj.Ten,
                    TenNguoiDaiDien = obj.TenNguoiDaiDien,
                    IsStatus = true
                };

                return model;
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<PagedResult<NhaCungCapVm>> GetPaging(GetPagingRequest request)
        {
            try
            {
                var query = from m in _context.NhaCungCap
                            where m.IsDelete == false
                            select new { m };

                if (!string.IsNullOrEmpty(request.Keyword))
                    query = query.Where(x => x.m.Ten.Contains(request.Keyword));
                //3. Paging
                int totalRow = query.Count();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new NhaCungCapVm()
                    {
                        Id = x.m.Id,
                        ChucVu = x.m.ChucVu,
                        EmailDoanhNghiep = x.m.EmailDoanhNghiep,
                        EmailNguoiDaiDien = x.m.EmailNguoiDaiDien,
                        MaSoDoanhNghiep = x.m.MaSoDoanhNghiep,
                        MoTa = x.m.MoTa,
                        NgayDangKy = x.m.NgayDangKy,
                        SDTDoanhNghiep = x.m.SDTDoanhNghiep,
                        SDTNguoiDaiDien = x.m.SDTNguoiDaiDien,
                        Ten = x.m.Ten,
                        TenNguoiDaiDien = x.m.TenNguoiDaiDien,
                        IsStatus = x.m.IsStatus
                    }).ToListAsync();

                //4. Select and projection
                return new PagedResult<NhaCungCapVm>()
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

        public async Task<ApiResult<int>> Update(int id, NhaCungCapUpdateRequets request)
        {
            try
            {
                var obj = await base.GetItem<NhaCungCap>("Id", id);
                if (obj == null)
                {
                    return new ApiErrorResult<int>($"Không tìm thấy dữ liệu tương ứng với id = {id}");
                }

                obj.ChucVu = request.ChucVu;
                obj.EmailDoanhNghiep = request.EmailDoanhNghiep;
                obj.EmailNguoiDaiDien = request.EmailNguoiDaiDien;
                obj.MaSoDoanhNghiep = request.MaSoDoanhNghiep;
                obj.MoTa = request.MoTa;
                obj.NgayDangKy = request.NgayDangKy;
                obj.SDTDoanhNghiep = request.SDTDoanhNghiep;
                obj.SDTNguoiDaiDien = request.SDTNguoiDaiDien;
                obj.Ten = request.Ten;
                obj.TenNguoiDaiDien = request.TenNguoiDaiDien;

                var trans = await base.CreateTransaction();


                var result = await base.UpdateItem<NhaCungCap>(trans, obj, "Id");

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
    }
}
