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

        //input
        public static void ResereTickets()
        {
            //select time
            TimeSelection();

            //select movie (includes room)
            MovieSelection();

            //select seat(s)
            SeatSelection();

            //read available/taken seats
            ReadSeatFile();
            //save available/taken seats
            SaveSeatFile();
        }




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
            while (!(0 <= checkedChoice && checkedChoice < timeLen))
            {
                Console.WriteLine("Invalid Input! Please enter your option:");
                choiceInput = Console.ReadLine();
                int.TryParse(choiceInput, out checkedChoice);
            }
            timeChoice = checkedChoice;
        }

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
            while (!(0 <= checkedChoice && checkedChoice < 3))
            {
                Console.WriteLine("Invalid Input! Please enter your option:");
                movieInput = Console.ReadLine();
                int.TryParse(movieInput, out checkedChoice);
            }
            movieChoice = checkedChoice;
        }

        static void SeatSelection()
        {

        }

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

        static void SaveSeatFile()
          a{
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
    }
}
