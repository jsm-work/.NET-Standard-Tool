using System;
using System.Collections.Generic;
using System.Text;

namespace Security
{
    public class MD5
    {
        public static string Encrypt(string input)
        {
            StringBuilder MD5Str = new StringBuilder();
            byte[] byteArr = Encoding.ASCII.GetBytes(input);
            byte[] resultArr = (new System.Security.Cryptography.MD5CryptoServiceProvider()).ComputeHash(byteArr);

            for (int i = 0; i < resultArr.Length; i++)
            {
                MD5Str.Append(resultArr[i].ToString("X2"));
            }
            return MD5Str.ToString().ToLower();
        }
    }
}
