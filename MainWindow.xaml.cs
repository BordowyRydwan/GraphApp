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
using GraphApp.ViewModels;
using GraphApp.UtilClasses;

namespace GraphApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        bool isTextMode = false;
        bool isGraphicalMode = true;

        SolidColorBrush activeButtonColor = RGBConverter.RGBStringToColorBrush("#159072");
        SolidColorBrush toPressButtonColor = RGBConverter.RGBStringToColorBrush("#5E807F");
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new GraphicalViewModel();
        }

        private void TextModeToggle_Click(object sender, RoutedEventArgs e)
        {
            if (isTextMode)
            {
                return;
            }

            isTextMode = true;
            isGraphicalMode = false;

            GraphicsModeToggle.Background = toPressButtonColor;
            TextModeToggle.Background = activeButtonColor;

            DataContext = new TextViewModel();
        }

        private void GraphicsModeToggle_Click(object sender, RoutedEventArgs e)
        {
            if (isGraphicalMode)
            {
                return;
            }

            isTextMode = false;
            isGraphicalMode = true;

            GraphicsModeToggle.Background = activeButtonColor;
            TextModeToggle.Background = toPressButtonColor;

            DataContext = new GraphicalViewModel();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            isTextMode = false;
            isGraphicalMode = false;

            GraphicsModeToggle.Background = toPressButtonColor;
            TextModeToggle.Background = toPressButtonColor;

            DataContext = new HelpViewModel();
        }
    }
}
