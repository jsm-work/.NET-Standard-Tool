using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Renci.SshNet;
using Renci.SshNet.Sftp;
namespace FTP
{
    public class SFTP
    {        
        //SFTP 정보
        string sftpHost = string.Empty;
        string sftpID = string.Empty;
        string sftpPW = string.Empty;
        ConnectionInfo connectionInfo;

        public SFTP(string FTP_HOST, string FTP_ID, string FTP_PW)
        {
            sftpHost = FTP_HOST;
            sftpID = FTP_ID;
            sftpPW = FTP_PW;
            connectionInfo = new ConnectionInfo(FTP_HOST, FTP_ID, new PasswordAuthenticationMethod(FTP_ID, FTP_PW));
        }

        /// <summary>
        /// http://theprost.synology.me:31111/khoa-encviewer-file/
        /// </summary>
        /// <param name="webFilePath"></param>
        /// <param name="webFileName"></param>
        /// <param name="localFilePath"></param>
        public void Upload(string webFilePath, string webFileName, string localFilePath)
        {
            using (var sftp = new SftpClient(connectionInfo))
            {
                // SFTP 서버 연결
                sftp.Connect();

                //// 현재 디렉토리 내용 표시
                //foreach (SftpFile f in sftp.ListDirectory("."))
                //{
                //    Console.WriteLine(f.Name);
                //}

                // SFTP 업로드
                using (var infile = File.Open(localFilePath, FileMode.Open))
                {
                    sftp.UploadFile(infile, webFilePath+@"/"+ webFileName);
                }

                sftp.Disconnect();
            }
        }


        public SFTP()
        {
            using (var sftp = new SftpClient(connectionInfo))
            {
                // SFTP 서버 연결
                sftp.Connect();

                // 현재 디렉토리 내용 표시
                foreach (SftpFile f in sftp.ListDirectory("."))
                {
                    Console.WriteLine(f.Name);
                }

                // SFTP 다운로드
                using (var outfile = File.Create("ftptest.txt"))
                {
                    sftp.DownloadFile("./ftptest.txt", outfile);
                }

                // SFTP 업로드
                using (var infile = File.Open("ftptest.txt", FileMode.Open))
                {
                    sftp.UploadFile(infile, "./myftp/ftptest.txt");
                }

                sftp.Disconnect();
            }
    }
    }
}
