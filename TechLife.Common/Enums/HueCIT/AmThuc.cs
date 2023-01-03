using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Common.Enums.HueCIT
{
    public enum KieuMon : int
    {
        [StringValue(@"Món chính")]
        MonChinh = 1,
        [StringValue(@"Khai vị")]
        KhaiVi = 2,
        [StringValue(@"Tráng miệng")]
        TrangMieng = 3,
    }
}
