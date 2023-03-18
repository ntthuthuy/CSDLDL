using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Common
{
    public class Result<T>
    {
        public bool IsSuccessed { get; set; }

        public string Message { get; set; }

        public T Obj { get; set; }
    }
    public class KetQuaXacThuc
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public UserSSo UserObj { get; set; }

    }
    public class UserSSo
    {
        public string TaiKhoan { get; set; }
        public string HoVaTen { get; set; }
        public string TenDonVi { get; set; }
    }
}
