using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{  //İşlem sonucunu default true vermiş olduk
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data,true,message)
        {

        }
        public SuccessDataResult(T data) : base(data,true)
        {

        }
        public SuccessDataResult(string message): base(default,true,message)  //Sadece mesaj vermek istiyorum. Datayı default haliyle kullanmak isteyebilir(nadir). base(default):Çalıştığım T'nin defaultu
        {

        }
        public SuccessDataResult() : base(default,true) //Hiçbir şey vermek istemiyorum
        {

        }
    }
}
