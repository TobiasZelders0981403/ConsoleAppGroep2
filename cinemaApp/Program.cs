using System;
using System.Xml;

namespace cinemaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cinema Application.";
            User user = new User();

            Console.WriteLine("Please pick an option.\n[1] Login.\n[2] Register.\n[3] Continue as guest.\n[0] Exit");
            int choice = ChoiceInput(0, 3);
            if (choice == 1) {
               Login(user);
            } else if (choice == 2) {
                user.CreateAccount();
            } else if (choice == 3) {
                user.ContinueAsGuest();
            } else if (choice == 0) {
                Environment.Exit(0);
            }

            if (user.username == "owner") {
                Navigation.OwnerNavigation();
            } else if (user.username == "manager") {
                Navigation.ManagerNavigation();
            } else if (user.username == "caterer") {
                Navigation.CatererNavigation();
            } else {
                Navigation.CustomerNavigation(user);
            }
        }

        static void Login(User user)
        {
                user.VerifyLogin();
            if (user.accountVerified == false) {
                Console.WriteLine("\nIncorrect! Please pick an option\n[1] Try again.\n[2] Continue as guest.\n[3] Register a new account.\n[0] Exit");
                int loginChoice = ChoiceInput(0, 3);
                if (loginChoice == 0) {
                    Environment.Exit(0);
                } else if (loginChoice == 1) {
                    Login(user);
                } else if (loginChoice == 2) {
                    user.ContinueAsGuest();
                } else if (loginChoice == 3) {
                    user.CreateAccount();
                }
            }
        }

        public static int ChoiceInput(int min, int max) {
            int choice;
            string choiceInput = Console.ReadLine();
            int.TryParse(choiceInput, out choice);
            while (max < choice || choice < min || string.IsNullOrWhiteSpace(choiceInput) || !int.TryParse(choiceInput, out choice)) {
                Console.WriteLine("Invalid Input! Please enter your option:");
                choiceInput = Console.ReadLine();
                int.TryParse(choiceInput, out choice);
            }
            return choice;
        }

        public static double DoubleInput(double min, double max) {
            double choice;
            string choiceInput = Console.ReadLine();
            double.TryParse(choiceInput, out choice);
            while (max < choice || choice < min || string.IsNullOrWhiteSpace(choiceInput) || !double.TryParse(choiceInput, out choice)) {
                Console.WriteLine("Invalid Input! Please enter your option:");
                choiceInput = Console.ReadLine();
                double.TryParse(choiceInput, out choice);
            }
            return choice;
        }

        public static string StringCheck() {
            string input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input)) {
                Console.WriteLine("Invalid Input! Please try again.");
                input = Console.ReadLine();
            }
            return input;
        }
    }
}
