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
            string Day;
            string Tickets = "";
            string Money = "";
            List<string> dayOptions = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            //DateTime today;
            using (DataTable dt = new DataTable("sales"))
            {

                dt.Columns.Add("Day", typeof(string));
                dt.Columns.Add("Tickets", typeof(int));
                dt.Columns.Add("Money", typeof(string));

                //today = DateTime.Now;
                //testing input
                
                Console.WriteLine("\nPlease select the day.");
                for (int i = 0; i < dayOptions.Count; i++) {
                    Console.WriteLine($"[{i+1}] {dayOptions[i]}");
                }
                Day = dayOptions[Program.ChoiceInput(1, 7)-1];
                
                
                //testing input
                int testcount = 0;
                int hulpTickets;
                int hulpMoney;
                while (testcount == 0)
                {
                    Console.WriteLine("How many tickets were sold?");
                    Tickets = Console.ReadLine();

                    try
                    {
                        hulpTickets = int.Parse(Tickets);
                        testcount = 1;
                        //Console.WriteLine("good");
                    }

                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input, try again!");
                    }
                }
                //testing input
                while (testcount == 1)
                {
                    Console.WriteLine("How much money is made?");
                    Money = Console.ReadLine();

                    try
                    {
                        hulpMoney = int.Parse(Money);
                        testcount = 2;
                        //Console.WriteLine("good");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input, try again!");
                    }
                
                    
                }



                
                using (StreamWriter sw = File.AppendText("SaleDataInput1.csv"))
                {
                    sw.WriteLine(Day + ";" + Tickets + ";" + Money);

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
                Console.WriteLine("Day = {0}\t \t Amount of tickets = {1}\t \t Money made = {2} euro", dr["Day"], dr["Tickets"], dr["Money"]);
            }
        }
    }
}
