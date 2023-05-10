using System;

namespace ADSeek.Domain.Models
{
    public class ActiveDirectoryResult
    {
        public ActiveDirectoryResult(bool isOk = true, int? errorCode = null, string errorMessage = null)
        {
            IsOk = isOk;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public ActiveDirectoryResult(Exception exception)
        {
            ErrorMessage = exception.Message;

            if (exception.InnerException is not null)
                ErrorMessage += $"\n{exception.InnerException.Message}";

            IsOk = false;
        }
        
        public bool IsOk { get; set; }
        public int? ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}