using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Common.Enums.HueCIT
{
    public enum FileType : int
    {
        [StringValue(@"Khác")]
        Other = 0,
        [StringValue(@"Ảnh")]
        Img = 1,
        [StringValue(@"Media")]
        Media = 2,
        [StringValue(@"Tài liệu")]
        Doc = 3,
    }

    public enum FileLoaiDoiTuong : int
    {
        [StringValue(@"Lễ hội")]
        LeHoi = 1,
    }
}
