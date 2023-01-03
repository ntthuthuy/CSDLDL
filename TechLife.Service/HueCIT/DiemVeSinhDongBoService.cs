using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Data.Repositories;
using System.Linq;
using TechLife.Model.HueCIT;

namespace TechLife.Service.HueCIT
{
    public interface IDiemVeSinhDongBoService
    {
        Task<List<DiemVeSinhModel>> GetAll(int nguondongbo);

        Task<DiemVeSinhModel> GetByDongBoId(int? id);

        Task<DiemVeSinhModel> Create(DiemVeSinhModel request);

        Task<int> Update(int id, DiemVeSinhModel request);

        Task<DiemVeSinhModelBanDo> GetById(int Id);

        Task<DiemVeSinh> EditBanDo(int id, HoSoBanDo request);

    }

    public class DiemVeSinhDongBoService : BaseRepository, IDiemVeSinhDongBoService
    {
        private readonly TLDbContext _context;
        private readonly ILogService _logService;
        public DiemVeSinhDongBoService(TLDbContext context, ILogService logService) : base(context)
        {
            _context = context;
            _logService = logService;
        }
        public async Task<List<DiemVeSinhModel>> GetAll(int nguondongbo)
        {
            try
            {
                var query = from m in _context.DiemVeSinh
                            where m.IsDelete == false && m.NguonDongBo == nguondongbo
                            select new DiemVeSinhModel
                            {
                                Id = m.Id,
                                ViTri = m.ViTri,
                                IsDelete = m.IsDelete,
                                IsStatus = m.IsStatus,
                                MoTa = m.MoTa,
                                Ten = m.Ten,
                                DonVi = m.DonVi,
                                DiemVeSinhID = m.DiemVeSinhID,
                                GhiChu = m.GhiChu,
                                HienTrang = m.HienTrang,
                                X = m.X,
                                Y = m.Y,
                                NguonDongBo = m.NguonDongBo
                            };
                var result = await query.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }
        
        public async Task<DiemVeSinhModel> GetByDongBoId(int? diemvesinhId)
        {
            try
            {
                var DiemVeSinh = await _context.DiemVeSinh.Where(x => x.DiemVeSinhID == diemvesinhId).Select(x => new DiemVeSinhModel()
                {
                    MoTa = x.MoTa,
                    ViTri = x.ViTri,
                    Id = x.Id,
                    Ten = x.Ten,
                    DonVi = x.DonVi,
                    DiemVeSinhID = x.DiemVeSinhID,
                    GhiChu = x.GhiChu,
                    HienTrang = x.HienTrang,
                    X = x.X,
                    Y = x.Y,
                    NguonDongBo = x.NguonDongBo
                }).ToListAsync();

                if (DiemVeSinh == null || DiemVeSinh.Count() <= 0)
                {
                    return null;
                }

                var obj = DiemVeSinh.FirstOrDefault();

                return obj;
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<DiemVeSinhModelBanDo> GetById(int Id)
        {
            try
            {
                var DiemVeSinh = await _context.DiemVeSinh.Where(x => x.Id == Id).Select(x => new DiemVeSinhModelBanDo()
                {
                    MoTa = x.MoTa,
                    ViTri = x.ViTri,
                    Id = x.Id,
                    Ten = x.Ten,
                    DonVi = x.DonVi,
                    DiemVeSinhID = x.DiemVeSinhID,
                    GhiChu = x.GhiChu,
                    HienTrang = x.HienTrang,
                    X = x.X,
                    Y = x.Y,
                    NguonDongBo = x.NguonDongBo
                }).ToListAsync();

                if (DiemVeSinh == null || DiemVeSinh.Count() <= 0)
                {
                    throw new TLException($"Không tìm thấy dữ liệu với id = {Id}");
                }

                var obj = DiemVeSinh.FirstOrDefault();

                return obj;
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<DiemVeSinhModel> Create(DiemVeSinhModel request)
        {
            try
            {
                var diemVeSinh = new DiemVeSinh()
                {
                    IsDelete = request.IsDelete,
                    IsStatus = request.IsStatus,
                    MoTa = request.MoTa,
                    ViTri = request.ViTri,
                    Ten = request.Ten,
                    DonVi = request.DonVi,
                    DiemVeSinhID = (int)request.DiemVeSinhID,
                    GhiChu = request.GhiChu,
                    HienTrang = request.HienTrang,
                    X = request.X,
                    Y = request.Y,
                    NguonDongBo = (int)request.NguonDongBo
                };
                _context.DiemVeSinh.Add(diemVeSinh);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    request.Id = diemVeSinh.Id;
                    return request;
                }
                return null;
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<int> Update(int id, DiemVeSinhModel request)
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
                model.DonVi = request.DonVi;
                model.DiemVeSinhID = (int)request.DiemVeSinhID;
                model.GhiChu = request.GhiChu;
                model.HienTrang = request.HienTrang;
                model.X = request.X;
                model.Y = request.Y;
                model.NguonDongBo = (int)request.NguonDongBo;
                model.IsDelete = false;

                _context.DiemVeSinh.Update(model);

                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return id;
                }
                return -1;
            }
            catch (Exception ex)
            {
                await _logService.Create(ex.Message, ex.StackTrace);

                throw new TLException("Đã có lỗi trong quá trình xử lý", ex);
            }
        }

        public async Task<DiemVeSinh> EditBanDo(int id, HoSoBanDo request)
        {
            try
            {
                var diemVeSinh = await _context.DiemVeSinh.Where(x => x.Id == id).ToListAsync();
                if (diemVeSinh == null || diemVeSinh.Count() <= 0)
                {
                    return null;
                }

                var model = diemVeSinh.FirstOrDefault();
                model.X = request.X;
                model.Y = request.Y;

                _context.DiemVeSinh.Update(model);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    DiemVeSinh res = _context.DiemVeSinh.FirstOrDefault(x => x.Id == id);

                    return res;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
