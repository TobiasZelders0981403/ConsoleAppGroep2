using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cinemaApp {
    class OwnerComment {
        static string input;
        static string title;
        static string question;
        static string questionlist;
        static string answer;
        private static int i;
        private static string line;
        private static List<string> data;
        private static List<string> displayData;
        private static List<string> allLines;

        public static void CoMaMain() {
            CoMaChoice();
        }
        public static void CoMaChoice() {
            Console.WriteLine("\nWhere would you like to go? \n[1] Reviews \n[2] Questions. \n[3] Back.");
            input = Console.ReadLine();
            CoMaAction();
        }
        public static void CoMaAction() {
            if (input == "1") {
                MaReview();
            } else if (input == "2") {
                MaQuestions();
            }
        }
        public static void MaReview() {
            Console.WriteLine("\nWhat would you like to do? \n[1] Read reviews. \n[2] Back.");
            input = Console.ReadLine();

            if (input == "1") {
                ReviewRead();
            } else if (input == "2") {
                CoMaMain();
            }
        }
        public static void ReviewRead() {
            Console.WriteLine("\nWhich movies would you like to read the reviews of?");
            List<string> movieTitles = new List<string>();
            string line;
            StreamReader streamreader = new StreamReader("filmlist.txt");
            while (streamreader.EndOfStream == false) {
                int i = 0;
                string[] data = new string[8];
                while (!string.IsNullOrWhiteSpace(line = streamreader.ReadLine())) {
                    data[i] = line;
                    i++;
                }
                movieTitles.Add(data[0]);
            }
            streamreader.Close();
            //movie choice
            for (int i = 0; i < movieTitles.Count; i++) {
                Console.WriteLine($"[{i}] {movieTitles[i]}");
            }

            title = movieTitles[Program.ChoiceInput(0, movieTitles.Count - 1)];

            if (File.Exists(title + "-review.txt")) {
                using (StreamReader sr = new StreamReader(title + "-review.txt")) {
                    while ((line = sr.ReadLine()) != null) {
                        Console.WriteLine(line);
                    }
                }
            } else {
                Console.WriteLine("There are no reviews for this movie.");
            }
            Console.WriteLine("\nWould you like to read more reviews? [Y]/[N].");
            input = Console.ReadLine();
            while (input.ToUpper() != "Y" && input.ToUpper() != "N") {
                Console.WriteLine("\nWould you like to read more reviews? [Y]/[N].");
                input = Console.ReadLine();
            }
            if (input.ToUpper() == "Y") {
                ReviewRead();
            } else if (input.ToUpper() == "N") {
                Console.WriteLine("\nWhere would you like to go? \n[1] Reviews. \n[2] Questions. \n[3] Back.");
                input = Console.ReadLine();
                if (input == "1") {
                    MaReview();
                } else if (input == "2") {
                    MaQuestions();
                }
            }
        }

        public static void MaQuestions() {
            Console.WriteLine("\nWhat would you like to do? \n[1] Read questions. \n[2] Answer questions. \n[3] Back.");
            input = Console.ReadLine();

            if (input == "1") {
                Qread();
            } else if (input == "2") {
                Qanswer();
            } else if (input == "3") {
                CoMaMain();
            }
        }
        public static void Qread() {
            if (File.Exists("questions.txt")) {
                Console.WriteLine(File.ReadAllText("questions.txt"));
                MaQuestions(); 
            } else {
                Console.WriteLine("There are no questions");
            }

        }
        public static void Qanswer() {
            allLines = new List<string>();
            loadQuestions();
            if (data.Count != 0){
                Console.WriteLine("\nWhich question would you like to answer?");
                for (int i = 0; i < displayData.Count; i++) {
                    Console.WriteLine($"[{i}] {displayData[i]}");
                }
                question = data[Program.ChoiceInput(0, displayData.Count - 1)];

                int index = allLines.IndexOf(question);

                Console.WriteLine("\nWrite your answer.");
                answer = "A (owner): " + Console.ReadLine() + "\n";
                allLines.Insert(index + 1, answer);
                string allText = "";
                for (int i = 0; i < allLines.Count; i++) {
                    allText += allLines[i];
                }
                File.WriteAllText(@"questions.txt", allText);
                MaQuestions();
            } else {
                Console.WriteLine("\nThere are no questions that need answering.");
                MaQuestions();
            }
        }

        private static void loadQuestions() {
            if (File.Exists(@"questions.txt")) {
                using (StreamReader sr = new StreamReader(@"questions.txt")) {
                    data = new List<string>();
                    displayData = new List<string>();
                    while (sr.EndOfStream == false) {
                        string s1 = "";
                        string s2 = "";
                        while ((line = sr.ReadLine()) != "" && line != null) {
                            s1 += line + "\n    ";
                            s2 += line + "\n";
                        }
                        if (!s2.Contains("(owner)")) {
                            displayData.Add(s1);
                            data.Add(s2);
                        }
                        allLines.Add(s2);
                        allLines.Add("\n");
                    }
                }
            } else {
                Console.WriteLine("There are no questions.");
            }
        }
    }
}