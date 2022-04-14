using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorResult : Result  //hata sonucu
    {
        public  ErrorResult(string message) : base(false, message) //Mesaj vermek istersek. base'e true ve message gönderdim
        {

        }

        public ErrorResult() : base(false)  //Mesaj vermek istemezsek. Base'in tek parametreli olanını(constr.) çalıştır
        {

        }
    }
}
