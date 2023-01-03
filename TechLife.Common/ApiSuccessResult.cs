using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public Errors[] ValidationErrors { get; set; }
        public ApiSuccessResult(T resultObj, string message = "")
        {
            IsSuccessed = true;
            ResultObj = resultObj;
            Message = message;
            ValidationErrors = null;
        }

        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }
    }
}
