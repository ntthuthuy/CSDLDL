using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Repository.Schedules
{
    public class PhanAnhHienTruongScheduleRepository : IPhanAnhHienTruongScheduleRepository
    {
        private readonly IConfiguration _config;
        private readonly IPhanAnhHienTruongLinhVucRepository _repositoryLinhVuc;
        private readonly IPhanAnhHienTruongRepository _repositoryPhanAnhHienTruong;
        private readonly IPhanAnhHienTruongHinhAnhRepository _repositoryPhanAnhHienTruongHinhAnh;
        private readonly IPhanAnhHienTruongCoQuanRepository _repositoryPhanAnhHienTruongCoQuan;
        private readonly ILogger<PhanAnhHienTruongScheduleRepository> _logger;

        private readonly string DANG_XU_LY = "0";
        private readonly string DA_XU_LY = "1";
        private readonly string TU_NGAY = "2018-04-21";
        private readonly string PAGE = "1";
        private readonly string PERPAGE = "100";

        public PhanAnhHienTruongScheduleRepository(IConfiguration config,
                                                   IPhanAnhHienTruongLinhVucRepository repositoryLinhVuc,
                                                   IPhanAnhHienTruongRepository repositoryPhanAnhHienTruong,
                                                   IPhanAnhHienTruongHinhAnhRepository repositoryPhanAnhHienTruongHinhAnh,
                                                   IPhanAnhHienTruongCoQuanRepository repositoryPhanAnhHienTruongCoQuan,
                                                   ILogger<PhanAnhHienTruongScheduleRepository> logger)
        {
            _config = config;
            _repositoryLinhVuc = repositoryLinhVuc;
            _repositoryPhanAnhHienTruong = repositoryPhanAnhHienTruong;
            _repositoryPhanAnhHienTruongHinhAnh = repositoryPhanAnhHienTruongHinhAnh;
            _repositoryPhanAnhHienTruongCoQuan = repositoryPhanAnhHienTruongCoQuan;
            _logger = logger;   
        }

        // Đồng bộ lĩnh vực phản ánh hiện trường
        public async Task GetLinhVuc()
        {
            try
            {
                var token = _config.GetValue<string>("PhanAnhToken");
                var baseUrl = _config.GetValue<string>("TuongTacAddress");

                #region Lưu lĩnh vực phản ánh hiện trường
                using (var httpClient = new HttpClient())
                {
                    var urlLinhVucs = $"{baseUrl}/chuyenmuc";

                    httpClient.DefaultRequestHeaders.Add("token", token);
                    httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

                    // Đồng bộ dữ liệu lĩnh vực
                    using (var responseLinhVucs = await httpClient.GetAsync(urlLinhVucs))
                    {
                        if (responseLinhVucs.IsSuccessStatusCode)
                        {
                            var apiResponseLinhVucs = await responseLinhVucs.Content.ReadAsStringAsync();
                            var dataLinhVucs = JsonConvert.DeserializeObject<DanhSachChuyenMucPhanAnhHienTruong>(apiResponseLinhVucs);

                            var dataLinhVucDB = await _repositoryLinhVuc.GetsLinhVucPhanAnhHienTruong();

                            foreach (var lv in dataLinhVucs.data)
                            {
                                var linhVucPhanAnh = new PhanAnhHienTruongLinhVuc { Id = lv.ChuyenMucID, LinhVuc = lv.TenChuyenMuc };
                                if (dataLinhVucDB.Count() > 0)
                                {
                                    var linhvuc = await _repositoryLinhVuc.GetLinhVucPhanAnhHienTruong(lv.ChuyenMucID);
                                    if (linhvuc == null)
                                    {
                                        await _repositoryLinhVuc.InsertLinhVucPhanAnhHienTruong(linhVucPhanAnh);
                                    }
                                    else
                                    {
                                        await _repositoryLinhVuc.UpdateLinhVucPhanAnhHienTruong(linhVucPhanAnh);
                                    }
                                }
                                else
                                {
                                    await _repositoryLinhVuc.InsertLinhVucPhanAnhHienTruong(linhVucPhanAnh);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        // Đồng bộ cơ quan phản ánh hiện trường
        public async Task GetCoQuan()
        {
            try
            {
                var baseUrl = _config.GetValue<string>("EgovapiAddress");

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

                    // Đồng bộ dữ liệu lĩnh vực
                    using (var response = await httpClient.GetAsync(baseUrl))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            var data = JsonConvert.DeserializeObject<DanhSachPhanAnhHienTruongCoQuanDongBo>(apiResponse);

                            var dataCoQuanDB = await _repositoryPhanAnhHienTruongCoQuan.GetsPhanAnhHienTruongCoQuan();

                            foreach (var lv in data.data)
                            {
                                var coQuanMoi = new PhanAnhHienTruongCoQuan
                                {
                                    Id = lv.UniqueCode.Trim(),
                                    TenCoQuan = lv.OrganizationName
                                };

                                if (dataCoQuanDB.Count() > 0)
                                {
                                    var coQuan = await _repositoryPhanAnhHienTruongCoQuan.GetPhanAnhHienTruongCoQuan(coQuanMoi.Id);
                                    if (coQuan == null)
                                    {
                                        await _repositoryPhanAnhHienTruongCoQuan.InsertPhanAnhHienTruongCoQuan(coQuanMoi);
                                    }
                                    else
                                    {
                                        await _repositoryPhanAnhHienTruongCoQuan.UpdatePhanAnhHienTruongCoQuan(coQuanMoi);
                                    }
                                }
                                else
                                {
                                    await _repositoryPhanAnhHienTruongCoQuan.InsertPhanAnhHienTruongCoQuan(coQuanMoi);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        // Đồng bộ phản ánh hiện trường theo đơn vị
        // Đã xử lý
        public async Task GetData()
        {
            try
            {
                var token = _config.GetValue<string>("PhanAnhToken");
                var baseUrl = _config.GetValue<string>("TuongTacAddress");
                var TuNgay = TU_NGAY;

                // Danh sách hình ảnh trên DB HueCIT
                var hinhAnhsDB = await _repositoryPhanAnhHienTruongHinhAnh.GetsPhanAnhHienTruongHinhAnh();
                // Danh sách lĩnh vực đang hoạt động
                var listLinhVucHoatDong = await _repositoryLinhVuc.GetsIsEnableLinhVucPhanAnhHienTruong(true);
                // Danh sách hiện trường đã xử lý
                var phanAnhHienTruongs = await _repositoryPhanAnhHienTruong.GetsPhanAnhHienTruongByLoaiXuLy(1);

                // Kiểm tra danh sách hiện trường đang xử lý trên DB HueCIT
                // Nếu có -> lấy [TuNgay] = Ngày tạo mới nhất trên DB
                // Nếu không -> [TuNgay] = Ngày hằng số [TU_NGAY]
                if (phanAnhHienTruongs.Any())
                {
                    foreach (var item in phanAnhHienTruongs)
                    {
                        if (DateTime.Compare(item.NgayTao, DateTime.Parse(TuNgay)) >= 0)
                        {
                            TuNgay = item.NgayTao.ToString("yyyy/MM/dd");
                            break;
                        }
                    }
                }

                #region Lưu lĩnh vực phản ánh hiện trường
                using (var httpClient = new HttpClient())
                {
                    var urlLinhVucs = $"{baseUrl}/chuyenmuc";

                    httpClient.DefaultRequestHeaders.Add("token", token);
                    httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

                    var DANH_SACH_MA_DINH_DANH = await _repositoryPhanAnhHienTruongCoQuan.GetsPhanAnhHienTruongCoQuan();
                    if (DANH_SACH_MA_DINH_DANH.Count() > 0)
                    {
                        foreach (var j in DANH_SACH_MA_DINH_DANH)
                        {
                            string madinhdanh = j.Id;
                            string tendinhdanh = j.TenCoQuan;
                            double totalrow = 0;
                            double row = 0;

                            #region Lưu cơ quan phản ánh hiện trường
                            var hientruong = new PhanAnhHienTruongCoQuan
                            {
                                Id = madinhdanh,
                                TenCoQuan = tendinhdanh
                            };

                            var coQuans = await _repositoryPhanAnhHienTruongCoQuan.GetsPhanAnhHienTruongCoQuan();
                            if (!coQuans.Any())
                            {
                                await _repositoryPhanAnhHienTruongCoQuan.InsertPhanAnhHienTruongCoQuan(hientruong);
                            }
                            else
                            {
                                var coQuan = await _repositoryPhanAnhHienTruongCoQuan.GetPhanAnhHienTruongCoQuan(madinhdanh);
                                if (coQuan == null)
                                {
                                    await _repositoryPhanAnhHienTruongCoQuan.InsertPhanAnhHienTruongCoQuan(hientruong);
                                }
                            }
                            #endregion

                            #region lấy thông tin LoaiXuLy=1 (đã xử lý)

                            var url = $"{baseUrl}/theodonvi";

                            var content = new PhanAnhHienTruongHttpContent
                            {
                                LoaiXuLy = DA_XU_LY,
                                MaDinhDanh = madinhdanh,
                                TuNgay = TuNgay,
                                DenNgay = DateTime.Now.ToString("yyyy-MM-dd"),
                                Page = PAGE,
                                Perpage = PERPAGE
                            };
                            var passedContent = JsonConvert.SerializeObject(content);
                            HttpContent httpContent = new StringContent(passedContent.ToString(), Encoding.UTF8, "application/json");

                            using (var response = await httpClient.PostAsync(url, httpContent))
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    var apiResponse = await response.Content.ReadAsStringAsync();
                                    var data = JsonConvert.DeserializeObject<DSPhanAnhHienTruongTheoDonVi>(apiResponse);

                                    totalrow = data.totalrow;
                                    row = Math.Round(totalrow / Convert.ToInt32(PERPAGE));
                                }
                            }

                            if (totalrow > 0)
                            {
                                for (var i = 1; i <= row + 1; i++)
                                {
                                    var contentLoop = new PhanAnhHienTruongHttpContent
                                    {
                                        LoaiXuLy = DA_XU_LY,
                                        MaDinhDanh = madinhdanh,
                                        TuNgay = TuNgay,
                                        DenNgay = DateTime.Now.ToString("yyyy-MM-dd"),
                                        Page = i.ToString(),
                                        Perpage = PERPAGE
                                    };
                                    var jsonLoop = JsonConvert.SerializeObject(contentLoop);
                                    HttpContent httpContentLoop = new StringContent(jsonLoop.ToString(), Encoding.UTF8, "application/json");

                                    using (var responseLoop = await httpClient.PostAsync(url, httpContentLoop))
                                    {
                                        if (responseLoop.IsSuccessStatusCode)
                                        {
                                            var apiResponseLoop = await responseLoop.Content.ReadAsStringAsync();
                                            var dataDaXuLy = JsonConvert.DeserializeObject<DSPhanAnhHienTruongTheoDonVi>(apiResponseLoop);

                                            if (dataDaXuLy.data.Count() > 0)
                                            {
                                                foreach (var phananhhientruong in dataDaXuLy.data)
                                                {
                                                    if (phananhhientruong.PhanAnhID > 0)
                                                    {
                                                        PhanAnhHienTruong phanAnhNew;

                                                        // Xử lý ngày
                                                        string datePhanAnh = phananhhientruong.NgayPhanAnh.Replace("--", "-");
                                                        string dateTraLoi = phananhhientruong.NgayTraLoi.Replace("--", "-");

                                                        DateTime? dateSend = null;
                                                        DateTime? dateResult = null;
                                                        try
                                                        {
                                                            dateSend = DateTime.Parse(datePhanAnh);

                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            dateSend = null;
                                                        }
                                                        try
                                                        {
                                                            dateResult = DateTime.Parse(dateTraLoi);

                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            dateResult = null;
                                                        }

                                                        PhanAnhHienTruong dataSaveChanges = new PhanAnhHienTruong
                                                        {
                                                            MaCoQuanXuLy = madinhdanh,
                                                            TenCoQuan = tendinhdanh,
                                                            MaLinhVuc = phananhhientruong.ChuyenMucID ?? 0,
                                                            NgayGui = dateSend,
                                                            NgayXuLy = dateResult,
                                                            NoiDung = phananhhientruong.NoiDungPhanAnh,
                                                            X = phananhhientruong.ViDo ?? 0,
                                                            Y = phananhhientruong.KinhDo ?? 0,
                                                            YKienXuLy = phananhhientruong.NoiDungTraLoi,
                                                            LoaiXuLy = true,
                                                            TieuDe = phananhhientruong.TieuDe,
                                                            DiaChiSuKien = phananhhientruong.DiaChiSuKien,
                                                            NgayTao = DateTime.Now,
                                                            PhanAnhId = phananhhientruong.PhanAnhID
                                                        };

                                                        // Kiểm tra lĩnh vực ID hoạt động
                                                        // Trường [IsEnable] = true : Có hoạt động
                                                        //                   = false : Không hoạt động
                                                        bool isHoatDong = listLinhVucHoatDong.Where(x => x.Id == dataSaveChanges.MaLinhVuc).Any();
                                                        if (isHoatDong)
                                                        {
                                                            // Kiểm tra dữ liệu trên database HueCIT với hiện trường đã xử lý 
                                                            // Nếu có -> cập nhật
                                                            // Nếu không -> thêm mới
                                                            if (!phanAnhHienTruongs.Any())
                                                            {
                                                                phanAnhNew = await _repositoryPhanAnhHienTruong.InsertPhanAnhHienTruong(dataSaveChanges);
                                                            }
                                                            else
                                                            {
                                                                var phanAnhHienTruong = await _repositoryPhanAnhHienTruong.GetByDongBoID(dataSaveChanges.PhanAnhId);

                                                                if (phanAnhHienTruong == null)
                                                                {
                                                                    phanAnhNew = await _repositoryPhanAnhHienTruong.InsertPhanAnhHienTruong(dataSaveChanges);
                                                                }
                                                                else
                                                                {
                                                                    dataSaveChanges.Id = phanAnhHienTruong.Id;
                                                                    dataSaveChanges.NguoiGui = phanAnhHienTruong.NguoiGui;
                                                                    phanAnhNew = await _repositoryPhanAnhHienTruong.UpdatePhanAnhHienTruong(dataSaveChanges);
                                                                }
                                                            }

                                                            if (hinhAnhsDB.Count() > 0)
                                                            {
                                                                // Kiểm tra dữ liệu hình ảnh phản ánh hiện trường trên database HueCIT
                                                                // Nếu Có --> cập nhật
                                                                // Nếu không --> thêm mới
                                                                var hinhAnhDB = await _repositoryPhanAnhHienTruongHinhAnh.GetsPhanAnhHienTruongHinhAnhByPhanAnhId(phanAnhNew.Id);
                                                                if (hinhAnhDB.Count() > 0)
                                                                {
                                                                    foreach (var hinhanh in hinhAnhDB)
                                                                    {
                                                                        var hinhAnhMoi = new PhanAnhHienTruongHinhAnh
                                                                        {
                                                                            HinhAnh = hinhanh.HinhAnh,
                                                                            MaPhanAnh = phanAnhNew.Id,
                                                                            HinhAnhThumb = hinhanh.HinhAnhThumb,
                                                                            Id = hinhanh.Id
                                                                        };

                                                                        // Kiểm tra IsKetQua --> cập nhật
                                                                        // true -> hình ảnh kết quả xử lý 
                                                                        // false -> hình ảnh gửi phản ánh
                                                                        if (hinhanh.IsKetQua == true)
                                                                        {
                                                                            hinhAnhMoi.IsKetQua = true;
                                                                            await _repositoryPhanAnhHienTruongHinhAnh.UpdatePhanAnhHienTruongHinhAnh(hinhAnhMoi);
                                                                        }
                                                                        else
                                                                        {
                                                                            hinhAnhMoi.IsKetQua = false;
                                                                            await _repositoryPhanAnhHienTruongHinhAnh.UpdatePhanAnhHienTruongHinhAnh(hinhAnhMoi);
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    // Thêm mới hình ảnh phản ánh trả lời
                                                                    foreach (var fileDinhKemKQ in phananhhientruong.DanhSachFileDinhKem_Kq)
                                                                    {
                                                                        await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                        {
                                                                            HinhAnh = fileDinhKemKQ.FileName,
                                                                            HinhAnhThumb = fileDinhKemKQ.FileName_Thumb,
                                                                            IsKetQua = true,
                                                                            MaPhanAnh = phanAnhNew.Id
                                                                        });
                                                                    }

                                                                    // Thêm mói hình ảnh gửi phản ánh
                                                                    foreach (var fileDinhKem in phananhhientruong.DanhSachFileDinhKem)
                                                                    {
                                                                        await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                        {
                                                                            HinhAnhThumb = fileDinhKem.FileName_Thumb,
                                                                            HinhAnh = fileDinhKem.FileName,
                                                                            IsKetQua = false,
                                                                            MaPhanAnh = phanAnhNew.Id
                                                                        });
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                // Thêm mới hình ảnh phản ánh trả lời
                                                                foreach (var fileDinhKemKQ in phananhhientruong.DanhSachFileDinhKem_Kq)
                                                                {
                                                                    await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                    {
                                                                        HinhAnhThumb = fileDinhKemKQ.FileName_Thumb,
                                                                        HinhAnh = fileDinhKemKQ.FileName,
                                                                        IsKetQua = true,
                                                                        MaPhanAnh = phanAnhNew.Id
                                                                    });
                                                                }

                                                                // Thêm mói hình ảnh gửi phản ánh
                                                                foreach (var fileDinhKem in phananhhientruong.DanhSachFileDinhKem)
                                                                {
                                                                    await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                    {
                                                                        HinhAnhThumb = fileDinhKem.FileName_Thumb,
                                                                        HinhAnh = fileDinhKem.FileName,
                                                                        IsKetQua = false,
                                                                        MaPhanAnh = phanAnhNew.Id
                                                                    });
                                                                }
                                                            }

                                                            if (phanAnhNew != null && phanAnhNew.X != null && phanAnhNew.X != 0 && phanAnhNew.Y != null && phanAnhNew.Y != 0)
                                                            {
                                                                TechLife.Model.HueCIT.ToaDo geo = new TechLife.Model.HueCIT.ToaDo
                                                                {
                                                                    x = phanAnhNew.Y,
                                                                    y = phanAnhNew.X
                                                                };

                                                                TechLife.Model.HueCIT.PhanAnhDongBoAdd data = new Model.HueCIT.PhanAnhDongBoAdd
                                                                {
                                                                    id = phanAnhNew.Id,
                                                                    tieude = phanAnhNew.TieuDe,
                                                                    diachisuki = phanAnhNew.DiaChiSuKien,
                                                                };

                                                                AddEditGIS(data, geo);
                                                            }
                                                        }
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
                }
                #endregion
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        // Đang xử lý
        public async Task GetDataWait()
        {
            try
            {
                var token = _config.GetValue<string>("PhanAnhToken");
                var baseUrl = _config.GetValue<string>("TuongTacAddress");
                var TuNgay = TU_NGAY;

                // Danh sách hình ảnh của hiện trường hiện có trên DB HueCIT
                var hinhAnhsDB = await _repositoryPhanAnhHienTruongHinhAnh.GetsPhanAnhHienTruongHinhAnh();
                // Danh sách hiện trường có trên DB HueCIT
                var phanAnhHienTruongs = await _repositoryPhanAnhHienTruong.GetsPhanAnhHienTruongByLoaiXuLy(0);
                // Danh sách lĩnh vực hoạt động
                var listLinhVucHoatDong = await _repositoryLinhVuc.GetsIsEnableLinhVucPhanAnhHienTruong(true);

                // Kiểm tra danh sách hiện trường đang xử lý trên DB HueCIT
                // Nếu có -> lấy [TuNgay] = Ngày tạo mới nhất trên DB
                // Nếu không -> [TuNgay] = Ngày hằng số [TU_NGAY]
                if (phanAnhHienTruongs.Any())
                {
                    foreach (var item in phanAnhHienTruongs)
                    {
                        if (DateTime.Compare(item.NgayTao, DateTime.Parse(TuNgay)) >= 0)
                        {
                            TuNgay = item.NgayTao.ToString("yyyy/MM/dd");
                            break;
                        }
                    }
                }

                #region lấy thông tin LoaiXuLy=0 (đang xử lý)
                var DANH_SACH_MA_DINH_DANH = await _repositoryPhanAnhHienTruongCoQuan.GetsPhanAnhHienTruongCoQuan();
                if (DANH_SACH_MA_DINH_DANH.Count() > 0)
                {
                    foreach (var j in DANH_SACH_MA_DINH_DANH)
                    {
                        string madinhdanh = j.Id;
                        string tendinhdanh = j.TenCoQuan;
                        double totalrow = 0;
                        double row = 0;

                        #region Lưu cơ quan phản ánh hiện trường
                        var coQuans = await _repositoryPhanAnhHienTruongCoQuan.GetsPhanAnhHienTruongCoQuan();
                        if (!coQuans.Any())
                        {
                            await _repositoryPhanAnhHienTruongCoQuan.InsertPhanAnhHienTruongCoQuan(new PhanAnhHienTruongCoQuan
                            {
                                Id = madinhdanh,
                                TenCoQuan = tendinhdanh
                            });
                        }
                        else
                        {
                            var coQuan = await _repositoryPhanAnhHienTruongCoQuan.GetPhanAnhHienTruongCoQuan(madinhdanh);
                            if (coQuan == null)
                            {
                                await _repositoryPhanAnhHienTruongCoQuan.InsertPhanAnhHienTruongCoQuan(new PhanAnhHienTruongCoQuan
                                {
                                    Id = madinhdanh,
                                    TenCoQuan = tendinhdanh
                                });
                            }
                        }
                        #endregion

                        using (var httpClient = new HttpClient())
                        {
                            var url = $"{baseUrl}/theodonvi";

                            httpClient.DefaultRequestHeaders.Add("token", token);
                            httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

                            var content = new PhanAnhHienTruongHttpContent
                            {
                                LoaiXuLy = DANG_XU_LY,
                                MaDinhDanh = madinhdanh,
                                TuNgay = TuNgay,
                                DenNgay = DateTime.Now.ToString("yyyy-MM-dd"),
                                Page = PAGE,
                                Perpage = PERPAGE
                            };
                            var passedContent = JsonConvert.SerializeObject(content);
                            HttpContent httpContent = new StringContent(passedContent, Encoding.UTF8, "application/json");

                            using (var response = await httpClient.PostAsync(url, httpContent))
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    var apiResponse = await response.Content.ReadAsStringAsync();
                                    var data = JsonConvert.DeserializeObject<DSPhanAnhHienTruongTheoDonVi>(apiResponse);

                                    totalrow = data.totalrow;
                                    row = Math.Round(totalrow / Convert.ToInt32(PERPAGE));
                                }
                            }

                            if (totalrow > 0)
                            {
                                for (var i = 1; i <= row + 1; i++)
                                {
                                    PhanAnhHienTruong phanAnhNew = null;

                                    var contentLoop = new PhanAnhHienTruongHttpContent
                                    {
                                        LoaiXuLy = DANG_XU_LY,
                                        MaDinhDanh = madinhdanh,
                                        TuNgay = TuNgay,
                                        DenNgay = DateTime.Now.ToString("yyyy-MM-dd"),
                                        Page = i.ToString(),
                                        Perpage = PERPAGE
                                    };

                                    var jsonLoop = JsonConvert.SerializeObject(contentLoop);
                                    HttpContent httpContentLoop = new StringContent(jsonLoop, Encoding.UTF8, "application/json");

                                    using (var responseLoop = await httpClient.PostAsync(url, httpContentLoop))
                                    {
                                        if (responseLoop.IsSuccessStatusCode)
                                        {
                                            var apiResponseLoop = await responseLoop.Content.ReadAsStringAsync();
                                            var dataDaXuLy = JsonConvert.DeserializeObject<DSPhanAnhHienTruongTheoDonVi>(apiResponseLoop);

                                            if (dataDaXuLy.data.Count() > 0)
                                            {
                                                foreach (var phananhhientruong in dataDaXuLy.data)
                                                {
                                                    if (phananhhientruong.PhanAnhID > 0)
                                                    {
                                                        string datePhanAnh = phananhhientruong.NgayPhanAnh.Replace("--", "-");
                                                        string dateXuLy = phananhhientruong.NgayTraLoi.Replace("--", "-");

                                                        DateTime? dateSend = null;
                                                        DateTime? dateTraLoi = null;

                                                        try
                                                        {
                                                            dateSend = DateTime.Parse(datePhanAnh);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            dateSend = null;
                                                        }
                                                        try
                                                        {
                                                            dateTraLoi = DateTime.Parse(dateXuLy);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            dateTraLoi = null;
                                                        }

                                                        PhanAnhHienTruong dataSaveChanges = new PhanAnhHienTruong
                                                        {
                                                            MaCoQuanXuLy = madinhdanh,
                                                            MaLinhVuc = phananhhientruong.ChuyenMucID ?? 0,
                                                            NgayGui = dateSend,
                                                            NgayXuLy = dateTraLoi,
                                                            NoiDung = phananhhientruong.NoiDungPhanAnh,
                                                            X = phananhhientruong.ViDo ?? 0,
                                                            Y = phananhhientruong.KinhDo ?? 0,
                                                            YKienXuLy = phananhhientruong.NoiDungTraLoi,
                                                            LoaiXuLy = false,
                                                            TieuDe = phananhhientruong.TieuDe,
                                                            DiaChiSuKien = phananhhientruong.DiaChiSuKien,
                                                            NgayTao = DateTime.Now,
                                                            PhanAnhId = phananhhientruong.PhanAnhID
                                                        };

                                                        bool isHoatDong = listLinhVucHoatDong.Where(x => x.Id == dataSaveChanges.MaLinhVuc).Any();

                                                        if (isHoatDong)
                                                        {
                                                            if (!phanAnhHienTruongs.Any())
                                                            {
                                                                phanAnhNew = await _repositoryPhanAnhHienTruong.InsertPhanAnhHienTruong(dataSaveChanges);
                                                            }
                                                            else
                                                            {
                                                                var phanAnhHienTruong = await _repositoryPhanAnhHienTruong.GetByDongBoID(phananhhientruong.PhanAnhID);
                                                                if (phanAnhHienTruong == null)
                                                                {
                                                                    phanAnhNew = await _repositoryPhanAnhHienTruong.InsertPhanAnhHienTruong(dataSaveChanges);
                                                                }
                                                                else
                                                                {
                                                                    if (String.IsNullOrEmpty(phanAnhHienTruong.TenCoQuan))
                                                                    {
                                                                        dataSaveChanges.TenCoQuan = phanAnhHienTruong.TenCoQuan;
                                                                    }

                                                                    dataSaveChanges.Id = phanAnhHienTruong.Id;
                                                                    dataSaveChanges.NguoiGui = phanAnhHienTruong.NguoiGui;
                                                                    phanAnhNew = await _repositoryPhanAnhHienTruong.UpdatePhanAnhHienTruong(dataSaveChanges);
                                                                }
                                                            }

                                                            if (hinhAnhsDB.Count() > 0)
                                                            {
                                                                var hinhAnhDB = await _repositoryPhanAnhHienTruongHinhAnh.GetsPhanAnhHienTruongHinhAnhByPhanAnhId(phanAnhNew.Id);
                                                                if (hinhAnhDB.Count() > 0)
                                                                {
                                                                    foreach (var hinhanh in hinhAnhDB)
                                                                    {
                                                                        var hinhAnhMoi = new PhanAnhHienTruongHinhAnh
                                                                        {
                                                                            HinhAnh = hinhanh.HinhAnh,
                                                                            MaPhanAnh = phanAnhNew.Id,
                                                                            Id = hinhanh.Id,
                                                                            HinhAnhThumb = hinhanh.HinhAnhThumb
                                                                        };

                                                                        if (hinhanh.IsKetQua == true)
                                                                        {
                                                                            hinhAnhMoi.IsKetQua = true;
                                                                        }
                                                                        else
                                                                        {
                                                                            hinhAnhMoi.IsKetQua = false;
                                                                        }
                                                                        await _repositoryPhanAnhHienTruongHinhAnh.UpdatePhanAnhHienTruongHinhAnh(hinhAnhMoi);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    foreach (var fileDinhKemKQ in phananhhientruong.DanhSachFileDinhKem_Kq)
                                                                    {
                                                                        await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                        {
                                                                            HinhAnh = fileDinhKemKQ.FileName,
                                                                            IsKetQua = true,
                                                                            MaPhanAnh = phanAnhNew.Id,
                                                                            HinhAnhThumb = fileDinhKemKQ.FileName_Thumb
                                                                        });
                                                                    }

                                                                    foreach (var fileDinhKem in phananhhientruong.DanhSachFileDinhKem)
                                                                    {
                                                                        await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                        {
                                                                            HinhAnh = fileDinhKem.FileName,
                                                                            IsKetQua = false,
                                                                            HinhAnhThumb = fileDinhKem.FileName_Thumb,
                                                                            MaPhanAnh = phanAnhNew.Id
                                                                        });
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                foreach (var fileDinhKemKQ in phananhhientruong.DanhSachFileDinhKem_Kq)
                                                                {
                                                                    await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                    {
                                                                        HinhAnh = fileDinhKemKQ.FileName,
                                                                        IsKetQua = true,
                                                                        MaPhanAnh = phanAnhNew.Id,
                                                                        HinhAnhThumb = fileDinhKemKQ.FileName_Thumb
                                                                    });
                                                                }

                                                                foreach (var fileDinhKem in phananhhientruong.DanhSachFileDinhKem)
                                                                {
                                                                    await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                    {
                                                                        HinhAnh = fileDinhKem.FileName,
                                                                        IsKetQua = false,
                                                                        HinhAnhThumb = fileDinhKem.FileName_Thumb,
                                                                        MaPhanAnh = phanAnhNew.Id
                                                                    });
                                                                }
                                                            }

                                                            if (phanAnhNew != null && phanAnhNew.X != null && phanAnhNew.X != 0 && phanAnhNew.Y != null && phanAnhNew.Y != 0)
                                                            {
                                                                TechLife.Model.HueCIT.ToaDo geo = new TechLife.Model.HueCIT.ToaDo
                                                                {
                                                                    x = phanAnhNew.Y,
                                                                    y = phanAnhNew.X
                                                                };

                                                                TechLife.Model.HueCIT.PhanAnhDongBoAdd data = new Model.HueCIT.PhanAnhDongBoAdd
                                                                {
                                                                    id = phanAnhNew.Id,
                                                                    tieude = phanAnhNew.TieuDe,
                                                                    diachisuki = phanAnhNew.DiaChiSuKien,
                                                                };

                                                                AddEditGIS(data, geo);
                                                            }
                                                        }
                                                    }
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        // Đồng bộ phản ánh hiện trường theo lĩnh vực
        // Đã xử lý
        public async Task GetDataLinhVuc(int linhvucId, bool isEnable)
        {
            try
            {
                if (isEnable)
                {
                    var token = _config.GetValue<string>("PhanAnhToken");
                    var baseUrl = _config.GetValue<string>("TuongTacAddress");
                    string TuNgay = TU_NGAY;

                    // Danh sách hình ảnh trên DB HueCIT
                    var hinhAnhsDB = await _repositoryPhanAnhHienTruongHinhAnh.GetsPhanAnhHienTruongHinhAnh();
                    // Danh sách hiện trường đã xử lý
                    var phanAnhHienTruongs = await _repositoryPhanAnhHienTruong.GetsPhanAnhHienTruongByLoaiXuLy(1);
                    // Ngày đồng bộ phản ánh hiện trường gần nhất
                    string DenNgay = phanAnhHienTruongs.FirstOrDefault().NgayTao.ToString("yyyy-MM-dd");

                    #region Lưu lĩnh vực phản ánh hiện trường
                    using (var httpClient = new HttpClient())
                    {
                        var url = $"{baseUrl}/theodonvi";
                        // Add Token
                        httpClient.DefaultRequestHeaders.Add("token", token);
                        httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

                        var DANH_SACH_MA_DINH_DANH = await _repositoryPhanAnhHienTruongCoQuan.GetsPhanAnhHienTruongCoQuan();
                        if (DANH_SACH_MA_DINH_DANH.Count() > 0)
                        {
                            foreach (var j in DANH_SACH_MA_DINH_DANH)
                            {
                                string madinhdanh = j.Id;
                                string tendinhdanh = j.TenCoQuan;
                                double totalrow = 0;
                                double row = 0;

                                #region lấy thông tin LoaiXuLy=1 (đã xử lý)
                                var content = new PhanAnhHienTruongHttpContent
                                {
                                    LoaiXuLy = DA_XU_LY,
                                    MaDinhDanh = madinhdanh,
                                    TuNgay = TuNgay,
                                    DenNgay = DenNgay,
                                    Page = PAGE,
                                    Perpage = PERPAGE
                                };
                                var passedContent = JsonConvert.SerializeObject(content);
                                HttpContent httpContent = new StringContent(passedContent.ToString(), Encoding.UTF8, "application/json");

                                using (var response = await httpClient.PostAsync(url, httpContent))
                                {
                                    if (response.IsSuccessStatusCode)
                                    {
                                        var apiResponse = await response.Content.ReadAsStringAsync();
                                        var data = JsonConvert.DeserializeObject<DSPhanAnhHienTruongTheoDonVi>(apiResponse);

                                        totalrow = data.totalrow;
                                        row = Math.Round(totalrow / Convert.ToInt32(PERPAGE));
                                    }
                                }

                                if (totalrow > 0)
                                {
                                    for (var i = 1; i <= row + 1; i++)
                                    {
                                        var contentLoop = new PhanAnhHienTruongHttpContent
                                        {
                                            LoaiXuLy = DA_XU_LY,
                                            MaDinhDanh = madinhdanh,
                                            TuNgay = TuNgay,
                                            DenNgay = DateTime.Now.ToString("yyyy-MM-dd"),
                                            Page = i.ToString(),
                                            Perpage = PERPAGE
                                        };
                                        var jsonLoop = JsonConvert.SerializeObject(contentLoop);
                                        HttpContent httpContentLoop = new StringContent(jsonLoop.ToString(), Encoding.UTF8, "application/json");

                                        using (var responseLoop = await httpClient.PostAsync(url, httpContentLoop))
                                        {
                                            if (responseLoop.IsSuccessStatusCode)
                                            {
                                                var apiResponseLoop = await responseLoop.Content.ReadAsStringAsync();
                                                var dataDaXuLy = JsonConvert.DeserializeObject<DSPhanAnhHienTruongTheoDonVi>(apiResponseLoop);

                                                if (dataDaXuLy.data.Count() > 0)
                                                {
                                                    foreach (var phananhhientruong in dataDaXuLy.data)
                                                    {
                                                        if (phananhhientruong.PhanAnhID > 0 && phananhhientruong.ChuyenMucID == linhvucId)
                                                        {
                                                            PhanAnhHienTruong phanAnhNew;

                                                            // Xử lý ngày
                                                            string datePhanAnh = phananhhientruong.NgayPhanAnh.Replace("--", "-");
                                                            string dateTraLoi = phananhhientruong.NgayTraLoi.Replace("--", "-");

                                                            DateTime? dateSend = null;
                                                            DateTime? dateResult = null;
                                                            try
                                                            {
                                                                dateSend = DateTime.Parse(datePhanAnh);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                dateSend = null;
                                                            }
                                                            try
                                                            {
                                                                dateResult = DateTime.Parse(dateTraLoi);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                dateResult = null;
                                                            }

                                                            PhanAnhHienTruong dataSaveChanges = new PhanAnhHienTruong
                                                            {
                                                                MaCoQuanXuLy = madinhdanh,
                                                                TenCoQuan = tendinhdanh,
                                                                MaLinhVuc = phananhhientruong.ChuyenMucID ?? 0,
                                                                NgayGui = dateSend,
                                                                NgayXuLy = dateResult,
                                                                NoiDung = phananhhientruong.NoiDungPhanAnh,
                                                                X = phananhhientruong.ViDo ?? 0,
                                                                Y = phananhhientruong.KinhDo ?? 0,
                                                                YKienXuLy = phananhhientruong.NoiDungTraLoi,
                                                                LoaiXuLy = true,
                                                                TieuDe = phananhhientruong.TieuDe,
                                                                DiaChiSuKien = phananhhientruong.DiaChiSuKien,
                                                                NgayTao = DateTime.Now,
                                                                PhanAnhId = phananhhientruong.PhanAnhID
                                                            };


                                                            // Kiểm tra dữ liệu trên database HueCIT với hiện trường đã xử lý 
                                                            // Nếu có -> cập nhật
                                                            // Nếu không -> thêm mới
                                                            if (!phanAnhHienTruongs.Any())
                                                            {
                                                                phanAnhNew = await _repositoryPhanAnhHienTruong.InsertPhanAnhHienTruong(dataSaveChanges);
                                                            }
                                                            else
                                                            {
                                                                var phanAnhHienTruong = await _repositoryPhanAnhHienTruong.GetByDongBoID(dataSaveChanges.PhanAnhId);

                                                                if (phanAnhHienTruong == null)
                                                                {
                                                                    phanAnhNew = await _repositoryPhanAnhHienTruong.InsertPhanAnhHienTruong(dataSaveChanges);
                                                                }
                                                                else
                                                                {
                                                                    dataSaveChanges.Id = phanAnhHienTruong.Id;
                                                                    phanAnhNew = await _repositoryPhanAnhHienTruong.UpdatePhanAnhHienTruong(dataSaveChanges);
                                                                }
                                                            }

                                                            if (hinhAnhsDB.Count() > 0)
                                                            {
                                                                // Kiểm tra dữ liệu hình ảnh phản ánh hiện trường trên database HueCIT
                                                                // Nếu Có --> cập nhật
                                                                // Nếu không --> thêm mới
                                                                var hinhAnhDB = await _repositoryPhanAnhHienTruongHinhAnh.GetsPhanAnhHienTruongHinhAnhByPhanAnhId(phanAnhNew.Id);
                                                                if (hinhAnhDB.Count() > 0)
                                                                {
                                                                    foreach (var hinhanh in hinhAnhDB)
                                                                    {
                                                                        var hinhAnhMoi = new PhanAnhHienTruongHinhAnh
                                                                        {
                                                                            HinhAnh = hinhanh.HinhAnh,
                                                                            MaPhanAnh = phanAnhNew.Id,
                                                                            HinhAnhThumb = hinhanh.HinhAnhThumb,
                                                                            Id = hinhanh.Id
                                                                        };

                                                                        // Kiểm tra IsKetQua --> cập nhật
                                                                        // true -> hình ảnh kết quả xử lý 
                                                                        // false -> hình ảnh gửi phản ánh
                                                                        if (hinhanh.IsKetQua == true)
                                                                        {
                                                                            hinhAnhMoi.IsKetQua = true;
                                                                            await _repositoryPhanAnhHienTruongHinhAnh.UpdatePhanAnhHienTruongHinhAnh(hinhAnhMoi);
                                                                        }
                                                                        else
                                                                        {
                                                                            hinhAnhMoi.IsKetQua = false;
                                                                            await _repositoryPhanAnhHienTruongHinhAnh.UpdatePhanAnhHienTruongHinhAnh(hinhAnhMoi);
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    // Thêm mới hình ảnh phản ánh trả lời
                                                                    foreach (var fileDinhKemKQ in phananhhientruong.DanhSachFileDinhKem_Kq)
                                                                    {
                                                                        await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                        {
                                                                            HinhAnh = fileDinhKemKQ.FileName,
                                                                            HinhAnhThumb = fileDinhKemKQ.FileName_Thumb,
                                                                            IsKetQua = true,
                                                                            MaPhanAnh = phanAnhNew.Id
                                                                        });
                                                                    }

                                                                    // Thêm mói hình ảnh gửi phản ánh
                                                                    foreach (var fileDinhKem in phananhhientruong.DanhSachFileDinhKem)
                                                                    {
                                                                        await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                        {
                                                                            HinhAnhThumb = fileDinhKem.FileName_Thumb,
                                                                            HinhAnh = fileDinhKem.FileName,
                                                                            IsKetQua = false,
                                                                            MaPhanAnh = phanAnhNew.Id
                                                                        });
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                // Thêm mới hình ảnh phản ánh trả lời
                                                                foreach (var fileDinhKemKQ in phananhhientruong.DanhSachFileDinhKem_Kq)
                                                                {
                                                                    await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                    {
                                                                        HinhAnhThumb = fileDinhKemKQ.FileName_Thumb,
                                                                        HinhAnh = fileDinhKemKQ.FileName,
                                                                        IsKetQua = true,
                                                                        MaPhanAnh = phanAnhNew.Id
                                                                    });
                                                                }

                                                                // Thêm mói hình ảnh gửi phản ánh
                                                                foreach (var fileDinhKem in phananhhientruong.DanhSachFileDinhKem)
                                                                {
                                                                    await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                    {
                                                                        HinhAnhThumb = fileDinhKem.FileName_Thumb,
                                                                        HinhAnh = fileDinhKem.FileName,
                                                                        IsKetQua = false,
                                                                        MaPhanAnh = phanAnhNew.Id
                                                                    });
                                                                }
                                                            }
                                                        }
                                                        else continue;
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
                    #endregion
                }
                else
                {
                    // Xóa phản ánh theo lĩnh vực không hoạt động nữa
                    var listPhanAnh = (await _repositoryPhanAnhHienTruong.GetsByLinhVuc(linhvucId)).ToList();

                    if (listPhanAnh.Any())
                    {
                        foreach (var phanAnh in listPhanAnh)
                        {
                            var hinhanh = await _repositoryPhanAnhHienTruongHinhAnh.GetsPhanAnhHienTruongHinhAnhByPhanAnhId(phanAnh.Id);

                            if (hinhanh.Any())
                            {
                                foreach (var item in hinhanh)
                                {
                                    await _repositoryPhanAnhHienTruongHinhAnh.Delete(item.Id);
                                }
                            }

                            await _repositoryPhanAnhHienTruong.Delete(phanAnh.Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        // Đang xử lý
        public async Task GetDataWaitLinhVuc(int linhvucId, bool isEnable)
        {
            try
            {
                if (isEnable)
                {
                    var token = _config.GetValue<string>("PhanAnhToken");
                    var baseUrl = _config.GetValue<string>("TuongTacAddress");
                    var TuNgay = TU_NGAY;

                    // Danh sách hình ảnh của hiện trường hiện có trên DB HueCIT
                    var hinhAnhsDB = await _repositoryPhanAnhHienTruongHinhAnh.GetsPhanAnhHienTruongHinhAnh();
                    // Danh sách hiện trường có trên DB HueCIT
                    var phanAnhHienTruongs = await _repositoryPhanAnhHienTruong.GetsPhanAnhHienTruongByLoaiXuLy(0);

                    // Ngày đồng bộ phản ánh hiện trường gần nhất
                    string DenNgay = phanAnhHienTruongs.FirstOrDefault().NgayTao.ToString("yyyy-MM-dd");

                    #region lấy thông tin LoaiXuLy=0 (đang xử lý)
                    var DANH_SACH_MA_DINH_DANH = await _repositoryPhanAnhHienTruongCoQuan.GetsPhanAnhHienTruongCoQuan();
                    if (DANH_SACH_MA_DINH_DANH.Count() > 0)
                    {
                        foreach (var j in DANH_SACH_MA_DINH_DANH)
                        {
                            string madinhdanh = j.Id;
                            string tendinhdanh = j.TenCoQuan;
                            double totalrow = 0;
                            double row = 0;

                            using (var httpClient = new HttpClient())
                            {
                                var url = $"{baseUrl}/theodonvi";

                                httpClient.DefaultRequestHeaders.Add("token", token);
                                httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

                                var content = new PhanAnhHienTruongHttpContent
                                {
                                    LoaiXuLy = DANG_XU_LY,
                                    MaDinhDanh = madinhdanh,
                                    TuNgay = TuNgay,
                                    DenNgay = DenNgay,
                                    Page = PAGE,
                                    Perpage = PERPAGE
                                };
                                var passedContent = JsonConvert.SerializeObject(content);
                                HttpContent httpContent = new StringContent(passedContent, Encoding.UTF8, "application/json");

                                using (var response = await httpClient.PostAsync(url, httpContent))
                                {
                                    if (response.IsSuccessStatusCode)
                                    {
                                        var apiResponse = await response.Content.ReadAsStringAsync();
                                        var data = JsonConvert.DeserializeObject<DSPhanAnhHienTruongTheoDonVi>(apiResponse);

                                        totalrow = data.totalrow;
                                        row = Math.Round(totalrow / Convert.ToInt32(PERPAGE));
                                    }
                                }

                                if (totalrow > 0)
                                {
                                    for (var i = 1; i <= row + 1; i++)
                                    {
                                        PhanAnhHienTruong phanAnhNew = null;

                                        var contentLoop = new PhanAnhHienTruongHttpContent
                                        {
                                            LoaiXuLy = DANG_XU_LY,
                                            MaDinhDanh = madinhdanh,
                                            TuNgay = TuNgay,
                                            DenNgay = DateTime.Now.ToString("yyyy-MM-dd"),
                                            Page = i.ToString(),
                                            Perpage = PERPAGE
                                        };

                                        var jsonLoop = JsonConvert.SerializeObject(contentLoop);
                                        HttpContent httpContentLoop = new StringContent(jsonLoop, Encoding.UTF8, "application/json");

                                        using (var responseLoop = await httpClient.PostAsync(url, httpContentLoop))
                                        {
                                            if (responseLoop.IsSuccessStatusCode)
                                            {
                                                var apiResponseLoop = await responseLoop.Content.ReadAsStringAsync();
                                                var dataDaXuLy = JsonConvert.DeserializeObject<DSPhanAnhHienTruongTheoDonVi>(apiResponseLoop);

                                                if (dataDaXuLy.data.Count() > 0)
                                                {
                                                    foreach (var phananhhientruong in dataDaXuLy.data)
                                                    {
                                                        if (phananhhientruong.PhanAnhID > 0 && phananhhientruong.ChuyenMucID == linhvucId)
                                                        {
                                                            // Xử lý ngày
                                                            string datePhanAnh = phananhhientruong.NgayPhanAnh.Replace("--", "-");
                                                            string dateXuLy = phananhhientruong.NgayTraLoi.Replace("--", "-");
                                                            DateTime? dateSend = null;
                                                            DateTime? dateTraLoi = null;

                                                            try
                                                            {
                                                                dateSend = DateTime.Parse(datePhanAnh);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                dateSend = null;
                                                            }
                                                            try
                                                            {
                                                                dateTraLoi = DateTime.Parse(dateXuLy);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                dateTraLoi = null;
                                                            }

                                                            PhanAnhHienTruong dataSaveChanges = new PhanAnhHienTruong
                                                            {
                                                                MaCoQuanXuLy = madinhdanh,
                                                                MaLinhVuc = phananhhientruong.ChuyenMucID ?? 0,
                                                                NgayGui = dateSend,
                                                                NgayXuLy = dateTraLoi,
                                                                NoiDung = phananhhientruong.NoiDungPhanAnh,
                                                                X = phananhhientruong.ViDo ?? 0,
                                                                Y = phananhhientruong.KinhDo ?? 0,
                                                                YKienXuLy = phananhhientruong.NoiDungTraLoi,
                                                                LoaiXuLy = false,
                                                                TieuDe = phananhhientruong.TieuDe,
                                                                DiaChiSuKien = phananhhientruong.DiaChiSuKien,
                                                                NgayTao = DateTime.Now,
                                                                PhanAnhId = phananhhientruong.PhanAnhID
                                                            };


                                                            if (!phanAnhHienTruongs.Any())
                                                            {
                                                                phanAnhNew = await _repositoryPhanAnhHienTruong.InsertPhanAnhHienTruong(dataSaveChanges);
                                                            }
                                                            else
                                                            {
                                                                var phanAnhHienTruong = await _repositoryPhanAnhHienTruong.GetByDongBoID(phananhhientruong.PhanAnhID);
                                                                if (phanAnhHienTruong == null)
                                                                {
                                                                    phanAnhNew = await _repositoryPhanAnhHienTruong.InsertPhanAnhHienTruong(dataSaveChanges);
                                                                }
                                                                else
                                                                {
                                                                    if (String.IsNullOrEmpty(phanAnhHienTruong.TenCoQuan))
                                                                    {
                                                                        dataSaveChanges.TenCoQuan = phanAnhHienTruong.TenCoQuan;
                                                                    }

                                                                    dataSaveChanges.Id = phanAnhHienTruong.Id;
                                                                    phanAnhNew = await _repositoryPhanAnhHienTruong.UpdatePhanAnhHienTruong(dataSaveChanges);
                                                                }
                                                            }

                                                            if (hinhAnhsDB.Count() > 0)
                                                            {
                                                                var hinhAnhDB = await _repositoryPhanAnhHienTruongHinhAnh.GetsPhanAnhHienTruongHinhAnhByPhanAnhId(phanAnhNew.Id);
                                                                if (hinhAnhDB.Count() > 0)
                                                                {
                                                                    foreach (var hinhanh in hinhAnhDB)
                                                                    {
                                                                        var hinhAnhMoi = new PhanAnhHienTruongHinhAnh
                                                                        {
                                                                            HinhAnh = hinhanh.HinhAnh,
                                                                            MaPhanAnh = phanAnhNew.Id,
                                                                            Id = hinhanh.Id,
                                                                            HinhAnhThumb = hinhanh.HinhAnhThumb
                                                                        };

                                                                        if (hinhanh.IsKetQua == true)
                                                                        {
                                                                            hinhAnhMoi.IsKetQua = true;
                                                                        }
                                                                        else
                                                                        {
                                                                            hinhAnhMoi.IsKetQua = false;
                                                                        }
                                                                        await _repositoryPhanAnhHienTruongHinhAnh.UpdatePhanAnhHienTruongHinhAnh(hinhAnhMoi);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    foreach (var fileDinhKemKQ in phananhhientruong.DanhSachFileDinhKem_Kq)
                                                                    {
                                                                        await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                        {
                                                                            HinhAnh = fileDinhKemKQ.FileName,
                                                                            IsKetQua = true,
                                                                            MaPhanAnh = phanAnhNew.Id,
                                                                            HinhAnhThumb = fileDinhKemKQ.FileName_Thumb
                                                                        });
                                                                    }

                                                                    foreach (var fileDinhKem in phananhhientruong.DanhSachFileDinhKem)
                                                                    {
                                                                        await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                        {
                                                                            HinhAnh = fileDinhKem.FileName,
                                                                            IsKetQua = false,
                                                                            HinhAnhThumb = fileDinhKem.FileName_Thumb,
                                                                            MaPhanAnh = phanAnhNew.Id
                                                                        });
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                foreach (var fileDinhKemKQ in phananhhientruong.DanhSachFileDinhKem_Kq)
                                                                {
                                                                    await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                    {
                                                                        HinhAnh = fileDinhKemKQ.FileName,
                                                                        IsKetQua = true,
                                                                        MaPhanAnh = phanAnhNew.Id,
                                                                        HinhAnhThumb = fileDinhKemKQ.FileName_Thumb
                                                                    });
                                                                }

                                                                foreach (var fileDinhKem in phananhhientruong.DanhSachFileDinhKem)
                                                                {
                                                                    await _repositoryPhanAnhHienTruongHinhAnh.InsertPhanAnhHienTruongHinhAnh(new PhanAnhHienTruongHinhAnh
                                                                    {
                                                                        HinhAnh = fileDinhKem.FileName,
                                                                        IsKetQua = false,
                                                                        HinhAnhThumb = fileDinhKem.FileName_Thumb,
                                                                        MaPhanAnh = phanAnhNew.Id
                                                                    });
                                                                }
                                                            }
                                                        }
                                                        else continue;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    // Xóa phản ánh theo lĩnh vực không hoạt động nữa
                    var listPhanAnh = (await _repositoryPhanAnhHienTruong.GetsByLinhVuc(linhvucId)).ToList();

                    if (listPhanAnh.Any())
                    {
                        foreach (var phanAnh in listPhanAnh)
                        {
                            var hinhanh = await _repositoryPhanAnhHienTruongHinhAnh.GetsPhanAnhHienTruongHinhAnhByPhanAnhId(phanAnh.Id);

                            if (hinhanh.Any())
                            {
                                foreach (var item in hinhanh)
                                {
                                    await _repositoryPhanAnhHienTruongHinhAnh.Delete(item.Id);
                                }
                            }

                            await _repositoryPhanAnhHienTruong.Delete(phanAnh.Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private void AddEditGIS(TechLife.Model.HueCIT.PhanAnhDongBoAdd data, TechLife.Model.HueCIT.ToaDo geo)
        {
            int layer = 13;
            string ctk = _config.GetValue<string>("ArcGisToken");
            List<TechLife.Model.HueCIT.DongBoDieuHanhPhanAnhAdd> dta = new List<TechLife.Model.HueCIT.DongBoDieuHanhPhanAnhAdd>();
            List<TechLife.Model.HueCIT.DongBoDieuHanhPhanAnhEdit> dte = new List<TechLife.Model.HueCIT.DongBoDieuHanhPhanAnhEdit>();

            var check = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/query?where=id%3D" + data.id + "&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Foot&relationParam=&outFields=*&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&havingClause=&gdbVersion=&historicMoment=&returnDistinctValues=false&returnIdsOnly=true&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&multipatchOption=xyFootprint&resultOffset=&resultRecordCount=&returnTrueCurves=false&returnExceededLimitFeatures=false&quantizationParameters=&returnCentroid=false&sqlFormat=none&resultType=&featureEncoding=esriDefault&datumTransformation=&f=json");
            check.Timeout = -1;
            var chk = new RestRequest(Method.GET);
            //chk.AddHeader("Cookie", "AGS_ROLES=\"419jqfa+uOZgYod4xPOQ8Q==\"");
            chk.AddHeader("Cookie", ctk);
            IRestResponse res = check.Execute(chk);
            var chkinfo = JsonConvert.DeserializeObject<TechLife.Model.HueCIT.CheckResponse>(res.Content);

            if (chkinfo.objectIds == null)
            {
                TechLife.Model.HueCIT.DongBoDieuHanhPhanAnhAdd dt = new TechLife.Model.HueCIT.DongBoDieuHanhPhanAnhAdd
                {
                    geometry = new TechLife.Model.HueCIT.ToaDo
                    {
                        x = geo.x,
                        y = geo.y,
                        spatialReference = new TechLife.Model.HueCIT.SpatialReference
                        {
                            wkid = 4326
                        }
                    },
                    attributes = new TechLife.Model.HueCIT.PhanAnhDongBoAdd
                    {
                        id = data.id,
                        tieude = data.tieude,
                        diachisuki = data.diachisuki,
                    }
                };

                dta.Add(dt);

                var client = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/addFeatures");
                client.Timeout = -1;
                var req = new RestRequest(Method.POST);
                //req.AddHeader("Cookie", "AGS_ROLES=\"419jqfa+uOZgYod4xPOQ8Q==\"");
                req.AddHeader("Cookie", ctk);
                req.AlwaysMultipartFormData = true;
                req.AddParameter("features", JsonConvert.SerializeObject(dta));
                req.AddParameter("f", "json");
                req.AddParameter("rollbackOnFailure", "false");
                IRestResponse response = client.Execute(req);
            }
            else
            {
                TechLife.Model.HueCIT.DongBoDieuHanhPhanAnhEdit dt = new TechLife.Model.HueCIT.DongBoDieuHanhPhanAnhEdit
                {
                    geometry = new TechLife.Model.HueCIT.ToaDo
                    {
                        x = geo.x,
                        y = geo.y,
                        spatialReference = new TechLife.Model.HueCIT.SpatialReference
                        {
                            wkid = 4326
                        }
                    },
                    attributes = new TechLife.Model.HueCIT.PhanAnhDongBoEdit
                    {
                        objectid = chkinfo.objectIds.First(),
                        id = data.id,
                        tieude = data.tieude,
                        diachisuki = data.diachisuki,
                    }
                };

                dte.Add(dt);

                string test = JsonConvert.SerializeObject(dta);

                var client = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/updateFeatures");
                client.Timeout = -1;
                var req = new RestRequest(Method.POST);
                //req.AddHeader("Cookie", "AGS_ROLES=\"419jqfa+uOZgYod4xPOQ8Q==\"");
                req.AddHeader("Cookie", ctk);
                req.AlwaysMultipartFormData = true;
                req.AddParameter("features", JsonConvert.SerializeObject(dte));
                req.AddParameter("f", "json");
                req.AddParameter("rollbackOnFailure", "false");
                IRestResponse response = client.Execute(req);
            }
        }
    }
}
