using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace cinemaApp {
    class Filters {
        private static StreamReader streamreader;
        private static string line;
        private static string[] data;
        private static int i;
        private static int checkedAge;
        private static string genre;

        //displays all current movies to user
        public static void AgeFilter(User user) {
            //readfile
            int count = 0;
            streamreader = new StreamReader("filmlist.txt");
            while (streamreader.EndOfStream == false) {
                i = 0;
                data = new string[8];
                while ((line = streamreader.ReadLine()) != "") {
                    data[i] = line;
                    i++;
                }
                //ageCheck
                int.TryParse(data[1], out checkedAge);
                if (user.age >= checkedAge) {
                    //output
                    Console.WriteLine("Title: " + data[0] + "\nAge: " + data[1] + "\nDescription: " + data[2] + "\nGenre: " + data[3] + "\nRelease Date: " + data[4] + "\nTime: " + data[5] + ", Room: " + data[6] + "\n");
                    count++;
                }
            }
            streamreader.Close();
            if (count == 0) {
                Console.WriteLine("Sorry, we have no movies you may see.");
            }
        }

        //search all current movies by genre
        public static void GenreFilter(User user) {
            //select genre
            genre = SelectGenre(user);
            if (genre != null) {
                Console.WriteLine();
                //readfile
                streamreader = new StreamReader("filmlist.txt");
                while (streamreader.EndOfStream == false) {
                    i = 0;
                    data = new string[8];
                    while ((line = streamreader.ReadLine()) != "") {
                        data[i] = line;
                        i++;
                    }
                    //ageCheck
                    int.TryParse(data[1], out checkedAge);
                    if (genre.ToLower() == data[3].ToLower() && user.age >= checkedAge) {
                        //output
                        Console.WriteLine("Title: " + data[0] + "\nAge: " + data[1] + "\nDescription: " + data[2] + "\nGenre: " + data[3] + "\nRelease Date: " + data[4] + "\nTime: " + data[5] + ", Room: " + data[6] + "\n");
                    }
                }
                streamreader.Close();
            } else {
                Console.WriteLine("Sorry, we have no movies you may see.");
            }
        }

        //a loop for all the options of displaying future movies
        public static void FutureMovies(User user) {
            //readfile
            int count = 0;
            streamreader = new StreamReader("futurefilm.txt");
            while (streamreader.EndOfStream == false) {
                i = 0;
                data = new string[2];
                while ((line = streamreader.ReadLine()) != "") {
                    data[i] = line;
                    i++;
                }
                //ageCheck
                int.TryParse(data[1], out checkedAge);
                if (user.age >= checkedAge) {
                    //output
                    count++;
                    Console.WriteLine("Title: " + data[0] + "\nRelease Date: " + data[1] + "\n");
                }
            }
            streamreader.Close();
            if (count == 0) {
                Console.WriteLine("Sorry, we have no movies you may see.");
            }
        }

        //genre selections for current movies
        private static string SelectGenre(User user) {
            List<string> genreList = new List<string>();
            //readfile
            streamreader = new StreamReader(@"filmlist.txt");
            while (streamreader.EndOfStream == false) {
                i = 0;
                data = new string[8];
                while ((line = streamreader.ReadLine()) != "") {
                    data[i] = line;
                    i++;
                }
                int.TryParse(data[1], out checkedAge);
                if (!(genreList.Contains(data[3])) && user.age >= checkedAge) {
                    genreList.Add(data[3]);
                }
            }
            streamreader.Close();
            if (genreList.Count != 0) {
                Console.WriteLine("\nSelect the genre you want to search by:");
                for (int i = 0; i < genreList.Count; i++) {
                    Console.WriteLine($"[{i}] {genreList[i]}.");
                }
                int choice = Program.ChoiceInput(0, genreList.Count);
                //selection
                return genreList[choice];
            } else {
                return null;
            }
        }
    }
}

