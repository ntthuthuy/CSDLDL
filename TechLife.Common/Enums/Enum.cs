using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Common.Enums
{
    public enum LinhVucKinhDoanh : int
    {
        [StringValue(@"Cơ sở lưu trú")]
        CoSoLuuTru = 1,
        [StringValue(@"Công ty lữ hành")]
        LuHanh = 2,
        [StringValue(@"Cơ sở mua sắm")]
        MuaSam = 3,
        [StringValue(@"Nhà hàng đạt chuẩn")]
        NhaHang = 4,
        [StringValue(@"Điểm du lịch")]
        DiemDuLich = 5,
        [StringValue(@"Hướng dẫn viên")]
        HDV = 6,
        [StringValue(@"Vệ sinh công cộng")]
        VSCC = 7,
        [StringValue(@"Khu du lịch")]
        KhuDuLich = 8,
        [StringValue(@"Khu vui chơi giải trí")]
        KhuVuiChoi = 9,
        [StringValue(@"Dịch vụ chăm sóc sức khỏe")]
        CSSK = 10,
        [StringValue(@"Dịch vụ thể thao")]
        TheThao = 11,
        [StringValue(@"Công ty vận chuyển")]
        VanChuyen = 12,
        [StringValue(@"Tour du lịch")]
        Tour = 13,
        [StringValue(@"Hướng dẫn viên")]
        HuongDanVien = 14,
        [StringValue(@"Di sản văn hóa")]
        DiSanVanHoa = 15
    }
    public enum LoaiBaoCao : int
    {
        [StringValue(@"Thống kê số liệu cơ sở lưu trú")]
        thongkecosoluutru = 8,
        [StringValue(@"Thống kê số liệu hướng dẫn viên")]
        thongkehuongdanvien = 9,
        [StringValue(@"Thống kê số liệu nhà hàng đạt chuẩn")]
        thongkenhahang = 10,
        [StringValue(@"Thống kê số liệu công ty lữ hành")]
        thongkechitietluhanh = 11,
        [StringValue(@"Thống kê số liệu điểm du lịch")]
        thongkediemdulich = 12,
        [StringValue(@"Thống kê số liệu cơ sở mua sắm")]
        thongkecosomuasam = 13,
        [StringValue(@"Thống kê chi tiết thanh tra, kiểm tra các cơ sơ")]
        thongkedulieuthanhtra = 14,
        [StringValue(@"Biểu đồ tổng hợp số lượng cơ sở lưu trú")]
        bieudocosoluutru = 1,
        [StringValue(@"Biểu đồ tổng hợp số lượng nhà hàng")]
        bieudonhahang = 2,
        [StringValue(@"Biểu đồ tổng hợp số lượng cơ sở mua sắm")]
        bieudocosomuasam = 3,
        [StringValue(@"Biểu đồ tổng hợp số lượng công ty lữ hành")]
        bieudocongtyluhanh = 4,
        [StringValue(@"Biểu đồ tổng hợp số lượng diểm du lịch")]
        bieudodiemdulich = 5,
        [StringValue(@"Biểu đồ tổng hợp số lượng hướng dẫn viên")]
        bieudohuongdanvien = 6,
        [StringValue(@"Biểu đồ tổng hợp số lượng tour du lịch")]
        bieudotourdulich = 7,
        [StringValue(@"Thống kê tùy chọn cơ sở dữ liệu du lịch")]
        thongketuychon = 15,

    }
    public enum CategoryMobile : int
    {
        [StringValue(@"Thông tin cần biết")]
        ThongTinCanBiet = -1,
        [StringValue(@"Lễ hội")]
        LeHoi = -2,
        [StringValue(@"Tour du lịch")]
        Tour = -3,
        [StringValue(@"Lưu trú")]
        CoSoLuuTru = 1,
        // [StringValue(@"Điểm mua sắm")]
        // MuaSam = 3,
        [StringValue(@"Nhà hàng, món ăn")]
        NhaHang = 4,
        [StringValue(@"Điểm du lịch, lịch sử, trải nghiệm")]
        DiemDuLich = 5,
        [StringValue(@"Thuê xe")]
        VanChuyen = 12
    }
    public enum TieuChuanCoSo : int
    {
        //[StringValue(@"Không xếp hạng")]
        //Khong = 0,
        [StringValue(@"1 Sao")]
        MotSao = 1,
        [StringValue(@"2 Sao")]
        HaiSao = 2,
        [StringValue(@"3 Sao")]
        BaSao = 3,
        [StringValue(@"4 Sao")]
        BonSao = 4,
        [StringValue(@"5 Sao")]
        NamSao = 5
    }
    public enum TieuChuanDanhGia : int
    {
        //[StringValue(@"Không xếp hạng")]
        //Khong = 0,
        [StringValue(@"1 Sao")]
        MotSao = 1,
        [StringValue(@"2 Sao")]
        HaiSao = 2,
        [StringValue(@"3 Sao")]
        BaSao = 3,
        [StringValue(@"4 Sao")]
        BonSao = 4,
        [StringValue(@"5 Sao")]
        NamSao = 5
    }
    public enum HinhThucTour : int
    {
        [StringValue(@"Tour riêng")]
        Rieng = 1,
        [StringValue(@"Tour ghép")]
        Ghep = 2
    }
    public enum LoaiTheHuongDanVien : int
    {
        [StringValue(@"Thẻ nội địa")]
        NoiDia = 1,
        [StringValue(@"Thẻ quốc tế")]
        QuocTe = 2
    }
    public enum LoaiFile
    {
        hosodulich,
        phong,
        hosohuongdanvien,
        tour,
        hanhtrinhtour,
        baiviet
    }
    public enum LoaiBinhLuan
    {
        hosodulich,
        tour,
        baiviet
    }
    public enum KetQuaThanhTra : int
    {
        [StringValue(@"Không vi phạm")]
        KhongViPham = 0,
        [StringValue(@"Nhắc nhở")]
        NhacNho = 1,
        [StringValue(@"Xử phạt vi phạm")]
        XuPhat = 2
    }
    public enum LoaiTaiKhoan : int
    {
        [StringValue(@"Tài khoản hệ thống")]
        HeThong = 1,
        [StringValue(@"Tài khoản đồng bộ từ SSO")]
        SSO = 2,
        [StringValue(@"Tài khoản cộng tác viên")]
        CTV = 3,
        [StringValue(@"Tài khoản khách du lịch")]
        ThanhVien = 4,
        [StringValue(@"Tài khoản mạng xã hội")]
        MangXaHoi = 5,
    }
    public enum LoginType
    {
        Default,
        SSO,
        SSOHueS
    }
}
