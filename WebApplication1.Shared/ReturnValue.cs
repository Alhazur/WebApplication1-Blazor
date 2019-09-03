using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Shared
{
    public class ReturnValue
    {
        public enum ErrorTypes
        {
            Success = 0,
            Warning = 1,
            Error = 2,
            UnhandledException = 128
        }

        private bool _Error = false;
        public bool Error
        {
            get { return _Error; }
            set
            {
                _Error = value;
                if (!_Error)
                    ErrorType = (_ErrorType == ErrorTypes.Warning ? _ErrorType : ErrorTypes.Success);       // warnings might not be errors
                else
                    ErrorType = ErrorTypes.Error;
            }
        }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        private ErrorTypes _ErrorType;
        public ErrorTypes ErrorType
        {
            get { return _ErrorType; }
            set
            {
                _ErrorType = value;
                if (_ErrorType == ErrorTypes.Success)
                {
                    _Error = false;
                    ErrorCode = "Success";
                }
                else if (_ErrorType != ErrorTypes.Warning)      // warnings might not be errors
                {
                    _Error = true;
                }
            }
        }
        public string NicerErrorMessage { get => string.Format("[{0}] {1}", this.ErrorCode, this.Message); }

        public Exception ErrorException { get; set; }

        public ReturnValue()
        {
            Error = true;
            Message = "";
            ErrorCode = "";
            ErrorType = ErrorTypes.Error;
            ErrorException = null;
        }

        public ReturnValue(bool pError = true, string pMessage = "", string pErrorCode = "", ErrorTypes pType = ErrorTypes.Error, Exception pException = null)
        {
            Error = pError;
            Message = pMessage;
            ErrorCode = pErrorCode;
            ErrorType = pType;
            ErrorException = pException;
        }

        public void Copy(ReturnValue pSource)
        {
            this.Error = pSource.Error;
            this.Message = pSource.Message;
            this.ErrorCode = pSource.ErrorCode;
            this.ErrorType = pSource.ErrorType;
            this.ErrorException = pSource.ErrorException;
        }

        public static System.Threading.Tasks.Task<ReturnValue> New()
        {
            return System.Threading.Tasks.Task.FromResult(new ReturnValue());
        }
    }

    public class ReturnValue<T> : ReturnValue
    {
        public T ReturnObject { get; set; }

        new public static System.Threading.Tasks.Task<ReturnValue<T>> New()
        {
            return System.Threading.Tasks.Task.FromResult(new ReturnValue<T>());
        }
    }
}
