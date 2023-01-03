using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Common.Enums.HueCIT;
using TechLife.Service.HueCIT;

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class AmThucRepository : ConnectDB, IAmThucRepository
    {
        private readonly int NGUON_DONG_BO = (int)NguonDongBo.SoHoa;
        private readonly string SERVICE_ID_AM_THUC = "peotlKEeLaXqH31UFxNn2Q==";

        private readonly SqlConnection _conn;
        private readonly IConfiguration _config;
        private readonly IFileUploaderDongBoService _fileUploaderDongBoService;
        private readonly IDanhMucRepository _danhMucRepository;
        private readonly IFileDongBoRepository _fileDongBoRepository;
        public AmThucRepository(IConfiguration configuration
                              , IFileUploaderDongBoService fileUploaderDongBoService
                              , IDanhMucRepository danhMucRepository
                              , IFileDongBoRepository fileDongBoRepository) : base(configuration)
        {
            _conn = IConnectData();
            _config = configuration;
            _fileUploaderDongBoService = fileUploaderDongBoService;
            _danhMucRepository = danhMucRepository;
            _fileDongBoRepository = fileDongBoRepository;
        }
        public async Task<IEnumerable<AmThucTrinhDien>> Gets(AmThucRequest data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Loai", data.Loai);
                    parameters.Add("@ID", data.AmThuc);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    IEnumerable<AmThucTrinhDien> list = conn.Query<AmThucTrinhDien>("SP_AmThucGets", param: parameters, commandType: CommandType.StoredProcedure);
                    foreach (AmThucTrinhDien l in list)
                    {
                        if (Enum.IsDefined(typeof(KieuMon), l.Kieu))
                        {
                            l.KieuMonAn = StringEnum.GetStringValue(l.Kieu);
                        }
                        else
                        {
                            l.KieuMonAn = "Không xác định";
                        }
                    }

                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }
        
        public async Task<AmThucTrinhDien> GetByAmThucID(int id, int nguondongbo)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@AmThucID", id);
                    parameters.Add("@NguonDongBo", nguondongbo);
                    AmThucTrinhDien list = conn.QueryFirstOrDefault<AmThucTrinhDien>("SP_AmThucGetByDongBoID", parameters, commandType: CommandType.StoredProcedure);
                    if (list != null)
                    {
                        if (Enum.IsDefined(typeof(KieuMon), list.Kieu))
                        {
                            list.KieuMonAn = StringEnum.GetStringValue(list.Kieu);
                        }
                        else
                        {
                            list.KieuMonAn = "Không xác định";
                        }

                        DynamicParameters param = new DynamicParameters();
                        param.Add("@LoaiDoiTuong", "DL_MonAnThucUong");
                        param.Add("@IDDoiTuong", id);
                        List<FileUpload> files = (conn.Query<FileUpload>("SP_ThuVienMediaGets", param, commandType: CommandType.StoredProcedure)).ToList();
                        list.Files = files;
                    }

                    return list;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<AmThucTrinhDien> Get(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    AmThucTrinhDien list = conn.QueryFirstOrDefault<AmThucTrinhDien>("SP_AmThucGet", parameters, commandType: CommandType.StoredProcedure);
                    if (Enum.IsDefined(typeof(KieuMon), list.Kieu))
                    {
                        list.KieuMonAn = StringEnum.GetStringValue(list.Kieu);
                    }
                    else
                    {
                        list.KieuMonAn = "Không xác định";
                    }

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@LoaiDoiTuong", "DL_MonAnThucUong");
                    param.Add("@IDDoiTuong", id);
                    List<FileUpload> files = (conn.Query<FileUpload>("SP_ThuVienMediaGets", param, commandType: CommandType.StoredProcedure)).ToList();
                    list.Files = files;

                    return list;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<AmThuc> Add(AmThuc data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MaLoai", data.MaLoai);
                    parameters.Add("@TenMon", data.TenMon);
                    parameters.Add("@KieuMon", data.Kieu);
                    parameters.Add("@ThucUong", data.ThucUong);
                    parameters.Add("@MoTa", data.MoTa);
                    parameters.Add("@AmThucID", data.AmThucID);
                    parameters.Add("@CachLam", data.CachLam);
                    parameters.Add("@ThanhPhan", data.ThanhPhan);
                    parameters.Add("@KhuyenNghiKhiDung", data.KhuyenNghiKhiDung);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    AmThuc list = await SqlMapper.QueryFirstOrDefaultAsync<AmThuc>(conn, "SP_AmThucAdd", parameters, commandType: CommandType.StoredProcedure);
                    return list;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<AmThuc> Edit(AmThuc data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", data.ID);
                    parameters.Add("@MaLoai", data.MaLoai);
                    parameters.Add("@TenMon", data.TenMon);
                    parameters.Add("@KieuMon", data.Kieu);
                    parameters.Add("@ThucUong", data.ThucUong);
                    parameters.Add("@MoTa", data.MoTa);
                    parameters.Add("@CachLam", data.CachLam);
                    parameters.Add("@ThanhPhan", data.ThanhPhan);
                    parameters.Add("@KhuyenNghiKhiDung", data.KhuyenNghiKhiDung);
                    AmThuc list = await SqlMapper.QueryFirstOrDefaultAsync<AmThuc>(conn, "SP_AmThucEdit", parameters, commandType: CommandType.StoredProcedure);
                    return list;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<int> Delete(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_AmThucDelete", parameters, commandType: CommandType.StoredProcedure);
                    return kq;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<IEnumerable<AmThucSearch>> GetsSearch()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<AmThucSearch> list = conn.Query<AmThucSearch>("SP_AmThucGetsSearch", commandType: CommandType.StoredProcedure);

                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        #region ĐỒNG BỘ
        public async Task GetData()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var url = _config.GetValue<string>("SoHoaAddress");
                var token = _config.GetValue<string>("CSDLDuLichToken");

                httpClient.DefaultRequestHeaders.Add("token", token);

                DuLieuDuLichRequestBody body = new DuLieuDuLichRequestBody
                {
                    serviceid = SERVICE_ID_AM_THUC,
                    thamso = new Thamso { tukhoa = "" },
                    page = 0,
                    perpage = 0
                };

                var bodyJson = JsonConvert.SerializeObject(body);
                HttpContent content = new StringContent(bodyJson.ToString(), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(url, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachAmThucDongBo>(apiResponse);

                        if (dataDaXuLy.totalrow > 0 || dataDaXuLy.code == 0)
                        {
                            bool isAnyAmThuc = (await this.Gets(new AmThucRequest() { })).ToList().Any() ? true : false;
                            foreach (var item in dataDaXuLy.data.ToList())
                            {
                                AmThuc amthucNew = null;
                                AmThuc amThuc = new AmThuc
                                {
                                    AmThucID = item.id,
                                    TenMon = item.truong001,
                                    MoTa = item.truong002,
                                    ThanhPhan = item.truong003,
                                    CachLam = item.truong004,
                                    KhuyenNghiKhiDung = item.truong006,
                                    ThucUong = false,
                                    Kieu = 1,
                                    NguonDongBo = NGUON_DONG_BO
                                };

                                if (item.truong005.Trim() == "Món chay")
                                {
                                    var loaiMon = await _danhMucRepository.GetByTenLoai("Chay");
                                    if (loaiMon != null)
                                    {
                                        amThuc.MaLoai = loaiMon.ID;
                                    }
                                    else
                                    {
                                        var loaiMonChay = await _danhMucRepository.InsertLoaiAmThuc(new LoaiAmThuc { TenLoai = "Chay" });
                                        amThuc.MaLoai = loaiMonChay.ID;
                                    }
                                }
                                else
                                {
                                    var loaiMon = await _danhMucRepository.GetByTenLoai("Phổ biến");
                                    if (loaiMon != null)
                                    {
                                        amThuc.MaLoai = loaiMon.ID;
                                    }
                                    else
                                    {
                                        var loaiMonPhoBien = await _danhMucRepository.InsertLoaiAmThuc(new LoaiAmThuc { TenLoai = "Phổ biến" });
                                        amThuc.MaLoai = loaiMonPhoBien.ID;
                                    }
                                }

                                if (isAnyAmThuc)
                                {
                                    var amthuc = await this.GetByAmThucID(amThuc.AmThucID, NGUON_DONG_BO);
                                    if (amthuc != null)
                                    {
                                        amThuc.ID = amthuc.ID;
                                        amthucNew = await this.Edit(amThuc);
                                    }
                                    else
                                    {
                                        amthucNew = await this.Add(amThuc);
                                    }
                                }
                                else
                                {
                                    amthucNew = await this.Add(amThuc);
                                }

                                if (!String.IsNullOrEmpty(item.truong007) && amthucNew != null)
                                {
                                    var findImage = await _fileDongBoRepository.GetsDongBo("DL_MonAnThucUong", amthucNew.ID.ToString(), NGUON_DONG_BO);
                                    if (findImage != null)
                                    {
                                        await _fileDongBoRepository.DeleteWithparentDongBo("DL_MonAnThucUong", amThuc.ID.ToString(), NGUON_DONG_BO);
                                    }

                                    await _fileUploaderDongBoService.UploadFileByUrl(item.truong007, "DL_MonAnThucUong", amthucNew.ID.ToString(), NGUON_DONG_BO);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
