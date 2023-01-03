using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Data;
using TechLife.Model;

namespace TechLife.Service
{
    public interface INgonNguService
    {
        Task<List<NgonNguVm>> GetAll();
    }

    public class NgonNguService : INgonNguService
    {
        private readonly TLDbContext _context;

        public NgonNguService(TLDbContext context)
        {
            _context = context;
        }

        public async Task<List<NgonNguVm>> GetAll()
        {
            var query = from m in _context.NgonNgu
                        select new NgonNguVm
                        {
                            Id = m.Id,
                            Ten = m.Ten,
                            IsDefault = m.IsDefault
                        };
            return await query.ToListAsync();
        }
    }
}