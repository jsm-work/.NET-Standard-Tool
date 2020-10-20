using System;
using System.IO;
using System.Net;

namespace FileTransferProtocol
{
    public class FTP
    {
        public FTP(string FTP_HOST, string PATH, string FTP_ID, string FTP_PW)
        {
            ftpHost = FTP_HOST;
            ftpPath = PATH;
            ftpID = FTP_ID;
            ftpPW = FTP_PW;
        }

        //FTP 정보
        string ftpHost = string.Empty;
        string ftpPath = string.Empty;
        string ftpID = string.Empty;
        string ftpPW = string.Empty;

        public void Download(string ftpPath, string ftpID, string ftpPW, string filePath, string webPath, string saveFileName)
        {
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(ftpPath + webPath);
            //경로+ 파일이름

            ftpWebRequest.Credentials = new NetworkCredential(ftpID, ftpPW);
            ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();

            Stream responsStream = ftpWebResponse.GetResponseStream();


            FileStream writerStream = new FileStream(filePath + @"\" + saveFileName, FileMode.Create);

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

        public void FtpUpload(string localFilePath, string fileName)
        {
            //string fileName = Path.GetFileName(localFilePath);

            CreateDirectory(ftpHost, ftpPath);

            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(ftpID, ftpPW);
                client.Encoding = System.Text.Encoding.UTF8;
                client.UploadFile(ftpHost + ftpPath + fileName, WebRequestMethods.Ftp.UploadFile, localFilePath);
                Console.WriteLine(ftpHost + ftpPath + fileName + "\t\t\t" + localFilePath);
            }
        }


        public bool FileCompare(string file1, string file2)
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
    }
}
