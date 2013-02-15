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
using System.Windows.Interop;

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
            MainGrid.Background = new SolidColorBrush(Color.FromRgb(250, 248, 195));
            MeImage.Visibility = System.Windows.Visibility.Hidden;
            OtherImage.Visibility = System.Windows.Visibility.Hidden;
            MeStat.Visibility = System.Windows.Visibility.Hidden;
            OtherStat.Visibility = System.Windows.Visibility.Hidden;
            Menu.Visibility = System.Windows.Visibility.Hidden;
            NameTitle.Visibility = System.Windows.Visibility.Hidden;
            OtherNameTitle.Visibility = System.Windows.Visibility.Hidden;
            MeBonusImage.Visibility = System.Windows.Visibility.Hidden;
            OtherBonusImage.Visibility = System.Windows.Visibility.Hidden;

            Login.Visibility = System.Windows.Visibility.Visible;
            SubmitImage.Source = this.CreateImage("../../Resources/button.gif");
            Submit.Background = new SolidColorBrush(Color.FromRgb(250, 248, 195));

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
                Login.Visibility = System.Windows.Visibility.Hidden;

                Menu.Visibility = System.Windows.Visibility.Visible;
                MeBonusImage.Visibility = System.Windows.Visibility.Visible;
                MeImage.Visibility = System.Windows.Visibility.Visible;
                this.client.GetBrute(BoxLogin.Text);

                MeImage.Source = this.CreateImage("MyBruteImg.jpg");
                MeStat.Visibility = System.Windows.Visibility.Visible;
                NameTitle.Visibility = System.Windows.Visibility.Visible;

                NameTitle.Text = this.client.MyBrute.Name;
                TextLevel.Text = Convert.ToString(this.client.MyBrute.Level);
                TextLife.Text = Convert.ToString(this.client.MyBrute.Life);
                TextStrength.Text = Convert.ToString(this.client.MyBrute.Strength);
                TextAgility.Text = Convert.ToString(this.client.MyBrute.Agility);
                TextSpeed.Text = Convert.ToString(this.client.MyBrute.Speed);

                for (int i = 0; i < this.client.MyBrute.BonusList.Count; i++)
                {
                    switch(i)
                    {
                        case 0:
                            MeBonus1Image.Source = this.CreateImage("Bonus" + i + ".png");
                            break;
                        case 1:
                            MeBonus2Image.Source = this.CreateImage("Bonus" + i + ".png");
                            break;
                        case 2:
                            MeBonus3Image.Source = this.CreateImage("Bonus" + i + ".png");
                            break;
                    }
                }           
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
            OtherImage.Source = this.CreateImage("OtherBruteImg.jpg"); 
            OtherBonusImage.Visibility = System.Windows.Visibility.Visible;
            OtherStat.Visibility = System.Windows.Visibility.Visible;
            OtherNameTitle.Visibility = System.Windows.Visibility.Visible;

            OtherNameTitle.Text = this.client.OtherBrute.Name;
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
            this.client.UpdateBrute(this.client.MyBrute.Name, true);
            this.client.GetBrute(this.client.MyBrute.Name);

            NameTitle.Text = this.client.MyBrute.Name;
            TextLevel.Text = Convert.ToString(this.client.MyBrute.Level);
            TextLife.Text = Convert.ToString(this.client.MyBrute.Life);
            TextStrength.Text = Convert.ToString(this.client.MyBrute.Strength);
            TextAgility.Text = Convert.ToString(this.client.MyBrute.Agility);
            TextSpeed.Text = Convert.ToString(this.client.MyBrute.Speed);

        }

        private void FightLose_Click(object sender, RoutedEventArgs e)
        {     
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.client.Deconnection();
            this.Initialize();
        }

        private void NewAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        private BitmapImage CreateImage(string path)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.CreateOptions = BitmapCreateOptions.IgnoreImageCache; 
            image.UriSource = new Uri(@""+path, UriKind.Relative);
            image.EndInit();
            return image;
        }
    }
}
