using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace cinemaApp
{
    class ReserveTickets
    {
        // Three movie rooms 150 seats, 300 seats and 500 seats
        // Room 1 : 10 rows of 15 seats [10][15]
        // Room 2 : 15 rows of 20 seats [15][20]
        // Rooom 3 : 25 rows of 25 seats [25][25]

        static string[][] Room1Seats = new string[10][];
        static string[][] Room2Seats = new string[15][];
        static string[][] Room3Seats = new string[25][];
        static List<string> timeOptions = new List<string>() { "13:00", "14:00" };
        static int timeLen = timeOptions.Count;
        static string[][] movieOptions = { new string[] { "up", "aladin", "lion king" }, new string[] { "aladin", "lion king", "up" } };
        static string movieOptionString = "";
        static string timeOptionString = "";
        static int timeChoice;
        static int movieChoice;
        static int checkedChoice;
        static int rowChoice = -1;
        static int seatChoice = -1;

        //input
        public static void ReserveTicketsMain(User user)
        {
            //select time
            TimeSelection();

            //select movie (includes room)
            MovieSelection();

            //select seat(s)
            SeatSelection(user);
        }

        //checks if input is valid
        static void TimeSelection()
        {
            for (int i = 0; i < timeLen; i++)
            {
                timeOptionString += i.ToString() + ":  " + timeOptions[i] + "\n";
            }
            Console.WriteLine("Please select a option.");
            Console.WriteLine(timeOptionString);
            string choiceInput = Console.ReadLine();
            int.TryParse(choiceInput, out checkedChoice);
            while (!(0 <= checkedChoice && checkedChoice < timeLen) || string.IsNullOrWhiteSpace(choiceInput))
            {
                Console.WriteLine("Invalid Input! Please enter your option:");
                choiceInput = Console.ReadLine();
                int.TryParse(choiceInput, out checkedChoice);
            }
            timeChoice = checkedChoice;
        }

        //checks if input is valid
        static void MovieSelection()
        {
            for (int i = 0; i < 3; i++)
            {
                movieOptionString += i.ToString() + ":  " + movieOptions[timeChoice][i] + "\n";
            }
            Console.WriteLine("Please select a option.");
            Console.WriteLine(movieOptionString);
            string movieInput = Console.ReadLine();
            int.TryParse(movieInput, out checkedChoice);
            while ((!(0 <= checkedChoice && checkedChoice < 3)) || string.IsNullOrWhiteSpace(movieInput))
            {
                Console.WriteLine("Invalid Input! Please enter your option:");
                movieInput = Console.ReadLine();
                int.TryParse(movieInput, out checkedChoice);
            }
            movieChoice = checkedChoice;
        }

        //lets the user select seats
        static void SeatSelection(User user)
        {
            int rowMax = 10;
            int seatsPerRow = 15;
            string[][] roomSeats = Room1Seats;
            ReadSeats(rowMax, seatsPerRow, roomSeats);
                while (TakenOrNot(roomSeats, rowChoice, seatChoice)){
                    if (movieChoice == 1)
                    {
                    rowMax = 15;
                        seatsPerRow = 20;
                        roomSeats = Room2Seats;
                    }
                    else if (movieChoice == 2)
                    {
                        rowMax = 25;
                        seatsPerRow = 25;
                        roomSeats = Room3Seats;
                    };
                    Console.WriteLine("Please Select a row:");
                    string row = Console.ReadLine();
                    int.TryParse(row, out checkedChoice);
                    while ((!(0 <= checkedChoice && checkedChoice < rowMax)) || string.IsNullOrWhiteSpace(row))
                    {
                        Console.WriteLine("Invalid Input! Please enter your option:");
                        row = Console.ReadLine();
                        int.TryParse(row, out checkedChoice);
                    }
                    rowChoice = checkedChoice;

                    Console.WriteLine("Please select a seat:");
                    string seat = Console.ReadLine();
                    int.TryParse(seat, out checkedChoice);
                    while ((!(0 <= checkedChoice && checkedChoice < seatsPerRow)) || string.IsNullOrWhiteSpace(seat))
                    {
                        Console.WriteLine("Invalid Input! Please enter your option:");
                        seat = Console.ReadLine();
                        int.TryParse(seat, out checkedChoice);
                    }
                    seatChoice = checkedChoice;
                }
            roomSeats[rowChoice][seatChoice] = "1";
            SaveSeatFile();
            AddToShoppingCart(user);
            Options(user);
        }

        //checks if a seat is taken
        static bool TakenOrNot(string[][] roomSeats, int row, int seat)
        {
            if (row < 0 || seat < 0) {
                return true;
            } else if (roomSeats[row][seat] == "1")
            {
                Console.WriteLine("This seat is taken. Please try again");
                return true;
            } else
            {
                return false;
            }
        }

        //ending options
        static void Options(User user)
        {
            //options
            Console.WriteLine("0: Select another seat\n1: Quit");
            string choice = Console.ReadLine();
            int.TryParse(choice, out checkedChoice);
            while ((!(0 <= checkedChoice && checkedChoice < 2)) || string.IsNullOrWhiteSpace(choice))
            {
                Console.WriteLine("Invalid Input! Please enter your option:");
                choice = Console.ReadLine();
                int.TryParse(choice, out checkedChoice);
            }
            if (checkedChoice == 0)
            {
                rowChoice = -1;
                seatChoice = -1;
                SeatSelection(user);
            }

        }

        //shows seat data from storage
        static void ReadSeats(int rowMax, int seatsPerRow, string[][] roomSeats)
        {
            ReadSeatFile();

            string data = "";

            for (int i = 0; i < rowMax; i++)
            {
                if (i < 10)
                {
                    data += $"row {i}:  ";
                }
                else
                {
                    data += $"row {i}: ";

                }
                for (int j = 0; j < seatsPerRow; j++)
                {
                    if (roomSeats[i][j] == "1")
                    {
                        data += "_ ";
                    }
                    else
                    {
                        data += $"{j} ";
                    }
                }
                data += "\n";
            }
            Console.WriteLine(data);

        }

        //reads seat data from storage
        static void ReadSeatFile()
        {
            string fileName = timeChoice + "-Seats.txt";
            StreamReader streamreader = new StreamReader(@fileName);
            string line;
            int i = 0;
            while ((line = streamreader.ReadLine()) != null)
            {
                string[] components = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (i < 10) {
                    Room1Seats[i] = components;
                } else if (i < 25) {
                    Room2Seats[i - 10] = components;
                } else if (i < 50) {
                    Room3Seats[i - 25] = components;
                }
                i++;
            }
            streamreader.Close();
        }

        //saves adjusted seat data in storage
        static void SaveSeatFile()
          {
            string fileName = timeChoice + "-Seats.txt";
            string data = "";

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    data += Room1Seats[i][j] + " ";
                }
                data += "\n";
            }

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    data += Room2Seats[i][j] + " ";
                }
                data += "\n";
            }

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    data += Room3Seats[i][j] + " ";
                }
                data += "\n";
            }
            StreamWriter streamwriter = new StreamWriter(@fileName);
            streamwriter.WriteLine(data);
            streamwriter.Close();
        }

        //adds chocen tickets to the shopping cart
        static void AddToShoppingCart(User user)
        {
            string fileName = user.username + "-ShoppingCart.txt";
            StreamWriter streamwriter = new StreamWriter(@fileName, append: true);
            string data = $"Time: {timeOptions[timeChoice]}\nMovie: {movieOptions[timeChoice][movieChoice]}\nRow: {rowChoice}, Seat: {seatChoice}\n";
            streamwriter.Write(data);
            streamwriter.Close();
        }
    }
}
