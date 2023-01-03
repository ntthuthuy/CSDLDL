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
    public interface IMucDoThongThaoNgoaiNguService
    {
        Task<List<MucDoThongThaoNgoaiNguModel>> GetAll();

        Task<PagedResult<MucDoThongThaoNgoaiNguModel>> GetPaging(GetPagingRequest request);

        Task<ApiResult<MucDoThongThaoNgoaiNguModel>> Create(MucDoThongThaoNgoaiNguModel request);

        Task<ApiResult<int>> Update(int id, MucDoThongThaoNgoaiNguModel request);

        Task<MucDoThongThaoNgoaiNguModel> GetById(int id);

        Task<ApiResult<int>> Delete(int id);

        Task<List<MucDoTTNNHoSoModel>> GetAllByHoSo(int hosoId);
    }

    public class MucDoThongThaoNgoaiNguService : IMucDoThongThaoNgoaiNguService
    {
        private readonly TLDbContext _context;

        public MucDoThongThaoNgoaiNguService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<MucDoThongThaoNgoaiNguModel>> Create(MucDoThongThaoNgoaiNguModel request)
        {
            var MucDoThongThaoNgoaiNgu = new MucDoThongThaoNgoaiNgu()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                MucDo = request.MucDo,
            };
            _context.MucDoThongThaoNgoaiNgu.Add(MucDoThongThaoNgoaiNgu);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = MucDoThongThaoNgoaiNgu.Id;
                return new ApiSuccessResult<MucDoThongThaoNgoaiNguModel>(request);
            }
            return new ApiErrorResult<MucDoThongThaoNgoaiNguModel>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var MucDoThongThaoNgoaiNgu = await _context.MucDoThongThaoNgoaiNgu.Where(x => x.Id == id).ToListAsync();
            if (MucDoThongThaoNgoaiNgu == null || MucDoThongThaoNgoaiNgu.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var obj = MucDoThongThaoNgoaiNgu.FirstOrDefault();
            obj.IsDelete = true;

            _context.MucDoThongThaoNgoaiNgu.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<MucDoThongThaoNgoaiNguModel>> GetAll()
        {
            var query = from m in _context.MucDoThongThaoNgoaiNgu
                        where m.IsDelete == false
                        select new MucDoThongThaoNgoaiNguModel
                        {
                            Id = m.Id,
                            MucDo = m.MucDo,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            MoTa = m.MoTa
                        };
            return await query.ToListAsync();
        }

        public async Task<MucDoThongThaoNgoaiNguModel> GetById(int id)
        {
            var mucdo = await _context.MucDoThongThaoNgoaiNgu.Where(x => x.Id == id).ToListAsync();
            if (mucdo == null || mucdo.Count() <= 0)
            {
                throw new TLException($"Không tìm thấy bảng ghi nào với id = {id}");
            }

            var obj = mucdo.FirstOrDefault();

            return new MucDoThongThaoNgoaiNguModel()
            {
                MoTa = obj.MoTa,
                MucDo = obj.MucDo,
                Id = obj.Id
            };

        }

        public async Task<PagedResult<MucDoThongThaoNgoaiNguModel>> GetPaging(GetPagingRequest request)
        {
            var query = from m in _context.MucDoThongThaoNgoaiNgu
                        where m.IsDelete == false
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.MucDo.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new MucDoThongThaoNgoaiNguModel()
                {
                    IsDelete = x.m.IsDelete,
                    IsStatus = x.m.IsStatus,
                    MoTa = x.m.MoTa,
                    MucDo = x.m.MucDo,
                    Id = x.m.Id
                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<MucDoThongThaoNgoaiNguModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
        }

        public async Task<ApiResult<int>> Update(int id, MucDoThongThaoNgoaiNguModel request)
        {
            var MucDoThongThaoNgoaiNgu = await _context.MucDoThongThaoNgoaiNgu.Where(x => x.Id == id).ToListAsync();
            if (MucDoThongThaoNgoaiNgu == null || MucDoThongThaoNgoaiNgu.Count() <= 0)
            {
                return new ApiErrorResult<int>("Lỗi! Không tìm thấy.");
            }

            var model = MucDoThongThaoNgoaiNgu.FirstOrDefault();

            model.MoTa = request.MoTa;
            model.MucDo = request.MucDo;

            _context.MucDoThongThaoNgoaiNgu.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }

        public async Task<List<MucDoTTNNHoSoModel>> GetAllByHoSo(int hosoId)
        {
            var bophan = await _context.MucDoTTNNHoSo.Where(v => v.HoSoId == hosoId).ToListAsync();

            var list = new List<MucDoTTNNHoSoModel>();
            foreach (var m in bophan)
            {
                list.Add(new MucDoTTNNHoSoModel()
                {
                    MucDoThongThao = await GetById(m.MucDoId),
                    MucDoId = m.MucDoId,
                    HoSoId = hosoId,
                    DonViTinhId = m.DonViTinhId,
                    SoLuong = m.SoLuong
                });
            }
            return list;
        }
    }
}