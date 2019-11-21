using System;
using System.Security.Cryptography;
using System.Text;

namespace ScientificPublications.Common.Utility
{
    public static class HashUtility
    {
        public static byte[] GetHash(string input)
        {
            var algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
        }

        public static string GetHashString(string input)
        {
            var stringBuilder = new StringBuilder();
            foreach (var b in GetHash(input))
            {
                stringBuilder.Append(b.ToString("X2"));
            }

            return stringBuilder.ToString().ToLower();
        }

        public static string GetNewSalt()
        {
            return GetHashString(Guid.NewGuid().ToString());
        }

        public static string HashPassword(string salt, string password)
        {
            var result = GetHashString(salt + password);
            for (int i = 0; i < 13; i++)
                result = GetHashString(result);

            return result;
        }
    }
}
