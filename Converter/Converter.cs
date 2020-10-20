using System;

namespace Converters
{
    public class Converter
    {
        /// <summary>
        /// Stream을 byte[]로 변환
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(System.IO.Stream stream)
        {
            System.IO.MemoryStream TempMemoryStream;
            Int32 reads = 0; //임시 메모리스트림에 작성 
            using (System.IO.Stream st = stream)
            {
                using (System.IO.MemoryStream output = new System.IO.MemoryStream())
                {
                    st.Position = 0; Byte[] buffer = new Byte[256];
                    while (0 < (reads = st.Read(buffer, 0, buffer.Length)))
                    {
                        output.Write(buffer, 0, reads);
                    }
                    TempMemoryStream = output;
                    output.Flush();
                } // in using 
            } // out using
            byte[] bytes = TempMemoryStream.ToArray();

            return bytes;
        }

        /// <summary>
        /// 16진수 문자를 16진수 Byte[]로 변환
        /// </summary>
        /// <param name="strHex"></param>
        /// <returns></returns>
        /// 
        public static byte[] HexStringToByteHex(string strHex)
        {
            if (strHex == null)
                return null;
            else if (strHex.Length == 0)
                return null;
            else if (strHex.Length % 2 != 0)
            {
                //MessageBox.Show("HexString는 홀수일 수 없습니다. - " + strHex);
                return null;
            }

            byte[] bytes = new byte[strHex.Length / 2];

            for (int count = 0; count < strHex.Length; count += 2)
            {
                bytes[count / 2] = System.Convert.ToByte(strHex.Substring(count, 2), 16);
            }
            return bytes;
        }

        /// <summary>
        /// 16진수 Byte[]를 16진수 문자로 변환
        /// </summary>
        /// <param name="strHex"></param>
        /// <returns></returns>
        /// 
        public static string ByteHexToHexString(byte[] strHex)
        {
            string result = string.Empty;
            foreach (byte c in strHex)
                result += c.ToString("x2").ToUpper();
            return result;
        }

        public static string ByteToString(byte[] strHex)
        {
            string result = string.Empty;
            foreach (byte c in strHex)
                result += c.ToString("x2");
            return result;
        }
    }
}
