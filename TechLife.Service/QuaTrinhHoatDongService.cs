using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model.HuongDanVien;

namespace TechLife.Service
{
    public interface IQuaTrinhHoatDongService
    {
        Task<List<QuaTrinhHoatDongModel>> GetAll(int hdvId);
    }

    public class QuaTrinhHoatDongService : IQuaTrinhHoatDongService
    {
        private readonly TLDbContext _context;

        public QuaTrinhHoatDongService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<List<QuaTrinhHoatDongModel>> GetAll(int hdvId)
        {
            var query = from m in _context.QuaTrinhHoatDong
                        where m.HDVId == hdvId
                        select new QuaTrinhHoatDongModel
                        {
                            Id = m.Id,
                            HDVId = m.HDVId,
                            KetQua = m.KetQua,
                            HoatDong = m.HoatDong,
                            ThoiGian = m.ThoiGian
                        };
            return await query.ToListAsync();
        }
    }
}