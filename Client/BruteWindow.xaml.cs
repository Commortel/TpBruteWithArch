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

namespace Client
{
    /// <summary>
    /// Logique d'interaction pour BruteWindow.xaml
    /// </summary>
    public partial class BruteWindow : UserControl
    {
        public BruteWindow()
        {
            InitializeComponent();
        }

        private void Initialize()
        {
            LifeImage.Source = this.CreateImage("Resources/life.gif");
            StrengthImage.Source = this.CreateImage("Resources/strength.gif");
            AgilityImage.Source = this.CreateImage("Resources/agility.gif");
            SpeedImage.Source = this.CreateImage("Resources/speed.gif");
        }

        public string NameTitleControl
        {
            get { return NameTitle.Text; }
            set { NameTitle.Text = value; }
        }

        public string BruteImageControl
        {
            set { BruteImage.Source = this.CreateImage(value); }
        }

        public List<String> StatBruteControl
        {
            get 
            { 
                List<String> tmp = new List<string>();
                tmp.Add(TextLevel.Text);
                tmp.Add(TextLife.Text);
                tmp.Add(TextStrength.Text);
                tmp.Add(TextAgility.Text);
                tmp.Add(TextSpeed.Text);
                return tmp;
            }
            set 
            {
                TextLevel.Text = value[0];
                TextLife.Text = value[1];
                TextStrength.Text = value[2];
                TextAgility.Text = value[3];
                TextSpeed.Text = value[4]; 
            }
        }

        private BitmapImage CreateImage(string path)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            image.UriSource = new Uri(@"" + path, UriKind.Relative);
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
    }
}
