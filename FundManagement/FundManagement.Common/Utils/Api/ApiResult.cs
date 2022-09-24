using System.Collections.Generic;

namespace FundManagement.Common.Api.Utils
{
    public class ApiResult<T>
    {
        public bool IsSuccessfully { get; set; }

        public string MessageError { get; set; }

        public T Data { get; set; }

        public ApiResult()
        {

        }
        public ApiResult(bool isSuccessfully, string message, T data)
        {
            IsSuccessfully = isSuccessfully;
            MessageError = message;
            Data = data;
        }
        public ApiResult(T data)
        {
            IsSuccessfully = true;
            MessageError = string.Empty;
            Data = data;
        }
        public ApiResult(bool isSuccessfully, string message)
        {
            IsSuccessfully = isSuccessfully;
            MessageError = message;
            Data = default;
        }
    }
}
