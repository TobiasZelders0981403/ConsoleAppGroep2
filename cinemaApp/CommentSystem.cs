using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace cinemaApp
{
    class CommentSystem
    {
        static string input;
        static string title;
        static string name;
        static string review;
        static string question;

        public static void CommentMain(User user)
        {
            Console.WriteLine("\nWhere would you like to go? \n[1] Reviews. \n[2] Questions. \n[3] Back.");
            input = Console.ReadLine();
            Action(user);
        }
        
        public static void Action(User user)
        {

            if (input == "1")
            {
                ReviewScreen(user);
            }
            else if (input == "2")
            {
                QuestionScreen(user);
            }
            else if (input == "3")
            {
                //CustomerNavigation(user);
            }
            else
            {
                CommentMain(user);
            }
        }

        public static void ReviewScreen(User user)
        {
            Console.WriteLine("\nWhat would you like to do? \n[1] Write a review. \n[2] Read reviews. \n[3] Back.");
            input = Console.ReadLine();

            if (input == "1")
            {
                AddReview(user);
            }
            else if (input == "2")
            {
                ReadReview(user);
            }
            else if (input == "3")
            {
                CommentMain(user);
            }
            else
            {
                CommentMain(user);
            }
        }

        public static void AddReview(User user)
        {
            Console.WriteLine("\nWhat movie would you like to review?");
            //load all movie titles
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
            for (int i =0; i < movieTitles.Count; i++) {
                Console.WriteLine($"[{i}] {movieTitles[i]}");
            }

            title = movieTitles[Program.ChoiceInput(0, movieTitles.Count - 1)];
            if (user.username != "Guest") {
                Console.WriteLine("\nWould you like to be anonymous? [Y]/[N].");
                input = Console.ReadLine();
                while (input.ToUpper() != "Y" && input.ToUpper() != "N") {
                    Console.WriteLine("\nWould you like to be anonymous? [Y]/[N].");
                    input = Console.ReadLine();
                }
                if (input.ToUpper() == "Y") {
                    name = "Anonymous";
                } else if (input.ToUpper() == "N") {
                    name = user.username;
                }
            } else {
                name = user.username;
            }
            Console.WriteLine("Please write your review:");
            review = Console.ReadLine();

            // adding review to designated txt file.
            using (StreamWriter sw = File.AppendText((title) + "-review.txt"))
            {
                sw.WriteLine(name + " says:");
                sw.WriteLine(review + "\n");
                sw.Close();
            }
            Console.WriteLine("\nWould you like to write another review? [Y]/[N].");
            input = Console.ReadLine();
            while (input.ToUpper() != "Y" && input.ToUpper() != "N") {
                Console.WriteLine("\nWould you like to write another review? [Y]/[N].");
                input = Console.ReadLine();
            }

            if (input.ToUpper() == "Y")
            {
                AddReview(user);
            }
            else if (input.ToUpper() == "N") {
                Console.WriteLine("\nWhere would you like to go? \n[1] Reviews. \n[2] Questions. \n[3] Back.");
                input = Console.ReadLine();
                if (input == "1")
                {
                    ReviewScreen(user);
                }
                else if (input == "2")
                {
                    CommentMain(user);
                }
                else if (input == "3")
                {
                    //CustomerNavigation(user);
                }
            }
        }

        public static void ReadReview(User user)
        {
            Console.WriteLine("Pick a movie.");
            // add code of list of existing movies.

            //load all movie titles
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
                Console.WriteLine("There are no reviews for the movie.");
            }
            Console.WriteLine("\nWould you like to read more reviews? [Y]/[N].");
            input = Console.ReadLine();
            while (input.ToUpper() != "Y" && input.ToUpper() != "N") {
                Console.WriteLine("\nWould you like to be anonymous? [Y]/[N].");
                input = Console.ReadLine();
            }
            if (input.ToUpper() == "Y")
            {
                ReadReview(user);
            }
            else if (input.ToUpper() == "N") {
                Console.WriteLine("\nWhere would you like to go? \n[1] Reviews. \n[2] Questions. \n[3] Back.");
                input = Console.ReadLine();
                if (input == "1")
                {
                    ReviewScreen(user);
                }
                else if (input == "2")
                {
                    CommentMain(user);
                }
                else if (input == "3")
                {
                    //CustomerNavigation(user);
                }
            }
        }
        public static void QuestionScreen(User user)
        {
            Console.WriteLine("\nWhat would you like to do? \n[1] Read FAQ. \n[2] Read all questions.\n[3] Ask a question.\n[4] Back.");
            input = Console.ReadLine();

            if (input == "1")
            {
                ReadFAQ();
                QuestionScreen(user);
            } 
            else if (input == "2") {
                if (File.Exists("questions.txt")) {
                    Console.WriteLine(File.ReadAllText("questions.txt"));
                } else {
                    Console.WriteLine("There are no questions.");
                }
                QuestionScreen(user);
            }
            else if (input == "3")
            {
                AskQuestion(user);
            }
            else if (input == "4")
            {
                CommentMain(user);
            }
            else
            {
                QuestionScreen(user);
            }
        }

        public static void ReadFAQ()
        {

            using (StreamReader sr = new StreamReader("FAQ.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }

        }
        public static void AskQuestion(User user)
        {
            if (user.username != "Guest") {
                Console.WriteLine("\nWould you like to be anonymous? [Y]/[N].");
                input = Console.ReadLine();
                while (input.ToUpper() != "Y" && input.ToUpper() != "N") {
                    Console.WriteLine("\nWould you like to be anonymous? [Y]/[N].");
                    input = Console.ReadLine();
                }
                if (input.ToUpper() == "Y") {
                    name = "Anonymous";
                } else if (input.ToUpper() == "N") {
                    name = user.username;
                }
            } else {
                name = user.username;
            }
            Console.WriteLine("\nWhat is your question?");
            question = Console.ReadLine();

            using (StreamWriter sw = File.AppendText("questions.txt"))
            {
                sw.WriteLine("From: " + name);
                sw.WriteLine("Q: " + question + "\n");
                sw.Close();
            }
            Console.WriteLine("\nWould you like to ask another question? [Y]/[N].");
            input = Console.ReadLine();
            while (input.ToUpper() != "Y" && input.ToUpper() != "N") {
                Console.WriteLine("\nWould you like to ask another question? [Y]/[N].");
                input = Console.ReadLine();
            }
            if (input.ToUpper() == "Y")
            {
                AskQuestion(user);
            }
            else if (input.ToUpper() == "N") {
                QuestionScreen(user);
            }
        }
    }
}

