using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model;
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.HoSoThanhTra;

namespace TechLife.Service
{
    public interface IHoSoThanhTraService
    {
        Task<List<HoSoThanhTraVm>> GetAll();

        Task<PagedResult<HoSoThanhTraVm>> GetPaging(ThanhTraPagingRequest request);

        Task<ApiResult<HoSoThanhTraVm>> Create(HoSoThanhTraCreateRequest request);

        Task<ApiResult<bool>> Update(int id, HoSoThanhTraUpdateRequest request);

        Task<HoSoThanhTraVm> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
    }

    public class HoSoThanhTraService : IHoSoThanhTraService
    {
        private readonly TLDbContext _context;
        private readonly ILogService _logService;
        private readonly IDuLieuDuLichService _duLieuDuLichService;

        public HoSoThanhTraService(TLDbContext context
           , ILogService logService, IDuLieuDuLichService duLieuDuLichService)
        {
            _context = context;
            _logService = logService;
            _duLieuDuLichService = duLieuDuLichService;
        }

        public async Task<ApiResult<HoSoThanhTraVm>> Create(HoSoThanhTraCreateRequest request)
        {
            var obj = new HoSoThanhTra()
            {
                HoSoId = request.HoSoId,
                KetQua = request.KetQua,
                KetLuan = request.KetLuan,
                ThoiGian = request.ThoiGian,
                NoiDung = request.NoiDung,
                TruongDoan = request.TruongDoan,
                UserId = request.UserId
            };
            _context.HoSoThanhTra.Add(obj);
            var result = await _context.SaveChangesAsync();
            if (request.DSVanBan != null)
            {
                foreach (var d in request.DSVanBan)
                {
                    var returnObjVB = new VanBanHoSoThanhTra()
                    {
                        FileName = d.FileName,
                        FilePath = d.FilePath,
                        HoSoThanhTraId = obj.Id,
                        NgayKy = d.NgayKy,
                        SoHieu = d.SoHieu,
                        TenVanBan = d.TenVanBan
                    };
                    _context.VanBanHoSoThanhTra.Add(returnObjVB);
                }
                result = await _context.SaveChangesAsync();
            }
            if (result > 0)
            {

                var returnObj = new HoSoThanhTraVm()
                {
                    HoSoId = request.HoSoId,
                    KetLuan = request.KetLuan,
                    KetQua = request.KetQua,
                    ThoiGian = request.ThoiGian,
                    NoiDung = request.NoiDung,
                    TruongDoan = request.TruongDoan,
                    UserId = request.UserId,
                    Id = obj.Id,
                    NgayTao = obj.NgayTao,
                    HoSo = await _duLieuDuLichService.GetById(obj.HoSoId)
                };
                return new ApiSuccessResult<HoSoThanhTraVm>(returnObj);
            }
            else
            {
                return new ApiErrorResult<HoSoThanhTraVm>("Đã có lỗi xãy ra!");
            }
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var hosothanhtra = await _context.HoSoThanhTra.FindAsync(id);
            if (hosothanhtra == null)
            {
                return new ApiErrorResult<int>("Không tìm thấy dữ liệu!");
            }

            hosothanhtra.IsDelete = true;

            _context.HoSoThanhTra.Update(hosothanhtra);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id, "Xóa thành công!");
            }
            return new ApiErrorResult<int>("Xóa không thành công!");
        }

        public async Task<List<HoSoThanhTraVm>> GetAll()
        {
            var query = from m in _context.HoSoThanhTra
                        where m.IsDelete == false
                        select new HoSoThanhTraVm
                        {
                            HoSoId = m.HoSoId,
                            KetLuan = m.KetLuan,
                            ThoiGian = m.ThoiGian,
                            NoiDung = m.NoiDung,
                            TruongDoan = m.TruongDoan,
                            UserId = m.UserId
                        };
            return await query.ToListAsync();
        }

        public async Task<HoSoThanhTraVm> GetById(int id)
        {
            var obj = await _context.HoSoThanhTra.FindAsync(id);

            if (obj != null)
            {
                var returnObj = new HoSoThanhTraVm()
                {
                    HoSoId = obj.HoSoId,
                    KetLuan = obj.KetLuan,
                    KetQua = obj.KetQua,
                    KetQuaThanhTra = (int)KetQuaThanhTra.KhongViPham == obj.KetQua ? StringEnum.GetStringValue(KetQuaThanhTra.KhongViPham)
                                        : (int)KetQuaThanhTra.NhacNho == obj.KetQua ? StringEnum.GetStringValue(KetQuaThanhTra.NhacNho)
                                        : StringEnum.GetStringValue(KetQuaThanhTra.KhongViPham),
                    ThoiGian = obj.ThoiGian,
                    NoiDung = obj.NoiDung,
                    TruongDoan = obj.TruongDoan,
                    UserId = obj.UserId,
                    Id = obj.Id,
                    NgayTao = obj.NgayTao,
                    HoSo = await _duLieuDuLichService.GetById(obj.HoSoId),
                    DSVanBan = _context.VanBanHoSoThanhTra.Where(v => v.HoSoThanhTraId == obj.Id).Select(v => new VanBanHoSoThanhTraVm()
                    {
                        FileName = v.FileName,
                        FilePath = v.FilePath,
                        SoHieu = v.SoHieu,
                        TenVanBan = v.TenVanBan,
                        NgayKy = v.NgayKy
                    }).ToList()
                };
                return returnObj;
            }
            else return null;
        }

        public async Task<PagedResult<HoSoThanhTraVm>> GetPaging(ThanhTraPagingRequest request)
        {
            var query = from m in _context.HoSoThanhTra
                        join h in _context.HoSo on m.HoSoId equals h.Id
                        where m.IsDelete == false
                        && (request.HoSoId == -1 || m.HoSoId == request.HoSoId)
                        && (request.KetLuanId == -1 || m.KetQua == request.KetLuanId)
                        orderby m.ThoiGian descending
                        select new { m, h };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.NoiDung.Contains(request.Keyword) || x.h.Ten.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new HoSoThanhTraVm()
                {
                    HoSoId = x.m.HoSoId,
                    KetLuan = x.m.KetLuan,
                    KetQua = x.m.KetQua,
                    ThoiGian = x.m.ThoiGian,
                    NoiDung = x.m.NoiDung,
                    TruongDoan = x.m.TruongDoan,
                    UserId = x.m.UserId,
                    Id = x.m.Id,
                    NgayTao = x.m.NgayTao,
                    KetQuaThanhTra = (int)KetQuaThanhTra.KhongViPham == x.m.KetQua ? StringEnum.GetStringValue(KetQuaThanhTra.KhongViPham)
                                        : (int)KetQuaThanhTra.NhacNho == x.m.KetQua ? StringEnum.GetStringValue(KetQuaThanhTra.NhacNho)
                                        : StringEnum.GetStringValue(KetQuaThanhTra.KhongViPham),

                    HoSo = _duLieuDuLichService.GetById(x.h.Id).Result,

                    DSVanBan = _context.VanBanHoSoThanhTra.Where(v => v.HoSoThanhTraId == x.m.Id).Select(v => new VanBanHoSoThanhTraVm()
                    {
                        FileName = v.FileName,
                        FilePath = v.FilePath,
                        SoHieu = v.SoHieu,
                        TenVanBan = v.TenVanBan,
                        NgayKy = v.NgayKy
                    }).ToList()

                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<HoSoThanhTraVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<bool>> Update(int id, HoSoThanhTraUpdateRequest request)
        {
            var obj = _context.HoSoThanhTra.Find(id);
            obj.HoSoId = request.HoSoId;
            obj.KetQua = request.KetQua;
            obj.KetLuan = request.KetLuan;
            obj.ThoiGian = request.ThoiGian;
            obj.NoiDung = request.NoiDung;
            obj.TruongDoan = request.TruongDoan;

            _context.HoSoThanhTra.Update(obj);
            var result = await _context.SaveChangesAsync();
            if (request.DSVanBan != null)
            {
                //var vanban = _context.VanBanHoSoThanhTra.Where(v => v.HoSoThanhTraId == obj.Id);
                //_context.VanBanHoSoThanhTra.RemoveRange(vanban);

                foreach (var d in request.DSVanBan)
                {
                    var returnObjVB = new VanBanHoSoThanhTra()
                    {
                        FileName = d.FileName,
                        FilePath = d.FilePath,
                        HoSoThanhTraId = obj.Id,
                        NgayKy = d.NgayKy,
                        SoHieu = d.SoHieu,
                        TenVanBan = d.TenVanBan
                    };
                    _context.VanBanHoSoThanhTra.Add(returnObjVB);
                }
                result = await _context.SaveChangesAsync();
            }
            if (result > 0) return new ApiSuccessResult<bool>(true, "Cập nhật thành công");
            else return new ApiErrorResult<bool>("Cập nhật không thành công");
        }
    }
}