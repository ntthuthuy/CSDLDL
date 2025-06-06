using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model;
using TechLife.Model.TienNghi;

namespace TechLife.Service
{
    public interface ITienNghiService
    {
        Task<List<TienNghiVm>> GetAll(int linhvucId, string ngonNguId = SystemConstants.DefaultLanguage);

        Task<PagedResult<TienNghiVm>> GetPaging(int linhvucId, GetPagingRequest request);

        Task<ApiResult<TienNghiVm>> Create(TienNghiCreateRequest request);

        Task<ApiResult<int>> Update(int id, TienNghiUpdateRequest request);
        Task<ApiResult<bool>> UpdateViTri(int id, int value);

        Task<TienNghiVm> GetById(int id);

        Task<ApiResult<int>> Delete(int id);
        Task<List<TienNghiHoSoModel>> GetAllByHoSo(int hosoId);
        Task<List<TienNghiHoSoModel>> GetAllByHoSoUsing(int hosoId);
    }

    public class TienNghiService : ITienNghiService
    {
        private readonly TLDbContext _context;

        public TienNghiService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<TienNghiVm>> Create(TienNghiCreateRequest request)
        {
            if (!await _context.NgonNgu.AnyAsync(x => x.Id == request.NgonNguId)) return new ApiErrorResult<TienNghiVm>("Ngôn ngữ không hợp lệ");

            if (request.LinhVucId == null || request.LinhVucId.Length == 0) return new ApiErrorResult<TienNghiVm>("Vui lòng nhập đầy đủ thông tin");

            var tiennghi = new TienNghi()
            {
                MoTa = request.MoTa,
                Ten = request.Ten,
                DonViTinhId = request.DonViTinhId,
                LinhVucId = string.Join(',', request.LinhVucId),
                ViTri = _context.TienNghi.Count(),
                NgonNguId = request.NgonNguId
            };
            _context.TienNghi.Add(tiennghi);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<TienNghiVm>(await GetById(tiennghi.Id), "Thêm tiện nghi thành công!");
            }
            return new ApiErrorResult<TienNghiVm>("Thêm tiện nghi không thành công!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var TienNghi = await _context.TienNghi.Where(x => x.Id == id).ToListAsync();
            if (TienNghi == null || TienNghi.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy dữ liệu!");
            }

            var obj = TienNghi.FirstOrDefault();
            obj.IsDelete = true;

            _context.TienNghi.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<TienNghiVm>> GetAll(int linhvucId, string ngonNguId)
        {
            var query = from m in _context.TienNghi
                        where !m.IsDelete && m.NgonNguId == ngonNguId
                        orderby m.ViTri
                        select new TienNghiVm
                        {
                            Id = m.Id,
                            Ten = m.Ten,
                            MoTa = m.MoTa,
                            DonViTinhId = m.DonViTinhId,
                            DonViTinhName = m.DonViTinh.Ten,
                            LinhVucId = m.LinhVucId,
                            ViTri = m.ViTri,
                            NgonNguId = m.NgonNguId,
                        };

            var data = await query.ToListAsync();

            return data.Where(v => (linhvucId == 0 || v.LinhVucId.Split(',').Contains(linhvucId.ToString()))).ToList();
        }

        public async Task<TienNghiVm> GetById(int id)
        {
            var obj = await _context.TienNghi.FindAsync(id);
            if (obj != null)
            {
                return new TienNghiVm()
                {
                    MoTa = obj.MoTa,
                    Ten = obj.Ten,
                    Id = obj.Id,
                    DonViTinhId = obj.DonViTinhId,
                    LinhVucId = obj.LinhVucId,
                    NgonNguId = obj.NgonNguId
                };
            }
            else return null;
        }

        public async Task<PagedResult<TienNghiVm>> GetPaging(int linhvucId, GetPagingRequest request)
        {
            var query = from m in _context.TienNghi
                        where m.IsDelete == false
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.Ten.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new TienNghiVm()
                {
                    MoTa = x.m.MoTa,
                    Ten = x.m.Ten,
                    Id = x.m.Id,
                    DonViTinhId = x.m.DonViTinhId,
                    LinhVucId = x.m.LinhVucId

                }).ToListAsync();

            //4. Select and projection
            return new PagedResult<TienNghiVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data.Where(v => (linhvucId == 0 || v.LinhVucId.Split(',').Contains(linhvucId.ToString()))).ToList()
            };
        }

        public async Task<ApiResult<int>> Update(int id, TienNghiUpdateRequest request)
        {
            var model = await _context.TienNghi.FindAsync(id);

            if (model == null) return new ApiErrorResult<int>($"Không tìm thấy tiện nghị với id = {id}");

            if (!await _context.NgonNgu.AnyAsync(x => x.Id == request.NgonNguId)) return new ApiErrorResult<int>("Ngôn ngữ không hợp lệ");

            model.MoTa = request.MoTa;
            model.Ten = request.Ten;
            model.LinhVucId = string.Join(',', request.LinhVucId);
            model.DonViTinhId = request.DonViTinhId;
            model.NgonNguId = request.NgonNguId;

            _context.TienNghi.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id, "Cập nhật tiện nghi thành công!");
            }
            return new ApiErrorResult<int>("Cập nhật tiện nghi thành công!");
        }
        public async Task<List<TienNghiHoSoModel>> GetAllByHoSo(int hosoId)
        {
            var tiennghi = await _context.TienNghiHoSo.Where(v => v.HoSoId == hosoId).ToListAsync();

            var list = new List<TienNghiHoSoModel>();
            foreach (var m in tiennghi)
            {
                list.Add(new TienNghiHoSoModel()
                {
                    TienNghi = await GetById(m.TienNghiId),
                    TienNghiId = m.TienNghiId,
                    HoSoId = hosoId,
                    SoLuong = m.SoLuong,
                    IsPhuPhi = m.IsPhuPhi,
                    IsSuDung = m.IsSuDung
                });
            }
            return list;
        }
        public async Task<List<TienNghiHoSoModel>> GetAllByHoSoUsing(int hosoId)
        {
            var tiennghi = await _context.TienNghiHoSo.Where(v => v.HoSoId == hosoId && v.IsSuDung).ToListAsync();

            var list = new List<TienNghiHoSoModel>();
            foreach (var m in tiennghi)
            {
                list.Add(new TienNghiHoSoModel()
                {
                    TienNghi = await GetById(m.TienNghiId),
                    TienNghiId = m.TienNghiId,
                    HoSoId = hosoId,
                    SoLuong = m.SoLuong,
                    IsPhuPhi = m.IsPhuPhi,
                    IsSuDung = m.IsSuDung
                });
            }
            return list;
        }
        public async Task<ApiResult<bool>> UpdateViTri(int id, int value)
        {
            var model = await _context.TienNghi.FindAsync(id);

            if (model == null) return new ApiErrorResult<bool>($"Không tìm thấy tiện nghị với id = {id}");

            model.ViTri = value;
            _context.TienNghi.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<bool>(true, "Cập nhật thành công!");
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công!");

        }
    }
}