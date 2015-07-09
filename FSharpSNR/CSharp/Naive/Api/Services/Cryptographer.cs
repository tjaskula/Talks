using System;
using System.Security.Cryptography;
using System.Text;

namespace Api.Services
{
    public class Cryptographer
    {
        public string CreateSalt()
        {
            const int size = 64;
            //Generate a cryptographic random number.
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        public string ComputeHash(string valueToHash)
        {
            HashAlgorithm algorithm = SHA512.Create();
            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(valueToHash));

            return Convert.ToBase64String(hash);
        }

        public string ComputeSha1Hash(string valueToHash)
        {
            var asciiEncoding = new ASCIIEncoding();
            byte[] hashValue, messageBytes = asciiEncoding.GetBytes(valueToHash);
            var sHhash = new SHA1Managed();
            string hex = "";

            hashValue = sHhash.ComputeHash(messageBytes);
            foreach (byte b in hashValue)
            {
                hex += String.Format("{0:x2}", b);
            }

            return hex;
        }

        public string GetPasswordHash(string password, string salt)
        {
            return ComputeHash(password + salt);
        }
    }
}