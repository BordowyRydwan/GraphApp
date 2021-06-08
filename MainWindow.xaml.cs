using System;
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
using System.Text.RegularExpressions;

namespace GraphApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        bool isTextMode = false;
        bool isAddMenuExpanded = false;

        SolidColorBrush activeButtonColor = RGBStringToColorBrush("#159072");
        SolidColorBrush toPressButtonColor = RGBStringToColorBrush("#5E807F");
        public MainWindow()
        {
            InitializeComponent();
        }

        private static SolidColorBrush RGBStringToColorBrush(string rgb)
        {
            const int RGB_LENGTH_WITH_HASH = 7;
            const int RGB_LENGTH_WOUT_HASH = 6;

            if (rgb.Length is not (RGB_LENGTH_WOUT_HASH or RGB_LENGTH_WITH_HASH))
            {
                throw new Exception("RGB color should be of pattern #XXXXXX or XXXXXX");
            }

            string pattern = @"[^#A-F0-9]";
            Regex regex = new Regex(pattern);

            if (regex.IsMatch(rgb))
            {
                throw new Exception("Non-hex values passed into RGB string");
            }

            if(rgb.Length == RGB_LENGTH_WITH_HASH)
            {
                rgb = rgb.Substring(1);
            }

            byte R = (byte)(Convert.ToInt32(rgb[0].ToString(), 16) * 16 + Convert.ToInt32(rgb[1].ToString(), 16));
            byte G = (byte)(Convert.ToInt32(rgb[2].ToString(), 16) * 16 + Convert.ToInt32(rgb[3].ToString(), 16));
            byte B = (byte)(Convert.ToInt32(rgb[4].ToString(), 16) * 16 + Convert.ToInt32(rgb[5].ToString(), 16));

            return new SolidColorBrush(Color.FromRgb(R, G, B));
        }

        private static SolidColorBrush RGBValuesToColorBrush(int R, int G, int B)
        {
            return new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
        }

        private void AddMenuShow()
        {
            AddEdgeButton.Visibility = Visibility.Visible;
            AddVertexButton.Visibility = Visibility.Visible;

            isAddMenuExpanded = true;
        }
        private void AddMenuHide()
        {
            AddEdgeButton.Visibility = Visibility.Hidden;
            AddVertexButton.Visibility = Visibility.Hidden;

            isAddMenuExpanded = false;
        }
        private void TextModeToggle_Click(object sender, RoutedEventArgs e)
        {
            if (isTextMode)
            {
                return;
            }

            isTextMode = true;

            GraphicsModeToggle.Background = toPressButtonColor;
            TextModeToggle.Background = activeButtonColor;
        }

        private void GraphicsModeToggle_Click(object sender, RoutedEventArgs e)
        {
            if (!isTextMode)
            {
                return;
            }

            isTextMode = false;

            GraphicsModeToggle.Background = activeButtonColor;
            TextModeToggle.Background = toPressButtonColor;
        }

        private void AddMenuToggle_Click(object sender, RoutedEventArgs e)
        {
            if (isAddMenuExpanded)
            {
                AddMenuHide();
            }
            else
            {
                AddMenuShow();
            }
        }

        private void AddVertexButton_Click(object sender, RoutedEventArgs e)
        {
            AddMenuHide();
        }

        private void AddEdgeButton_Click(object sender, RoutedEventArgs e)
        {
            AddMenuHide();
        }

        private void RunAlgorithmButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
