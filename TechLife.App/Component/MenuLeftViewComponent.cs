using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.Models;
using TechLife.Common;
using TechLife.Model;

namespace TechLife.App.Component
{
    public class MenuLeftViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new List<MenuViewModel>();
            try
            {
                var userInfo = new UserModel();
                try
                {
                    var data = HttpContext.Session.GetString(SystemConstants.AppSettings.UserInfo);
                    userInfo = JsonConvert.DeserializeObject<UserModel>(data);
                }
                catch (Exception ex)
                {
                    userInfo = null;
                }

                ViewBag.User = userInfo;

                model.Add(new MenuViewModel() { Id = 1, Name = "Trang chủ", Url = "/", Icon = "fa-tachometer-alt" });
                if (userInfo != null)
                {
                    //Dữ liệu du lịch
                    if (userInfo.Roles.Contains("view_luutru")
                        || userInfo.Roles.Contains("view_nhahang")
                        || userInfo.Roles.Contains("view_muasam")
                        || userInfo.Roles.Contains("view_diemdulich")
                        || userInfo.Roles.Contains("view_khudulich")
                        || userInfo.Roles.Contains("view_congtyluhanh")
                        || userInfo.Roles.Contains("view_tourdulich")
                        || userInfo.Roles.Contains("view_huongdanvien")
                        || userInfo.Roles.Contains("view_congtyvanchuyen")
                        || userInfo.Roles.Contains("view_dichvuvuichoi")
                        || userInfo.Roles.Contains("view_dichvuthethao")
                        || userInfo.Roles.Contains("view_vesinhcongcong")
                        || userInfo.Roles.Contains("view_donviquanly")
                        || userInfo.Roles.Contains("view_dichvucssk")
                        || userInfo.Roles.Contains("view_diadiemanuong")
                        || userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 3, Name = "Dữ liệu du lịch", Url = "#", Icon = "fa-database" });
                    }

