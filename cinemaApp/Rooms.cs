using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// The code i made for the caterer and the seat prices
// Three movie rooms 150 seats, 300 seats and 500 seats
// Room 1 : 10 rows of 15 seats
// Room 2 : 15 rows of 20 seats
// Rooom 3 : 25 rows of 25 seats

namespace CinemaApp
{

    class Rooms
    {
        public double[][] seatPrices { get; set; }


        public Rooms(int rows, int seats)
        {
            double[][] jaggedArray = new double[rows][];
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                jaggedArray[i] = new double[seats];
            }
            this.seatPrices = jaggedArray;
        }

        public Rooms(double[][] arr)
        {
            this.seatPrices = arr;
        }

        public Rooms(Rooms r)
        {
            this.seatPrices = r.seatPrices;
        }

        public void overview()
        {
            Console.WriteLine();
            for (int j = 0; j < seatPrices.Length; j++)
            {
                for (int i = 0; i < seatPrices[0].Length; i++)
                {
                    int currentRow = j + 1;
                    int currentSeat = i + 1;
                    Console.Write("R-" + currentRow + " S-" + currentSeat + " = " + seatPrices[j][i] + " || ");
                }
                Console.WriteLine("\n");
            }
        }

        public void overviewRow()
        {
            Console.WriteLine("Which row?");
            string row = Console.ReadLine();
            bool b = Int32.TryParse(row, out int j);
            if (b)
            {
                for (int i = 0; i < seatPrices[0].Length; i++)
                {
                    int currentRow = j + 1;
                    int currentSeat = i + 1;
                    Console.Write("Row_" + currentRow + " Seat_" + currentSeat + " = " + seatPrices[j][i] + "  ");
                }
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("Row doesn't exist");
            }
        }

        public void setPriceSeat()
        {
            Console.WriteLine("\nWhich row?");
            string userRow = Console.ReadLine();
            var intRow = 0;
            try
            {
                intRow = Int32.Parse(userRow);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input");
                setPriceSeat();
                return;
            }
            while (intRow <= 0 || intRow > seatPrices.Length + 1)
            {
                Console.WriteLine("There are only " + seatPrices.Length + " rows");
                Console.WriteLine("Which row?");
                userRow = Console.ReadLine();
                intRow = Convert.ToInt32(userRow);
            }

            Console.WriteLine("Which seat?");
            string userSeat = Console.ReadLine();
            var intSeat = 0;
            try
            {
                intSeat = Int32.Parse(userSeat);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input");
                setPriceSeat();
                return;
            }
            while (intSeat <= 0 || intSeat > seatPrices[0].Length + 1)
            {
                Console.WriteLine("There are only " + seatPrices[0].Length + " seats");
                Console.WriteLine("Which seat?");
                userSeat = Console.ReadLine();
                intSeat = Convert.ToInt32(userSeat);
            }

            Console.WriteLine("What price?");
            string userPrice = Console.ReadLine();
            var doublePrice = 0.0;
            try
            {
                doublePrice = Convert.ToDouble(userPrice);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input");
                setPriceSeat();
                return;
            }
            while (doublePrice < 0)
            {
                Console.WriteLine("Price can't be negative?");
                Console.WriteLine("What price?");
                userPrice = Console.ReadLine();
                doublePrice = Convert.ToDouble(userPrice);
            }

            for (int j = 0; j < seatPrices.Length; j++)
            {
                for (int i = 0; i < seatPrices[0].Length; i++)
                {
                    seatPrices[intRow - 1][intSeat - 1] = doublePrice;
                }
            }

            Console.WriteLine("You have changed row " + userRow + "  seat " + userSeat + "  price to " + userPrice);
        }

