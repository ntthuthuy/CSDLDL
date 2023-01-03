using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model.TinTuc;
using TechLife.Service.Common;

namespace TechLife.Service
{
    public interface IChuyenMucService
    {
        Task<List<ChuyenMucVm>> GetAll(string languageId, int parentId = -1);

        Task<PagedResult<ChuyenMucVm>> GetPaging(string languageId, GetPagingRequest request);

        Task<ApiResult<ChuyenMucVm>> Create(ChuyenMucCreateRequest request);

        Task<ApiResult<int>> Update(int id, ChuyenMucUpdateRequest request);
        Task<ApiResult<int>> UpdateStatusMenu(int id);

        Task<ChuyenMucVm> GetById(int id);

        Task<ApiResult<int>> Delete(int id);

        Task<string> SaveFile(IFormFile file);
        Task<List<ChuyenMucVm>> DSDaSuDung(string languageId);
    }

    public class ChuyenMucService : IChuyenMucService
    {
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private readonly TLDbContext _context;
        private readonly IStorageService _storageService;

        public ChuyenMucService(TLDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<ApiResult<ChuyenMucVm>> Create(ChuyenMucCreateRequest request)
        {
            var obj = new ChuyenMuc()
            {
                MoTa = request.MoTa,
                Ten = request.Ten,
                CreateDate = DateTime.Now,
                IsHienThiMenu = request.IsHienThiMenu,
                ParentId = request.ParentId,
                TieuDe = request.TieuDe,
                TuKhoa = request.TuKhoa,
                UserId = request.UserId,
                UrlImage = request.UrlImage,
                ThuTuHienThi = _context.ChuyenMuc.Where(v => v.ParentId == request.ParentId).Count() + 1,
                NgonNguId = request.NgonNguId,
                IconMobile = request.IconMoblie,
                IconWeb = request.IconWeb,
            };
            _context.ChuyenMuc.Add(obj);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<ChuyenMucVm>(await GetById(obj.Id));
            }
            return new ApiErrorResult<ChuyenMucVm>("Thêm lỗi!");
        }

        public async Task<ApiResult<int>> Delete(int id)
        {
            var obj = await _context.ChuyenMuc.FindAsync(id);

            obj.IsDelete = true;

            _context.ChuyenMuc.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Xóa lỗi!");
        }

        public async Task<List<ChuyenMucVm>> GetAll(string languageId, int parentId = -1)
        {
            var query = from m in _context.ChuyenMuc
                        where m.IsDelete == false
                        && m.NgonNguId == languageId
                        && (parentId == -1 || m.ParentId == parentId)
                        select new ChuyenMucVm
                        {
                            Id = m.Id,
                            Ten = m.Ten,
                            MoTa = m.MoTa,
                            UserId = m.UserId,
                            UrlImage = m.UrlImage,
                            TuKhoa = m.TuKhoa,
                            CreateDate = m.CreateDate,
                            IsHienThiMenu = m.IsHienThiMenu,
                            ParentId = m.ParentId,
                            ThuTuHienThi = m.ThuTuHienThi,
                            TieuDe = m.TieuDe,
                            IconMoblie = m.IconMobile,
                            IconWeb = m.IconWeb,
                        };
            return await query.ToListAsync();
        }

        public async Task<List<ChuyenMucVm>> DSDaSuDung(string languageId)
        {
            var query = from t in _context.TinTuc join m in _context.ChuyenMuc on t.ChuyenMucId equals m.Id
                        where t.IsDelete == false && t.IsTinLeHoi
                        && t.NgonNguId == languageId
                   
                        select new ChuyenMucVm
                        {
                            Id = m.Id,
                            Ten = m.Ten,
                            MoTa = m.MoTa,
                            UserId = m.UserId,
                            UrlImage = m.UrlImage,
                            TuKhoa = m.TuKhoa,
                            CreateDate = m.CreateDate,
                            IsHienThiMenu = m.IsHienThiMenu,
                            ParentId = m.ParentId,
                            ThuTuHienThi = m.ThuTuHienThi,
                            TieuDe = m.TieuDe,
                            IconMoblie = m.IconMobile,
                            IconWeb = m.IconWeb,
                        };
            return await query.Distinct().ToListAsync();
        }

