using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);  //anahtar olarak securityKey, şifreleme olarak da güvenlik algoritmaları HmacSha512
        }
    }
}
//Web Api'nin kullanabileceği(doğrulayacağı) JWT tokenlarının oluşturulabilmesi için  
//kullanıcı adı parola user Credential'ımdır(bizim anahtarımız)