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
        Task<Result<bool>> Import(List<TongHopImportRequest> request);
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
                SoLieu = new Dictionary<int, decimal> { { data.Thang, data.SoLieu } }
            };
        }

        public async Task<PagedResult<TongHopVm>> GetPaging(TongHopFormRequest request)
        {
            try
            {
                var query = _context.TongHop
                    .Include(x => x.QuocTich)
                    .Where(x => !x.IsDelete && x.Nam == request.Nam && (request.Thang == 0 || request.Thang == x.Thang));

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    query = query.Where(x => x.QuocTich.TenQuocTich.ToLower().Contains(request.Search.ToLower()));
                }

                var data = await query.ToListAsync();

                var listQuocTich = await _context.QuocTich.Where(x => !x.IsDelete).ToListAsync();

                var result = new List<TongHopVm>();

                var allMonths = Enumerable.Range(1, 12).ToList();

                var total = new TongHopVm
                {
                    TenQuocTich = "Tổng cộng",
                    SoLieu = new(),
                    ThiPhan = 100
                };

                if (request.Thang == 0)
                {
                    foreach (var month in allMonths)
                    {
                        total.SoLieu.Add(month, data.Where(x => x.Thang == month).Sum(x => x.SoLieu));
                    }
                }
                else
                {
                    total.SoLieu.Add(request.Thang, data.Where(x => x.Thang == request.Thang).Sum(x => x.SoLieu));
                }

                foreach (var quocTich in listQuocTich)
                {
                    var t = new TongHopVm
                    {
                        QuocTichId = quocTich.Id,
                        TenQuocTich = quocTich.TenQuocTich,
                        MoTa = quocTich.MoTa,
                        SoLieu = new()
                    };

                    var list = data.Where(x => x.QuocTichId == quocTich.Id).ToList();

                    if (request.Thang != 0)
                    {
                        decimal soLieu = list.Where(x => x.Thang == request.Thang).Sum(x => (decimal?)x.SoLieu) ?? 0;
                        t.SoLieu.Add(request.Thang, soLieu);
                        t.ThiPhan = total.SoLieu.Values.Sum() != 0 ? Math.Truncate(Math.Round((soLieu / total.SoLieu.Values.Sum()), 4) * 100 * 100) / 100.0m : 0;
                    }
                    else
                    {
                        foreach (var month in allMonths)
                        {
                            decimal soLieu = list.Where(x => x.Thang == month).Sum(x => (decimal?)x.SoLieu) ?? 0;
                            t.SoLieu.Add(month, soLieu);
                        }
                        t.ThiPhan = total.SoLieu.Values.Sum() != 0 ? Math.Truncate(Math.Round((t.SoLieu.Values.Sum() / total.SoLieu.Values.Sum()), 4) * 100 * 100) / 100.0m : 0;
                    }

                    result.Add(t);
                }

                result = result.OrderByDescending(x => x.SoLieu.Values.Sum()).ToList();

                result.Add(total);

                return new PagedResult<TongHopVm>
                {
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalRecords = result.Count,
                    Items = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList(),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Result<bool>> Import(List<TongHopImportRequest> request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var quocTichDict = await _context.QuocTich.Where(x => !x.IsDelete).ToDictionaryAsync(x => x.TenQuocTich.ToLowerInvariant());

                foreach (var items in request)
                {
                    var existData = await _context.TongHop
                    .Where(x => !x.IsDelete && x.Thang == items.Thang && x.Nam == items.Nam)
                    .Include(x => x.QuocTich)
                    .ToListAsync();

                    var dataDict = existData.ToDictionary(x => x.QuocTich.TenQuocTich.ToLowerInvariant());

                    existData.Clear();

                    var newQuocTich = new List<QuocTich>();

                    var newData = new List<TongHop>();

                    foreach (var item in items.Items)
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
                                Thang = items.Thang,
                                Nam = items.Nam,
                                QuocTich = quocTich
                            };

                            newData.Add(entity);
                            dataDict[key] = entity;
                        }
                        else
                        {
                            entity.SoLieu = decimal.Parse(item.SoLieu);
                            existData.Add(entity);
                        }
                    }

                    if (newQuocTich.Count > 0) await _context.QuocTich.AddRangeAsync(newQuocTich);

                    if (newData.Count > 0) await _context.TongHop.AddRangeAsync(newData);

                    if (existData.Count > 0) _context.TongHop.UpdateRange(existData);

                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();

                return new Result<bool>() { IsSuccessed = true, Message = "Import thành công" };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
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
