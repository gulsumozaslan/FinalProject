using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{  
    //Temel voidler için başlangıç. Void olan yerde IResult diyorum
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
