using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.Data.Repositories;
using System.Linq;
using TechLife.Model.HueCIT;
using TechLife.Data;

namespace TechLife.Service.HueCIT
{
    public interface IDanhMucDongBoService
    {
        Task<List<DanhMucModel>> GetAll(int loaiId);
        Task<DanhMucModel> GetById(int id);
        Task<DanhMucModel> GetByDongBoID(int loaiId, int? dongboId);
        Task<DanhMucModel> Create(DanhMucModel request);
        Task<DanhMucModel> Update(int id, DanhMucModel request);
        Task<int> UpdateDongBoID(int id, int? dongboId);
    }
    public class DanhMucDongBoService : BaseRepository, IDanhMucDongBoService
    {
        private readonly TLDbContext _context;
        public DanhMucDongBoService(TLDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<DanhMucModel>> GetAll(int loaiId)
        {
            var query = from m in _context.DanhMuc
                        where m.IsDelete == false && m.LoaiId == loaiId
                        select new DanhMucModel
                        {
                            Id = m.Id,
                            Ten = m.Ten,
                            IsDelete = m.IsDelete,
                            IsStatus = m.IsStatus,
                            LoaiId = m.LoaiId,
                            MoTa = m.MoTa,
                            DongBoID = m.DongBoID,
                            NguonDongBo = m.NguonDongBo,
                        };
            return await query.ToListAsync();
        }
        public async Task<DanhMucModel> GetById(int id)
        {
            var DanhMuc = await _context.DanhMuc.Where(x => x.Id == id).ToListAsync();
            if (DanhMuc == null || DanhMuc.Count() <= 0)
            {
                return null;
            }

            var obj = DanhMuc.FirstOrDefault();

            return new DanhMucModel()
            {
                MoTa = obj.MoTa,
                Ten = obj.Ten,
                Id = obj.Id,
                LoaiId = obj.LoaiId,
                DongBoID = obj.DongBoID,
                NguonDongBo = obj.NguonDongBo,
            };
        }
        public async Task<DanhMucModel> Create(DanhMucModel request)
        {
            var DanhMuc = new TechLife.Data.Entities.DanhMuc()
            {
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
                MoTa = request.MoTa,
                Ten = request.Ten,
                LoaiId = request.LoaiId,
                DongBoID = request.DongBoID,
                NguonDongBo = request.NguonDongBo,
            };
            _context.DanhMuc.Add(DanhMuc);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = DanhMuc.Id;
                return request;
            }
            return null;
        }
        public async Task<DanhMucModel> Update(int id, DanhMucModel request)
        {
            var DanhMuc = await _context.DanhMuc.Where(x => x.Id == id).ToListAsync();
            if (DanhMuc == null || DanhMuc.Count() <= 0)
            {
                return null;
            }

            var model = DanhMuc.FirstOrDefault();

            model.MoTa = request.MoTa;
            model.Ten = request.Ten;
            model.LoaiId = request.LoaiId;
            model.DongBoID = request.DongBoID;
            model.NguonDongBo = request.NguonDongBo;
            model.IsDelete = request.IsDelete;
            model.IsStatus = request.IsStatus;

            _context.DanhMuc.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = id;
                return request;
            }
            return null;
        }
        public async Task<int> UpdateDongBoID(int id, int? dongboId)
        {
            var DanhMuc = await _context.DanhMuc.Where(x => x.Id == id).ToListAsync();
            if (DanhMuc == null || DanhMuc.Count() <= 0)
            {
                return -1;
            }

            var model = DanhMuc.FirstOrDefault();

            model.DongBoID = dongboId;

            _context.DanhMuc.Update(model);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return id;
            }
            return -1;
        }
        public async Task<DanhMucModel> GetByDongBoID(int loaiId, int? dongboId)
        {
            var DanhMuc = await _context.DanhMuc.Where(x => x.DongBoID == dongboId && x.LoaiId == loaiId).ToListAsync();
            if (DanhMuc == null || DanhMuc.Count() <= 0)
            {
                return null;
            }

            var obj = DanhMuc.FirstOrDefault();

            return new DanhMucModel()
            {
                MoTa = obj.MoTa,
                Ten = obj.Ten,
                Id = obj.Id,
                LoaiId = obj.LoaiId,
                DongBoID = obj.DongBoID,
                NguonDongBo = obj.NguonDongBo,
            };
        }
    }
}
