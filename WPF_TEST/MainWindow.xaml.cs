using System.Collections.Generic;
using System.Windows;
using API;
using Database_Item;
using Database_Mysql;
using Database_Oracle;
using Security;
using Compress;
using System.IO.Compression;

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
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            #region API
            //JSResult a = REST_API.Post_JSResult(   "http://theprost.synology.me:7553/api/aes/aes128-encrypt", 
            //                            REST_API.DictionaryToJson(new Dictionary<string, object>
            //                            {
            //                                { "Data", "40384B45B54596201114FE99042201" },
            //                                { "Key", "4D5A79677065774A7343705272664F72" }
            //                            })
            //);
            //MessageBox.Show(a.GetStringValue("encrytedData"));


            //JSResults b = REST_API.Get_JSResults("http://toolkit-api.theprost.com/api/s100toolkit/program/last/all?programType_idx=-1&rankMin=-1&rankMax=-1");
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
        }
    }
}
