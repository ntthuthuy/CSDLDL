using System.Collections.Generic;

namespace TechLife.Model.TongHop
{
    public class TongHopImportRequest
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public List<TongHopImportVm> Items { get; set; }
    }

    public class TongHopImportVm
    {
        public string TenQuocTich { get; set; }
        public string SoLieu { get; set; }
        public string CongDon { get; set; }
        public string ThiPhan { get; set; }
    }
}
