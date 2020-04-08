using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films2_0
{
    class Films
    {
        static string username;
        static string input;
        static string input2;
        static string filmName;
        static string filmDescription;
        static string Genre;
        static string Release;
        static string Time;
        static string filmList;
        static string Replacement;
        static string Edit;
        static string FullList;
        static string Delete;
        static int BIndex;
        static int EIndex;

        public static void FilmMain()
        {
            MovieChoice();
        }

        static void MovieChoice()
        {
            Console.WriteLine("What would you like to do? Please type 1-3 and press Enter to continue.");
            Console.WriteLine("1: Add film.");
            Console.WriteLine("2: Edit film.");
            Console.WriteLine("3: Remove film.");

            input = Console.ReadLine();
            ChoiceAction();
        }

        static void ChoiceAction()
        {
            if (input == "1") {
                AddMovie();
            }
            else if (input == "2") {
                EditMovie();
            }
            else if (input == "3"){
                RemoveMovie();
            }
            else {
                MovieChoice();
            }
        }

        static void AddMovie()
        {
            Console.WriteLine("Enter film name:");
            filmName = Console.ReadLine();
            Console.WriteLine("Enter film description:");
            filmDescription = Console.ReadLine();
            Console.WriteLine("Enter film genre:");
            Genre = Console.ReadLine();
            Console.WriteLine("Enter release date:");
            Release = Console.ReadLine();
            Console.WriteLine("Enter time:");
            Time = Console.ReadLine();
            filmList += filmName + "\n" + filmDescription + "\n" + Genre + "\n" + Release + "\n" + Time + "\n";

            StreamWriter sw = new StreamWriter(@"filmlist.txt", append: true);
                sw.WriteLine(filmList);
                sw.Close();
            filmList = "";
            FullList = "\n" + File.ReadAllText(@"filmlist.txt");
            Console.WriteLine(FullList);

            Console.WriteLine("Would you like to continue? Y/N?");
                input = Console.ReadLine();
            if (input == "Y")
            {
                AddMovie();
            }
            else if (input == "N") {
                Console.WriteLine("Would you like to do something else? Y/N?");
                input = Console.ReadLine();
                if (input == "Y")
                {
                    MovieChoice();
                }
            }
        }

        static void EditMovie()
        {
            Edit = File.ReadAllText(@"filmlist.txt");
            Console.WriteLine(Edit);
            Console.WriteLine("What would you like to edit?");
            input = Console.ReadLine();
            Replacement = Edit.Replace(input, Console.ReadLine());
            StreamWriter sw = new StreamWriter(@"filmlist.txt");
            sw.WriteLine(Replacement);
            sw.Close();
            Edit = "";

            Console.WriteLine("Would you like to continue? Y/N?");
            input = Console.ReadLine();
            if (input == "Y")
            {
                EditMovie();
            }
            else if (input == "N")
            {
                Console.WriteLine("Would you like to do something else? Y/N?");
                input = Console.ReadLine();
                if (input == "Y")
                {
                    MovieChoice();
                }
            }
        }
        static void RemoveMovie()
        {
            Console.WriteLine("What movie would you like to remove?");
            Edit = File.ReadAllText(@"filmlist.txt");
            Console.WriteLine(Edit);
            input = Console.ReadLine();
            Console.WriteLine("Until where would you like to remove? (Copy exact phrase)");
            input2 = Console.ReadLine();

            if (Edit.Contains(input))
            {
                BIndex = Edit.IndexOf(input);
                EIndex = Edit.IndexOf(input2, BIndex);
                Delete = Edit.Remove(BIndex, EIndex+6);
           
                StreamWriter sw = new StreamWriter(@"filmlist.txt");
                sw.WriteLine(Delete);
                sw.Close();
                Delete = "";
                BIndex = 0;
                EIndex = 0;
            }
            Console.WriteLine("Would you like to continue? Y/N?");
            input = Console.ReadLine();
            if (input == "Y")
            {
                RemoveMovie();
            }
            else if (input == "N")
            {
                Console.WriteLine("Would you like to do something else? Y/N?");
                input = Console.ReadLine();
                if (input == "Y")
                {
                    MovieChoice();
                }
            }
        }
    }
}
