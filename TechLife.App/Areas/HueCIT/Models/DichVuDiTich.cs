using System;
using System.Collections.Generic;

namespace TechLife.App.Areas.HueCIT.Models
{
    #region MODEL DỊCH VỤ DI TÍCH
    public class DichVuDiTich
    {
        public int ID { get; set; }
        public string TieuDe { get; set; }
        public string TomTat { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public string BangGia { get; set; }
        public string ToaDoX { get; set; }
        public string ToaDoY { get; set; }
        public int SapXep { get; set; }
        public string DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }

    public class DichVuDiTichTrinhDien
    {
        public int ID { get; set; }
        public string TieuDe { get; set; }
        public string TomTat { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string BangGia { get; set; }
        public string ToaDoX { get; set; }
        public string ToaDoY { get; set; }
        public int SapXep { get; set; }
        public string DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
        public string URLHinhAnh { get; set; }
    }
    #endregion

    #region HÌNH ẢNH
    public class DichVuDiTichHinhAnh
    {
        public int ID { get; set; }
        public int MaDVDT { get; set; }
        public string TieuDeAnh { get; set; }
        public string URLHinhAnh { get; set; }
        public bool IsAnhDaiDien { get; set; }
        public int SapXep { get; set; }
    }

    public class DichVuDiTichHinhAnhTrinhDien
    {
        public int ID { get; set; }
        public int MaDVDT { get; set; }
        public string TieuDeAnh { get; set; }
        public string URLHinhAnh { get; set; }
        public bool IsAnhDaiDien { get; set; }
        public int SapXep { get; set; }
    }
    #endregion

    #region ĐỒNG BỘ
    public class DichVuDiTichDongBo
    {
        public string id { get; set; }
        public string title { get; set; }
        public string typediadiem { get; set; }
        public string summary { get; set; }
        public string publishTime { get; set; }
        public string content { get; set; }
        public string source { get; set; }
        public string author { get; set; }
        public int order { get; set; }
        public string language { get; set; }
        public Category category { get; set; }
        public ImgNews imgNews { get; set; }
        public ImgNewsThumb imgNewsThumb { get; set; }
        public List<object> listimg { get; set; }
        public List<object> listvideo { get; set; }
        public string listfile { get; set; }
        public string listRelatedNews { get; set; }
        public Geo geo { get; set; }
    }

    public class DanhSachDichVuDiTichDongBo
    {
        public int totalCount { get; set; }
        public List<DichVuDiTichDongBo> newsList { get; set; }
    }
    public class Category
    {
        public string id { get; set; }
        public int idextra { get; set; }
        public string tenma { get; set; }
        public string title { get; set; }
        public string parent { get; set; }
        public string url { get; set; }
        public string anhbieutuong { get; set; }
        public string anhdaidien { get; set; }
    }

    public class Geo
    {
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class ImgNews
    {
        public string mimeType { get; set; }
        public string url { get; set; }
    }

    public class ImgNewsThumb
    {
        public string mimeType { get; set; }
        public string url { get; set; }
    }

    #endregion
}
