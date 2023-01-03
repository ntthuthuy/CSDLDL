using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Repositories;
using TechLife.Model.HueCIT;

namespace TechLife.Service.HueCIT
{
    public interface ILoaiDichVuDongBoService
    {
        Task<List<LoaiDichVu>> GetAll();

        Task<LoaiDichVu> Create(LoaiDichVu request);

        Task<LoaiDichVu> Update(int id, LoaiDichVu request);

        Task<LoaiDichVu> GetByID(int id);
        Task<LoaiDichVu> GetByDongBoID(int? id);
    }

    public class LoaiDichVuDongBoService : BaseRepository, ILoaiDichVuDongBoService
    {
        private readonly TLDbContext _context;
        public LoaiDichVuDongBoService(TLDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<LoaiDichVu> Create(LoaiDichVu request)
        {
            var loaiDichVu = new TechLife.Data.Entities.LoaiDichVu()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                TenLoai = request.TenLoai,
                DongBoID = request.DongBoID,
                NguonDongBo = request.NguonDongBo
            };
            _context.LoaiDichVu.Add(loaiDichVu);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return request;
            }
            return null;
        }

        public async Task<List<LoaiDichVu>> GetAll()
        {
            var query = from m in _context.LoaiDichVu
                        where m.IsDelete == false
                        select new LoaiDichVu
                        {
                            Id = m.Id,
                            TenLoai = m.TenLoai,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            MoTa = m.MoTa,
                            DongBoID = m.DongBoID,
                            NguonDongBo = m.NguonDongBo,
                        };
            return await query.ToListAsync();
        }

        public async Task<LoaiDichVu> GetByID(int Id)
        {
            var loaiDichVu = await _context.LoaiDichVu.Where(x => x.Id == Id).ToListAsync();
            if (loaiDichVu == null || loaiDichVu.Count() <= 0)
            {
                return null;
            }

            var obj = loaiDichVu.FirstOrDefault();

            return new LoaiDichVu()
            {
                MoTa = obj.MoTa,
                TenLoai = obj.TenLoai,
                Id = obj.Id,
                DongBoID = obj.DongBoID,
                NguonDongBo = obj.NguonDongBo,
            };
        }

        public async Task<LoaiDichVu> GetByDongBoID(int? Id)
        {
            var loaiDichVu = await _context.LoaiDichVu.Where(x => x.DongBoID == Id).ToListAsync();
            if (loaiDichVu == null || loaiDichVu.Count() <= 0)
            {
                return null;
            }

            var obj = loaiDichVu.FirstOrDefault();

            return new LoaiDichVu()
            {
                MoTa = obj.MoTa,
                TenLoai = obj.TenLoai,
                Id = obj.Id,
                DongBoID = obj.DongBoID,
                NguonDongBo = obj.NguonDongBo,
            };
        }

        public async Task<LoaiDichVu> Update(int id, LoaiDichVu request)
        {
            var LoaiDichVu = await _context.LoaiDichVu.Where(x => x.Id == id).ToListAsync();
            if (LoaiDichVu == null || LoaiDichVu.Count() <= 0)
            {
                return null;
            }

            var model = LoaiDichVu.FirstOrDefault();

            model.MoTa = request.MoTa;
            model.TenLoai = request.TenLoai;
            model.DongBoID = request.DongBoID;
            model.NguonDongBo = request.NguonDongBo;
            model.IsDelete = request.IsDelete;

            _context.LoaiDichVu.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = id;
                return request;
            }
            return null;
        }
    }
}
