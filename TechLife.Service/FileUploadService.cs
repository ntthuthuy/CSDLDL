using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model;
using TechLife.Model.DuLieuDuLich;
using TechLife.Service.Common;

namespace TechLife.Service
{
    public interface IFileUploadService
    {
        Task<List<FileUploadModel>> GetImageByHoSoId(int hosoid, string type);

        Task<FileUploadModel> GetFileByHoSoId(int hosoid, string type);

        Task<List<FileUploadModel>> GetAllImage();

        Task<FileUploadModel> GetFileById(int id);

        Task<List<ImageVm>> GetImageHoSo(int hosoid);

        Task<ApiResult<bool>> SetIsAvata(FileUploadModel request);
        Task<ApiResult<bool>> Delete(int id);

        Task<ApiResult<bool>> CrateFile(FileUploadModel request);

        Task<string> SaveFile(IFormFile file);
    }

    public class FileUploadService : IFileUploadService
    {
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private readonly TLDbContext _context;
        private readonly IStorageService _storageService;

        public FileUploadService(TLDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<List<FileUploadModel>> GetImageByHoSoId(int hosoid, string type)
        {
            var query = from m in _context.FileUploads
                        where m.IsImage && m.Id == hosoid && m.Type == type
                        select new FileUploadModel
                        {
                            Id = m.FileId,
                            FileName = m.FileName,
                            IsImage = m.IsImage,
                            FileId = m.FileId,
                            FileUrl = m.FileUrl,
                            IsStatus = m.IsStatus,
                            Type = m.Type,
                            //HueCIT
                            FileType = m.FileType == null ? 0 : m.FileType,
                            MoTa = m.MoTa,
                        };

            return await query.ToListAsync();
        }

        public async Task<List<ImageVm>> GetImageHoSo(int hosoid)
        {
            var query = from m in _context.FileUploads
                        where m.IsImage && m.Id == hosoid && m.Type == LoaiFile.hosodulich.ToString()
                        select new ImageVm
                        {
                            Id = m.FileId,
                            Name = m.FileName,
                            Url = m.FileUrl
                        };

            return await query.ToListAsync();
        }

        public async Task<FileUploadModel> GetFileByHoSoId(int hosoid, string type)
        {
            var query = from m in _context.FileUploads
                        where !m.IsImage && m.Id == hosoid && m.Type == type
                        orderby m.FileId descending
                        select new FileUploadModel
                        {
                            Id = m.Id,
                            FileName = m.FileName,
                            IsImage = m.IsImage,
                            FileId = m.FileId,
                            FileUrl = m.FileUrl,
                            IsStatus = m.IsStatus,
                            Type = m.Type,
                            //HueCIT
                            FileType = m.FileType == null ? 0 : m.FileType
                        };

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<FileUploadModel>> GetAllImage()
        {
            var query = from m in _context.FileUploads
                        where m.IsImage
                        orderby m.FileId descending
                        select new FileUploadModel
                        {
                            Id = m.Id,
                            FileName = m.FileName,
                            IsImage = m.IsImage,
                            FileId = m.FileId,
                            FileUrl = m.FileUrl,
                            IsStatus = m.IsStatus,
                            Type = m.Type,
                            NgayTao = m.NgayTao
                        };

            return await query.ToListAsync();
        }

        public async Task<FileUploadModel> GetFileById(int id)
        {
            var m = await _context.FileUploads.FindAsync(id);

            return new FileUploadModel
            {
                Id = m.Id,
                FileName = m.FileName,
                IsImage = m.IsImage,
                FileId = m.FileId,
                FileUrl = m.FileUrl,
                IsStatus = m.IsStatus,
                Type = m.Type
            };
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);

            var resutl = "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;

            FileUpload f = new FileUpload()
            {
                FileName = file.FileName,
                FileUrl = resutl,
                IsImage = true,
                IsStatus = true,
                Type = LoaiFile.baiviet.ToString(),
                NgayTao = DateTime.Now
            };
            _context.FileUploads.Add(f);
            await _context.SaveChangesAsync();

            return resutl;
        }

        public async Task<ApiResult<bool>> SetIsAvata(FileUploadModel request)
        {
            var images = _context.FileUploads.Where(v => v.Id == request.Id && v.IsImage);
            foreach (var img in images)
            {
                if (img.IsStatus)
                {
                    img.IsStatus = false;
                }
                if (img.FileId == request.FileId)
                {
                    img.IsStatus = true;
                }
                _context.FileUploads.Update(img);
            }

            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return new ApiSuccessResult<bool>(true, "Cập nhật thành công!");
            else return new ApiErrorResult<bool>("Cập nhật không thành công!");
        }

        public async Task<ApiResult<bool>> Delete(int FileId)
        {
            var obj = await _context.FileUploads.FindAsync(FileId);
            if (obj == null)
            {
                return new ApiErrorResult<bool>($"Không tìm thấy file id ={FileId}");
            }

            _context.FileUploads.Remove(obj);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<bool>(true, $"Xóa file thành công!");
            }
            else
            {
                return new ApiErrorResult<bool>("Xóa file không thành công!");
            }
        }

        public async Task<ApiResult<bool>> CrateFile(FileUploadModel request)
        {
            var obj = new FileUpload()
            {
                FileName = request.FileName,
                FileUrl = request.FileUrl,
                Id = request.Id,
                IsImage = request.IsImage,
                NgayTao = DateTime.Now,
                Type = request.Type,
                IsStatus = true
            };
            _context.FileUploads.Add(obj);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<bool>(true, $"Thêm file thành công!");
            }
            else
            {
                return new ApiErrorResult<bool>("Thêm file không thành công!");
            }
        }
    }
}