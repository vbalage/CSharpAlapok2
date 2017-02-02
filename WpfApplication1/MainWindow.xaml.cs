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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly uint[] thicknesses = { 50, 100 };

        public MainWindow()
        {
            DataContext = new ViewModel();
            InitializeComponent();
        }

        private void ell2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ell2.Margin.Bottom == thicknesses[0])
                ell2.Margin = new Thickness(thicknesses[1]);
            else
                ell2.Margin = new Thickness(thicknesses[0]);
        }

        private void ell1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ell1.Margin.Bottom == thicknesses[0])
                ell1.Margin = new Thickness(thicknesses[1]);
            else
                ell1.Margin = new Thickness(thicknesses[0]);
        }
    }
}
