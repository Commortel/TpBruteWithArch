using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.IO;
using Protocole;

namespace Client
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPAddress ip;
        private IPEndPoint ipEnd;
        private Socket ClientSocket;
        private SocketClient client;

        public MainWindow()
        {
            InitializeComponent();
            this.Initialize();
        }

        private void Initialize()
        {
            MeImage.Visibility = System.Windows.Visibility.Hidden;
            OhterImage.Visibility = System.Windows.Visibility.Hidden;

            this.ip = IPAddress.Parse("127.0.0.1");
            this.ipEnd = new IPEndPoint(ip, ProtocoleImplementation.PORT_ID);
            this.ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientSocket.Connect(ipEnd);
                if (ClientSocket.Connected)
                {
                    this.client = new SocketClient(ClientSocket);
                }
            }
            catch (SocketException E)
            {
                MessageBox.Show("Connection" + E.Message);
            }

            if (this.client.Login(BoxLogin.Text, BoxPassword.Text))
            {
                TextPassword.Visibility = System.Windows.Visibility.Hidden;
                TextLogin.Visibility = System.Windows.Visibility.Hidden;
                BoxPassword.Visibility = System.Windows.Visibility.Hidden;
                BoxLogin.Visibility = System.Windows.Visibility.Hidden;
                Submit.Visibility = System.Windows.Visibility.Hidden;
                NewAccount.Visibility = System.Windows.Visibility.Hidden;

                MeImage.Visibility = System.Windows.Visibility.Visible;
                OhterImage.Visibility = System.Windows.Visibility.Visible;
                this.client.GetBrute(BoxLogin.Text);
                BitmapImage _image = new BitmapImage();
                _image.BeginInit();
                _image.CacheOption = BitmapCacheOption.OnLoad;
                _image.UriSource = new Uri(@"MyBruteImg.jpg", UriKind.Relative);
                _image.EndInit();
                MeImage.Source = _image;
            }
            else
            {
                MessageBox.Show("Fail Connection");
            }
        }

        protected override void OnClosed(EventArgs e) 
        { 
            this.client.Deconnection();
        }
    }
}
