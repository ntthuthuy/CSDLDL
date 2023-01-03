namespace TechLife.App.Areas.HueCIT.Models
{
    public class DuLieuDuLichRequestBody
    {
        public string serviceid { get; set; }
        public Thamso thamso { get; set; }
        public int page { get; set; }
        public int perpage { get; set; }
    }

    public class Thamso
    {
        public string tukhoa { get; set; }
        public int? loaidiadiem { get; set; }
        public int? trangthaipheduyet { get; set; }
    }
}
