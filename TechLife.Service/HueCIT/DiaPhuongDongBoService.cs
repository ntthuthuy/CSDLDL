using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Repositories;
using TechLife.Model;
using TechLife.Model.HueCIT;

namespace TechLife.Service.HueCIT
{
    public interface IDiaPhuongDongBoService
    {
        Task<List<DiaPhuongModelDongBo>> GetAllDongBo(int nguondongbo);
        Task<DiaPhuongModelDongBo> GetByName(string name);
        Task<DiaPhuongModelDongBo> GetByDongBo(int? dongboId, int? nguondongbo);
        Task<int> Edit(int id, DiaPhuongModelDongBo request);
    }
    public class DiaPhuongDongBoService : BaseRepository, IDiaPhuongDongBoService
    {
        private readonly TLDbContext _context;
        public DiaPhuongDongBoService(TLDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<DiaPhuongModelDongBo>> GetAllDongBo(int nguondongbo)
        {
            try
            {
                var query = from m in _context.DiaPhuong
                            where m.IsDelete == false && m.NguonDongBo == nguondongbo
                            select new DiaPhuongModelDongBo
                            {
                                Id = m.Id,
                                TenDiaPhuong = m.TenDiaPhuong,
                                IsDelete = m.IsDelete,
                                IsStatus = m.IsStatus,
                                ParentId = m.ParentId,
                                MoTa = m.MoTa
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DiaPhuongModelDongBo> GetByDongBo(int? dongboId, int? nguondongbo)
        {
            try
            {
                var diaPhuong = await _context.DiaPhuong.Where(x => x.DongBoID == dongboId && x.NguonDongBo == nguondongbo).ToListAsync();
                if (diaPhuong == null || diaPhuong.Count() <= 0)
                {
                    return null;
                }

                var obj = diaPhuong.FirstOrDefault();

                return new DiaPhuongModelDongBo()
                {
                    MoTa = obj.MoTa,
                    TenDiaPhuong = obj.TenDiaPhuong,
                    Id = obj.Id,
                    ParentId = obj.ParentId
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DiaPhuongModelDongBo> GetByName(string name)
        {
            try
            {
                var diaPhuong = await _context.DiaPhuong.Where(x => x.TenDiaPhuong.ToUpper() == name.ToUpper()).ToListAsync();
                if (diaPhuong == null || diaPhuong.Count() <= 0)
                {
                    return null;
                }

                var obj = diaPhuong.FirstOrDefault();

                return new DiaPhuongModelDongBo()
                {
                    MoTa = obj.MoTa,
                    TenDiaPhuong = obj.TenDiaPhuong,
                    Id = obj.Id,
                    ParentId = obj.ParentId,
                    DongBoID = obj.DongBoID,
                    NguonDongBo = obj.NguonDongBo,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Edit(int id, DiaPhuongModelDongBo request)
        {
            try
            {
                var diaPhuong = await _context.DiaPhuong.Where(x => x.Id == id).ToListAsync();
                if (diaPhuong == null || diaPhuong.Count() <= 0)
                {
                    return -1;
                }

                var model = diaPhuong.FirstOrDefault();

                model.DongBoID = request.DongBoID;
                model.NguonDongBo = request.NguonDongBo;

                _context.DiaPhuong.Update(model);

                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return id;
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
