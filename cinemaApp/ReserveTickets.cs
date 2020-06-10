using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Linq;
using Newtonsoft.Json;
using System.Xml;

namespace cinemaApp
{
    class ReserveTickets
    {
        // Three movie rooms 150 seats, 300 seats and 500 seats
        // Room 1 : 10 rows of 15 seats [10][15]
        // Room 2 : 15 rows of 20 seats [15][20]
        // Rooom 3 : 25 rows of 25 seats [25][25]

        static string[][] RoomSeats;
        static List<string> dayOptions = new List<string>() { "monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        static List<string> timeOptions;
        static List<string> timeTemplate = new List<string>() { "12:00", "14:00", "16:00", "18:00", "20:00", "22:00", "24:00" };
        static List<string> movieOptions;
        static List<string> rooms = new List<string>();
        static int dayChoice;
        static int timeChoice;
        static int movieChoice;
        static int rowChoice;
        static int seatChoice;
        static string room;
        static bool full = true;
        static double[][] priceArray;

        //input
        public static void ReserveTicketsMain(User user)
        {
                rowChoice = -1;
                seatChoice = -1;
            movieOptions = new List<string>();
            timeOptions = new List<string>();

            //LoadMovies(user);
            MovieSelection(user);

            //DaySelection();

            //LoadTimeOptions();
            //TimeSelection(); //includes room

            //SeatSelection(user);

            }

        static void MovieSelection(User user) {
            movieOptions = new List<string>();
            LoadMovies(user);
            int i;
            Console.WriteLine("\nPlease select a movie.");
            for (i = 0; i < movieOptions.Count; i++) {
                Console.WriteLine($"[{i}] {movieOptions[i]}");
            }
            Console.WriteLine($"[{i}] Go Back.");
            movieChoice = Program.ChoiceInput(0, movieOptions.Count);
            if (movieChoice != movieOptions.Count) {
                DaySelection(user);
            }
        }

        static void DaySelection(User user) {
            int i;
            Console.WriteLine("\nPlease select a day.");
            for (i = 0; i < dayOptions.Count; i++) {
                Console.WriteLine($"[{i}] {dayOptions[i]}");
            }
            Console.WriteLine($"[{i}] Go Back.");
            dayChoice = Program.ChoiceInput(0, 7);
            if (dayChoice != 7) {
                timeOptions = new List<string>();
                LoadTimeOptions();
                TimeSelection(user);
            } else { MovieSelection(user); }
        }

        static void TimeSelection(User user) {
            int i;
            Console.WriteLine("Please select a time.");
            for (i = 0; i < timeOptions.Count; i++) {
                Console.WriteLine($"[{i}] {timeOptions[i]}");
            }
            Console.WriteLine($"[{i}] Go Back.");
            int choice = Program.ChoiceInput(0, timeOptions.Count);
            if (choice != timeOptions.Count) {
                timeChoice = timeTemplate.IndexOf(timeOptions[choice]);
                room = rooms[choice];
                Console.WriteLine("[0] Select seats.\n[1] Go Back");
                int choice2 = Program.ChoiceInput(0, 1);
                if (choice2 != 1) {
                    SeatSelection(user);
                } else { TimeSelection(user); }
            } else { DaySelection(user); };
        }

        static void SeatSelection(User user) {
            int rowMax;
            int seatsPerRow;
            if (room == "room1") {
                rowMax = 10;
                seatsPerRow = 15;
                RoomSeats = new string[10][];
            } else if (room == "room2") {
                rowMax = 15;
                seatsPerRow = 20;
                RoomSeats = new string[15][];
            } else {
                rowMax = 25;
                seatsPerRow = 25;
                RoomSeats = new string[25][];
            }

            while (TakenOrNot(rowChoice, seatChoice)) {
                ReadSeatFile();
                ReadSeats(rowMax, seatsPerRow);

                if (!full) {
                    Console.WriteLine("Please Select a row:");
                    rowChoice = Program.ChoiceInput(1, rowMax) - 1;

                    Console.WriteLine("Please select a seat:");
                    seatChoice = Program.ChoiceInput(1, seatsPerRow) - 1;
                } else {
                    break;
                }
            }

            RoomSeats[rowChoice][seatChoice] = "1";
            SaveToShoppingCart(user);
            SaveSeatFile();
            Options(user);
        }


        static void Options(User user) {
            //options
            Console.WriteLine("\n[1] Select another seat\n[0] Quit");
            int choice = Program.ChoiceInput(0, 1);
            if (choice == 1) {
                rowChoice = -1;
                seatChoice = -1;
                SeatSelection(user);
            }
        }

        //checks if a seat is taken
        static bool TakenOrNot(int row, int seat) {
            if (row < 0 || seat < 0) {
                return true;
            } else if (RoomSeats[row][seat] == "1") {
                Console.WriteLine("This seat is taken. Please try again");
                return true;
            } else {
                return false;
            }
        }

        static void ReadSeats(int rowMax, int seatsPerRow) {
            for (int i =0; i < RoomSeats.Length; i++) {
                if (RoomSeats[i].Contains("0")) {
                    full = false;
                }
            }

            if (full) {
                Console.WriteLine("There are no seats available at this day and time.\nPlease select another day/time.");
            } else {
                string data = "";
                for (int i = 0; i < rowMax; i++) {
                    if (i < 9) {
                        data += $"row {i + 1}:  ";
                    } else {
                        data += $"row {i + 1}: ";
                    }
                    for (int j = 0; j < seatsPerRow; j++) {
                        if (RoomSeats[i][j] == "1") {
                            if (j > 9) {
                                data += "__ ";
                            } else {
                                data += "_ ";
                            }
                        } else {
                            data += $"{j + 1} ";
                        }
                    }
                    data += "\n";
                }
                Console.WriteLine(data);
            }
        }

        static void ReadSeatFile() {
            string fileName = dayOptions[dayChoice] + "/" + timeChoice + "-Seats.txt";
            StreamReader streamreader = new StreamReader(@fileName);

            string line;
            int i = 0;
            int index = 0;
            int start;
            int stop;
            if (room == "room1") {
                start = 0;
                stop = 10;
            } else if (room == "room2") {
                start = 10;
                stop = 25;
            } else {
                start = 25;
                stop = 50;
            }

            while (!string.IsNullOrWhiteSpace(line = streamreader.ReadLine())) {
                if (i >= start && i < stop) {
                    string[] components = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    RoomSeats[index] = components;
                    index++;
                }
                i++;
            }
            streamreader.Close();
        }

        static void SaveSeatFile() {
            //Read File
            string fileName = dayOptions[dayChoice] + "/" + timeChoice + "-Seats.txt";
            StreamReader streamreader = new StreamReader(@fileName);
            string line;
            string[][] allSeats = new string[50][];
            int i = 0;
            int index = 0;
            int start;
            int stop;
            if (room == "room1") {
                start = 0;
                stop = 10;
            } else if (room == "room2") {
                start = 10;
                stop = 25;
            } else {
                start = 25;
                stop = 50;
            }
            while (!string.IsNullOrWhiteSpace(line = streamreader.ReadLine())) {
                string[] components = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                allSeats[i] = components;
                if (i >= start && i < stop) {
                    allSeats[i] = RoomSeats[index];
                    index++;
                }
                i++;
            }
            streamreader.Close();

            string data = "";
            for (i = 0; i < 10; i++) {
                for (int j = 0; j < 15; j++) {
                    data += allSeats[i][j] + " ";
                }
                data += "\n";
            }

            for (i = 10; i < 25; i++) {
                for (int j = 0; j < 20; j++) {
                    data += allSeats[i][j] + " ";
                }
                data += "\n";
            }

            for (i = 25; i < 50; i++) {
                for (int j = 0; j < 25; j++) {
                    data += allSeats[i][j] + " ";
                }
                if (i != 49) {
                    data += "\n";
                }
            }
            StreamWriter streamwriter = new StreamWriter(@fileName);
            streamwriter.WriteLine(data);
            streamwriter.Close();
        }

        static void LoadMovies(User user) {
            //read
            string[] data;
            string line;
            int checkedAge;
            StreamReader streamreader = new StreamReader("filmlist.txt");
            while (streamreader.EndOfStream == false) {
                int i = 0;
                data = new string[8];
                while (!string.IsNullOrWhiteSpace(line = streamreader.ReadLine())) {
                    data[i] = line;
                    i++;
                }
                int.TryParse(data[1], out checkedAge);
                if (user.age >= checkedAge && (!movieOptions.Contains(data[0]))) {
                    movieOptions.Add(data[0]);
                }
            }
            streamreader.Close();
        }

        static void LoadTimeOptions() {
            rooms = new List<string>();
            string[] data;
            string line;
            StreamReader streamreader = new StreamReader("filmlist.txt");
            while (streamreader.EndOfStream == false) {
                int i = 0;
                data = new string[8];
                while (!string.IsNullOrWhiteSpace(line = streamreader.ReadLine())) {
                    data[i] = line;
                    i++;
                }
                if (data[0] == movieOptions[movieChoice]) {
                    timeOptions.Add(data[5]);
                    rooms.Add(data[6]);
                }
            }
            streamreader.Close();
        }

        static void SaveToShoppingCart(User user) {
            if (room == "room1") {
                string fileName = "roomOne.json";
                string rawJson = File.ReadAllText(@fileName);
                priceArray = JsonConvert.DeserializeObject<double[][]>(rawJson);
            } else if (room == "room2") {
                string fileName = "roomTwo.json";
                string rawJson = File.ReadAllText(fileName);
                priceArray = JsonConvert.DeserializeObject<double[][]>(rawJson);
            } else {
                string fileName = "roomThree.json";
                string rawJson = File.ReadAllText(fileName);
                priceArray = JsonConvert.DeserializeObject<double[][]>(rawJson);
            }
            string filename = $"{user.username}-ShoppingCart.json";
            double price = priceArray[rowChoice][seatChoice];
            string[] s = new string[] {price.ToString(), movieOptions[movieChoice], dayOptions[dayChoice], timeTemplate[timeChoice], room, rowChoice.ToString(), seatChoice.ToString()};
            if (user.username != "Guest") {
                if (File.Exists(@filename)) {
                    string rawJSON = File.ReadAllText(filename);
                    string[][] data = JsonConvert.DeserializeObject<string[][]>(rawJSON);
                    Array.Resize(ref data, data.Length + 1);
                    data[data.Length - 1] = s;
                    string shoppingData = JsonConvert.SerializeObject(data);
                    File.WriteAllText(filename, shoppingData);
                } else {
                    string[][] data = new string[1][] { s };
                    string shoppingData = JsonConvert.SerializeObject(data);
                    File.AppendAllText(filename, shoppingData);
                }
            } else {
                user.shoppingCart.Add(s);
            }
            /* else {
                List<string> data = new List<string> { movieOptions[movieChoice], dayOptions[dayChoice], timeTemplate[timeChoice], room, rowChoice.ToString(), seatChoice.ToString()};
                user.shoppingCart.Add(data);
            }*/
        }
    }
}
