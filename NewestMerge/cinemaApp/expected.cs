using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cinemaApp
{
    class expected
    {
        public static void ExpectedCustomers()
        {
            using (var reader = new StreamReader(@"SaleDataInput1.csv"))
            {
                var A = 0;
                var B = 0;
                var C = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(";");
                    C = B;
                    B = A;
                    A = Int32.Parse(values[1]);

                }
                var expected = (A + B + C) / 3;
                Console.WriteLine("\nThe expected amount of customers: " + expected);

            }
        }
    }
}
