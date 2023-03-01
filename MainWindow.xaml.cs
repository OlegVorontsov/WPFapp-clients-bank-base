using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
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
        static int clientSelectedId;
        static Account accountSelected;
        static Account accountSource;
        static Account accountDestination;

        public MainWindow()
        {
            InitializeComponent();

            data = Repository.CreateRepository(Path);
            clientsList.ItemsSource = data.Clients;
        }

        private void clientsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clientSelectedId = (clientsList.SelectedItem as Client).clientId;
            listAccounts.ItemsSource = data.ClientsAccounts.Where(find);
        }

        private void listAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            accountSelected = listAccounts.SelectedItem as Account;
        }

        private bool find(Account arg)
        {
            return arg.clientId == (clientsList.SelectedItem as Client).clientId;
        }
        //открытие депозитного счета
        private void openDepositBtn_Click(object sender, RoutedEventArgs e)
        {
            var temp = openDepositBtn.Content;
            string TypeAccount = Convert.ToString(temp);
            string Result = data.openAccount(clientSelectedId, TypeAccount);
            resultOfOperation.Text = Result;
        }
        //открытие текущего счета
        private void openCurrentBtn_Click(object sender, RoutedEventArgs e)
        {
            var temp = openCurrentBtn.Content;
            string TypeAccount = Convert.ToString(temp);
            string Result = data.openAccount(clientSelectedId, TypeAccount);
            resultOfOperation.Text = Result;
        }
        //закрытие счета
        private void closeAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            string Result = data.closeAccount(accountSelected);
            resultOfOperation.Text = Result;
        }
        //выбор счетов для перевода
        private void acceptAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            if (accountSource == null)
            {
                accountSource = accountSelected;
                accountIdToGetSum.Text = accountSource.accountId.ToString();
            }
            else
            {
                accountDestination = accountSelected;
                accountIdToPutSum.Text = accountDestination.accountId.ToString();
            }
        }
        //очистка выбранных счетов для перевода
        private void clearAccountsBtn_Click(object sender, RoutedEventArgs e)
        {
            accountSource = null;
            accountDestination = null;
            accountIdToGetSum.Text = null;
            accountIdToPutSum.Text = null;
        }
        //перевод между счетами
        private void transferSumBtn_Click(object sender, RoutedEventArgs e)
        {
            string Result = string.Empty;
            double Sum;
            if (double.TryParse(sumToTransfer.Text, out Sum))
            {
                if (Sum > 0)
                {
                    data.transferSumBetweenAccounts(accountSource, accountDestination, Sum);
                    Result = "Деньги переведены";
                    resultOfOperation.Text = Result;
                }
                else
                {
                    Result = "Введите число > 0";
                    resultOfOperation.Text = Result;
                }
            }
            else
            {
                Result = "Введите число";
                resultOfOperation.Text = Result;
            }
        }
        //пополнение счета
        private void additionAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            string Result = string.Empty;
            double Sum;
            if (double.TryParse(sumToAddition.Text, out Sum))
            {
                if (Sum > 0)
                {
                    Result = data.additionAccount(accountSelected, Sum);
                    resultOfOperation.Text = Result;
                }
                else
                {
                    Result = "Введите число > 0";
                    resultOfOperation.Text = Result;
                }
            }
            else
            {
                Result = "Введите число";
                resultOfOperation.Text = Result;
            }
        }


        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            listAccounts.Items.Refresh();
        }


    }
}
