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
        static string PathBase = "База.txt";
        static string PathJournal = "Журнал.txt";
        static Repository data;
        static Journal log;
        static string postSelected = string.Empty;
        static int clientSelectedId;
        static Account accountSelected;
        static Account accountSource;
        static Account accountDestination;

        public MainWindow()
        {
            InitializeComponent();
            postSelector.ItemsSource = new string[] { "Консультант", "Менеджер" };

            data = Repository.CreateRepository(PathBase, PathJournal);
            clientsList.ItemsSource = data.Clients;

            log = Repository.CreateJournal();
            operationsList.ItemsSource = log.Lines;
        }

        private void postSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            postSelected = postSelector.SelectedValue.ToString();
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
            if (postSelected != string.Empty)
            {
                var temp = openDepositBtn.Content;
                string TypeAccount = Convert.ToString(temp);
                resultOfOperation.Text = data.openAccount(clientSelectedId, TypeAccount, postSelected);
            }
            else
            {
                resultOfOperation.Text = "Выберете должность";
            }
        }
        //открытие текущего счета
        private void openCurrentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (postSelected != string.Empty)
            {
                var temp = openCurrentBtn.Content;
                string TypeAccount = Convert.ToString(temp);
                resultOfOperation.Text = data.openAccount(clientSelectedId, TypeAccount, postSelected);
            }
            else
            {
                resultOfOperation.Text = "Выберете должность";
            }
        }
        //закрытие счета
        private void closeAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            if (postSelected != string.Empty)
            {
                string Result = data.closeAccount(accountSelected);
                resultOfOperation.Text = Result;
            }
            else
            {
                resultOfOperation.Text = "Выберете должность";
            }
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
            if (postSelected != string.Empty)
            {
                double Sum;
                if (double.TryParse(sumToTransfer.Text, out Sum))
                {
                    if (Sum > 0)
                    {
                        resultOfOperation.Text = data.transferSumBetweenAccounts(accountSource, accountDestination, Sum);
                    }
                    else
                    {
                        resultOfOperation.Text = "Введите число > 0";
                    }
                }
                else
                {
                    resultOfOperation.Text = "Введите число";
                }
            }
            else
            {
                resultOfOperation.Text = "Выберете должность";
            }
        }
        //пополнение счета
        private void additionAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            if (postSelected != string.Empty)
            {
                double Sum;
                if (double.TryParse(sumToAddition.Text, out Sum))
                {
                    if (Sum > 0)
                    {
                        resultOfOperation.Text = data.additionAccount(accountSelected, Sum);
                    }
                    else
                    {
                        resultOfOperation.Text = "Введите число > 0";
                    }
                }
                else
                {
                    resultOfOperation.Text = "Введите число";
                }
            }
            else
            {
                resultOfOperation.Text = "Выберете должность";
            }
        }


        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            operationsList.Items.Refresh();
        }
    }
}
