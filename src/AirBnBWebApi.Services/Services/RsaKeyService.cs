using System.Security.Cryptography;

namespace AirBnBWebApi.Services.Services
{
    public class RsaKeyService
    {
        public static (string publicKey, string privateKey) GenerateRsaKeys()
        {
            using (var rsa = RSA.Create(2048))
            {
                var privateKey = System.Convert.ToBase64String(rsa.ExportRSAPrivateKey());
                var publicKey = System.Convert.ToBase64String(rsa.ExportRSAPublicKey());
                return (publicKey, privateKey);
            }
        }
    }
}
