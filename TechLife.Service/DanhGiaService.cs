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
    public interface IDanhGiaService
    {
        Task<List<DanhGiaModel>> GetAll(int hosoId, string loai);
        Task<ApiResult<DanhGiaModel>> Create(DanhGiaModel request);
        Task<ApiResult<DanhGiaModel>> Update(DanhGiaModel request);
        Task<ApiResult<string>> Delete(int id);
        Task<DanhGiaModel> GetByID(int id);
    }

    public class DanhGiaService : IDanhGiaService
    {
        private readonly TLDbContext _context;
        private readonly ILogService _logService;


        public DanhGiaService(TLDbContext context
            , ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<ApiResult<DanhGiaModel>> Create(DanhGiaModel request)
        {
            try
            {
                var danhgia = new DanhGia()
                {
                    Email = request.Email,
                    GhiChu = request.GhiChu,
                    HoSoId = request.HoSoId,
                    HoVaTen = request.HoVaTen,
                    SoDienThoai = request.SoDienThoai,
                    SoSao = request.SoSao,
                    Loai = request.Loai
                };
                _context.DanhGia.Add(danhgia);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    request.Id = danhgia.Id;
                    return new ApiSuccessResult<DanhGiaModel>(request, "Thêm mới thành công!");
                }
                return new ApiErrorResult<DanhGiaModel>("Sửa lỗi!");
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<ApiResult<string>> Delete(int id)
        {
            try
            {
                var danhgia = await _context.DanhGia.FindAsync(id);

                _context.DanhGia.Remove(danhgia);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {

                    return new ApiSuccessResult<string>("Xóa đánh giá của " + danhgia.HoVaTen + "", "Cập nhật thành công!");
                }
                return new ApiErrorResult<string>("Xóa lỗi!");

            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<List<DanhGiaModel>> GetAll(int hosoId, string loai = "hosodulich")
        {
            try
            {
                var query = from m in _context.DanhGia
                            where m.HoSoId == hosoId && m.Loai == loai
                            select new DanhGiaModel
                            {
                                Id = m.Id,
                                Email = m.Email,
                                GhiChu = m.GhiChu,
                                HoVaTen = m.HoVaTen,
                                HoSoId = m.HoSoId,
                                SoDienThoai = m.SoDienThoai,
                                SoSao = m.SoSao
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }

        }
        public async Task<DanhGiaModel> GetByID(int id)
        {
            try
            {
                var data = await _context.DanhGia.FindAsync(id);
                var resultObj = new DanhGiaModel
                {
                    Id = data.Id,
                    Email = data.Email,
                    GhiChu = data.GhiChu,
                    HoVaTen = data.HoVaTen,
                    HoSoId = data.HoSoId,
                    SoDienThoai = data.SoDienThoai,
                    SoSao = data.SoSao
                };
                return resultObj;
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }

        }

        public async Task<ApiResult<DanhGiaModel>> Update(DanhGiaModel request)
        {
            try
            {
                var danhgia = await _context.DanhGia.FindAsync(request.Id);

                danhgia.GhiChu = request.GhiChu;
                danhgia.SoSao = request.SoSao;

                _context.DanhGia.Update(danhgia);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    request.Id = danhgia.Id;
                    return new ApiSuccessResult<DanhGiaModel>(request, "Cập nhật thành công!");
                }
                return new ApiErrorResult<DanhGiaModel>("Thêm lỗi!");

            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }

        }
    }
}
