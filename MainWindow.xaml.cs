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

namespace _12._5_HomeWork_WPFapp_clients_bank_base
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string Path = "База.txt";
        static Repository data;
        public MainWindow()
        {
            InitializeComponent();

            data = Repository.CreateRepository(Path);

            clientsList.ItemsSource = data.Clients;
        }

        private void btnRef(object sender, RoutedEventArgs e)
        {

        }

        private void clientsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listAccounts.ItemsSource = data.Accounts.Where(find);
        }

        private bool find(Account arg)
        {
            return arg.ClientId == (clientsList.SelectedItem as Client).clientId;
        }
    }
}
