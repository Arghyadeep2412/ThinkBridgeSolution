using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThinkBridgeSolu.Models.CodeModels
{
    public enum ErrorCode
    {
        ITEM_DOES_NOT_EXISTS,
        ITEM_FINISHED,
        USER_NOT_ADMIN
    }
    public enum ResponseStatus
    {
        Fail = 0,
        Success = 1,
        Error = 2
    }
    public class BaseResponse
    {
        public BaseResponse(ResponseStatus status)
        {
            Status = status;
        }

        public ResponseStatus Status { get; set; }
        public object Data { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public ErrorCode? ErrorCode { get; set; }
    }
}
