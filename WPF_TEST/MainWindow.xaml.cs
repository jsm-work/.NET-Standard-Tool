using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using API;
using Database_Item;

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
            JSResult a = REST_API.Post_JSResult(   "http://theprost.synology.me:7553/api/aes/aes128-encrypt", 
                                        REST_API.DictionaryToRawString(new Dictionary<string, object>
                                        {
                                            { "Data", "40384B45B54596201114FE99042201" },
                                            { "Key", "4D5A79677065774A7343705272664F72" }
                                        })
            );
            MessageBox.Show((string)a["encrytedData"]);

            JSResults b = REST_API.Get_JSResults("http://toolkit-api.theprost.com/api/s100toolkit/program/last/all?programType_idx=-1&rankMin=-1&rankMax=-1");
            





        }


    }
}
