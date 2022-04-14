using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message) //Mesaj vermek istersek. base'e true ve message gönderdim
        {

        }
        public SuccessResult() : base(true)  //Mesaj vermek istemezsek. Base'in tek parametreli olanını(constr.) çalıştır
        {

        }

    }//true'yu default vermiş olduk
}
