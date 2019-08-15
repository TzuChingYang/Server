using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Server
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Title_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void Button_start_Click(object sender, RoutedEventArgs e)
        {
            connect(Textbox_output);
        }

       
        public void Textbox_output_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        /* ===================================================== */
        // About Socket server
        //
        /* ===================================================== */
        // State object for reading client data asynchronously 

        static void connect(TextBox t)
        {
            int port = 9527;
            string host = "10.211.55.3";
            ///創建終結點（EndPoint）
            IPAddress ip = IPAddress.Parse(host);//把IP地址字符串轉換為IPAddress類型的實例
            IPEndPoint ipe = new IPEndPoint(ip, port);//用指定的端口和ip初始化IPEndPoint類的新實例 
                                                      ///創建socket並開始監聽
                                                      ///
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//創建一個socket對像，如果用udp協議，則要用SocketType.Dgram類型的套接字
            s.Bind(ipe);//綁定EndPoint對像（2000端口和ip地址）
            s.Listen(0);//開始監聽
            t.Text = "Waiting for connect... \n";
            Console.WriteLine("等待客戶端連接");
            ///接受到client連接，為此連接建立新的socket，並接受信息
            Socket temp = s.Accept();//為新建連接創建新的socket
            t.AppendText(" Connect success \n");
            Console.WriteLine("建立連接");
            string recvStr = "";
            byte[] recvBytes = new byte[1024];
            int bytes;
            bytes = temp.Receive(recvBytes, recvBytes.Length, 0);//從客戶端接受信息
            recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);
            ///給client端返回信息
            ///
            Console.WriteLine("server get message:{0}", recvStr);//把客戶端傳來的信息顯示出來
            t.AppendText("Server get message: "+recvStr+"\n");
            string sendStr = "Server get message:"+recvStr;
            byte[] bs = Encoding.ASCII.GetBytes(sendStr);
            temp.Send(bs, bs.Length, 0);//返回信息給客戶端
            temp.Close();
            s.Close();
            Console.ReadLine();
        }

    }


}
