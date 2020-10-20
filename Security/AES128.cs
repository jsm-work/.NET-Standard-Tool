using System;
using Convertors;

namespace Security
{
    public class AES128

    {
        /// <summary>
        /// 암호화 Encrypt - bytes to bytes
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] input, byte[] key, byte[] iv)
        {
            byte[] cipherText;
            //byte[] plain = { 0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08 };


            // Create an Aes object
            // with the specified key and IV.
            using (System.Security.Cryptography.Aes aesAlg = System.Security.Cryptography.Aes.Create())
            {
                aesAlg.BlockSize = 128;
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = System.Security.Cryptography.CipherMode.CBC;
                aesAlg.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                // Create an encryptor to perform the stream transform.
                System.Security.Cryptography.ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                cipherText = encryptor.TransformFinalBlock(input, 0, input.Length);
            }

            return cipherText;

            //byte[] result = new byte[newPlain.Length];
            //System.Buffer.BlockCopy(cipherText, 0, result, 0, newPlain.Length);

            //// Return the encrypted bytes from the memory stream.
            //return result;
        }

        /// <summary>
        /// 복호화 Decrypt - bytes to bytes
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] input, byte[] key, byte[] iv)
        {
            byte[] plainText;
            // Create an Aes object
            // with the specified key and IV.
            using (System.Security.Cryptography.Aes aesAlg = System.Security.Cryptography.Aes.Create())
            {
                aesAlg.BlockSize = 128;
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = System.Security.Cryptography.CipherMode.CBC;
                aesAlg.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                System.Security.Cryptography.ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                plainText = decryptor.TransformFinalBlock(input, 0, input.Length);
            }

            return plainText;

        }


        /// <summary>
        /// 암호화 Encrypt - string To Decrypt_String
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key_HexString"></param>
        /// <param name="iv_HexString"></param>
        /// <returns></returns>
        public static string Encrypt_String(string input, string key_HexString, string iv_HexString)
        {
            byte[] bytes_input = System.Text.Encoding.UTF8.GetBytes(input);
            input = Convertor.ByteHexToHexString(bytes_input);
            return Convertor.ByteHexToHexString(AES128.Encrypt(Convertor.HexStringToByteHex(input), Convertor.HexStringToByteHex(key_HexString), Convertor.HexStringToByteHex(iv_HexString)));
        }

        /// <summary>
        /// 복호화 Decrypt - Encrypt_HexString To string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key_HexString"></param>
        /// <param name="iv_HexString"></param>
        /// <returns></returns>
        public static string Decrypt_String(string input, string key_HexString, string iv_HexString)
        {
            string HexString = Convertor.ByteHexToHexString(AES128.Decrypt(Convertor.HexStringToByteHex(input), Convertor.HexStringToByteHex(key_HexString), Convertor.HexStringToByteHex(iv_HexString)));
            byte[] bytes_output = Convertor.HexStringToByteHex(HexString);
            return System.Text.Encoding.UTF8.GetString(bytes_output);
        }

    }
}
