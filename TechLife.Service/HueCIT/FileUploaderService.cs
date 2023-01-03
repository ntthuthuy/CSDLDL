using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model;
using TechLife.Model.HueCIT;
using TechLife.Service.Common;

namespace TechLife.Service.HueCIT
{
    public interface IFileUploaderService
    {
        Task<ApiResult<bool>> UploadFile(TechLife.Model.HueCIT.FileUploadRequest file, string table, string ID);
        int CheckFileType(string name);
    }

    public class FileUploaderService : Connect, IFileUploaderService
    {
        private readonly SqlConnection _conn;

        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private const string IMG_TYPE = "bmp, png, jpg, jpeg, gif, webp";
        private const string MEDIA_TYPE = "mp4, mov, wmv, avi, flv, mkv, 3gp, mp3, wav";
        private const string DOC_TYPE = "doc, docx, xls, xlsx, pdf";

        private readonly IStorageService _storageService;

        public FileUploaderService(IConfiguration configuration, IStorageService storageService) : base(configuration)
        {
            _conn = IConnectData();
            _storageService = storageService;
        }
        
        public async Task<ApiResult<bool>> UploadFile(TechLife.Model.HueCIT.FileUploadRequest file, string table, string ID)
        {
            try
            {
                if (file.Files != null)
                {
                    using (SqlConnection conn = IConnectData())
                    {
                        await conn.OpenAsync();
                        foreach (var f in file.Files)
                        {
                            try
                            {
                                DynamicParameters parameters = new DynamicParameters();
                                parameters.Add("@TenFile", f.FileName);
                                parameters.Add("@DuongDan", await this.SaveFile(f));
                                parameters.Add("@LoaiDoiTuong", table);
                                parameters.Add("@LoaiFile", CheckFileType(f.FileName));
                                parameters.Add("@LoaiHinhDuLieu", "");
                                parameters.Add("@TrangThai", true);
                                parameters.Add("@NgayTao", DateTime.Now);
                                parameters.Add("@IDDoiTuong", ID);
                                FileUpload list = await SqlMapper.QueryFirstOrDefaultAsync<FileUpload>(conn, "SP_ThuVienMediaAdd", parameters, commandType: CommandType.StoredProcedure);
                                //return new ApiSuccessResult<bool>();
                            }
                            catch (Exception ex)
                            {
                                if (conn != null)
                                {
                                    conn.Close();
                                }

                                return new ApiErrorResult<bool>(ex.Message);
                            }
                        }

                        if (conn != null)
                        {
                            conn.Close();
                        }
                    }
                }
                return new ApiErrorResult<bool>("Upload lỗi");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }
        
        public int CheckFileType(string name)
        {
            var FileExtension = name.Substring(name.LastIndexOf('.') + 1).ToLower();
            int ft = (int)FileType.Other;
            if (IMG_TYPE.Contains(FileExtension))
            {
                ft = (int)FileType.Img;
            }
            else if (MEDIA_TYPE.Contains(FileExtension))
            {
                ft = (int)FileType.Media;
            }
            else if (DOC_TYPE.Contains(FileExtension))
            {
                ft = (int)FileType.Doc;
            }

            return ft;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

    }
}