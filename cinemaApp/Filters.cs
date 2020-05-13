using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace cinemaApp {
    class Filters {
        private static StreamReader streamreader;
        private static string line;
        private static string[] data;
        private static int i;
        private static int checkedAge;
        private static string genre;
        private static int count = 0;

        public static void AgeFilter(User user) {
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
                if (user.age >= checkedAge) {
                    //output
                    Console.WriteLine(data[0] + "\n" + data[1] + "\n" + data[2] + "\n" + data[3] + "\n" + data[4] + "\n" + data[5] + "\n" + data[6] + "\n");
                }
            }
            streamreader.Close();
        }

        public static void GenreFilter(User user) {
            Console.WriteLine("\nSelect the genre you want to search by:");
            //select genre
            genre = SelectGenre(user);
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
                        Console.WriteLine(data[0] + "\n" + data[1] + "\n" + data[2] + "\n" + data[3] + "\n" + data[4] + "\n" + data[5] + "\n" + data[6] + "\n");
                    count++;
                }
            }
            streamreader.Close();

            if (count == 0) {
                Console.WriteLine("Sorry! We have no movies with that genre for you.");
            }
        }

        private static string SelectGenre(User user) {
            List<string> genreList = new List<string>();
            //readfile
            streamreader = new StreamReader("filmlist.txt");
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

            for (int i =0; i < genreList.Count; i++) {
                Console.WriteLine($"{i}: {genreList[i]}.");
            }
            int choice = Program.ChoiceInput(0, genreList.Count);
            //selection
            return genreList[choice];
        }
    }
}
