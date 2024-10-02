// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Cryptography;

namespace AirBnBWebApi.Services.Utils;

public class RSAKeyGenerator
{
    public static (string privateKey, string publicKey) GenerateKeys()
    {
        using (var rsa = RSA.Create(2048))
        {
            var privateKey = System.Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            var publicKey = System.Convert.ToBase64String(rsa.ExportRSAPublicKey());

            return (privateKey, publicKey);
        }
    }

    public static bool VerifyKey(string publicKey, string privateKey)
    {
        try
        {
            using (var rsa = RSA.Create())
            {
                rsa.ImportRSAPrivateKey(System.Convert.FromBase64String(privateKey), out _);
                rsa.ImportRSAPublicKey(System.Convert.FromBase64String(publicKey), out _);

                return true;
            }
        }
        catch
        {
            return false;
        }
    }
}
