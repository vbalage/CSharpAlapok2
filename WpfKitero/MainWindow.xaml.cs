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

namespace WpfKitero
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task<string> GetMySuperString()
        {
            await Task.Delay(2000);
            return "Akos";
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            tbTest.Text = GetMySuperString().Result; // ez szar. blokkolja a futast.
        }
    }
}
