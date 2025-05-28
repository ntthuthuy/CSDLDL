using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model.DanhMucDuLieuThongKe;

namespace TechLife.Service
{
    public interface IDanhMucDuLieuThongKeService
    {
        Task<PagedResult<DanhMucDuLieuThongKeVm>> GetPaging(DanhMucDuLieuThongKeFormRequets requets);
        Task<Result<bool>> Create(DanhMucDuLieuThongKeCreateRequest request);
        Task<Result<bool>> Update(DanhMucDuLieuThongKeUpdateRequest request);
        Task<Result<bool>> Delete(int id);
        Task<List<DanhMucDuLieuThongKeVm>> GetAll();
        Task<List<DanhMucDuLieuThongKeVm>> GetHierarchy();
        Task<DanhMucDuLieuThongKeVm> GetById(int id);
        Task<bool> CheckIsParent(int id);
        Task<List<int>> ListParent();
        Task<Result<bool>> UpdateOrder(int id, int value);
    }

    public class DanhMucDuLieuThongKeService : IDanhMucDuLieuThongKeService
    {
        private readonly TLDbContext _context;
        private readonly IDbConnectionService _dbConnectionService;
        private readonly ILogger<DanhMucDuLieuThongKeService> _logger;

        public DanhMucDuLieuThongKeService(TLDbContext context
            , IDbConnectionService dbConnectionService
            , ILogger<DanhMucDuLieuThongKeService> logger)
        {
            _context = context;
            _dbConnectionService = dbConnectionService;
            _logger = logger;
        }

        public async Task<bool> CheckIsParent(int id)
        {
            return await _context.DanhMucDuLieuThongKe.AnyAsync(x => x.ParentId == id);
        }

