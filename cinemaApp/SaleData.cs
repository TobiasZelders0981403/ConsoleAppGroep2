using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace cinemaApp
{
    class SaleData
    {
        public static void SaleDatainput()
        {
            int Day;
            int Tickets;
            double Money;
            List<string> dayOptions = new List<string>() { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
            using (DataTable dt = new DataTable("sales"))
            {
                List<List<string>> saleData = new List<List<string>>();

                dt.Columns.Add("Day", typeof(string));
                dt.Columns.Add("Tickets", typeof(int));
                dt.Columns.Add("Money", typeof(string));

                using (var reader = new StreamReader(@"SaleDataInput1.csv")) {
                    while (!reader.EndOfStream) {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        saleData.Add(values.ToList<string>());
                    }
                }

                Console.WriteLine("\nPlease select the day.");
                for (int i = 0; i < dayOptions.Count; i++) {
                    Console.WriteLine($"[{i}] {dayOptions[i]}");
                }
                Day = Program.ChoiceInput(0, 6);

                Console.WriteLine("How many tickets were sold?");
                int maxInt = int.MaxValue;
                Tickets = Program.ChoiceInput(0, maxInt);

                Console.WriteLine("How much money is made?");
                double maxDouble = double.MaxValue;
                Money = Program.DoubleInput(0, maxDouble);

                //add values to array
                int tickets;
                double money;
                int.TryParse(saleData[Day][1], out tickets);
                double.TryParse(saleData[Day][2], out money);
                tickets += Tickets;
                money += Money;
                saleData[Day][1] = tickets.ToString();
                saleData[Day][2] = money.ToString();

                string csv = "";
                for (int i =0; i < saleData.Count; i++) {
                    string s = String.Join(";", saleData[i]) + "\n"; 
                    csv += s;
                }

                using (var writer = new StreamWriter(@"SaleDataInput1.csv")) {
                    writer.Write(csv);
                }
            }
        }

        public static void ShowSaleData() {
            //DateTime today;
            DataTable dt = new DataTable("sales");
            dt.Columns.Add("Day", typeof(string));
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
                Console.WriteLine("Day = {0}\t Amount of tickets = {1}\t Money made = {2} euro", dr["Day"], dr["Tickets"], dr["Money"]);
            }
        }

        public static void AddData(string DayName, double TicketPrice) {
            int Day;
            int Tickets;
            double Money;
            List<string> dayOptions = new List<string>() { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
            using (DataTable dt = new DataTable("sales")) {
                List<List<string>> saleData = new List<List<string>>();

                dt.Columns.Add("Day", typeof(string));
                dt.Columns.Add("Tickets", typeof(int));
                dt.Columns.Add("Money", typeof(string));

                using (var reader = new StreamReader(@"SaleDataInput1.csv")) {
                    while (!reader.EndOfStream) {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        saleData.Add(values.ToList<string>());
                    }
                }

                Day = dayOptions.IndexOf(DayName);

                Tickets = 1;

                Money = TicketPrice;

                //add values to array
                int tickets;
                double money;
                int.TryParse(saleData[Day][1], out tickets);
                double.TryParse(saleData[Day][2], out money);
                tickets += Tickets;
                money += Money;
                saleData[Day][1] = tickets.ToString();
                saleData[Day][2] = money.ToString();

                string csv = "";
                for (int i = 0; i < saleData.Count; i++) {
                    string s = String.Join(";", saleData[i]) + "\n";
                    csv += s;
                }

                using (var writer = new StreamWriter(@"SaleDataInput1.csv")) {
                    writer.Write(csv);
                }
            }
        }
    }
}
