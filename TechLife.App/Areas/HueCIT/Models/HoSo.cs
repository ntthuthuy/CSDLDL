using System.Collections.Generic;

namespace TechLife.App.Areas.HueCIT.Models
{
    #region Vui chơi giải trí
    public class VcgtDongBo
    {
        public int id { get; set; }
        public string tendoituong { get; set; }
        public string nhomlh { get; set; }
        public string loaihinh { get; set; }
        public string diachi { get; set; }
        public string kinhdo { get; set; }
        public string vido { get; set; }
        public string mota { get; set; }
        public string giatu { get; set; }
        public string giaden { get; set; }
        public string mocua { get; set; }
        public string dongcua { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string anh { get; set; }
        public int? masothue { get; set; }
        public int? maquanhuyen { get; set; }
        public string? tenmaquanhuyen { get; set; }
        public int? maphuongxa { get; set; }
        public string? tenmaphuongxa { get; set; }
    }

    public class DanhSachVcgtDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<VcgtDongBo> data { get; set; }
        public int totalrow { get; set; }
    }
    #endregion

    #region Công ty vận chuyển
    public class CongTyVanChuyen
    {
        public int id { get; set; }
        public string tendoituong { get; set; }
        public string nhomlh { get; set; }
        public string loaihinh { get; set; }
        public string diachi { get; set; }
        public string kinhdo { get; set; }
        public string vido { get; set; }
        public string mota { get; set; }
        public string giatu { get; set; }
        public string giaden { get; set; }
        public string mocua { get; set; }
        public string dongcua { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string anh { get; set; }
        public int? masothue { get; set; }
        public int? maquanhuyen { get; set; }
        public string? tenmaquanhuyen { get; set; }
        public int? maphuongxa { get; set; }
        public string? tenmaphuongxa { get; set; }
    }

    public class DanhSachCongTyVanChuyen
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<CongTyVanChuyen> data { get; set; }
        public int totalrow { get; set; }
    }


    public class CongTyLuHanhDongBo
    {
        public int id { get; set; }
        public string loaihinh { get; set; }
        public string ten { get; set; }
        public string sdt { get; set; }
        public string fax { get; set; }
        public string diachi { get; set; }
        public string truongbplh { get; set; }
        public string email { get; set; }
        public string kd { get; set; }
        public string vd { get; set; }
        public int? masothue { get; set; }
        public int? maquanhuyen { get; set; }
        public string? tenmaquanhuyen { get; set; }
        public int? maphuongxa { get; set; }
        public string? tenmaphuongxa { get; set; }
    }
    #endregion

