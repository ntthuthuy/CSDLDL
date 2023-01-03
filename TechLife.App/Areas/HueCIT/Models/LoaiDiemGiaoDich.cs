namespace TechLife.App.Areas.HueCIT.Models
{
    public class LoaiDiemGiaoDich
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }

    public class LoaiDiemGiaoDichTrinhDien
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }
}
