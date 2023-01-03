using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model;

namespace TechLife.Service
{
    public interface ILoaiPhongService
    {
        Task<List<LoaiPhongModel>> GetAll(int luutruId);

        Task<PagedResult<LoaiPhongModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<LoaiPhongModel>> Create(LoaiPhongModel request);

        Task<ApiResult<int>> Update(int id, LoaiPhongModel request);

        Task<LoaiPhongModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
        Task<List<LoaiPhongHoSoModel>> GetAllByHoSo(int hosoId);
        Task<ApiResult<int>> CreateByOrgan(int organId, LoaiPhongModel request);
    }

    public class LoaiPhongService : ILoaiPhongService
    {
        private readonly TLDbContext _context;
        private readonly ILoaiGiuongService _loaiGiuongService;

        public LoaiPhongService(TLDbContext context
            , ILoaiGiuongService loaiGiuongService)
        {
            _context = context;
            _loaiGiuongService = loaiGiuongService;
        }

        public async Task<ApiResult<LoaiPhongModel>> Create(LoaiPhongModel request)
        {
            var LoaiPhong = new LoaiPhong()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                Ten = request.Ten,
                LuuTruId = request.Id
            };
            _context.LoaiPhong.Add(LoaiPhong);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = LoaiPhong.Id;
                return new ApiSuccessResult<LoaiPhongModel>(request);
            }
            return new ApiErrorResult<LoaiPhongModel>("Thêm lỗi!");
        }
        public async Task<ApiResult<int>> CreateByOrgan(int organId, LoaiPhongModel request)
        {
            var LoaiPhong = new LoaiPhong()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                Ten = request.Ten,
                LuuTruId = organId,
            };
            _context.LoaiPhong.Add(LoaiPhong);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = LoaiPhong.Id;
                return new ApiSuccessResult<int>(LoaiPhong.Id);
            }
            return new ApiErrorResult<int>("Thêm lỗi!");
        }
        public async Task<ApiResult<int>> Delete(int id)
        {
            var LoaiPhong = await _context.LoaiPhong.Where(x => x.Id == id).ToListAsync();
            if (LoaiPhong == null || LoaiPhong.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var obj = LoaiPhong.FirstOrDefault();
            obj.IsDelete = true;

            _context.LoaiPhong.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }
        public async Task<List<LoaiPhongModel>> GetAll(int luutruId)
        {
            var query = from request in _context.LoaiPhong
                        where request.IsDelete == false
                        && (request.LuuTruId == 0 || request.LuuTruId == luutruId)
                        select new LoaiPhongModel
                        {
                            IsDelete = request.IsDelete,
                            IsStatus = request.IsStatus,
                            MoTa = request.MoTa,
                            Ten = request.Ten,
                            Id = request.Id,
                            OrganId = request.LuuTruId
                        };
            return await query.ToListAsync();
        }
        public async Task<LoaiPhongModel> GetById(int id)
        {
            var loaiphong = await _context.LoaiPhong.Where(x => x.Id == id).ToListAsync();
            if (loaiphong == null || loaiphong.Count() <= 0)
            {
                throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
            }

            var obj = loaiphong.FirstOrDefault();

            return new LoaiPhongModel()
            {
                MoTa = obj.MoTa,
                Ten = obj.Ten,
                Id = obj.Id
            };
        }
        public async Task<PagedResult<LoaiPhongModel>> GetPaging(GetPagingRequest request)
        {
            var query = from p in _context.LoaiPhong
                        where p.IsDelete == false
                        select new LoaiPhong
                        {
                            IsDelete = p.IsDelete,
                            IsStatus = p.IsStatus,
                            MoTa = p.MoTa,
                            Ten = p.Ten,
                            Id = p.Id
                        };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.Ten.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new LoaiPhongModel()
                {
                    IsDelete = x.IsDelete,
                    IsStatus = x.IsStatus,
                    MoTa = x.MoTa,
                    Ten = x.Ten,
                    Id = x.Id
                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<LoaiPhongModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }
        public async Task<ApiResult<int>> Update(int id, LoaiPhongModel request)
        {
            var LoaiPhong = await _context.LoaiPhong.Where(x => x.Id == id).ToListAsync();
            if (LoaiPhong == null || LoaiPhong.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var model = LoaiPhong.FirstOrDefault();
            model.MoTa = request.MoTa;
            model.Ten = request.Ten;

            _context.LoaiPhong.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }
        public async Task<List<LoaiPhongHoSoModel>> GetAllByHoSo(int hosoId)
        {
            var loaiphong = await _context.LoaiPhong.ToListAsync();

            var list = new List<LoaiPhongHoSoModel>();
            foreach (var p in loaiphong)
            {
                var loaigiuong = new List<LoaiGiuongHoSoModel>();
                var lphs = await _context.LoaiPhongHoSo.Where(v => v.HoSoId == hosoId && v.LoaiPhongId == p.Id).ToListAsync();
                foreach (var val in lphs)
                {
                    loaigiuong.Add(new LoaiGiuongHoSoModel()
                    {
                        DienTich = val.DienTich,
                        GiaPhong = val.GiaPhong,
                        SoPhong = val.SoPhong,
                        SoTreEm = val.SoTreEm,
                        SoNguoiLon = val.SoNguoiLon,
                        Id = val.LoaiGiuongId,
                        TenHienThi = val.TenHienThi,
                    });
                }
                list.Add(new LoaiPhongHoSoModel()
                {
                    LoaiPhong = await GetById(p.Id),
                    LoaiPhongId = p.Id,
                    HoSoId = hosoId,
                    DSLoaiGiuong = loaigiuong
                });
            }

            return list;
        }
    }
}