        public void setPriceRow()
        {
            Console.WriteLine("\nWhich row?");
            string userRow = Console.ReadLine();
            var intRow = 0;
            try
            {
                intRow = Int32.Parse(userRow);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input");
                setPriceRow();
                return;
            }
            while (intRow <= 0 || intRow > seatPrices.Length + 1)
            {
                Console.WriteLine("There are only " + seatPrices.Length + " rows");
                Console.WriteLine("Which row?");
                userRow = Console.ReadLine();
                intRow = Convert.ToInt32(userRow);
            }

            Console.WriteLine("What price?");
            string userPrice = Console.ReadLine();
            var doublePrice = 0.0;
            try
            {
                doublePrice = Convert.ToDouble(userPrice);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input");
                setPriceRow();
                return;
            }
            while (doublePrice < 0)
            {
                Console.WriteLine("Price can't be negative?");
                Console.WriteLine("What price?");
                userPrice = Console.ReadLine();
                doublePrice = Convert.ToDouble(userPrice);
            }

            for (int i = 0; i < seatPrices[0].Length; i++)
            {
                seatPrices[intRow - 1][i] = doublePrice;
            }

            Console.WriteLine("You changed row " + userRow + " prices to " + userPrice);

        }

        public void setPriceRoom()
        {
            Console.WriteLine("\nWhat price?");
            string userPrice = Console.ReadLine();
            var doublePrice = 0.0;
            try
            {
                doublePrice = Convert.ToDouble(userPrice);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input");
                setPriceRoom();
                return;
            }

            while (doublePrice < 0)
            {
                Console.WriteLine("Price can't be negative?");
                Console.WriteLine("What price?");
                userPrice = Console.ReadLine();
                doublePrice = Convert.ToDouble(userPrice);
            }

            for (int j = 0; j < seatPrices.Length; j++)
            {
                for (int i = 0; i < seatPrices[0].Length; i++)
                {
                    seatPrices[j][i] = doublePrice;
                }
            }

            Console.WriteLine("You have changed all seat prices in this room to " + userPrice);
        }

        public void setPriceMiddle()
        {
            int midRow = seatPrices.Length / 2;
            int slackRow = seatPrices.Length / 5;
            int midSeat = seatPrices.Length / 2;
            int slackSeat = seatPrices.Length / 6;

            Console.WriteLine("\nWhat price?");
            string userPrice = Console.ReadLine();
            var doublePrice = 0.0;
            try
            {
                doublePrice = Convert.ToDouble(userPrice);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input");
                setPriceMiddle();
                return;
            }
            while (doublePrice < 0)
            {
                Console.WriteLine("Price can't be negative?");
                Console.WriteLine("What price?");
                userPrice = Console.ReadLine();
                doublePrice = Convert.ToDouble(userPrice);
            }

            for (int i = midRow - slackRow; i <= midRow + slackRow; i++)
            {
                for (int j = midSeat - slackSeat; j <= midSeat + slackSeat; j++)
                {
                    seatPrices[i][j] = doublePrice;
                }
            }

            Console.WriteLine("You have changed middle seat prices to " + userPrice);
        }

        static void displayOptions(int rows, int seats)
        {
            Console.WriteLine("\nThis room has " + rows + " rows with " + seats + " seats");
            Console.WriteLine("what would you like to do.\nYou can: \n" +
                "1. See an overview of all seat prices\n" +
                "2. See an overview of prices of a row\n"+
                "3. Set price individual seat\n" +
                "4. Set price for a whole row\n" +
                "5. Set price for the whole room\n" +
                "6. Set price for the middle seats\n" +
                "Press q to exit");
        }

        static void roomOne()
        {

            string fileName = "roomOne.json";
            string rawJson = File.ReadAllText(fileName);
            double[][] jsonArray = JsonConvert.DeserializeObject<double[][]>(rawJson);
            
            var room1 = new Rooms(jsonArray);
            bool busy = true;

            while (busy)
            {
                Console.WriteLine("\nRoom 1");
                displayOptions(room1.seatPrices.Length, room1.seatPrices[0].Length);

                var operation = Console.ReadKey().Key;

                if (operation == ConsoleKey.Q)
                {
                    busy = false;
                    break;
                }
                else if (operation == ConsoleKey.D1)
                {
                    room1.overview();
                }
                else if (operation == ConsoleKey.D2)
                {
                    room1.overviewRow();
                }
                else if (operation == ConsoleKey.D3)
                {
                    room1.setPriceSeat();
                }
                else if (operation == ConsoleKey.D4)
                {
                    room1.setPriceRow();
                }
                else if (operation == ConsoleKey.D5)
                {
                    room1.setPriceRoom();
                }
                else if (operation == ConsoleKey.D6)
                {
                    room1.setPriceMiddle();
                }

            }

            string newJson = JsonConvert.SerializeObject(room1.seatPrices);
            File.WriteAllText("roomOne.json", newJson);
        }

