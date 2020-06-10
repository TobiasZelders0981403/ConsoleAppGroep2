using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentSystem
{
    class CSystem
    {
        static string username;
        static string input;
        static string title;
        static string name;
        static string review;
        static string question;
        private static int i;
        private static string line;
        private static string[] data;

        public static void CommentMain()
        {

            CommentChoice();
        }

        public static void CommentChoice()
        {

            Console.WriteLine("Where would you like to go? \n 1. Reviews. \n 2. Questions. \n 3. Back.");
            input = Console.ReadLine();
            Action();
        }
        public static void Action()
        {

            if (input == "1")
            {
                ReviewScreen();
            }
            else if (input == "2")
            {
                QuestionScreen();
            }
            else if (input == "3")
            {
                //CustomerNavigation(user);
            }
            else
            {
                CommentChoice();
            }
        }

        public static void ReviewScreen()
        {
            Console.WriteLine("What would you like to do? \n 1. Write a review. \n 2. Read reviews. \n 3. Back.");
            input = Console.ReadLine();

            if (input == "1")
            {
                AddReview();
            }
            else if (input == "2")
            {
                ReadReview();
            }
            else if (input == "3")
            {
                CommentChoice();
            }
            else
            {
                ReviewScreen();
            }
        }
        public static void AddReview()
        {
            loadFilmTitles();


            Console.WriteLine("What movie would you like to review?");
            // add code with list of existing movie names.

            title = Console.ReadLine();

            Console.WriteLine("Would you like to be anonymous? Y/N.");
            input = Console.ReadLine();

            if (input == "Y")
            {
                name = "Anonymous";
            }
            else if (input == "N")
            {
                name = username;
            }

            Console.WriteLine("Please write your review:");
            review = Console.ReadLine();

            // adding review to designated txt file.
            using (StreamWriter sw = File.AppendText((title) + ".txt"))
            {
                sw.WriteLine(name + " says:");
                sw.WriteLine(review + "\n");
                sw.Close();
            }
            Console.WriteLine("Would you like to write another review? Y/N.");
            input = Console.ReadLine();

            if (input == "Y")
            {
                AddReview();
            }
            else if (input == "N")
            {
                Console.WriteLine("Where would you like to go? \n 1. Review screen. \n 2. Commenting screen. \n 3. Home. ");
                input = Console.ReadLine();
                if (input == "1")
                {
                    ReviewScreen();
                }
                else if (input == "2")
                {
                    CommentChoice();
                }
                else if (input == "3")
                {
                    //CustomerNavigation(user);
                }
            }

        }
        public static void ReadReview()
        {
            Console.WriteLine("Pick a movie.");
            // add code of list of existing movies.

            loadFilmTitles();

            title = Console.ReadLine();

            using (StreamReader sr = new StreamReader(title + ".txt"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine("Would you like to read more reviews? Y/N.");
            input = Console.ReadLine();

            if (input == "Y")
            {
                ReadReview();
            }
            else if (input == "N")
            {
                Console.WriteLine("Where would you like to go? \n 1. Review screen. \n 2. Commenting screen. \n 3. Home. ");
                input = Console.ReadLine();
                if (input == "1")
                {
                    ReviewScreen();
                }
                else if (input == "2")
                {
                    CommentChoice();
                }
                else if (input == "3")
                {
                    //CustomerNavigation(user);
                }
            }
        }
        public static void QuestionScreen()
        {
            Console.WriteLine("What would you like to do? \n 1. Read FAQ. \n 2. Ask a question. \n 3. Back.");
            input = Console.ReadLine();

            if (input == "1")
            {
                ReadFAQ();
            }
            else if (input == "2")
            {
                AskQuestion();
            }
            else if (input == "3")
            {
                CommentChoice();
            }
            else
            {
                QuestionScreen();
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
        public static void AskQuestion()
        {
            Console.WriteLine("Would you like to be Anonymous? Y/N");
            input = Console.ReadLine();
            if (input == "Y")
            {
                name = "Anonymous";
            }
            else if (input == "N")
            {
                name = username;
            }

            Console.WriteLine("What is your question?");
            question = Console.ReadLine();

            using (StreamWriter sw = File.AppendText("questions.txt"))
            {
                sw.WriteLine("From: " + name);
                sw.WriteLine("Q: " + question + "\n");
                sw.Close();
            }
            Console.WriteLine("Would you like to ask another question? Y/N.");
            input = Console.ReadLine();

            if (input == "Y")
            {
                AskQuestion();
            }
            else if (input == "N")
            {
                Console.WriteLine("Where would you like to go? \n 1. Question screen. \n 2. Commenting screen. \n 3. Home. ");
                input = Console.ReadLine();
                if (input == "1")
                {
                    QuestionScreen();
                }
                else if (input == "2")
                {
                    CommentChoice();
                }
                else if (input == "3")
                {
                    //CustomerNavigation(user);
                }
            }
        }

        public static void loadFilmTitles()
        {
            //readfile
            List<string> filmTitles = new List<string>();
            StreamReader sr = new StreamReader(@"filmlist.txt");
            while (sr.EndOfStream == false)
            {
                i = 0;
                data = new string[8];
                while ((line = sr.ReadLine()) != "")
                {
                    data[i] = line;
                    i++;
                }
                Console.WriteLine(data[0]);
            }
            sr.Close();
        }
    }
}

