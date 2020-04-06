﻿using System;
// Three movie rooms 150 seats, 300 seats and 500 seats
// Room 1 : 10 rows of 15 seats
// Room 2 : 15 rows of 20 seats
// Rooom 3 : 25 rows of 25 seats

namespace Cinema_
{
    class Rooms
    {
        public double[,] seatPrices;


        public Rooms(double[,] seats)
        {
            this.seatPrices = seats;

        }

        public void overview()
        {
            Console.WriteLine();
            for (int j = 0; j < seatPrices.GetLength(0); j++)
            {
                for (int i = 0; i < seatPrices.GetLength(1); i++)
                {
                    int currentRow = j + 1;
                    int currentSeat = i + 1;
                    Console.Write("Row_" + currentRow + " Seat_" + currentSeat + " = " + seatPrices[j, i] + "  ");
                }
                Console.WriteLine("\n");
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
            while (intRow <= 0 || intRow > seatPrices.GetLength(0) + 1)
            {
                Console.WriteLine("There are only " + seatPrices.GetLength(0) + " rows");
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
            while (intSeat <= 0 || intSeat > seatPrices.GetLength(1) + 1)
            {
                Console.WriteLine("There are only " + seatPrices.GetLength(1) + " seats");
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

            for (int j = 0; j < seatPrices.GetLength(0); j++)
            {
                for (int i = 0; i < seatPrices.GetLength(1); i++)
                {
                    seatPrices[intRow - 1, intSeat - 1] = doublePrice;
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
            while (intRow <= 0 || intRow > seatPrices.GetLength(0) + 1)
            {
                Console.WriteLine("There are only " + seatPrices.GetLength(0) + " rows");
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

            for (int i = 0; i < seatPrices.GetLength(1); i++)
            {
                seatPrices[intRow - 1, i] = doublePrice;
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

            for (int j = 0; j < seatPrices.GetLength(0); j++)
            {
                for (int i = 0; i < seatPrices.GetLength(1); i++)
                {
                    seatPrices[j, i] = doublePrice;
                }
            }

            Console.WriteLine("You have changed all seat prices in this room to " + userPrice);
        }

        public void setPriceMiddle()
        {
            int midRow = seatPrices.GetLength(0) / 2;
            int slackRow = seatPrices.GetLength(0) / 5;
            int midSeat = seatPrices.GetLength(0) / 2;
            int slackSeat = seatPrices.GetLength(0) / 6;

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
                    seatPrices[i, j] = doublePrice;
                }
            }

            Console.WriteLine("You have changed middle seat prices to " + userPrice);
        }
    }

    class User
    {
        static void Main()
        {
            bool busy = true;

            while (busy)
            {
                Console.WriteLine("Which room would you like to acess: \nRoom 1\nRoom 2\nRoom 3");
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

        static void displayOptions(int rows, int seats)
        {
            Console.WriteLine("\nThis room has " + rows + " rows with " + seats + " seats");
            Console.WriteLine("what would you like to do.\nYou can: \n" +
                "-See an overview of all seat prices(1)\n" +
                "-Set price individual seat(2)\n" +
                "-Set price for a whole row(3)\n" +
                "-Set price for the whole room(4)\n" +
                "-Set price for the middle seats(5)\n" +
                "Press q to exit");
        }

        static void roomOne()
        {
            var room1 = new Rooms(new double[10, 15]);
            bool busy = true;

            while (busy)
            {

                displayOptions(room1.seatPrices.GetLength(0), room1.seatPrices.GetLength(1));

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
                    room1.setPriceSeat();
                }
                else if (operation == ConsoleKey.D3)
                {
                    room1.setPriceRow();
                }
                else if (operation == ConsoleKey.D4)
                {
                    room1.setPriceRoom();
                }
                else if (operation == ConsoleKey.D5)
                {
                    room1.setPriceMiddle();
                }

            }
        }

        static void roomTwo()
        {
            var room2 = new Rooms(new double[15, 20]);
            bool busy = true;

            while (busy)
            {

                displayOptions(room2.seatPrices.GetLength(0), room2.seatPrices.GetLength(1));

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
                    room2.setPriceSeat();
                }
                else if (operation == ConsoleKey.D3)
                {
                    room2.setPriceRow();
                }
                else if (operation == ConsoleKey.D4)
                {
                    room2.setPriceRoom();
                }
                else if (operation == ConsoleKey.D5)
                {
                    room2.setPriceMiddle();
                }

            }
        }

        static void roomThree()
        {
            var room3 = new Rooms(new double[25, 25]);
            bool busy = true;

            while (busy)
            {

                displayOptions(room3.seatPrices.GetLength(0), room3.seatPrices.GetLength(1));

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
                    room3.setPriceSeat();
                }
                else if (operation == ConsoleKey.D3)
                {
                    room3.setPriceRow();
                }
                else if (operation == ConsoleKey.D4)
                {
                    room3.setPriceRoom();
                }
                else if (operation == ConsoleKey.D5)
                {
                    room3.setPriceMiddle();
                }

            }
        }

    }
}