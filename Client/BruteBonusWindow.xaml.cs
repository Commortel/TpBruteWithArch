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
    /// Logique d'interaction pour BruteBonusWindow.xaml
    /// </summary>
    public partial class BruteBonusWindow : UserControl
    {
        public BruteBonusWindow()
        {
            InitializeComponent();
            this.Initialize();
        }

        private void Initialize()
        {
            MainGrid.Background = this.BitmapToBrush(this.CreateImage("Resources/tableau_bois.png"));
        }

        public List<String> SetBonusImage
        {
            get 
            { 
                List<String> tmp = new List<String>();
                return tmp;
            }
            set 
            {
                for(int i=0; i < value.Count; i++)
                {
                    if(MainGrid.Children[i] is Image)
                        ((Image)MainGrid.Children[i]).Source = this.CreateImage(value[i]);
                }
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
