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
        private static int count;

        public static void AgeFilter(User user) {
            if (user.age >= checkedAge) {
                Console.WriteLine(data[0] + "\n" + data[1] + "\n" + data[2] + "\n" + data[3] + "\n" + data[4] + "\n" + data[5] + "\n" + data[6] + "\n");
            }
        }

        public static void GenreFilter(User user) {
            Console.WriteLine("\nEnter the genre you want to search by:");
            genre = Console.ReadLine();
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
                //output
                if (genre.ToLower() == data[3].ToLower()) {
                    AgeFilter(user);
                    count++;
                }
            }
            streamreader.Close();

            if (count == 0) {
                Console.WriteLine("We have no movies with that genre.");
            }
        }
    }
}
