﻿using System.Collections.Generic;
using System.Windows;
using API;
using Database_Item;
using Database_Mysql;
using Database_Oracle;
using Security;
using Compress;
using FileTransferProtocol;
using RestSharp;
using System.IO;
using System.Drawing;
using System.Linq;
using Convertors;

namespace WPF_TEST
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_sample_Click(object sender, RoutedEventArgs e)
        {
            #region API
            //POST
            //JSResult a = REST_API.Post_JSResult(   "http://theprost.synology.me:7553/api/aes/aes128-encrypt", 
            //                            REST_API.DictionaryToJson(new Dictionary<string, object>
            //                            {
            //                                { "Data", "40384B45B54596201114FE99042201" },
            //                                { "Key", "4D5A79677065774A7343705272664F72" }
            //                            })
            //);
            //MessageBox.Show(a.GetStringValue("encrytedData"));

            //GET
            //JSResults b = REST_API.Get_JSResults("http://toolkit-api.theprost.com/api/s100toolkit/program/last/all?programType_idx=-1&rankMin=-1&rankMax=-1");


            #region GetDownload
            //string ftpHost = "ftp://theprost.synology.me:21/";
            //string webFilePath = "web/File/20_10_19 08_33_50_2248";
            //string ftpID = "downloader";
            //string ftpPW = "123123123";

            //string uri = "http://toolkit-api.theprost.com/api/s100toolkit/program/download?host=" + ftpHost + "&webFilePath=" + webFilePath + "&ftpID=" + ftpID + "&ftpPW=" + ftpPW + "&fileName=" + "a.exe";

            //new System.Net.WebClient().DownloadFileTaskAsync(new System.Uri(uri), @"C:\Users\JS\Downloads\a.exe");
            #endregion


            #endregion

            #region MySQL
            //myMySql mysql = new myMySql("localhost", "root", "18932", "program", 5332);
            //foreach (JSResult item in mysql.Select("SELECT * FROM groups"))
            //{
            //    int? idx = item.GetIntValue("idx");                
            //    string groupName = item.GetStringValue("groupName");
            //}
            #endregion

            #region Oracle
            //myOracle oracle = new myOracle("localhost", "root", "18932", 5333);            
            //foreach (JSResult item in oracle.Select("SELECT * FROM groups"))
            //{
            //    int? idx = item.GetIntValue("idx");
            //    string groupName = item.GetStringValue("groupName");
            //}
            #endregion

            #region AES128
            //string input = "jsmun";
            //string Encrypt_String = AES128.Encrypt_String(input, "00000000000000000000000000000000", "00000000000000000000000000000000");
            //string Decrypt_String = AES128.Decrypt_String(Encrypt_String, "00000000000000000000000000000000", "00000000000000000000000000000000");
            //string output = Decrypt_String;
            //if (input == output)
            //    MessageBox.Show("성공");
            #endregion

            #region Compress
            //Zip.Compression(@"C:\Users\JS\Downloads\FCB", @"C:\Users\JS\Downloads\FCB.zip");

            //Zip.Decompression(@"C:\Users\JS\Downloads\FCB.zip", @"C:\Users\JS\Downloads\FCB", true);
            #endregion

            #region FTP
            //FTP.Download("ftp://theprost.synology.me:21/", "web/File/20_10_19 08_33_50_2248", @"C:\Users\JS\Downloads",  "a.exe", "downloader", "1234567891!");
            #endregion
        }

        private void btn_Image_Resize_Click(object sender, RoutedEventArgs e)
        {
            #region File To File
            // 2배 크기로 이미지 저장
            Image_Convertor.ImageResize_PathToPath(@"C:\Users\JS\Desktop\전자기린64x64.png", @"C:\Users\JS\Desktop\전자기린128x128.png", 2);

            // 100 x 100 크기로 이미지 저장
            Image_Convertor.ImageResize_PathToPath(@"C:\Users\JS\Desktop\전자기린64x64.png", @"C:\Users\JS\Desktop\전자기린100x100.png", 100, 100);
            #endregion

            //이미지 -> Stream
            Bitmap bitmap = new Bitmap(@"C:\Users\JS\Desktop\전자기린64x64.png");
            MemoryStream ms = Image_Convertor.ImageToStream(bitmap, System.Drawing.Imaging.ImageFormat.Png);

            #region Stream To Stream
            // 3배 크기로 이미지 저장
            Bitmap bitmap3 = new Bitmap(Image_Convertor.ImageResize_StreamToStream(ms, System.Drawing.Imaging.ImageFormat.Png, 3));
            #endregion
            bitmap3.Save(@"C:\Users\JS\Desktop\전자기린192x192.png");




        }
    }
}
