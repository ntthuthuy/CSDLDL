namespace TechLife.Data.Entities
{
    public class TongHop
    {
        public int Id { get; set; }
        public decimal SoLieu { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public decimal CongDon { get; set; }
        public decimal ThiPhan { get; set; }
        public bool IsDelete { get; set; }
        public int QuocTichId { get; set; }
        public virtual QuocTich QuocTich { get; set; }
    }
}
