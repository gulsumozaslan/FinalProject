using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{//Bizim için servis olduğundan bir interfaceden implemente edilmesine ihtiyaç yok
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)  //passwordHash ve passwordSalt'ı dışarıya çıkarır
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;  //Kullandığımız SHA512 algoritması her kullanıcı için başka bir key oluşturur
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));  //passwordü byte çevirip öyle attık
            }
        }

        //Sonradan sisteme girmek isteyen kişinin verdiği passwordünün bizim veri kaynağımızdaki hash ile ilgili salta göre eşleşip eşleşmediğini verdiğimiz yer
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)  
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));  
                for (int i = 0; i < computedHash.Length; i++)   //Hesaplanan hash'in bütün değerlerini tek tek dolaş
                {
                    if (computedHash[i] != passwordHash[i])  //Hesaplanan hash'in i. değeri veritabanından gönderilen hash'in i. değerine eşit değilse
                    {
                        return false;
                    }
                }
                return true;
            }
            
        } 
    }
}//HashingHelper hash oluşturmaya ve onu doğrulamaya yarar
