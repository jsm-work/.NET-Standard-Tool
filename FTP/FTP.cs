using System;
using System.IO;
using System.Net;
using Convertors;

namespace FileTransferProtocol
{
    public class FTP
    {

        public FTP(string FTP_HOST, string FTP_ID, string FTP_PW)
        {
            ftpHost = FTP_HOST;
            ftpID = FTP_ID;
            ftpPW = FTP_PW;
        }

        //FTP 정보
        string ftpHost = string.Empty;
        string ftpID = string.Empty;
        string ftpPW = string.Empty;

        /// <summary>
        /// 파일 다운로드
        /// </summary>
        /// <param name="ftpHost">호스트 주소</param>
        /// <param name="webFilePath">내려받는 파일 경로 (호스트 경로를 제외한 나머지)</param>
        /// <param name="filePath">파일이 저장되는 폴더 경로</param>
        /// <param name="fileName">파일명(확장자 포함)</param>
        /// <param name="ftpID">접근 계정</param>
        /// <param name="ftpPW">접근 계정 암호</param>
        public static async System.Threading.Tasks.Task<Stream> Download_Stream(string ftpHost, string webFilePath, string ftpID, string ftpPW)
        {
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(ftpHost + webFilePath);
            //경로+ 파일이름

            ftpWebRequest.Credentials = new NetworkCredential(ftpID, ftpPW);
            ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();

            Stream responsStream = ftpWebResponse.GetResponseStream();
            return responsStream;
        }

        /// <summary>
        /// 파일 업로드
        /// </summary>
        /// <param name="ftpHost">호스트 주소</param>
        /// <param name="webFilePath">파일이 저장되는 폴더 경로 (호스트 경로를 제외한 나머지)</param>
        /// <param name="webFileName">파일명(확장자 포함)</param>
        /// <param name="ftpID">접근 계정</param>
        /// <param name="ftpPW">접근 계정 암호</param>
        /// <param name="stream">파일 Stream</param>
        public static async void Upload_Stream(string ftpHost, string webFilePath, string webFileName, string ftpID, string ftpPW, Stream stream)
        {
            //string fileName = Path.GetFileName(localFilePath);

            CreateDirectory(ftpHost, webFilePath, ftpID, ftpPW);

            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(ftpID, ftpPW);
                client.Encoding = System.Text.Encoding.UTF8;
                //client.UploadFile(ftpHost + webFilePath + webFileName, WebRequestMethods.Ftp.UploadFile, localFilePath);
                client.UploadData(ftpHost + webFilePath + webFileName, String_Convertor.StreamToBytes(stream));
            }
        }

