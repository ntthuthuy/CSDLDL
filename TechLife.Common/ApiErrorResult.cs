using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Common
{
    public class Errors
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public Errors[] ValidationErrors { get; set; }

        public ApiErrorResult()
        {
        }

        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
            ValidationErrors = null;
        }

        public ApiErrorResult(Errors[] validationErrors,string message)
        {
            IsSuccessed = false;
            ValidationErrors = validationErrors;
            Message = message;
        }
    }
}
