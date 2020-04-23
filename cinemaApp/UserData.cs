using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace cinemaApp
{
    class UserData
    {
        public static void UserDataInput()
        {
            string Date;
            string Tickets;
            string Money;
            //DateTime today;
            using (DataTable dt = new DataTable("sales"))
            {

                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("Tickets", typeof(int));
                dt.Columns.Add("Money", typeof(string));

                //today = DateTime.Now;
                Console.WriteLine("What is the Date?");
                Date = Console.ReadLine();

                Console.WriteLine("How many tickets were sold?");
                Tickets = Console.ReadLine();

                Console.WriteLine("How much money is made?");
                Money = Console.ReadLine();



                using (StreamWriter sw = File.AppendText(@"SaleDataInput1.csv"))
                {
                    sw.WriteLine(Date + ";" + Tickets + ";" + Money);

                }
            }
        }

        public static void ShowSaleData() {
            //DateTime today;
            DataTable dt = new DataTable("sales");
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Tickets", typeof(int));
            dt.Columns.Add("Money", typeof(string));

            using (var reader = new StreamReader(@"SaleDataInput1.csv")) {
                while (!reader.EndOfStream) {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    dt.Rows.Add(new object[] { values[0], values[1], values[2] });
                }
            }
                foreach (DataRow dr in dt.Rows) {
                Console.WriteLine("Date = {0}\t \t Amount of tickets = {1}\t \t Money made = {2} euro", dr["Date"], dr["Tickets"], dr["Money"]);
            }
        }
    }
}
