using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Model.HueCIT;

namespace TechLife.Service.HueCIT
{
    public interface IFileUploaderDongBoService
    {
        Task<ApiResult<bool>> UploadFileByUrl(string url, string table, string ID, int nguondongbo);
    }
    public class FileUploaderDongBoService : Connect, IFileUploaderDongBoService
    {
        private readonly IFileUploaderService _fileUploaderService;
        public FileUploaderDongBoService(IConfiguration configuration, IFileUploaderService fileUploaderService) : base(configuration)
        {
            _fileUploaderService = fileUploaderService;
        }

        public async Task<ApiResult<bool>> UploadFileByUrl(string url, string table, string ID, int nguondongbo)
        {
            try
            {
                using (SqlConnection conn = IConnectData())
                {
                    try
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@TenFile", url.Substring(url.LastIndexOf('/') + 1));
                        parameters.Add("@DuongDan", url);
                        parameters.Add("@LoaiDoiTuong", table);
                        parameters.Add("@LoaiFile", _fileUploaderService.CheckFileType(url.Substring(url.LastIndexOf('/') + 1)));
                        parameters.Add("@LoaiHinhDuLieu", "");
                        parameters.Add("@TrangThai", true);
                        parameters.Add("@NgayTao", DateTime.Now);
                        parameters.Add("@IDDoiTuong", ID);
                        parameters.Add("@NguonDongBo", nguondongbo);
                        FileUpload list = await SqlMapper.QueryFirstOrDefaultAsync<FileUpload>(conn, "SP_ThuVienMediaAdd", parameters, commandType: CommandType.StoredProcedure);

                        return new ApiSuccessResult<bool>();
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
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }
    }
}
