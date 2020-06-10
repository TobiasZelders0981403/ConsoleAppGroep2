using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentManager
{
    class CommentManager
    {
        static string username;
        static string input;
        static string title;
        static string question;
        static string questionlist;
        static string answer;
        private static int i;
        private static string line;
        private static string[] data;

        public static void CoMaMain()
        {
            CoMaChoice();
        }
        public static void CoMaChoice()
        {
            Console.WriteLine("Where would you like to go? \n 1. Reviews \n 2. Questions. \n 3. Back.");
            input = Console.ReadLine();
            CoMaAction();
        }
        public static void CoMaAction()
        {
            if (input == "1")
            {
                MaReview();
            }
            else if (input == "2")
            {
                MaQuestions();
            }
            else if (input == "3")
            {
                OwnerNavigation();
            }
        }
        public static void MaReview()
        {
            Console.WriteLine("What would you like to do? \n 1. Read reviews. \n 2. Back.");
            input = Console.ReadLine();

            if (input == "1")
            {
                ReviewRead();
            }
            
            else if (input == "2")
            {
                CoMaMain();
            }
        }
        public static void ReviewRead()
        {
            Console.WriteLine("Which movies would you like to read the reviews of?");
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
                ReviewRead();
            }
            else if (input == "N")
            {
                Console.WriteLine("Where would you like to go? \n 1. Review screen. \n 2. Commenting screen. \n 3. Home. ");
                input = Console.ReadLine();
                if (input == "1")
                {
                    MaReview();
                }
                else if (input == "2")
                {
                    MaQuestions();
                }
                else if (input == "3")
                {
                    OwnerNavigation();
                }
            }
        }
       
        public static void MaQuestions()
        {
            Console.WriteLine("What would you like to do? \n 1. Read questions. \n 2. Answer questions. \n 3. Back.");
            input = Console.ReadLine();

            if (input == "1")
            {
                Qread();
            }
            else if (input == "2")
            {
                Qanswer();
            }
            else if (input == "3")
            {
                CoMaMain();
            }
        }
            public static void Qread()
            {
            using (StreamReader sr = new StreamReader("questions.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }

                MaQuestions();
            }

        }
        public static void Qanswer()
        {
            loadQuestions();
            Console.WriteLine("Which question would you like to answer?");
            question = Console.ReadLine();

            questionlist = File.ReadAllText(@"questions.txt");
            int index = questionlist.IndexOf(question) + question.Length + 1;

            Console.WriteLine("Write your answer.");
            answer = Console.ReadLine();
            questionlist = questionlist.Insert(index, "\n" + "A: " + answer);
            File.WriteAllText(@"questions.txt", questionlist);
        }

        private static void loadQuestions()
        {
            using (StreamReader sr = new StreamReader(@"questions.txt"))
            {
                while (sr.EndOfStream == false) {
                    
                    i = 0;
                    data = new string[8];
                    while ((line = sr.ReadLine()) != "")
                    {
                        data[i] = line;
                        
                        i++;
                        
                    }
                    Console.WriteLine(data[0]);
                    
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