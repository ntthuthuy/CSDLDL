using System.Collections.Generic;

namespace TechLife.Model.TongHop
{
    public class TongHopVm
    {
        public int Id { get; set; }

        public int QuocTichId { get; set; }

        public string TenQuocTich { get; set; }

        public Dictionary<int, decimal> SoLieu { get; set; }

        public decimal ThiPhan { get; set; }
    }
}
