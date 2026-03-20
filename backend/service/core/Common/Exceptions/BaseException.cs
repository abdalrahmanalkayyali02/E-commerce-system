using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public abstract class BaseException : Exception
    {
        public int code { get; }
        protected BaseException(string message, int code = 400 ) : base(message)
        {
            this.code = code;
        }
    }

    


    public abstract class DomainException : BaseException
    {
        public int code { get; }

        public string message { get; }
         

        protected DomainException(string message, int code=400) : base(message)
        {

        }
    }

}