        static void roomTwo()
        {
            string fileName = "roomTwo.json";
            string rawJson = File.ReadAllText(fileName);
            double[][] jsonArray = JsonConvert.DeserializeObject<double[][]>(rawJson);

            var room2 = new Rooms(jsonArray);
            bool busy = true;

            while (busy)
            {
                Console.WriteLine("\nRoom 2");
                displayOptions(room2.seatPrices.Length, room2.seatPrices[0].Length);

                var operation = Console.ReadKey().Key;

                if (operation == ConsoleKey.Q)
                {
                    busy = false;
                    break;
                }
                else if (operation == ConsoleKey.D1)
                {
                    room2.overview();
                }
                else if (operation == ConsoleKey.D2)
                {
                    room2.overviewRow();
                }
                else if (operation == ConsoleKey.D3)
                {
                    room2.setPriceSeat();
                }
                else if (operation == ConsoleKey.D4)
                {
                    room2.setPriceRow();
                }
                else if (operation == ConsoleKey.D5)
                {
                    room2.setPriceRoom();
                }
                else if (operation == ConsoleKey.D6)
                {
                    room2.setPriceMiddle();
                }

            }
            string newJson = JsonConvert.SerializeObject(room2.seatPrices);
            File.WriteAllText("roomTwo.json", newJson);
        }

        static void roomThree()
        {
            string fileName = "roomThree.json";
            string rawJson = File.ReadAllText(fileName);
            double[][] jsonArray = JsonConvert.DeserializeObject<double[][]>(rawJson);

            var room3 = new Rooms(jsonArray);

            bool busy = true;

            while (busy)
            {
                Console.WriteLine("\nRoom 3");
                displayOptions(room3.seatPrices.Length, room3.seatPrices[0].Length);

                var operation = Console.ReadKey().Key;

                if (operation == ConsoleKey.Q)
                {
                    busy = false;
                    break;
                }
                else if (operation == ConsoleKey.D1)
                {
                    room3.overview();
                }
                else if (operation == ConsoleKey.D2)
                {
                    room3.overviewRow();
                }
                else if (operation == ConsoleKey.D3)
                {
                    room3.setPriceSeat();
                }
                else if (operation == ConsoleKey.D4)
                {
                    room3.setPriceRow();
                }
                else if (operation == ConsoleKey.D5)
                {
                    room3.setPriceRoom();
                }
                else if (operation == ConsoleKey.D6)
                {
                    room3.setPriceMiddle();
                }

            }
            string newJson = JsonConvert.SerializeObject(room3.seatPrices);
            File.WriteAllText("roomThree.json", newJson);
        }

        public static void Manager()
        {
            bool busy = true;
            while (busy)
            {
                Console.WriteLine("\nWhich rooms would you like to acess: \nRoom 1\nRoom 2\nRoom 3");
                Console.WriteLine("Type in 1, 2 or 3");
                Console.WriteLine("Press q to exit...");
                var roomChoice = Console.ReadKey().Key;

                if (roomChoice == ConsoleKey.Q)
                {
                    busy = false;
                    break;
                }
                else if (roomChoice == ConsoleKey.D1)
                {
                    roomOne();
                }
                else if (roomChoice == ConsoleKey.D2)
                {
                    roomTwo();
                }
                else if (roomChoice == ConsoleKey.D3)
                {
                    roomThree();
                }
                else
                {
                    Console.WriteLine("Please type choose between 1, 2 or 3");
                }

            }
        }



    }

    /*
    public class Test
    {
        public static void Main(string[] args)
        {

            bool busy = true;
            while (busy)
            {
                Console.WriteLine("\n1.Rooms\n2.Food menu\n3.Orders Caterer\n4.Order costumer");
                var doChoice = Console.ReadKey().Key;
                if (doChoice == ConsoleKey.D1)
                {
                    Rooms.Manager();
                }

                else if (doChoice == ConsoleKey.D2)
                {
                    Food.Caterer();
                }
                else if (doChoice == ConsoleKey.D3)
                {
                    FoodOrder.Cater();
                }
                else if (doChoice == ConsoleKey.D4)
                {
                    FoodOrder.Costumer();
                }
                else
                {
                    busy = false;
                }
            }
        }
    }*/
}
