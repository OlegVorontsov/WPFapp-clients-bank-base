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

            fileOperator.AccountWasOpen += makeNote;
        }

        /// <summary>
        /// Чтение файла журнала
        /// </summary>
        private void readFile()
        {
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
        private void makeNote(Account AccountOpened, string Post)
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
            MessageBox.Show("Счет открыт");
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
