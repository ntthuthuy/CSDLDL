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
    public interface IHanhTrinhService
    {
        Task<List<HanhTrinhModel>> GetAll();

        Task<ApiResult<PagedResult<HanhTrinhModel>>> GetPaging(GetPagingRequest request);

        Task<ApiResult<HanhTrinhModel>> Create(HanhTrinhModel request);

        Task<ApiResult<int>> Update(int id, HanhTrinhModel request);

        Task<ApiResult<HanhTrinhModel>> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
    }

    public class HanhTrinhService : IHanhTrinhService
    {
        private readonly TLDbContext _context;

        public HanhTrinhService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<HanhTrinhModel>> Create(HanhTrinhModel request)
        {
            var HanhTrinh = new HanhTrinh()
            {
                IsStatus = request.IsStatus,
                Gio = request.Gio,
                Mota = request.Mota,
                Ngay = request.Ngay,
                NoiDenId = request.NoiDenId,
                Phut = request.Phut,
                ThoiGian = request.ThoiGian,
                TourId = request.TourId,
            };
            _context.HanhTrinh.Add(HanhTrinh);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = HanhTrinh.Id;
                return new ApiSuccessResult<HanhTrinhModel>(request);
            }
            return new ApiErrorResult<HanhTrinhModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var HanhTrinh = await _context.HanhTrinh.Where(x => x.Id == id).ToListAsync();
            if (HanhTrinh == null || HanhTrinh.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy dữ liệu!");
            }

            var obj = HanhTrinh.FirstOrDefault();

            _context.HanhTrinh.Remove(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<HanhTrinhModel>> GetAll()
        {
            var query = from request in _context.HanhTrinh

                        select new HanhTrinhModel
                        {
                            IsStatus = request.IsStatus,
                            Gio = request.Gio,
                            Mota = request.Mota,
                            Ngay = request.Ngay,
                            NoiDenId = request.NoiDenId,
                            Phut = request.Phut,
                            ThoiGian = request.ThoiGian,
                            TourId = request.TourId,
                            Id = request.Id
                        };
            return await query.ToListAsync();
        }

        public async Task<ApiResult<HanhTrinhModel>> GetById(int id)
        {
            var HanhTrinh = await _context.HanhTrinh.Where(x => x.Id == id).ToListAsync();
            if (HanhTrinh == null || HanhTrinh.Count() <= 0)
            {
                return new ApiErrorResult<HanhTrinhModel>("Không tìm thấy dữ liệu!");
            }

            var request = HanhTrinh.FirstOrDefault();

            var model = new HanhTrinhModel()
            {
                Gio = request.Gio,
                Mota = request.Mota,
                Ngay = request.Ngay,
                NoiDenId = request.NoiDenId,
                Phut = request.Phut,
                ThoiGian = request.ThoiGian,
                TourId = request.TourId,
                Id = request.Id
            };

            return new ApiSuccessResult<HanhTrinhModel>(model);
        }

        public async Task<ApiResult<PagedResult<HanhTrinhModel>>> GetPaging(GetPagingRequest request)
        {
            var query = from h in _context.HanhTrinh
                        select new HanhTrinh
                        {
                            IsStatus = h.IsStatus,
                            Gio = h.Gio,
                            Mota = h.Mota,
                            Ngay = h.Ngay,
                            NoiDenId = h.NoiDenId,
                            Phut = h.Phut,
                            ThoiGian = h.ThoiGian,
                            TourId = h.TourId,
                            Id = h.Id
                        };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.Mota.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new HanhTrinhModel()
                {
                    IsStatus = x.IsStatus,
                    Gio = x.Gio,
                    Mota = x.Mota,
                    Ngay = x.Ngay,
                    NoiDenId = x.NoiDenId,
                    Phut = x.Phut,
                    ThoiGian = x.ThoiGian,
                    TourId = x.TourId,
                    Id = x.Id
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<HanhTrinhModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<HanhTrinhModel>>(pagedResult);
        }

        public async Task<ApiResult<int>> Update(int id, HanhTrinhModel request)
        {
            var HanhTrinh = await _context.HanhTrinh.Where(x => x.Id == id).ToListAsync();
            if (HanhTrinh == null || HanhTrinh.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy dữ liệu!");
            }

            var model = HanhTrinh.FirstOrDefault();
            model.IsStatus = request.IsStatus;
            model.Gio = request.Gio;
            model.Mota = request.Mota;
            model.Ngay = request.Ngay;
            model.NoiDenId = request.NoiDenId;
            model.Phut = request.Phut;
            model.ThoiGian = request.ThoiGian;
            model.TourId = request.TourId;

            _context.HanhTrinh.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }
    }
}