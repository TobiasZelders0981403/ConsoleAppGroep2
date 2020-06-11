using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cinemaApp
{
    public class Food
    {

        public static int LastID = 0;
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string subCategory { get; set; }
        public string size { get; set; }
        public double price { get; set; }


        public Food(string Snackname, string cat, string subCat, string Size, double price)
        {
            this.id = LastID;
            this.name = Snackname;
            this.category = cat;
            this.subCategory = subCat;
            this.size = Size;
            this.price = price;
            LastID = id + 1;
        }

        public static double convertPrice(string p)
        {
            double priceDouble = 0;
            try
            {
                priceDouble = Convert.ToDouble(p);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input");
                Console.WriteLine("What price?");
                string priceStr = Console.ReadLine();
                convertPrice(priceStr);
            }

            while (priceDouble < 0)
            {
                Console.WriteLine("Price can't be negative?");
                Console.WriteLine("What price?");
                string priceStr = Console.ReadLine();
                priceDouble = Convert.ToDouble(priceStr);
            }

            return priceDouble;
        }

        public static string getCategory()
        {
            Console.WriteLine("\nThe category:\n1. Snack\n2. Drink");
            //string category;
            string choice = Console.ReadLine();
            int ch = 0;
            //int ch = Console.Read();
            int.TryParse(choice, out ch);
            if (ch == 1)
            {
                return "Snack";
            }
            else if (ch == 2)
            {
                return "Drink";
            }
            else
            {
                Console.WriteLine("Choose between 1 and 2.");
                return getCategory();
            }
        }

        public static string getSubCategory(string cat)
        {
            string[] snacksSubs = { "Popcorn", "Sweets", "Hot Dog", "Nachos", "Pizza" };
            string[] drinkSubs = { "Water", "Soda", "Juice", "Milkshake", "Slushie" };
            if (cat == "Snack")
            {
                int c = 1;
                Console.WriteLine("\nThe sub-category:");
                for (int i = 0; i < snacksSubs.Length; i++)
                {
                    Console.WriteLine(c + ". " + snacksSubs[i]);
                    c++;
                }
                string choice = Console.ReadLine();
                int ch = 0;
                bool convert = int.TryParse(choice, out ch);

                if (convert && ch - 1 >= 0 && ch - 1 < snacksSubs.Length)
                {
                    return snacksSubs[ch - 1];
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    return getSubCategory(cat);
                }
            }

            else if (cat == "Drink")
            {
                int c = 1;
                Console.WriteLine("The sub-category:");
                for (int i = 0; i < drinkSubs.Length; i++)
                {
                    Console.WriteLine(c + ". " + drinkSubs[i]);
                    c++;
                }
                string choice = Console.ReadLine();
                int ch = 0;
                bool convert = int.TryParse(choice, out ch);

                if (convert && ch - 1 >= 0 && ch - 1 < drinkSubs.Length)
                {
                    return drinkSubs[ch - 1];
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    return getSubCategory(cat);
                }

            }
            return "";
        }

        public static string getSize()
        {
            Console.WriteLine("The size:\n(S, M, L and XL) or press enter to leave empty");
            string[] sizes = { "S", "M", "L", "XL", "", " " };
            string size = Console.ReadLine();
            if (sizes.Contains(size))
            {
                return size;
            }

            else
            {
                Console.WriteLine("Invalid input");
                return getSize();
            }

        }

        public void display()
        {
            Console.WriteLine("-----");
            Console.Write("Item Id: " + this.id + " || " + "Name: " + this.name + " || ");
            Console.Write("Category: " + this.category + " || " + "Sub-category: " + this.subCategory + " || ");
            if (this.size == "" || this.size == " ")
            {
                Console.Write("Price: " + this.price);
            }
            else
            {
                Console.Write("Size: " + this.size+ " || " + "Price: " + this.price);
            }
            Console.WriteLine("\n-----");
        }

        public void displayCostumer()
        {
            Console.WriteLine("-----");
            Console.Write("Item Id: " + this.id + " || " + "Name: " + this.name + " || ");
            Console.Write("Category: " + this.subCategory + " || ");
            if (this.size == "" || this.size == " ")
            {
                Console.Write("Price: " + this.price);
            }
            else
            {
                Console.Write("Size: " + this.size + " || " + "Price: " + this.price);
            }
            Console.WriteLine("\n-----");
        }





    }
}