        public async Task<ChuyenMucVm> GetById(int id)
        {
            var obj = await _context.ChuyenMuc.FindAsync(id);

            return new ChuyenMucVm()
            {
                MoTa = obj.MoTa,
                Ten = obj.Ten,
                Id = obj.Id,
                CreateDate = obj.CreateDate,
                IsHienThiMenu = obj.IsHienThiMenu,
                ParentId = obj.ParentId,
                ThuTuHienThi = obj.ThuTuHienThi,
                TieuDe = obj.TieuDe,
                TuKhoa = obj.TuKhoa,
                UrlImage = obj.UrlImage,
                UserId = obj.UserId,
                IconMoblie = obj.IconMobile,
                IconWeb = obj.IconWeb,
            };
        }

        public async Task<PagedResult<ChuyenMucVm>> GetPaging(string languageId, GetPagingRequest request)
        {
            var query = from m in _context.ChuyenMuc
                        where m.IsDelete == false && m.NgonNguId == languageId
                        select new { m };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.Ten.Contains(request.Keyword));
            //3. Paging
            int totalRow = query.Count();


            var data = await query.Select(x => new ChuyenMucVm()
            {
                MoTa = x.m.MoTa,
                Ten = x.m.Ten,
                Id = x.m.Id,
                CreateDate = x.m.CreateDate,
                IsHienThiMenu = x.m.IsHienThiMenu,
                ParentId = x.m.ParentId,
                ThuTuHienThi = x.m.ThuTuHienThi,
                TieuDe = x.m.TieuDe,
                TuKhoa = x.m.TuKhoa,
                UrlImage = x.m.UrlImage,
                UserId = x.m.UserId ,
                IconMoblie = x.m.IconMobile,
                IconWeb = x.m.IconWeb
            }).ToListAsync();

            listChuyenMuc = new List<ChuyenMucVm>();
            listChuyenMuc = ListChuyenMuc(data, 0, 0);

            listChuyenMuc = listChuyenMuc.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();

            //4. Select and projection
            return new PagedResult<ChuyenMucVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = listChuyenMuc
            };
        }

        private List<ChuyenMucVm> listChuyenMuc;

        private List<ChuyenMucVm> ListChuyenMuc(List<ChuyenMucVm> data, int parentId, int level)
        {
            var list = data.Where(v => v.ParentId == parentId).ToList();
            foreach (var x in list)
            {
                string space = "";
                for (int i = 1; i <= level; i++)
                {
                    space += "- - - ";
                }
                x.Ten = space + x.Ten;
                listChuyenMuc.Add(x);
                var list_chirld = data.Where(v => v.ParentId == x.Id).ToList();
                if (list_chirld.Count > 0)
                {
                    int level_next = level + 1;
                    ListChuyenMuc(data, x.Id, level_next);
                }
            }
            return listChuyenMuc;
        }

        public async Task<ApiResult<int>> Update(int id, ChuyenMucUpdateRequest request)
        {
            var obj = await _context.ChuyenMuc.FindAsync(id);

            obj.MoTa = request.MoTa;
            obj.Ten = request.Ten;
            obj.TieuDe = request.TieuDe;
            obj.TuKhoa = request.TuKhoa;
            obj.ParentId = request.ParentId;
            obj.IsHienThiMenu = request.IsHienThiMenu;
            obj.IconMobile = request.IconMoblie;
            obj.IconWeb = request.IconWeb;
            if (!String.IsNullOrEmpty(request.UrlImage))
            {
                obj.UrlImage = request.UrlImage;
            }
            _context.ChuyenMuc.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id);
            }
            return new ApiErrorResult<int>("Sửa lỗi!");
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<ApiResult<int>> UpdateStatusMenu(int id)
        {
            var obj = await _context.ChuyenMuc.FindAsync(id);
            if (obj.IsHienThiMenu)
            {
                obj.IsHienThiMenu = false;
            }
            else
            {
                obj.IsHienThiMenu = true;
            }
            _context.ChuyenMuc.Update(obj);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<int>(id, "Cập nhật thành công!");
            }
            return new ApiErrorResult<int>("Cập nhật lỗi!");
        }
    }
}