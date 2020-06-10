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
            Console.WriteLine("\nPlease pick a option.\n[1] Look at all future movies.\n[2] Search by genre.\n[3] Exit.");
            int choice = Program.ChoiceInput(1, 2);
            while (choice != 3) {
                if (choice == 1) {
                    //displays all future movies to user

                    //readfile
                    int count = 0;
                    streamreader = new StreamReader("futurefilm.txt");
                    while (streamreader.EndOfStream == false) {
                        i = 0;
                        data = new string[5];
                        while ((line = streamreader.ReadLine()) != "") {
                            data[i] = line;
                            i++;
                        }
                        //ageCheck
                        int.TryParse(data[1], out checkedAge);
                        if (user.age >= checkedAge) {
                            //output
                            count++;
                            Console.WriteLine("Title: " + data[0] + "\nAge: " + data[1] + "\nDescription: " + data[2] + "\nGenre: " + data[3] + "\nRelease Date: " + data[4] + "\n");
                        }
                    }
                    streamreader.Close();
                    if (count == 0) {
                        Console.WriteLine("Sorry, we have no movies you may see.");
                        choice = 3;
                    } else {
                        Console.WriteLine("\nPlease pick a option.\n[1] Look at all future movies.\n[2] Search by genre.\n[3] Exit.");
                        choice = Program.ChoiceInput(1, 2);
                    }
                } else if (choice == 2) {
                    //search all future movies by genre

                    //select genre
                    genre = SelectFutureGenre(user);
                    if (genre != null) {
                        Console.WriteLine();
                        //readfile
                        streamreader = new StreamReader("futurefilm.txt");
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
                                Console.WriteLine("Title: " + data[0] + "\nAge: " + data[1] + "\nDescription: " + data[2] + "\nGenre: " + data[3] + "\nRelease Date: " + data[4] + "\n");
                            }
                        }
                        streamreader.Close();
                        Console.WriteLine("\nPlease pick a option.\n[1] Look at all future movies.\n[2] Search by genre.\n[3] Exit.");
                        choice = Program.ChoiceInput(1, 2);
                    } else {
                        Console.WriteLine("Sorry, we have no movies you may see.");
                        choice = 3;
                    }
                }
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

        //genre selections for future movies
        private static string SelectFutureGenre(User user) {
            List<string> genreList = new List<string>();
            //readfile
            streamreader = new StreamReader(@"futurefilm.txt");
            while (streamreader.EndOfStream == false) {
                i = 0;
                data = new string[5];
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

