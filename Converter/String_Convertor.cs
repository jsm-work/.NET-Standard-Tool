using System;
using System.Drawing;
using System.IO;

namespace Convertors
{
    public class Image_Convertor
    {
        #region Image ↔ byte[]  
        /// <summary>
        /// byte[] to image 
        /// * System.Drawing.Common
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static Bitmap BytesToBitmap(byte[] Bytes)
        {
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(Bytes);
                return new Bitmap((Image)new Bitmap(stream));
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
        }

        /// <summary>
        /// Image to byte[]   
        /// * System.Drawing.Common
        /// </summary>
        /// <param name="Bitmap"></param>
        /// <returns></returns>
        public static byte[] BitmapToBytes(Bitmap Bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                Bitmap.Save(ms, Bitmap.RawFormat);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }
        #endregion

        #region Image ↔ Stream
        public static MemoryStream ImageToStream(Bitmap bitmap, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, imageFormat);
            return memoryStream;
        }

        public static Bitmap StreamToImage(Stream stream, int width = 0, int height = 0)
        {
            if (width <= 0 || height <= 0)
                return new Bitmap(stream);
            else
                return new Bitmap(new Bitmap(stream), new Size(width, height));
        }
        #endregion

        #region Image ReSize
        /// <summary>
        /// * System.Drawing.Common
        /// </summary>
        /// <param name="Input_FilePath"></param>
        /// <param name="Output_FIlePath"></param>
        /// <param name="output_width"></param>
        /// <param name="output_height"></param>
        /// <returns></returns>
        public static bool ImageResize_PathToPath(string Input_FilePath, string Output_FIlePath, int output_width, int output_height)
        {
            Bitmap Input_bitmap = new Bitmap(Input_FilePath);
            Bitmap Output_bitmap = new Bitmap(Input_bitmap, new Size(output_width, output_height));
            Output_bitmap.Save(Output_FIlePath);

            return new FileInfo(Output_FIlePath).Exists;
        }

        /// <summary>
        /// * System.Drawing.Common
        /// </summary>
        /// <param name="Input_FilePath"></param>
        /// <param name="Output_FIlePath"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static bool ImageResize_PathToPath(string Input_FilePath, string Output_FIlePath, double scale)
        {
            Bitmap Input_bitmap = new Bitmap(Input_FilePath);
            Bitmap Output_bitmap = new Bitmap(Input_bitmap, new Size((int)((double)Input_bitmap.Width * scale), (int)((double)Input_bitmap.Height * scale)));
            Output_bitmap.Save(Output_FIlePath);

            return new FileInfo(Output_FIlePath).Exists;
        }

        /// <summary>
        /// * System.Drawing.Common
        /// </summary>
        /// <param name="Input_Stream"></param>
        /// <param name="output_width"></param>
        /// <param name="output_height"></param>
        /// <returns></returns>
        public static Stream ImageResize_StreamToStream(Stream Input_Stream, int output_width, int output_height)
        {
            Bitmap Input_bitmap = new Bitmap(Input_Stream);
            Bitmap Output_bitmap = new Bitmap(Input_bitmap, new Size(output_width, output_height));

            return new MemoryStream(BitmapToBytes(Output_bitmap));
        }

        /// <summary>
        /// * System.Drawing.Common
        /// </summary>
        /// <param name="Input_Stream"></param>
        /// <param name="Output_Stream"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Stream ImageResize_StreamToStream(Stream Input_Stream, System.Drawing.Imaging.ImageFormat imageFormat, double scale)
        {
            Bitmap Input_bitmap = new Bitmap(Input_Stream);
            Bitmap Output_bitmap = new Bitmap(Input_bitmap, new Size((int)((double)Input_bitmap.Width * scale), (int)((double)Input_bitmap.Height * scale)));
            
            return ImageToStream(Output_bitmap, imageFormat);
        }
        #endregion
    }
    public class String_Convertor
    {
        #region Stream ↔ Byte[]
        /// <summary>
        /// Stream을 byte[]로 변환
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            MemoryStream TempMemoryStream;
            Int32 reads = 0; //임시 메모리스트림에 작성 
            using (Stream st = stream)
            {
                using (MemoryStream output = new MemoryStream())
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
        /// byte[]를 Stream으로 변환
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream s = new MemoryStream();
            using (var writer = new BinaryWriter(s))
            {
                writer.Write(bytes);
            }
            return s;
        }
        #endregion

        #region HexString ↔ Byte[]
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
        public static string ByteHexToHexString(byte[] bytes)
        {
            string result = string.Empty;
            foreach (byte c in bytes)
                result += c.ToString("x2").ToUpper();
            return result;
        }

        #endregion

        #region String ↔ Byte[]
        /// <summary>
        /// 바이트 배열을 String으로 변환 
        /// </summary>
        /// <param name="strByte"></param>
        /// <returns></returns>
        public static string ByteToString(byte[] strByte)
        {
            string str = System.Text.Encoding.UTF8.GetString(strByte);
            return str;
        }

        /// <summary>
        /// String을 바이트 배열로 변환 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] StringToByte(string str)
        {
            byte[] StrByte = System.Text.Encoding.UTF8.GetBytes(str);
            return StrByte;
        }
        #endregion

        #region String ↔ HexString
        /// <summary>
        /// 문자를 16진수 문자로 변환
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToHexString(string str)
        {
            byte[] bytes = StringToByte(str);            
            return ByteHexToHexString(bytes);
        }

        /// <summary>
        /// 16진수 문자를 string으로 변환
        /// </summary>
        /// <param name="strHex"></param>
        /// <returns></returns>
        public static string HexStringToString(string strHex)
        {
            if (strHex.Length % 2 == 0)
            {
                string result = string.Empty;
                for (int i = 0; i < strHex.Length; i += 2)
                {

                    result += (char)Convert.ToInt32(strHex.Substring(i, 2), 16);
                }
                return result;
            }
            else
                return null;
        }
        #endregion
    }

    public class File_Read
    {
        /// <summary>
        /// txt 파일을 읽어 Dictionary에 저장해서 반환합니다.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static System.Collections.Generic.Dictionary<string, string> ReadAppInfo(string filePath, char separator)
        {
            System.Collections.Generic.Dictionary<string, string> dicResult = new System.Collections.Generic.Dictionary<string, string>();

            string[] lines = System.IO.File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (line.Trim().Length > 0)
                {
                    string[] l = line.Split(separator);
                    if (l.Length == 2)
                    {
                        dicResult.Add(l[0].Trim(), l[1].Trim());
                    }
                }
            }
            return dicResult;
        }
    }
}