        public async Task<Result<bool>> Create(DanhMucDuLieuThongKeCreateRequest request)
        {
            try
            {
                int? parentId = !string.IsNullOrEmpty(request.ParentId) ? Convert.ToInt32(HashUtil.DecodeID(request.ParentId)) : null;

                int nextOrder = await _context.DanhMucDuLieuThongKe.Where(x => x.ParentId == parentId).Select(x => (int?)x.Order).MaxAsync() ?? 0;

                var data = new DanhMucDuLieuThongKe
                {
                    Code = request.Code?.Trim(),
                    Name = request.Name?.Trim(),
                    DVT = request.DVT?.Trim(),
                    ParentId = parentId,
                    IsDelete = false,
                    Order = ++nextOrder
                };

                await _context.DanhMucDuLieuThongKe.AddAsync(data);

                await _context.SaveChangesAsync();

                return new Result<bool>() { IsSuccessed = true, Message = "Thêm danh mục thành công" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            var data = await _context.DanhMucDuLieuThongKe.FindAsync(id);

            if (data == null || data.IsDelete) return new Result<bool>() { IsSuccessed = false, Message = "Dữ liệu không tồn tại" };

            data.IsDelete = true;

            await _context.SaveChangesAsync();

            return new Result<bool>() { IsSuccessed = true, Message = "Xóa danh mục thành công" };
        }

        public async Task<List<DanhMucDuLieuThongKeVm>> GetAll()
        {
            var data = await _context.DanhMucDuLieuThongKe.Where(x => !x.IsDelete).ToListAsync();
            return data.Select(x => new DanhMucDuLieuThongKeVm
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                DVT = x.DVT,
                ParentId = x.ParentId,
            }).ToList();
        }

        public async Task<DanhMucDuLieuThongKeVm> GetById(int id)
        {
            var data = await _context.DanhMucDuLieuThongKe.FindAsync(id);

            if (data == null || data.IsDelete) return null;

            return new DanhMucDuLieuThongKeVm
            {
                Id = id,
                Code = data.Code,
                Name = data.Name,
                DVT = data.DVT,
                ParentId = data.ParentId,
            };
        }

        public async Task<List<DanhMucDuLieuThongKeVm>> GetHierarchy()
        {
            using var connection = await _dbConnectionService.GetConnectionAsync();

            string query = @"
            WITH DanhMuc AS
            (
	            SELECT Id, Name = COALESCE(Code + '. ', '') + Name, ParentId, IdString = CONVERT(VARCHAR(1000), Id), ParentString = COALESCE(CONVERT(VARCHAR(1000), ParentId),'0'), [Order]
	            FROM DanhMucDuLieuThongKe
	            WHERE IsDelete = 0
            ),
            DanhMucRecursive(Id, Name, IdString, Parents, Level, [Order]) AS
            (
	            SELECT Id, Name, IdString, CAST(IdString AS VARCHAR(1000)), 0 AS Level, [Order]
	            FROM DanhMuc
	            WHERE DanhMuc.ParentId IS NULL
	            UNION ALL
	            SELECT b.Id, b.Name, b.IdString, CAST((CONCAT(a.Parents + ',', COALESCE(b.IdString, ','))) AS VARCHAR(1000)), a.Level + 1, b.[Order]
	            FROM DanhMucRecursive AS a INNER JOIN DanhMuc AS b ON a.Id = b.ParentId
            )
            SELECT *
            FROM DanhMucRecursive
            ORDER BY DanhMucRecursive.Parents";

            var data = await _dbConnectionService.ExecuteToListAsync<DanhMucDuLieuThongKeVm>(connection, query);

            data = SortHierarchy(data);

            return data;
        }

        public async Task<PagedResult<DanhMucDuLieuThongKeVm>> GetPaging(DanhMucDuLieuThongKeFormRequets requets)
        {
            try
            {
                using var connection = await _dbConnectionService.GetConnectionAsync();

                string search = string.IsNullOrWhiteSpace(requets.Search) ? "" : requets.Search.Trim();

                var parameters = new Dictionary<string, object>()
                {
                    { "Search", search }
                };

                string query = @"
                WITH DanhMuc AS
                (
	                SELECT Id, Name, ParentId, IdString = CONVERT(VARCHAR(1000), Id), ParentString = COALESCE(CONVERT(VARCHAR(1000), ParentId),'0'), [Order]
	                FROM csdldl.DanhMucDuLieuThongKe
	                WHERE IsDelete = 0 AND (@Search = '' OR Name LIKE N'%' + @Search + '%' OR Code LIKE N'%' + @Search + '%')
                ),
                DanhMucRecursive(Id, Name, IdString, Parents, Level, [Order]) AS
                (
	                SELECT Id, Name, IdString, CAST(IdString AS VARCHAR(1000)), 0 AS Level, [Order]
	                FROM DanhMuc
	                WHERE DanhMuc.ParentId IS NULL
	                UNION ALL
	                SELECT b.Id, b.Name, b.IdString, CAST((CONCAT(a.Parents + ',', COALESCE(b.IdString, ','))) AS VARCHAR(1000)), a.Level + 1, b.[Order]
	                FROM DanhMucRecursive AS a INNER JOIN DanhMuc AS b ON a.Id = b.ParentId
                )
                SELECT dm.*, cte.Level, cte.Parents
                FROM DanhMucRecursive cte INNER JOIN DanhMucDuLieuThongKe dm ON dm.Id = cte.Id
                ORDER BY cte.Parents";

                var data = await _dbConnectionService.ExecuteToListAsync<DanhMucDuLieuThongKeVm>(connection, query, parameters);

                int totalRow = data.Count;

                var result = SortHierarchy(data);

                return new PagedResult<DanhMucDuLieuThongKeVm>
                {
                    TotalRecords = totalRow,
                    Items = result.Skip((requets.PageIndex - 1) * requets.PageSize).Take(requets.PageSize).ToList(),
                    PageIndex = requets.PageIndex,
                    PageSize = requets.PageSize,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private static List<DanhMucDuLieuThongKeVm> SortHierarchy(List<DanhMucDuLieuThongKeVm> items)
        {
            var lookup = items.ToLookup(item =>
            {
                var parentIds = item.Parents.Split(',').Select(int.Parse).ToList();
                return parentIds.Count > 1 ? parentIds[^2] : (int?)null;
            });

            List<DanhMucDuLieuThongKeVm> result = new();

            void AddChildren(int? parentId, int level)
            {
                foreach (var child in lookup[parentId].OrderBy(x => x.Order))
                {
                    result.Add(child);
                    AddChildren(child.Id, level + 1);
                }
            }

            AddChildren(null, 0);

            return result;
        }

        public async Task<List<int>> ListParent()
        {
            var data = await _context.DanhMucDuLieuThongKe.Where(x => !x.IsDelete && x.ParentId != null).Select(x => (int)x.ParentId).ToListAsync();
            return data.Distinct().ToList();
        }

        public async Task<Result<bool>> Update(DanhMucDuLieuThongKeUpdateRequest request)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(request.Id));

            int? parentId = string.IsNullOrWhiteSpace(request.ParentId) ? null : Convert.ToInt32(HashUtil.DecodeID(request.ParentId));

            var data = await _context.DanhMucDuLieuThongKe.FindAsync(id);

            if (data == null || data.IsDelete) return new Result<bool>() { IsSuccessed = false, Message = "Dữ liệu không tồn tại" };

            data.Code = request.Code?.Trim();
            data.Name = request.Name.Trim();
            data.ParentId = parentId;
            data.DVT = request.DVT?.Trim();

            _context.DanhMucDuLieuThongKe.Update(data);

            await _context.SaveChangesAsync();

            return new Result<bool>() { IsSuccessed = true, Message = "Cập nhật thành công" };
        }

        public async Task<Result<bool>> UpdateOrder(int id, int value)
        {
            var data = await _context.DanhMucDuLieuThongKe.FindAsync(id);

            if (data == null || data.IsDelete) return new Result<bool>() { IsSuccessed = false, Message = "Dữ liệu không tồn tại" };

            data.Order = value;

            _context.DanhMucDuLieuThongKe.Update(data);

            await _context.SaveChangesAsync();

            return new Result<bool>() { IsSuccessed = true, Message = "Cập nhật vị trí thành công" };
        }
    }
}
