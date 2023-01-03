using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;


namespace TechLife.Model.Tour
{
    public class HanhTrinhTheoNgayVm
    {
        public int Ngay { get; set; }
        public string Avata { get; set; }
        public List<DiaDiemTheoNgayVm> DSDiaDiem { get; set; }
    }
}
