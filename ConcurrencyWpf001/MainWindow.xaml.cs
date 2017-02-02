using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ConcurrencyWpf001
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

        private Task<string> GetMySuperContent(int i)
        {
            
            switch (i)
            {
                case 1:
                    return Task.Run(() =>
                    {
                        // shit code http://blog.stephencleary.com/2013/11/taskrun-etiquette-examples-dont-use.html
                        Thread.Sleep(2000);
                        return "task scheduler";
                    });
                case 2:
                    return Task.Run(() =>
                    {
                        Thread.Sleep(2000);
                        return "async";
                    });
                case 3:
                    return GetMyEvenMoreSuperContent();
            }
            return Task.FromResult("default");
        }

        private async Task<string> GetMyEvenMoreSuperContent()
        {
            await Task.Delay(2000);
            return "Superb content";
        }

        public async void getMyBestEverContent()
        {
            await Task.Delay(4000);
            tbContent.Text = "mi a banat ez??";
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => GetMySuperContent(1))
                .ContinueWith((completedTask) =>
                {
                    tbContent.Text = completedTask.Result;
                }, TaskScheduler.FromCurrentSynchronizationContext());            
        }

        private async void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            string s = await Task.Run((() => GetMySuperContent(2)));
            tbContent.Text = s;
        }

        private async void ButtonBase3_OnClick(object sender, RoutedEventArgs e)
        {
            string s = await GetMySuperContent(3);
            tbContent.Text = s;
        }

        private async void ButtonBase4_OnClick(object sender, RoutedEventArgs e)
        {
            getMyBestEverContent();
            MessageBox.Show("Done");
        }

    }
}
