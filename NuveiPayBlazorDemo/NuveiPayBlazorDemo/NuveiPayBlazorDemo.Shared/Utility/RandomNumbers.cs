//using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Cryptography;
using System.Text;

namespace NuveiPayBlazorDemo.Shared.Utility
{
    public class RandomNumbers
    {
        public static string Generate6Number()
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 1000000);

            return randomNumber.ToString();

        }

        public static string Generate10Number()
        {
            Random random = new Random();
            int randomNumber = random.Next(100000000, 1000000000);

            return randomNumber.ToString();

        }

        public static string GenerateRandomPassword(int length = 12)
        {
            string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";

            var byteBuffer = new byte[length];

            RandomNumberGenerator.Fill(byteBuffer);

            var password = new char[length];
            for (int i = 0; i < length; i++)
            {
                int charIndex = byteBuffer[i] % validChars.Length;
                password[i] = validChars[charIndex];
            }

            return new string(password);
        }


        public static string GenerateLongDateTime(int length = 12)
        {
            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("yyMMddHHmmss");

            return formattedDateTime;
        }


        public static string GenerateUniqueId(int maxLength)
        {
            string guidPart = Guid.NewGuid().ToString("N"); // 32 characters without dashes

            string timestampPart = DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");

            string randomPart = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                .Replace("=", "").Replace("+", "").Replace("/", "");

            string uniqueId = $"{guidPart}{timestampPart}{randomPart}";

            return uniqueId.Length > maxLength ? uniqueId.Substring(0, maxLength) : uniqueId;
        }

        public static string GenerateSessionChecksum256Hash(string merchantId, string merchantSiteId, string clientRequestId, string timeStamp, string merchantSecretKey)
        {
            string concatString = $"{merchantId}{merchantSiteId}{clientRequestId}{timeStamp}{merchantSecretKey}";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(concatString));

                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2")); //byte to a 2-digit hex
                }

                return hashString.ToString();
            }
        }

        public static string GenerateSaleChecksum256Hash(string merchantId, string merchantSiteId, string clientRequestId, string amount, string currency, string timeStamp, string merchantSecretKey)
        {
            string concatString = $"{merchantId}{merchantSiteId}{clientRequestId}{amount}{currency}{timeStamp}{merchantSecretKey}";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(concatString));

                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2")); //byte to a 2-digit hex
                }

                return hashString.ToString();
            }
        }

        public static string GenerateRetChecksum256Hash(string merchantId, string merchantSiteId, string clientRequestId, string clientUniqueId, string amount, string currency, string relatedTransactionId, string comment, string timeStamp, string merchantSecretKey)
        {
            string concatString = $"{merchantId}{merchantSiteId}{clientRequestId}{clientUniqueId}{amount}{currency}{relatedTransactionId}{timeStamp}{merchantSecretKey}";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(concatString));

                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2")); //byte to a 2-digit hex
                }

                return hashString.ToString();
            }
        }

        public static string GenerateDetailChecksum256Hash(string merchantId, string merchantSiteId, string transactionId, string timeStamp, string merchantSecretKey)
        {
            string concatString = $"{merchantId}{merchantSiteId}{transactionId}{timeStamp}{merchantSecretKey}";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(concatString));

                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2")); //byte to a 2-digit hex
                }

                return hashString.ToString();
            }
        }

    }
}
