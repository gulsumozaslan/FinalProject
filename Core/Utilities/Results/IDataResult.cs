using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResult<T> : IResult   //T'ye burada kısıtlama yazmıyoruz her şey olabilir, Exception bile olabilir
    {
        T Data { get; }
    }
}
