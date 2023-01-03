using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Common.Enums.HueCIT
{
    public enum CapQuanLyLeHoi : int
    {
        [StringValue(@"Quốc gia")]
        QuocGia = 1,
        [StringValue(@"Khu vực")]
        KhuVuc = 2,
        [StringValue(@"Tỉnh")]
        Tinh = 3,
        [StringValue(@"Huyện")]
        Huyen = 4,
        [StringValue(@"Xã")]
        Xa = 5,
    }
}
