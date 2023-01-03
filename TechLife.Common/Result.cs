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
}
