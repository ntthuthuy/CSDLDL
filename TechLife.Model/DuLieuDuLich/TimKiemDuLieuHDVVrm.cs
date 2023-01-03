using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
  public class TimKiemDuLieuHDVVrm
    {
        public int Id { get; set; }
        public string HoVaTen { get; set; }
        public string DiaChi { get; set; }        
        public string SoDienThoai { get; set; }
        public string LoaiThe { get; set; }
        public string NgonNgu { get; set; }
        public DateTime NgayCapThe { get; set; }
        public DateTime NgayHetHan { get; set; }
        public string Keyword { get; set; }
        public string TinhTrang { get; set; }
      
    }
}
