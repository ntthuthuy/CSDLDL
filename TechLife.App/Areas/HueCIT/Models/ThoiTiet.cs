using System;
using System.Collections.Generic;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class ThoiTiet
    {
        public string ID { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string DuBao { get; set; }
        public string SymbolID { get; set; }
        public string SymbolName { get; set; }
        public string Title { get; set; }
        public string Temperature { get; set; }
        public string PlainArea { get; set; }
        public string MountainousRegion { get; set; }
        public string HueCityArea { get; set; }
        public string MarineWeather { get; set; }
        public string ForestfiresForecast { get; set; }
        public string Warning { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
        public bool Published { get; set; }
        public string OwnerCode { get; set; }
        public int ModuleId { get; set; }
        public int CreatedByUserId { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime NgayTao { get; set; }
    }

    public class ThoiTietSymbol
    {
        public string ID { get; set; }
        public string Language { get; set; }
        public string SymbolName { get; set; }
        public string IDFile { get; set; }
        public string TenFileDinhKem { get; set; }
        public int SortOrder { get; set; }
        public bool Published { get; set; }
        public DateTime NgayTao { get; set; }
    }

    public class ThoiTietModel
    {
        public string ID { get; set; }
        public string SymbolID { get; set; }
        public string Title { get; set; }
        public string WeatherOfDay { get; set; }
        public string Temperature { get; set; }
        public string PlainArea { get; set; }
        public string MountainousRegion { get; set; }
        public string HueCityArea { get; set; }
        public string MarineWeather { get; set; }
        public string ForestfiresForecast { get; set; }
        public string Warning { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
        public string Published { get; set; }
        public string OwnerCode { get; set; }
        public string ModuleId { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
    }
    public class ThoiTietSymbolModel
    {
        public string ID { get; set; }
        public string Language { get; set; }
        public string SymbolName { get; set; }
        public string IDFile { get; set; }
        public string TenFileDinhKem { get; set; }
        public string SortOrder { get; set; }
        public string Published { get; set; }
    }
    public class ThoiTietTrinhDien
    {
        public string ThoiTietID { get; set; }
        public DateTime ThoiGian { get; set; }
        public string TieuDe { get; set; }
        public string AnhBieuTuong { get; set; }
        public string TenSymbol { get; set; }
        public string NhietDo { get; set; }
        public string VungDongBang { get; set; }
        public string VungNui { get; set; }
        public string ThanhPhoHue { get; set; }
        public string ThoiTietTrenBien { get; set; }
        public string DuBaoCapChayRung { get; set; }
        public string DuBao { get; set; }
    }
    public class ThoiTietRequest
    {
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
    }
}
