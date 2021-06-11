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

namespace GraphApp.Views
{
    /// <summary>
    /// Logika interakcji dla klasy GraphicalModeView.xaml
    /// </summary>
    public partial class GraphicalModeView : UserControl
    {
        bool isAddMenuExpanded = false;

        public GraphicalModeView()
        {
            InitializeComponent();
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
