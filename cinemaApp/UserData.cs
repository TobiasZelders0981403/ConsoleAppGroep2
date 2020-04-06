using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace cinemaApp
{
    class UserData
    {
        public static void UserDataMain()
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

                Console.WriteLine("Do you want to add sales? yes or no");

                string antw;
                antw = Console.ReadLine();
                //today = DateTime.Now;

                if ((antw.ToLower()) == "yes")
                {

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

                Console.WriteLine("\nI will show you the sale data.\n");


                using (var reader = new StreamReader(@"SaleDataInput1.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        dt.Rows.Add(new object[] { values[0], values[1], values[2] });
                    }
                }
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine("Date = {0}\t \t Amount of tickets = {1}\t \t Money made = {2} euro", dr["Date"], dr["Tickets"], dr["Money"]);
                }
            }
            Console.ReadKey();
        }
    }
}
