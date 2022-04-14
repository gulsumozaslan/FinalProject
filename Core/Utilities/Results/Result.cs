using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
       //Constructor'ımız 2 tane parametre istiyor
        public Result(bool success, string message) : this(success) //Result' ın tek parametreli constructorına success'i yolla
        {
            Message = message;
        }

        public Result(bool success)  //Bilgilendirme mesajı döndürmek istemiyorum, true false yeterli
        {
            Success = success;
        }

        public bool Success { get; }
        public string Message { get; }  //Read onlyler constructorda set edilebilirler
    }
}
