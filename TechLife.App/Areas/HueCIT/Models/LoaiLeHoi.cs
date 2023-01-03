namespace TechLife.App.Areas.HueCIT.Models
{
    public class LoaiLeHoiModel
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }

    public class LoaiLeHoiTrinhDien
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }

}
