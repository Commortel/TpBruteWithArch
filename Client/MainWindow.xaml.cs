﻿using System;
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
        #region Fields

        private IPAddress ip;
        private IPEndPoint ipEnd;
        private Socket ClientSocket;
        private SocketClient client;

        #endregion Fields

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
            this.Initialize();
        }

        private void Initialize()
        {
            MainGrid.Background = new SolidColorBrush(Color.FromRgb(250, 248, 195));
            BruteTest.Visibility = System.Windows.Visibility.Hidden;
            BruteTestOther.Visibility = System.Windows.Visibility.Hidden;
            MeBonusImage.Visibility = System.Windows.Visibility.Hidden;
            OtherBonusImage.Visibility = System.Windows.Visibility.Hidden;
            Menu.Visibility = System.Windows.Visibility.Hidden;

            Login.Visibility = System.Windows.Visibility.Visible;
            SubmitImage.Source = this.CreateImage("Resources/button.gif");
            Submit.Background = new SolidColorBrush(Color.FromRgb(250, 248, 195));
            NewAccountImage.Source = this.CreateImage("Resources/button.gif");
            NewAccount.Background = new SolidColorBrush(Color.FromRgb(250, 248, 195));
            MeBonusImage.Background = this.BitmapToBrush(this.CreateImage("Resources/tableau_bois.png"));
            OtherBonusImage.Background = this.BitmapToBrush(this.CreateImage("Resources/tableau_bois.png"));
            GetOpponent.Background = this.BitmapToBrush(this.CreateImage("Resources/button.gif"));
            FightWin.Background = this.BitmapToBrush(this.CreateImage("Resources/button.gif"));
            FightLose.Background = this.BitmapToBrush(this.CreateImage("Resources/button.gif"));
            Exit.Background = this.BitmapToBrush(this.CreateImage("Resources/button.gif"));

            this.ip = IPAddress.Parse("127.0.0.1");
            this.ipEnd = new IPEndPoint(ip, ProtocoleImplementation.PORT_ID);
            this.ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        #endregion Constructors

        #region Methods

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
                BruteTest.Visibility = System.Windows.Visibility.Visible;
                MeBonusImage.Visibility = System.Windows.Visibility.Visible;
                this.client.GetBrute(BoxLogin.Text);

                BruteTest.BruteImageControl = "MyBruteImg.jpg";
                BruteTest.NameTitleControl = this.client.MyBrute.Name;
                BruteTest.StatBruteControl = this.StatBruteMaker(this.client.MyBrute);

                for (int i = 0; i < this.client.MyBrute.BonusList.Count; i++)
                {
                    switch(i)
                    {
                        case 0:
                            MeBonus1Image.Source = this.CreateImage("BruteBonus" + i + ".png");
                            break;
                        case 1:
                            MeBonus2Image.Source = this.CreateImage("BruteBonus" + i + ".png");
                            break;
                        case 2:
                            MeBonus3Image.Source = this.CreateImage("BruteBonus" + i + ".png");
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
            GetOpponent.IsEnabled = false;
            this.client.GetOpponent();
            BruteTestOther.Visibility = System.Windows.Visibility.Visible;
            OtherBonusImage.Visibility = System.Windows.Visibility.Visible;

            BruteTestOther.BruteImageControl = "OtherBruteImg.jpg";
            BruteTestOther.NameTitleControl = this.client.OtherBrute.Name;
            BruteTestOther.StatBruteControl = this.StatBruteMaker(this.client.OtherBrute);

            for (int i = 0; i < this.client.OtherBrute.BonusList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        OtherBonus1Image.Source = this.CreateImage("OtherBruteBonus" + i + ".png");
                        break;
                    case 1:
                        OtherBonus2Image.Source = this.CreateImage("OtherBruteBonus" + i + ".png");
                        break;
                    case 2:
                        OtherBonus3Image.Source = this.CreateImage("OtherBruteBonus" + i + ".png");
                        break;
                }
            }
            GetOpponent.IsEnabled = true;
        }

        protected override void OnClosed(EventArgs e) 
        { 
            this.client.Deconnection();
        }

        private void FightWin_Click(object sender, RoutedEventArgs e)
        {
            this.client.UpdateBrute(this.client.MyBrute.Name, true);
            this.client.GetBrute(this.client.MyBrute.Name);

            BruteTest.StatBruteControl = this.StatBruteMaker(this.client.MyBrute);
        }

        private void FightLose_Click(object sender, RoutedEventArgs e)
        {
            this.client.UpdateBrute(this.client.OtherBrute.Name, true);
            this.client.GetBrute(this.client.OtherBrute.Name);

            BruteTestOther.StatBruteControl = this.StatBruteMaker(this.client.OtherBrute);
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

        private ImageBrush BitmapToBrush(BitmapImage _image)
        {
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = _image;
            myBrush.ImageSource = image.Source;
            return myBrush;
        }

        private List<string> StatBruteMaker(Brute brute)
        {
            List<String> tmp = new List<String>();
            tmp.Add(Convert.ToString(brute.Level));
            tmp.Add(Convert.ToString(brute.Life));
            tmp.Add(Convert.ToString(brute.Strength));
            tmp.Add(Convert.ToString(brute.Agility));
            tmp.Add(Convert.ToString(brute.Speed));
            return tmp;
        }

        #endregion Methods
    }
}
