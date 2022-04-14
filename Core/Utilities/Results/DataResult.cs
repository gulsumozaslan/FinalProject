using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data,bool success,string message) : base(success,message)  //Result voidler için, DataResult aynı zamanda datası old için bu şekilde 
        {
            Data = data;
        }
        public DataResult(T data,bool success) : base(success) //Mesajı göndermek istemeyebilirdi
        {
            Data = data;
        }

        public T Data { get; }

    }
}
