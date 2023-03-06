using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _12._5_HomeWork_WPFapp_clients_bank_base
{
    class Journal
    {
        private static string path;
        private static FileOperator fileOper;
        public List<Line> Lines = new List<Line>();

        public Journal(string Path, FileOperator fileOperator)
        {
            path = Path;
            fileOper = fileOperator;
            readFile();

            fileOperator.AccountWasOpen += makeNoteOpen;
            fileOperator.AccountWasClosed += makeNoteClose;
            fileOperator.AccountWasRefilled += makeNoteRefill;
            fileOperator.TransferWasDone += makeNoteTransfer;
        }

        /// <summary>
        /// Чтение файла журнала
        /// </summary>
        private void readFile()
        {
            Lines.Clear();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');
                    Line newLine = new Line(double.Parse(args[0]), args[1], args[2], args[3], args[4]);
                    Lines.Add(newLine);
                }
            }
        }

        /// <summary>
        /// Запись в журнале об открытии счета
        /// </summary>
        /// <param name="AccountOpened"></param>
        private void makeNoteOpen(Account AccountOpened, string Post)
        {
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                string line = string.Empty;
                string nowDate = DateTime.Now.ToShortDateString();
                string nowTime = DateTime.Now.ToShortTimeString();
                string dateAndTime = $"{nowDate} {nowTime}";
                line = $"{AccountOpened.accountId}#" +
                        "открытие#" +
                        "0#" +
                       $"{dateAndTime}#" +
                       $"{Post}";
                sw.WriteLine(line);
            }
            MessageBox.Show($"Счет {AccountOpened.accountId} открыт");
            readFile();
        }

        /// <summary>
        /// Запись в журнале о закрытии счета
        /// </summary>
        /// <param name="AccountClosed"></param>
        /// <param name="Post"></param>
        private void makeNoteClose(Account AccountClosed, string Post)
        {
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                string line = string.Empty;
                string nowDate = DateTime.Now.ToShortDateString();
                string nowTime = DateTime.Now.ToShortTimeString();
                string dateAndTime = $"{nowDate} {nowTime}";
                line = $"{AccountClosed.accountId}#" +
                        "закрытие#" +
                        "0#" +
                       $"{dateAndTime}#" +
                       $"{Post}";
                sw.WriteLine(line);
            }
            MessageBox.Show($"Счет {AccountClosed.accountId} закрыт");
            readFile();
        }

        /// <summary>
        /// Запись в журнале о пополнении счета
        /// </summary>
        /// <param name="AccountRefilled"></param>
        /// <param name="Sum"></param>
        /// <param name="Post"></param>
        private void makeNoteRefill(Account AccountRefilled, double Sum, string Post)
        {
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                string line = string.Empty;
                string nowDate = DateTime.Now.ToShortDateString();
                string nowTime = DateTime.Now.ToShortTimeString();
                string dateAndTime = $"{nowDate} {nowTime}";
                line = $"{AccountRefilled.accountId}#" +
                        "пополнение#" +
                       $"{Sum}#" +
                       $"{dateAndTime}#" +
                       $"{Post}";
                sw.WriteLine(line);
            }
            MessageBox.Show($"Счет {AccountRefilled.accountId} пополнен на сумму {Sum}");
            readFile();
        }

        /// <summary>
        /// Запись в журнале о переводе между счетами
        /// </summary>
        /// <param name="AccountDecreased"></param>
        /// <param name="AccountIncreased"></param>
        /// <param name="Sum"></param>
        /// <param name="Post"></param>
        private void makeNoteTransfer(Account AccountDecreased, Account AccountIncreased, double Sum, string Post)
        {
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                string lineDecrease = string.Empty;
                string nowDate1 = DateTime.Now.ToShortDateString();
                string nowTime1 = DateTime.Now.ToShortTimeString();
                string dateAndTime1 = $"{nowDate1} {nowTime1}";
                lineDecrease = $"{AccountDecreased.accountId}#" +
                                "перевод#" +
                               $"-{Sum}#" +
                               $"{dateAndTime1}#" +
                               $"{Post}";
                sw.WriteLine(lineDecrease);

                string lineIncrease = string.Empty;
                string nowDate2 = DateTime.Now.ToShortDateString();
                string nowTime2 = DateTime.Now.ToShortTimeString();
                string dateAndTime2 = $"{nowDate2} {nowTime2}";
                lineDecrease = $"{AccountIncreased.accountId}#" +
                                "перевод#" +
                               $"+{Sum}#" +
                               $"{dateAndTime2}#" +
                               $"{Post}";
                sw.WriteLine(lineDecrease); 
            }
            MessageBox.Show($"Со счета {AccountDecreased.accountId} на счет {AccountIncreased.accountId} переведена сумма {Sum}");
            readFile();
        }
    }

    /// <summary>
    /// Класс строки для Журнала операций
    /// </summary>
    class Line
    {
        public double accountId { get; set; }
        public string operationType { get; set; }
        public string operationSum { get; set; }
        public string dateAndTime { get; set; }
        public string post { get; set; }

        public Line(double AccountId, string OperationType, string OperationSum, string DateAndTime, string Post)
        {
            this.accountId = AccountId;
            this.operationType = OperationType;
            this.operationSum = OperationSum;
            this.dateAndTime = DateAndTime;
            this.post = Post;
        }
    }
}