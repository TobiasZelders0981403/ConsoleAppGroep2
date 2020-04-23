using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cinemaApp
{
    class expected
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader(@"C:\Users\admin\Documents\school\project b\schetsen\SaleDataInput1.csv"))
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
                Console.WriteLine("The expected amount of customers: " + expected);

            }
        }
    }
}