        /// <summary>
        /// 파일 다운로드
        /// </summary>
        /// <param name="ftpHost">호스트 주소</param>
        /// <param name="webFilePath">내려받는 파일 경로 (호스트 경로를 제외한 나머지)</param>
        /// <param name="filePath">파일이 저장되는 폴더 경로</param>
        /// <param name="fileName">파일명(확장자 포함)</param>
        /// <param name="ftpID">접근 계정</param>
        /// <param name="ftpPW">접근 계정 암호</param>
        public static void Download(string ftpHost, string webFilePath, string filePath, string fileName, string ftpID, string ftpPW)
        {
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(ftpHost + webFilePath);
            //경로+ 파일이름

            ftpWebRequest.Credentials = new NetworkCredential(ftpID, ftpPW);
            ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();

            Stream responsStream = ftpWebResponse.GetResponseStream();


            FileStream writerStream = new FileStream(filePath + @"\" + fileName, FileMode.Create);

            int Length = 2048;

            Byte[] buffer = new byte[Length];

            int bytesRead = responsStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writerStream.Write(buffer, 0, bytesRead);
                bytesRead = responsStream.Read(buffer, 0, Length);
            }
            responsStream.Close();
            writerStream.Close();
        }
        public static void Upload(string ftpHost, string webFilePath, string webFileName, string ftpID, string ftpPW, string localFilePath )
        {
            //string fileName = Path.GetFileName(localFilePath);

            CreateDirectory(ftpHost, webFilePath, ftpID, ftpPW);

            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(ftpID, ftpPW);
                client.Encoding = System.Text.Encoding.UTF8;
                client.UploadFile(ftpHost + webFilePath + webFileName, WebRequestMethods.Ftp.UploadFile, localFilePath);
                Console.WriteLine(ftpHost + webFilePath + webFileName + "\t\t\t" + localFilePath);
            }
        }

        /// <summary>
        /// 파일 다운로드
        /// </summary>
        /// <param name="webFilePath">내려받는 파일 경로 (호스트 경로를 제외한 나머지)</param>
        /// <param name="filePath">파일이 저장되는 폴더 경로</param>
        /// <param name="fileName">파일명(확장자 포함)</param>
        public void Download(string webFilePath, string filePath, string fileName)
        {
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(ftpHost + webFilePath);
            //경로+ 파일이름

            ftpWebRequest.Credentials = new NetworkCredential(ftpID, ftpPW);
            ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();

            Stream responsStream = ftpWebResponse.GetResponseStream();


            FileStream writerStream = new FileStream(filePath + @"\" + fileName, FileMode.Create);

            int Length = 2048;

            Byte[] buffer = new byte[Length];

            int bytesRead = responsStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writerStream.Write(buffer, 0, bytesRead);
                bytesRead = responsStream.Read(buffer, 0, Length);
            }
            responsStream.Close();
            writerStream.Close();
        }
        public void Upload(string localFilePath, string webPath, string fileName)
        {
            //string fileName = Path.GetFileName(localFilePath);

            CreateDirectory(ftpHost, webPath);

            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(ftpID, ftpPW);
                client.Encoding = System.Text.Encoding.UTF8;
                client.UploadFile(ftpHost + webPath + fileName, WebRequestMethods.Ftp.UploadFile, localFilePath);
                Console.WriteLine(ftpHost + webPath + fileName + "\t\t\t" + localFilePath);
            }
        }



        public static bool FileCompare(string file1, string file2)
        {
            if (file1.Contains("SQLite.Interop.dll") == true)
            {
            }

            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            // Determine if the same file was referenced two times.

            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }



            // Open the two files.

            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);


            // Check the file sizes. If they are not the same, the files 

            // are not the same.

            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a

            // non-matching set of bytes is found or until the end of

            // file1 is reached.

            do

            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }

            while ((file1byte == file2byte) && (file1byte != -1));


            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);

        }
        public void CreateDirectory(string host, string path, string ftpProxy = null)
        {
            FtpWebRequest reqFTP = null;
            Stream ftpStream = null;

            string[] subDirs = path.Split('/');

            string currentDir = host;

            foreach (string subDir in subDirs)
            {
                try
                {
                    currentDir = currentDir + "/" + subDir;
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(currentDir);
                    reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(ftpID, ftpPW);
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    ftpStream = response.GetResponseStream();
                    ftpStream.Close();
                    response.Close();
                }
                catch (Exception ex)
                {
                    //directory already exist I know that is weak but there is no way to check if a folder exist on ftp...
                }
            }
        }
        public static void CreateDirectory(string host, string path, string ftpID, string ftpPW, string ftpProxy = null)
        {
            FtpWebRequest reqFTP = null;
            Stream ftpStream = null;

            string[] subDirs = path.Split('/');

            string currentDir = host;

            foreach (string subDir in subDirs)
            {
                try
                {
                    currentDir = currentDir + "/" + subDir;
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(currentDir);
                    reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(ftpID, ftpPW);
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    ftpStream = response.GetResponseStream();
                    ftpStream.Close();
                    response.Close();
                }
                catch (Exception ex)
                {
                    //directory already exist I know that is weak but there is no way to check if a folder exist on ftp...
                }
            }
        }
    }
}
