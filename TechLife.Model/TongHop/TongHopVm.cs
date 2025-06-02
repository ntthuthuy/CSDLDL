using System.Collections.Generic;

namespace TechLife.Model.TongHop
{
    public class TongHopVm
    {
        public int Id { get; set; }

        public int QuocTichId { get; set; }

        public string TenQuocTich { get; set; }

        public List<ListSoLieu> List { get; set; }
    }

    public class ListSoLieu
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public decimal SoLieu { get; set; }
        public decimal CongDon { get; set; }

        public decimal ThiPhan { get; set; }
    }
}
