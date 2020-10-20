using System;
using System.Collections.Generic;
using System.Text;
using Convertors;

namespace Security
{
    public class Userpermit
    {
        /// <summary>
        /// 입력된 값으로 userpermit을 생성하여 반환
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="mkey"></param>
        /// <param name="hwid"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string GetUserpermit(string mid, string mkey, string hwid, string iv = "00000000000000000000000000000000")
        {
            //String To HEX
            byte[] IV = Convertor.HexStringToByteHex(iv);
            byte[] M_ID = Convertor.HexStringToByteHex(mid);
            byte[] M_KEY = Convertor.HexStringToByteHex(mkey);
            byte[] HW_ID = Convertor.HexStringToByteHex(hwid);

            //암호화
            //Encrypred HW ID = Aes(HW_ID, M_KEY, IV)
            byte[] Encrypred_HW_ID = AES128.Encrypt(HW_ID, M_KEY, IV);
            //String To HEX
            string strEncrypred_HW_ID = Convertor.ByteHexToHexString(Encrypred_HW_ID);
            //Checksum = CRC32(EncrypredHWID)
            string checksum = CRC32.StringToCRC32(strEncrypred_HW_ID).ToString("X2");

            return strEncrypred_HW_ID + checksum + mid;
        }
    }
}
