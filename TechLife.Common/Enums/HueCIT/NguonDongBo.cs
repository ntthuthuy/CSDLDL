using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Common.Enums.HueCIT
{
    public enum NguonDongBo : int
    {
        [StringValue(@"Cập nhật")]
        TuNhap = 0,
        [StringValue(@"Số hóa")]
        SoHoa = 1,
        [StringValue(@"Hệ thống thông tin doanh nghiệp")]
        HTTTDN = 2,
        [StringValue(@"Trung tâm bảo tồn di tích")]
        TTBTDT = 3,
        [StringValue(@"Cổng thông tin điện tử tỉnh")]
        CONGTTH = 4,
        [StringValue(@"Công ty tMonitor")]
        tMonitor = 5,
    }
}
