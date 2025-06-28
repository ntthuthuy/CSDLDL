using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model.TongHop;

namespace TechLife.Service
{
    public interface ITongHopService
    {
        Task<PagedResult<TongHopVm>> GetPaging(TongHopFormRequest request);
        Task<List<TongHopVm>> GetAll();
        Task<TongHopVm> GetById(int id);
        Task<Result<bool>> Create(TongHopCreateRequest request);
        Task<Result<bool>> Update(TongHopUpdateRequest request);
        Task<Result<bool>> Delete(TongHopDeleteRequest request);
        Task<Result<bool>> Import(TongHopImportRequest request);
    }

    public class TongHopService : ITongHopService
    {
        private readonly TLDbContext _context;
        private readonly ILogger<TongHopService> _logger;

        public TongHopService(TLDbContext context
            , ILogger<TongHopService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<Result<bool>> Create(TongHopCreateRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Result<bool>> Delete(TongHopDeleteRequest request)
        {
            try
            {
                var data = await _context.TongHop.Where(x => !x.IsDelete && x.Thang == request.Thang && x.Nam == request.Nam).ToListAsync();

                if (data.Count == 0) return new Result<bool>() { IsSuccessed = false, Message = "Dữ liệu không tồn tại" };

                foreach (var item in data)
                {
                    item.IsDelete = true;
                }

                _context.TongHop.UpdateRange(data);

                await _context.SaveChangesAsync();

                return new Result<bool>() { IsSuccessed = true, Message = "Xóa thành công" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public Task<List<TongHopVm>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<TongHopVm> GetById(int id)
        {
            var data = await _context.TongHop.Include(x => x.QuocTich).FirstOrDefaultAsync(x => x.Id == id);

            if (data == null || data.IsDelete) return null;

            return new TongHopVm
            {
                Id = id,
                QuocTichId = data.QuocTichId,
                TenQuocTich = data.QuocTich.TenQuocTich,
                List = new()
                {
                    new()
                    {
                        SoLieu = data.SoLieu,
                        CongDon = data.CongDon,
                        ThiPhan = data.ThiPhan
                    }
                }
            };
        }

        public async Task<PagedResult<TongHopVm>> GetPaging(TongHopFormRequest request)
        {
            try
            {
                var query = _context.TongHop
                    .AsNoTracking()
                    .Where(x => !x.IsDelete && x.Nam == request.Nam && (request.Thang == 0 || request.Thang == x.Thang));

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    query = query.Where(x => x.QuocTich.TenQuocTich.ToLower().Contains(request.Search.ToLower()));
                }

                var data = await query
                    .GroupBy(g => new
                    {
                        g.QuocTichId
                    })
                    .Select(x => new TongHopVm
                    {
                        QuocTichId = x.Key.QuocTichId,
                        TenQuocTich = _context.QuocTich.First(v => v.Id == x.Key.QuocTichId).TenQuocTich,
                        List = x.Select(v => new ListSoLieu
                        {
                            Thang = v.Thang,
                            Nam = v.Nam,
                            SoLieu = v.SoLieu,
                            CongDon = v.CongDon,
                            ThiPhan = v.ThiPhan
                        }).ToList()
                    }).ToListAsync();

                if (data.Count > 0)
                {
                    data.Add(new TongHopVm
                    {
                        TenQuocTich = "Tổng cộng",
                        List = data.SelectMany(x => x.List)
                        .GroupBy(g => new { g.Thang, g.Nam })
                        .Select(x => new ListSoLieu
                        {
                            Thang = x.Key.Thang,
                            Nam = x.Key.Nam,
                            SoLieu = x.Sum(v => v.SoLieu),
                            CongDon = x.Sum(v => v.CongDon),
                            ThiPhan = 100
                        }).ToList()
                    });
                }
                else
                {
                    var listQuocTich = await _context.QuocTich.Where(x => !x.IsDelete && x.IsStatus).ToListAsync();

                    foreach (var item in listQuocTich)
                    {
                        data.Add(new()
                        {
                            TenQuocTich = item.TenQuocTich,
                            List = new()
                        });
                    }

                    data.Add(new()
                    {
                        TenQuocTich = "Tổng cộng",
                        List = new()
                    });
                }

                return new PagedResult<TongHopVm>
                {
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalRecords = data.Count,
                    Items = data.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList(),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Result<bool>> Import(TongHopImportRequest request)
        {
            try
            {
                var quocTichDict = await _context.QuocTich.Where(x => !x.IsDelete).ToDictionaryAsync(x => x.TenQuocTich.ToLowerInvariant());

                var existData = await _context.TongHop
                    .Where(x => !x.IsDelete && x.Thang == request.Thang && x.Nam == request.Nam)
                    .Include(x => x.QuocTich)
                    .ToListAsync();

                var dataDict = existData.ToDictionary(x => x.QuocTich.TenQuocTich.ToLowerInvariant());

                existData.Clear();

                var newQuocTich = new List<QuocTich>();

                var newData = new List<TongHop>();

                foreach (var item in request.Items)
                {
                    string key = item.TenQuocTich.ToLowerInvariant();

                    if (!quocTichDict.TryGetValue(key, out var quocTich))
                    {
                        quocTich = new QuocTich
                        {
                            TenQuocTich = item.TenQuocTich,
                            IsDelete = false,
                            IsStatus = true
                        };

                        newQuocTich.Add(quocTich);
                        quocTichDict[key] = quocTich;
                    }

                    if (!dataDict.TryGetValue(key, out var entity))
                    {
                        entity = new TongHop
                        {
                            SoLieu = decimal.Parse(item.SoLieu),
                            Thang = request.Thang,
                            Nam = request.Nam,
                            CongDon = decimal.Parse(item.CongDon),
                            ThiPhan = Math.Truncate(Math.Round(decimal.Parse(item.ThiPhan), 4) * 100 * 100) / 100.0m,
                            QuocTich = quocTich
                        };

                        newData.Add(entity);
                        dataDict[key] = entity;
                    }
                    else
                    {
                        entity.SoLieu = decimal.Parse(item.SoLieu);
                        entity.CongDon = decimal.Parse(item.CongDon);
                        entity.ThiPhan = Math.Truncate(Math.Round(decimal.Parse(item.ThiPhan), 4) * 100 * 100) / 100.0m;

                        existData.Add(entity);
                    }
                }

                if (newQuocTich.Count > 0) await _context.QuocTich.AddRangeAsync(newQuocTich);

                if (newData.Count > 0) await _context.TongHop.AddRangeAsync(newData);

                if (existData.Count > 0) _context.TongHop.UpdateRange(existData);

                await _context.SaveChangesAsync();

                return new Result<bool>() { IsSuccessed = true, Message = "Import thành công" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Result<bool>> Update(TongHopUpdateRequest request)
        {
            try
            {
                int quocTichId = Convert.ToInt32(HashUtil.DecodeID(request.QuocTichId));
                var data = await _context.TongHop.FirstOrDefaultAsync(x => x.Thang == request.Month && x.Nam == request.Year && x.QuocTichId == quocTichId);
                if (data == null || data.IsDelete) return new Result<bool>() { IsSuccessed = false, Message = "Dữ liệu không tồn tại" };

                data.SoLieu = decimal.Parse(request.SoLieu);
                data.CongDon = decimal.Parse(request.CongDon);
                data.ThiPhan = request.ThiPhan;

                _context.TongHop.Update(data);
                await _context.SaveChangesAsync();

                return new Result<bool>() { IsSuccessed = true, Message = "Cập nhật thành công" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
