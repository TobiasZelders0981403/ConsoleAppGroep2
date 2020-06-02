using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cinemaApp {
    public class Films {
        static string[][] movieList;
        static string input;
        static string filmName;
        static string filmAge;
        static string filmDescription;
        static string Genre;
        static string Release;
        static string Time;
        static string filmList;
        static string FullList;
        static string filmID;
        static string Room;
        static List<string> SelectableRooms = new List<string> { "room1", "room2", "room3" };
        static List<string> timeOptions = new List<string>() { "12:00", "14:00", "16:00", "18:00", "20:00", "22:00", "24:00" };


        public static void FilmMain() {
            MovieChoice();
        }

        static void MovieChoice() {
            Console.WriteLine("What would you like to do? Please type 1-3 and press Enter to continue.");
            Console.WriteLine("[1] Add film.");
            Console.WriteLine("[2] Edit film.");
            Console.WriteLine("[3] Remove film.");

            input = Console.ReadLine();
            ChoiceAction();
        }

        static void ChoiceAction() {
            if (input == "1") {
                AddMovie();
            } else if (input == "2") {
                EditMovie();
            } else if (input == "3") {
                RemoveMovie();
            } else {
                MovieChoice();
            }
        }

        static void AddMovie() {
            Console.WriteLine("Enter film name:");
            filmName = Program.StringCheck();
            Console.WriteLine("Enter age (min is 3):");
            filmAge = AgeCheck();
            Console.WriteLine("Enter film description:");
            filmDescription = Program.StringCheck();
            Console.WriteLine("Enter film genre:");
            Genre = Program.StringCheck();
            Console.WriteLine("Enter release date:");
            Release = Program.StringCheck();
            Console.WriteLine("Select time:");
            timeSelection();
            Console.WriteLine("Select room:");
            RoomSelection();
            Console.WriteLine("Enter film ID:");
            IDSelection();

            filmList += filmName + "\n" + filmAge + "\n" + filmDescription + "\n" + Genre + "\n" + Release + "\n" + Time + "\n" + Room + "\n" + filmID + "\n";

            StreamWriter sw = new StreamWriter(@"filmlist.txt", append: true);
            sw.WriteLine(filmList);
            sw.Close();
            filmList = "";
            FullList = "\n" + File.ReadAllText(@"filmlist.txt");
            Console.WriteLine(FullList);
            Console.WriteLine("Would you like to do something else? Y/N?");
            input = Console.ReadLine();
            if (input == "Y") {
                MovieChoice();
            }
        }

        static void EditMovie() {
            ReadMovies();
            //choice
            Console.WriteLine("\nWhat movie would you like to edit?.");
            for (int i = 0; i < movieList.Length; i++) {
                Console.WriteLine($"[{i}] {movieList[i][0]} , {movieList[i][5]}");
            }
            int choice = Program.ChoiceInput(0, movieList.Length - 1);
            //edit
            Console.WriteLine("\nWhat would you like to edit?");
            for (int k = 0; k < 8; k++) {
                Console.WriteLine($"[{k}] {movieList[choice][k]}");
            }
            int choice2 = Program.ChoiceInput(0, 7);
            string newString = "";
            if (choice2 == 0) {
                newString = Program.StringCheck();
            } else if (choice2 == 1) {
                newString = AgeCheck();
            } else if (choice2 == 2 || choice2 == 3 || choice2 ==4) {
                newString = Program.StringCheck();
            } else if (choice2 == 5) {
                timeSelection();
                newString = Time;
            } else if (choice2 == 6) {
                RoomSelection();
                newString = Room;
            } else if (choice2 == 7) {
                IDSelection();
                newString = filmID;
            }
            movieList[choice][choice2] = newString;
            //save
            SaveMovies();
            Console.WriteLine("Would you like to do something else? [Y]/[N]?");
            input = Console.ReadLine();
            if (input == "Y") {
                MovieChoice();
            }
        }
        static void RemoveMovie() {
            ReadMovies();
            Console.WriteLine("What movie would you like to remove?");
            for (int i = 0; i < movieList.Length; i++) {
                Console.WriteLine($"[{i}] {movieList[i][0]} , {movieList[i][5]}");
            }
            int choice = Program.ChoiceInput(0, movieList.Length - 1);

            movieList[choice] = null;
            SaveMovies();
            Console.WriteLine("Would you like to do something else? [Y]/[N]?");
            input = Console.ReadLine();
            if (input == "Y") {
                MovieChoice();
            }
        }

        static void ReadMovies() {
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
            movieList = new string[count][];
            int j = 0;
            streamreader.Close();
            streamreader = new StreamReader("filmlist.txt");
            while (streamreader.EndOfStream == false) {
                int i = 0;
                data = new string[8];
                while ((line = streamreader.ReadLine()) != "") {
                    data[i] = line;
                    i++;
                }
                movieList[j] = data;
                j++;
            }
            streamreader.Close();
        }

        static void SaveMovies() {
            string data = "";
            for (int i = 0; i < movieList.Length; i++) {
                if (movieList[i] == null) { } else {
                    for (int j = 0; j < movieList[i].Length; j++) {
                        data += movieList[i][j] + "\n";
                    }
                    if (i != movieList.Length - 1) {
                        data += "\n";
                    }
                }
            }

            StreamWriter streamwriter = new StreamWriter(@"filmlist.txt");
            streamwriter.WriteLine(data);
            streamwriter.Close();
        }

        static void timeSelection() {
            int j = 0;
            List<string> timeOptions2 = new List<string>(); 
            for (int index = 0; index < timeOptions.Count; index++) {
                if (TimeCheck(timeOptions[index])) {
                    timeOptions2.Add(timeOptions[index]);
                    Console.WriteLine($"[{j}] {timeOptions[index]}");
                    j++;
                }
            }
            if (j != 0) {
                int choice = Program.ChoiceInput(0, j);
                Time = timeOptions2[choice];
            } else {
                Console.WriteLine("There are no rooms left at this day.");
            }
        }

        static bool TimeCheck(string time) {
            ReadMovies();
            int count = 0;
            for (int i = 0; i < movieList.Length; i++) {
                if (movieList[i][5] == time) {
                    count++;
                }
            }
            if (count < 3) {
                return true;
            } else {
                return false;
            }
        }

        static void RoomSelection() {
            List<string> RoomList = new List<string>();
            List<string> RoomList2 = new List<string>();
            int count = 0;
            for (int i = 0; i < movieList.Length; i++) {
                if (movieList[i][5] == Time) {
                    RoomList.Add(movieList[i][6]);
                }
            }
            string data = "";
            for (int i = 0; i < SelectableRooms.Count; i++) {
                bool used = false;
                for (int j = 0; j < RoomList.Count; j++) {
                    if (SelectableRooms[i] == RoomList[j]) {
                        used = true;
                    }
                }
                if (!used) {
                    RoomList2.Add(SelectableRooms[i]);
                    data += $"[{count}] {SelectableRooms[i]}\n";
                    count++;
                }
            }

            if (count != 0) {
                Console.WriteLine(data);
                int choice = Program.ChoiceInput(0, count - 1);
                Room = RoomList2[choice];
            } else {
                Console.WriteLine("All rooms are taken at this time.");
            }
        }

        static void IDSelection() {
            string input = Console.ReadLine();
            while (IDCheck(input) || string.IsNullOrWhiteSpace(input)) {
                Console.WriteLine("This ID is already taken or invalid. Please enter a different film ID");
                input = Console.ReadLine();
            }
            filmID = input;
        }

        static bool IDCheck(string input) {
            //stel zit erin --> return true
            ReadMovies();
            for (int i = 0; i < movieList.Length; i++) {
                if (movieList[i][7] == input) {
                    return true;
                }
            }
            return false;
        }
        static string AgeCheck() {
            int check;
            string ageInput = Console.ReadLine();
            int.TryParse(ageInput, out check);
            while (check < 3) {
                Console.WriteLine("Invalid Input! Please enter age:");
                ageInput = Console.ReadLine();
                int.TryParse(ageInput, out check);
            }
            return ageInput;
        }
    }
}
