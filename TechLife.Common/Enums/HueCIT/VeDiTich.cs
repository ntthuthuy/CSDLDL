using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Common.Enums.HueCIT
{
    public enum LoaiKhachVeDiTich : int
    {
        [StringValue(@"Người lớn (NL)")]
        NguoiLon = 1,
        [StringValue(@"Trẻ em (TE)")]
        TreEm = 2,
        [StringValue(@"Đối tượng đặc biệt")]    
        DoiTuongDacBiet = 3,
        [StringValue(@"Khuyết tật")]
        KhuyetTat = 8,
    }
}