                    if (userInfo.Roles.Contains("view_luutru") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 4, Name = "Cơ sở lưu trú", Url = "/Hoso/Cosoluutru/", Icon = "fa-circle", GroupId = 3 });
                    if (userInfo.Roles.Contains("view_diemdulich") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 8, Name = "Điểm du lịch", Url = "/Hoso/Diemdulich/", Icon = "fa-circle", GroupId = 3 });
                    if (userInfo.Roles.Contains("view_khudulich") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 53, Name = "Khu du lịch", Url = "/Hoso/Khudulich/", Icon = "fa-circle", GroupId = 3 });
                    if (userInfo.Roles.Contains("view_disan") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 188, Name = "Di sản văn hóa", Url = "/HueCIT/DiSanVanHoa/Index/", Icon = "fa-circle", GroupId = 3 });
                    if (userInfo.Roles.Contains("view_tourdulich") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 61, Name = "Tour du lịch", Url = "/Dichvu/Tour/", Icon = "fa-circle", GroupId = 3 });
                    if (userInfo.Roles.Contains("view_congtyluhanh") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 61, Name = "Công ty lữ hành", Url = "/Hoso/Congtyluhanh/", Icon = "fa-circle", GroupId = 3 });
                    if (userInfo.Roles.Contains("view_huongdanvien") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 9, Name = "Hướng dẫn viên", Url = "/Hoso/Huongdanvien/", Icon = "fa-circle", GroupId = 3 });

                    //Dữ liệu dịch vụ
                    if (userInfo.Roles.Contains("view_luutru")
                        || userInfo.Roles.Contains("view_nhahang")
                        || userInfo.Roles.Contains("view_muasam")
                        || userInfo.Roles.Contains("view_diemdulich")
                        || userInfo.Roles.Contains("view_khudulich")
                        || userInfo.Roles.Contains("view_congtyluhanh")
                        || userInfo.Roles.Contains("view_tourdulich")
                        || userInfo.Roles.Contains("view_huongdanvien")
                        || userInfo.Roles.Contains("view_congtyvanchuyen")
                        || userInfo.Roles.Contains("view_dichvuvuichoi")
                        || userInfo.Roles.Contains("view_dichvuthethao")
                        || userInfo.Roles.Contains("view_vesinhcongcong")
                        || userInfo.Roles.Contains("view_donviquanly")
                        || userInfo.Roles.Contains("view_dichvucssk")
                        || userInfo.Roles.Contains("view_diadiemanuong")
                        || userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 200, Name = "Dữ liệu dịch vụ", Url = "#", Icon = "fa-database" });
                    }
                    //HueCIT
                    if (userInfo.Roles.Contains("view_diadiemanuong") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 105, Name = "Địa điểm ăn uống", Url = "/HueCIT/DiaDiemAnUong/Index/", Icon = "fa-circle", GroupId = 200 });

                    if (userInfo.Roles.Contains("view_nhahang") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 5, Name = "Nhà hàng", Url = "/Hoso/Nhahang/", Icon = "fa-circle", GroupId = 200 });
                    if (userInfo.Roles.Contains("view_muasam") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 7, Name = "Cơ sở mua sắm", Url = "/Hoso/Cosomuasam/", Icon = "fa-circle", GroupId = 200 });
                    if (userInfo.Roles.Contains("view_congtyvanchuyen") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 61, Name = "Công ty vận chuyển", Url = "/Hoso/Vanchuyen/", Icon = "fa-circle", GroupId = 200 });
                    if (userInfo.Roles.Contains("view_dichvuvuichoi") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 54, Name = "Dịch vụ vui chơi, giải trí", Url = "/Hoso/Vcgt/", Icon = "fa-circle", GroupId = 200 });
                    if (userInfo.Roles.Contains("view_dichvucssk") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 55, Name = "Dịch vụ chăm sóc sức khỏe", Url = "/Hoso/Cssk/", Icon = "fa-circle", GroupId = 200 });
                    if (userInfo.Roles.Contains("view_dichvuthethao") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 56, Name = "Dịch vụ thể thao", Url = "/Hoso/Thethao/", Icon = "fa-circle", GroupId = 200 });
                    if (userInfo.Roles.Contains("view_vesinhcongcong") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 10, Name = "Vệ sinh công cộng", Url = "/Hoso/Vesinhcongcong/", Icon = "fa-circle", GroupId = 200 });
                    if (userInfo.Roles.Contains("view_donviquanly") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 10, Name = "Đơn vị quản lý", Url = "/Hoso/Nhacungcap/", Icon = "fa-circle", GroupId = 200 });
                    //HueCIT
                    if (userInfo.Roles.Contains("view_thoitiet") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 110, Name = "Dự báo thời tiết", Url = "/HueCIT/ThoiTiet/Index/", Icon = "fa-circle", GroupId = 200 });
                    if (userInfo.Roles.Contains("view_diemgiaodich") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 111, Name = "Điểm giao dịch", Url = "/HueCIT/DiemGiaoDich/Index/", Icon = "fa-circle", GroupId = 200 });
                    if (userInfo.Roles.Contains("view_duongdaynong") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 112, Name = "Đường dây nóng", Url = "/HueCIT/DuongDayNong/Index/", Icon = "fa-circle", GroupId = 200 });
                    if (userInfo.Roles.Contains("view_quantrac") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 166, Name = "Quan trắc môi trường", Url = "/HueCIT/QuanTracMoiTruong/Index/", Icon = "fa-circle", GroupId = 200 });
                    if (userInfo.Roles.Contains("view_quantrac") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 167, Name = "Tỷ giá", Url = "/HueCIT/TyGia/Index/", Icon = "fa-circle", GroupId = 200 });

                    //Dữ liệu chuyên ngành liên thông
                    //HueCIT
                    if (userInfo.Roles.Contains("view_lienthongve")
                        || userInfo.Roles.Contains("view_lienthonghues")
                        || userInfo.Roles.Contains("view_lienthongdulich")
                        || userInfo.Roles.Contains("view_lienthongdoanhnghiep")
                        || userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 101, Name = "Dữ liệu chuyên ngành", Url = "#", Icon = "fa-database" });
                    }
                    if (userInfo.Roles.Contains("view_lienthongve") || userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 102, Name = "Vé điện tử tham quan di tích", Url = "/HueCIT/Ve/Index/", Icon = "fa-circle", GroupId = 101 });
                    }
                    if (userInfo.Roles.Contains("view_lienthonghues") || userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 103, Name = "Phản ánh hiện trường Hue-S", Url = "/HueCIT/HienTruong/Index/", Icon = "fa-circle", GroupId = 101 });
                    }
                    if (userInfo.Roles.Contains("view_lienthongdoanhnghiep") || userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 104, Name = "Doanh nghiệp", Url = "/HueCIT/DoanhNghiep/Index/", Icon = "fa-circle", GroupId = 101 });
                    }
                    if (userInfo.Roles.Contains("view_lienthongsukien") || userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 105, Name = "Sự kiện", Url = "/HueCIT/SuKien/Index/", Icon = "fa-circle", GroupId = 101 });
                    }
                    if (userInfo.Roles.Contains("view_lienthonglehoi") || userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 108, Name = "Lễ hội", Url = "/HueCIT/LeHoi/Index/", Icon = "fa-circle", GroupId = 101 });
                    }
                    if (userInfo.Roles.Contains("view_lienthongamthuc") || userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 109, Name = "Ẩm thực", Url = "/HueCIT/AmThuc/Index/", Icon = "fa-circle", GroupId = 101 });
                    }
                    if (userInfo.Roles.Contains("view_lienthongdichvuditich") || userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 110, Name = "Dịch vụ di tích", Url = "/HueCIT/DichVuDiTich/Index/", Icon = "fa-circle", GroupId = 101 });
                    }

                    if (userInfo.Roles.Contains("view_dulieuthanhtra") || userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 30, Name = "Dữ liệu thanh tra", Url = "#", Icon = "fa-list-alt" });
                    }
                    if (userInfo.Roles.Contains("create_dulieuthanhtra") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 16, Name = "Thêm hồ sơ", Url = "/Thanhtra/Themmoi/", Icon = "fa-circle", GroupId = 30 });
                    if (userInfo.Roles.Contains("view_dulieuthanhtra") || userInfo.Roles.Contains("root"))
                        model.Add(new MenuViewModel() { Id = 16, Name = "Danh sách hồ sơ", Url = "/Thanhtra/Danhsach/", Icon = "fa-circle", GroupId = 30 });

                    if (userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 60, Name = "Dịch vụ công", Url = "/Dvc/Tracuu/", Icon = "fa-book" });
                    }

                    if (userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 11, Name = "Quản trị danh mục", Url = "#", Icon = "fa-list-alt" });
                        model.Add(new MenuViewModel() { Id = 16, Name = "Loại điểm du lịch", Url = "/Danhmuc/Diemdulich/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 16, Name = "Loại khu du lịch", Url = "/Danhmuc/Khudulich/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 16, Name = "Loại khu vui chơi", Url = "/Danhmuc/Khuvcgt/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 16, Name = "Loại CS chăm sóc sức khỏe", Url = "/Danhmuc/Cssk/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 16, Name = "Loại cơ sở thể thao", Url = "/Danhmuc/Thethao/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 14, Name = "Loại công ty lữ hành", Url = "/Danhmuc/Congtyluhanh/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 14, Name = "Loại công ty vận chuyển", Url = "/Danhmuc/Vanchuyen/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 20, Name = "Loại văn bản liên quan", Url = "/Danhmuc/Giayphep/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 22, Name = "Loại nhà hàng", Url = "/Danhmuc/Dichvu/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 23, Name = "Loại mua sắm", Url = "/Danhmuc/Loaidichvu/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 24, Name = "Loại dịch vụ, tiện nghi", Url = "/Danhmuc/Tiennghi/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 25, Name = "Loại hình lưu trú", Url = "/Danhmuc/Loaihinhkinhdoanh/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 20, Name = "Quốc tịch", Url = "/Danhmuc/Quoctich/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 21, Name = "Bộ phận", Url = "/Danhmuc/Bophan/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 26, Name = "Ngoại ngữ", Url = "/Danhmuc/Ngoaingu/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 27, Name = "Loại phòng", Url = "/Danhmuc/Loaiphong/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 23, Name = "Loại giường", Url = "/Danhmuc/Loaigiuong/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 23, Name = "Đơn vị tính", Url = "/Danhmuc/Donvitinh/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 28, Name = "Mức độ thành thạo ngoại ngữ", Url = "/Danhmuc/Dothanhthaongonngu/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 29, Name = "Loại hình lao động", Url = "/Danhmuc/Loaihinhlaodong/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 30, Name = "Tính chất lao động", Url = "/Danhmuc/Tinhchatlaodong/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 31, Name = "Trình độ lao động", Url = "/Danhmuc/Trinhdo/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 31, Name = "Địa phương", Url = "/Danhmuc/Diaphuong/", Icon = "fa-circle", GroupId = 11 });
                        //HueCIT
                        model.Add(new MenuViewModel() { Id = 169, Name = "Loại di sản văn hóa", Url = "/HueCIT/DanhMuc/LoaiDiSan/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 170, Name = "Lĩnh vực phản ánh hiện trường", Url = "/HueCIT/DanhMuc/LinhVucHienTruong/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 171, Name = "Loại vé di tích", Url = "/HueCIT/DanhMuc/LoaiVeDiTich/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 172, Name = "Loại lễ hội", Url = "/HueCIT/DanhMuc/LoaiLeHoi/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 173, Name = "Loại điểm giao dịch", Url = "/HueCIT/DanhMuc/LoaiDiemGiaoDich/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 106, Name = "Loại địa điểm ăn uống", Url = "/HueCIT/DanhMuc/LoaiDiaDiemAnUong/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 107, Name = "Loại ẩm thực", Url = "/HueCIT/DanhMuc/LoaiAmThuc/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 111, Name = "Loại ẩm thực địa điểm ăn uống", Url = "/HueCIT/DanhMuc/LoaiAmThucDiaDiemAnUong/", Icon = "fa-circle", GroupId = 11 });
                        model.Add(new MenuViewModel() { Id = 119, Name = "Chủ đề sự kiện", Url = "/HueCIT/DanhMuc/ChuDeSuKien/", Icon = "fa-circle", GroupId = 11 });
                    }

                    if (userInfo.Roles.Contains("report") || userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 40, Name = "Thống kê, báo cáo", Url = "/Thongke/", Icon = "fa-database" });
                        //model.Add(new MenuViewModel() { Id = 41, Name = "Biểu đồ số liệu lưu trú", Url = "/Thongke/Cosoluutru/", Icon = "fa-circle", GroupId = 40 });
                        //model.Add(new MenuViewModel() { Id = 42, Name = "Biểu đồ số liệu nhà hàng", Url = "/Thongke/Nhahang/", Icon = "fa-circle", GroupId = 40 });
                        //model.Add(new MenuViewModel() { Id = 43, Name = "Biểu đồ số liệu lữ hành", Url = "/Thongke/Luhanh/", Icon = "fa-circle", GroupId = 40 });
                        //model.Add(new MenuViewModel() { Id = 44, Name = "Biểu đồ số liệu cơ sở mua sắm", Url = "/Thongke/Cosomuasam/", Icon = "fa-circle", GroupId = 40 });
                        ////model.Add(new MenuViewModel() { Id = 45, Name = "Tour du lịch", Url = "/Thongke/Tour/", Icon = "fa-circle", GroupId = 40 });
                        //model.Add(new MenuViewModel() { Id = 47, Name = "Hướng dẫn viên", Url = "/Thongke/Huongdanvien/", Icon = "fa-circle", GroupId = 40 });
                        //model.Add(new MenuViewModel() { Id = 47, Name = "Tùy chọn báo cáo", Url = "/Thongke/Tuychon/", Icon = "fa-circle", GroupId = 40 });
                    }

                    if (userInfo.Roles.Contains("root"))
                    {
                        model.Add(new MenuViewModel() { Id = 47, Name = "Quản trị hệ thống", Url = "#", Icon = "fa-users-cog" });
                        model.Add(new MenuViewModel() { Id = 49, Name = "Phòng ban, trung tâm", Url = "/User/Phongban/", Icon = "fa-circle", GroupId = 47 });
                        model.Add(new MenuViewModel() { Id = 50, Name = "Tài khoản", Url = "/User/Index/", Icon = "fa-circle", GroupId = 47 });
                        model.Add(new MenuViewModel() { Id = 51, Name = "Nhóm quyền", Url = "/User/Group/", Icon = "fa-circle", GroupId = 47 });
                        model.Add(new MenuViewModel() { Id = 52, Name = "Chức năng", Url = "/User/Role/", Icon = "fa-circle", GroupId = 47 });
                        model.Add(new MenuViewModel() { Id = 53, Name = "Lịch sử sử dụng", Url = "/User/Tracking/", Icon = "fa-circle", GroupId = 47 });
                    }
                }

            }
            catch (Exception ex)
            {
                model = new List<MenuViewModel>();
            }

            return await Task.Run(() =>
                 View("_Left", model)
            );
        }
    }

}
