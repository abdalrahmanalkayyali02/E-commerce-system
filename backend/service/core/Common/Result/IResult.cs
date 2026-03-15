using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Result
{
    public interface IResult
    {
        List<Error?> Errors { get; }
        bool IsSuccess { get; }
        //bool isFailes { get; } // extra 
      //  DateTime Timestamp { get; }  // extra 

    }

    public interface IResult<out TValue> : IResult
    {
        TValue Value { get; }
    }
}