    #region Công ty lữ hành
    public class DanhSachCongTyLuHanhDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<CongTyLuHanhDongBo> data { get; set; }
        public int totalrow { get; set; }
    }

    public class DiemDuLichDongBo
    {
        public int id { get; set; }
        public string tendoituong { get; set; }
        public string nhomloaihinh { get; set; }
        public string loaihinh { get; set; }
        public string diachi { get; set; }
        public string kinhdo { get; set; }
        public string vido { get; set; }
        public string mota { get; set; }
        public string giathamkhaotu { get; set; }
        public string giathamkhaoden { get; set; }
        public string mocua { get; set; }
        public string dongcua { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string anh { get; set; }
        public string groupid { get; set; }
        public int? masothue { get; set; }
        public int? maquanhuyen { get; set; }
        public string? tenmaquanhuyen { get; set; }
        public int? maphuongxa { get; set; }
        public string? tenmaphuongxa { get; set; }
    }

    public class DanhSachDiemDuLichDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<DiemDuLichDongBo> data { get; set; }
        public int totalrow { get; set; }
    }
    #endregion

    #region Địa điểm ăn uống
    public class DiaDiemAnUongDongBo
    {
        public int id { get; set; }
        public string tendoituong { get; set; }
        public string nhomloaihinh { get; set; }
        public string loaihinh { get; set; }
        public string diachi { get; set; }
        public string kinhdo { get; set; }
        public string vido { get; set; }
        public string mota { get; set; }
        public string giathamkhaotu { get; set; }
        public string giathamkhaoden { get; set; }
        public string mocua { get; set; }
        public string dongcua { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string anh { get; set; }
        public string groupid { get; set; }
        public int? masothue { get; set; }
        public int? maquanhuyen { get; set; }
        public string? tenmaquanhuyen { get; set; }
        public int? maphuongxa { get; set; }
        public string? tenmaphuongxa { get; set; }
    }

    public class DanhSachDiaDiemAnUongDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<DiaDiemAnUongDongBo> data { get; set; }
        public int totalrow { get; set; }
    }
    #endregion

    #region Cơ sở mua sắm
    public class CoSoMuaSamDongBo
    {
        public int id { get; set; }
        public int loaidiadiem { get; set; }
        public string tenloaidiadiem { get; set; }
        public string tendiadiem { get; set; }
        public string anhdaidien { get; set; }
        public string diachi { get; set; }
        public float? kinhdo { get; set; }
        public float? vido { get; set; }
        public string thoigianmocua { get; set; }
        public string gioithieu { get; set; }
        public string lienhe { get; set; }
        public string trangthaipheduyet { get; set; }
        public string tentrangthaipheduyet { get; set; }
        public string placename { get; set; }
        public string introduce { get; set; }
        public List<SttttLuoihinhanhdiadiemmuasamDulich> stttt_luoihinhanhdiadiemmuasam_dulich { get; set; }
        public int? masothue { get; set; }
        public int? maquanhuyen { get; set; }
        public string? tenmaquanhuyen { get; set; }
        public int? maphuongxa { get; set; }
        public string? tenmaphuongxa { get; set; }
    }

    public class DanhSachCoSoMuaSamDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<CoSoMuaSamDongBo> data { get; set; }
        public int totalrow { get; set; }
    }

    public class SttttLuoihinhanhdiadiemmuasamDulich
    {
        public int id { get; set; }
        public string hinhanhdinhkem { get; set; }
        public string ghichu { get; set; }
        public int eformid { get; set; }
        public int teneformid { get; set; }
    }
    #endregion

    #region Di sản văn hóa
    public class DiSanVanHoaDongBo
    {
        public int id { get; set; }
        public string tendisan { get; set; }
        public int phanloaidisan { get; set; }
        public string tenphanloaidisan { get; set; }
        public int loaihinhdisan { get; set; }
        public string tenloaihinhdisan { get; set; }
        public string anhdaidien { get; set; }
        public string thongtingioithieu { get; set; }
        public string thongtinchitiet { get; set; }
        public string giave { get; set; }
        public string giavetreem { get; set; }
        public List<Phuongtiendichuyen> phuongtiendichuyen { get; set; }
        public int tinh { get; set; }
        public string tentinh { get; set; }
        public int huyenthithanh { get; set; }
        public string tenhuyenthithanh { get; set; }
        public int xaphuong { get; set; }
        public string tenxaphuong { get; set; }
        public int thonto { get; set; }
        public string tenthonto { get; set; }
        public string diachi { get; set; }
        public double kinhdo { get; set; }
        public double vido { get; set; }
        public string xem3d { get; set; }
        public string video { get; set; }
        public int sapxep { get; set; }
        public string heritagename { get; set; }
        public string introduce { get; set; }
        public int trangthaipheduyet { get; set; }
        public string tentrangthaipheduyet { get; set; }
        public string details { get; set; }
        public List<object> stttt_hinhanhdisan { get; set; }
    }

    public class Phuongtiendichuyen
    {
        public string eformid { get; set; }
        public string tenloai { get; set; }
    }

    public class DanhSachDiSanVanHoaDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<DiSanVanHoaDongBo> data { get; set; }
        public int totalrow { get; set; }
    }
    #endregion
}
