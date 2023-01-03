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
    public interface IThucDonService
    {
        Task<List<ThucDonHoSoModel>> GetAllByHoSo(int hosoId);
    }

    public class ThucDonService : IThucDonService
    {
        private readonly TLDbContext _context;

        public ThucDonService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<List<ThucDonHoSoModel>> GetAllByHoSo(int hosoId)
        {
            var ThucDon = await _context.ThucDonHoSo.Where(v => v.HosoId == hosoId).ToListAsync();

            var listThucDon = new List<ThucDonHoSoModel>();
            foreach (var m in ThucDon)
            {
                listThucDon.Add(new ThucDonHoSoModel()
                {
                    MoTa = m.MoTa,
                    DonGia = m.DonGia,
                    HosoId = m.HosoId,
                    TenThucDon = m.TenThucDon,
                    Id = m.Id
                });
            }
            return listThucDon;
        }
    }
}