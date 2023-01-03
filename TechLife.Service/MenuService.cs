using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model;

namespace TechLife.Service
{
    public interface IMenuService
    {
        Task<List<MenuModel>> GetAll();

        Task<ApiResult<PagedResult<MenuModel>>> GetMenusPaging(GetPagingRequest request);

        Task<ApiResult<MenuModel>> Create(MenuModel request);

        Task<ApiResult<int>> Update(int id, MenuModel request);

        Task<ApiResult<MenuModel>> GetById(int id);

        Task<ApiResult<int>> Delete(int id);

    }
    public class MenuService : IMenuService
    {
        private readonly TLDbContext _context;
        private readonly IRoleService _roleService;
        public MenuService(TLDbContext context,
            IRoleService roleService)
        {
            _context = context;
            _roleService = roleService;
        }
        public async Task<ApiResult<MenuModel>> Create(MenuModel request)
        {
            var menu = new Menu()
            {
                Description = request.Description,
                Icon = request.Icon,
                IsDeleted = request.IsDeleted,
                IsStatus = request.IsStatus,
                Name = request.Name,
                ParentId = request.ParentId,
                RoleId = request.RoleId,
                Url = request.Url
            };
            _context.Menus.Add(menu);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                request.Id = menu.Id;
                return new ApiSuccessResult<MenuModel>(request);
            }
            return new ApiErrorResult<MenuModel>("Không thêm được menu");

        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var menus = await _context.Menus.Where(x => x.Id == id).ToListAsync();
            if (menus == null || menus.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy menu cần xóa");
            }

            var menu = menus.FirstOrDefault();
            menu.IsDeleted = true;

            _context.Menus.Update(menu);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Không xóa được menu");

        }

        public Task<List<MenuModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<MenuModel>> GetById(int id)
        {
            var menus = await _context.Menus.Where(x => x.Id == id).ToListAsync();
            if (menus == null || menus.Count() <= 0)
            {
                return new ApiErrorResult<MenuModel>("Không tìm thấy menu");
            }

            var obj = menus.FirstOrDefault();

            var memu = new MenuModel()
            {
                Description = obj.Description,
                Icon = obj.Icon,
                Name = obj.Name,
                ParentId = obj.ParentId,
                RoleId = obj.RoleId,
                Url = obj.Url  ,
                Role = _roleService.GetById(obj.RoleId).Result.ResultObj,
                Id = obj.Id
            };

            return new ApiSuccessResult<MenuModel>(memu);
        }

        public async Task<ApiResult<PagedResult<MenuModel>>> GetMenusPaging(GetPagingRequest request)
        {
            var query = from m in _context.Menus
                        where m.IsDeleted == false
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.Name.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new MenuModel()
                {
                    Description = x.m.Description,
                    Icon = x.m.Icon,
                    IsDeleted = x.m.IsDeleted,
                    IsStatus = x.m.IsStatus,
                    Name = x.m.Name,
                    ParentId = x.m.ParentId,
                    RoleId = x.m.RoleId,
                    Url = x.m.Url,
                    Role =  _roleService.GetById(x.m.RoleId).Result.ResultObj
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<MenuModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<MenuModel>>(pagedResult);
        }

        public async Task<ApiResult<int>> Update(int id, MenuModel request)
        {
            var menus = await _context.Menus.Where(x => x.Id == id).ToListAsync();
            if (menus == null || menus.Count() <= 0)
            {
                return new ApiErrorResult<int>("Không tìm thấy menu cần sửa");
            }

            var menu = menus.FirstOrDefault();

            menu.Description = request.Description;
            menu.Icon = request.Icon;
            menu.Name = request.Name;
            menu.ParentId = request.ParentId;
            menu.RoleId = request.RoleId;
            menu.Url = request.Url;

            _context.Menus.Update(menu);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Không sửa được menu");
        }
    }
}
