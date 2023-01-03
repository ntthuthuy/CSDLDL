using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common.Enums.HueCIT;

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class DichVuDiTichRepository : ConnectDB, IDichVuDiTichRepository
    {
        private readonly int NGUON_DONG_BO = (int)NguonDongBo.TTBTDT;

        private readonly IConfiguration _config;
        private readonly SqlConnection _conn;
        private readonly IDichVuDiTichHinhAnhRepository _dichVuDiTichHinhAnhRepository;
        public DichVuDiTichRepository(IConfiguration configuration,
                                      IDichVuDiTichHinhAnhRepository dichVuDiTichHinhAnhRepository) : base(configuration)
        {
            _config = configuration;
            _conn = IConnectData();
            _dichVuDiTichHinhAnhRepository = dichVuDiTichHinhAnhRepository;
        }

        #region CRUD
        public async Task<DichVuDiTich> Add(DichVuDiTich data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TieuDe", data.TieuDe);
                    parameters.Add("@TomTat", data.TomTat);
                    parameters.Add("@NoiDung", data.NoiDung);
                    parameters.Add("@NgayCapNhat", data.NgayCapNhat);
                    parameters.Add("@BangGia", data.BangGia);
                    parameters.Add("@ToaDoX", data.ToaDoX);
                    parameters.Add("@ToaDoY", data.ToaDoY);
                    parameters.Add("@SapXep", data.SapXep);
                    parameters.Add("@DongBoID", data.DongBoID);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    DichVuDiTich list = await SqlMapper.QueryFirstOrDefaultAsync<DichVuDiTich>(conn, "SP_DichVuDiTichAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DichVuDiTich> Edit(DichVuDiTich data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", data.ID);
                    parameters.Add("@TieuDe", data.TieuDe);
                    parameters.Add("@TomTat", data.TomTat);
                    parameters.Add("@NoiDung", data.NoiDung);
                    parameters.Add("@NgayCapNhat", data.NgayCapNhat);
                    parameters.Add("@BangGia", data.BangGia);
                    parameters.Add("@ToaDoX", data.ToaDoX);
                    parameters.Add("@ToaDoY", data.ToaDoY);
                    parameters.Add("@SapXep", data.SapXep);
                    parameters.Add("@DongBoID", data.DongBoID);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    DichVuDiTich list = await SqlMapper.QueryFirstOrDefaultAsync<DichVuDiTich>(conn, "SP_DichVuDiTichEdit", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DichVuDiTichTrinhDien> GetByDongBo(string dongBoId)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@DongBoID", dongBoId);
                    DichVuDiTichTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<DichVuDiTichTrinhDien>(conn, "SP_DichVuDiTichGetByDongBo", parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<IEnumerable<DichVuDiTichTrinhDien>> Gets()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<DichVuDiTichTrinhDien> list = conn.Query<DichVuDiTichTrinhDien>("SP_DichVuDiTichGets", commandType: CommandType.StoredProcedure);
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
        #endregion

        #region ĐỒNG BỘ
        public async Task GetData()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // URL mới là URL hiển thì tổng số tất cả dịch vụ di tích
                    var BASE_URL = _config.GetValue<string>("DichVuDiTichAddress");
                    string url = $"{BASE_URL}?CodeName=DichVu&isgetcatechild=true&size={Int32.MaxValue}";

                    using (var res = await httpClient.GetAsync(url))
                    {
                        // StatusCode OK
                        if (res.IsSuccessStatusCode)
                        {
                            var apiRes = await res.Content.ReadAsStringAsync();
                            // Danh sách dịch vụ di tích lấy từ API
                            var dataLoop = JsonConvert.DeserializeObject<DanhSachDichVuDiTichDongBo>(apiRes);

                            // Danh sách dịch vụ di tích trên database HueCIT
                            var DSDichVuDiTich = await this.Gets();

                            // Kiểm tra có dữ liệu không
                            if (dataLoop.totalCount > 0 && dataLoop.newsList.Count() > 0)
                            {
                                foreach (var item in dataLoop.newsList.ToList())
                                {
                                    // Dịch vụ di tích chuẩn bị thêm vào
                                    DichVuDiTich dichVuDiTichMoi = null;

                                    // Xử lý ngày
                                    DateTime? ngayCapNhat = null;
                                    try
                                    {
                                        ngayCapNhat = DateTime.ParseExact(item.publishTime, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
                                    }
                                    catch (Exception ex)
                                    {
                                        ngayCapNhat = null;
                                    }

                                    // Model
                                    DichVuDiTich dichVuDiTich = new DichVuDiTich
                                    {
                                        NgayCapNhat = ngayCapNhat,
                                        NoiDung = item.content,
                                        SapXep = item.order,
                                        TieuDe = item.title,
                                        ToaDoX = item.geo.latitude,
                                        ToaDoY = item.geo.longitude,
                                        TomTat = item.summary,
                                        DongBoID = item.id,
                                        NguonDongBo = NGUON_DONG_BO,
                                    };

                                    // DỊCH VỤ DI TÍCH
                                    // Kiểm tra danh sách dịch vụ di tích database HueCIT có rỗng
                                    // Nếu có : cập nhật
                                    // Nếu không : thêm mới
                                    if (DSDichVuDiTich.Count() > 0)
                                    {
                                        // Kiểm tra dịch vụ di tích bằng [ID] có tồn tại
                                        var dvdt = await this.GetByDongBo(dichVuDiTich.DongBoID);
                                        if (dvdt != null)
                                        {
                                            dichVuDiTich.ID = dvdt.ID;
                                            dichVuDiTichMoi = await this.Edit(dichVuDiTich);
                                        }
                                        else
                                        {
                                            dichVuDiTichMoi = await this.Add(dichVuDiTich);
                                        }
                                    }
                                    else
                                    {
                                        dichVuDiTichMoi = await this.Add(dichVuDiTich);
                                    }

                                    // HÌNH ẢNH
                                    // Kiểm tra đường dẫn hình ảnh dịch vụ di tích có tồn tại
                                    // Nếu có : cập nhật
                                    // Nếu không : thêm mới
                                    if (!String.IsNullOrEmpty(item.imgNews.url) || !String.IsNullOrEmpty(item.imgNewsThumb.url))
                                    {
                                        // Kiểm tra thêm mới/cập nhật dịch vụ di tích có thành công
                                        if (dichVuDiTichMoi != null)
                                        {
                                            DichVuDiTichHinhAnh hinhAnh = new DichVuDiTichHinhAnh
                                            {
                                                MaDVDT = dichVuDiTichMoi.ID,
                                                TieuDeAnh = dichVuDiTichMoi.TieuDe,
                                                URLHinhAnh = item.imgNews.url,
                                                IsAnhDaiDien = false,
                                            };

                                            // Get Hình ảnh bằng [MaDVDT] - [dbo].[CN_DichVuDiTich] database HueCIT
                                            var ha = await _dichVuDiTichHinhAnhRepository.GetByMaDVDT(dichVuDiTichMoi.ID);
                                            if (ha == null)
                                            {
                                                await _dichVuDiTichHinhAnhRepository.Add(hinhAnh);
                                            }
                                            else
                                            {
                                                hinhAnh.ID = ha.ID;
                                                await _dichVuDiTichHinhAnhRepository.Edit(hinhAnh);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
