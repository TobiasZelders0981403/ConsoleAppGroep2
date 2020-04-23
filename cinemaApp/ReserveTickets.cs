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
        static List<string> dayOptions = new List<string>() { "monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        static List<string> timeOptions = new List<string>() { "12:00", "14:00", "16:00", "18:00", "20:00", "22:00", "24:00"};
        static int timeLen = timeOptions.Count;
        static List<string> movieOptions = new List<string>();
        static string movieOptionString = "";
        static int dayChoice;
        static int timeChoice;
        static int movieChoice;
        static int rowChoice = -1;
        static int seatChoice = -1;

        //input
        public static void ReserveTicketsMain(User user)
        {
            //select day
            DaySelection();

            //select time
            TimeSelection();

            //select movie (includes room)
            MovieSelection();

            //select seat(s)
            SeatSelection(user);
        }

        static void DaySelection() {
            Console.WriteLine("\nPlease select a day.");
            for (int i =0; i < dayOptions.Count; i++) {
                Console.WriteLine($"{i}: {dayOptions[i]}");
            }
            dayChoice = Program.ChoiceInput(0, 6);
        }

        //checks if input is valid
        static void TimeSelection()
        {
            Console.WriteLine("Please select a time.");
            for (int i = 0; i < timeLen; i++)
            {
                Console.WriteLine($"{i}: {timeOptions[i]}");
            }
            timeChoice = Program.ChoiceInput(0,timeLen);
        }

        //checks if input is valid
        static void MovieSelection()
        {
            LoadMovies();
            Console.WriteLine("\nPlease select a movie.");
            for (int i = 0; i < movieOptions.Count; i++)
            {
                Console.WriteLine($"{i}: {movieOptions[i]}");
            }
            movieChoice = Program.ChoiceInput(0,movieOptions.Count);
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
                    }
                    Console.WriteLine("Please Select a row:");
                    rowChoice = Program.ChoiceInput(0,rowMax - 1);

                    Console.WriteLine("Please select a seat:");
                    seatChoice = Program.ChoiceInput(0, seatsPerRow - 1);
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
            int choice = Program.ChoiceInput(0, 1);
            if (choice == 0)
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
            string fileName = dayOptions[dayChoice] + "/" + timeChoice + "-Seats.txt";
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
            string fileName = dayOptions[dayChoice] + "/" + timeChoice + "-Seats.txt";
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

        //loads in movies
        static void LoadMovies() {
            //read
            int count = 0;
            string[] data;
            string line;
            StreamReader streamreader = new StreamReader("filmlist.txt");
            while (streamreader.EndOfStream == false) {
                if ((line = streamreader.ReadLine()) == "") {
                    count++;
                }
            }

            streamreader.Close();
            streamreader = new StreamReader("filmlist.txt");
            while (streamreader.EndOfStream == false) {
                int i = 0;
                data = new string[8];
                while ((line = streamreader.ReadLine()) != "") {
                    data[i] = line;
                    i++;
                }
                if (data[5] == timeOptions[timeChoice]) {
                    movieOptions.Add(data[0]);
                }
            }
            streamreader.Close();
        }

    }
}
