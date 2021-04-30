using System;

namespace CustomeExceptions
{
    public class InternalBaseException : Exception
    {
        public int Code { get; set; }
        public InternalBaseException(int code, string message) : base(message) 
        {
            Code = code;
        }
    }
}
