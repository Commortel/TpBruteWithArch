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
            OtherImage.Visibility = System.Windows.Visibility.Hidden;
            MeStat.Visibility = System.Windows.Visibility.Hidden;
            OtherStat.Visibility = System.Windows.Visibility.Hidden;
            GetOpponent.Visibility = System.Windows.Visibility.Hidden;
            FightWin.Visibility = System.Windows.Visibility.Hidden;
            FightLose.Visibility = System.Windows.Visibility.Hidden;
            Exit.Visibility = System.Windows.Visibility.Hidden;
            Name.Visibility = System.Windows.Visibility.Hidden;
            OtherName.Visibility = System.Windows.Visibility.Hidden;

            TextPassword.Visibility = System.Windows.Visibility.Visible;
            TextLogin.Visibility = System.Windows.Visibility.Visible;
            BoxPassword.Visibility = System.Windows.Visibility.Visible;
            BoxLogin.Visibility = System.Windows.Visibility.Visible;
            Submit.Visibility = System.Windows.Visibility.Visible;
            NewAccount.Visibility = System.Windows.Visibility.Visible;

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

                GetOpponent.Visibility = System.Windows.Visibility.Visible;
                FightWin.Visibility = System.Windows.Visibility.Visible;
                FightLose.Visibility = System.Windows.Visibility.Visible;
                Exit.Visibility = System.Windows.Visibility.Visible;
                MeImage.Visibility = System.Windows.Visibility.Visible;
                this.client.GetBrute(BoxLogin.Text);
                BitmapImage _image = new BitmapImage();
                _image.BeginInit();
                _image.CacheOption = BitmapCacheOption.OnLoad;
                _image.UriSource = new Uri(@"MyBruteImg.jpg", UriKind.Relative);
                _image.EndInit();
                MeImage.Source = _image;
                MeStat.Visibility = System.Windows.Visibility.Visible;
                Name.Visibility = System.Windows.Visibility.Visible;

                Name.Text = this.client.MyBrute.Name;
                TextLevel.Text = Convert.ToString(this.client.MyBrute.Level);
                TextLife.Text = Convert.ToString(this.client.MyBrute.Life);
                TextStrength.Text = Convert.ToString(this.client.MyBrute.Strength);
                TextAgility.Text = Convert.ToString(this.client.MyBrute.Agility);
                TextSpeed.Text = Convert.ToString(this.client.MyBrute.Speed);
            }
            else
            {
                MessageBox.Show("Fail Connection");
            }
        }

        private void GetOpponent_Click(object sender, RoutedEventArgs e)
        {
            this.client.GetOpponent();
            OtherImage.Visibility = System.Windows.Visibility.Visible;
            BitmapImage _image = new BitmapImage();
            _image.BeginInit();
            _image.CacheOption = BitmapCacheOption.OnLoad;
            _image.UriSource = new Uri(@"OtherBruteImg.jpg", UriKind.Relative);
            _image.EndInit();
            OtherImage.Source = _image;

            OtherStat.Visibility = System.Windows.Visibility.Visible;
            OtherName.Visibility = System.Windows.Visibility.Visible;

            OtherName.Text = this.client.MyBrute.Name;
            TextOtherLevel.Text = Convert.ToString(this.client.OtherBrute.Level);
            TextOtherLife.Text = Convert.ToString(this.client.OtherBrute.Life);
            TextOtherStrength.Text = Convert.ToString(this.client.OtherBrute.Strength);
            TextOtherAgility.Text = Convert.ToString(this.client.OtherBrute.Agility);
            TextOtherSpeed.Text = Convert.ToString(this.client.OtherBrute.Speed);
        }

        protected override void OnClosed(EventArgs e) 
        { 
            this.client.Deconnection();
        }

        private void FightWin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FightLose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.client.Deconnection();
            this.Initialize();
        }
    }
}
