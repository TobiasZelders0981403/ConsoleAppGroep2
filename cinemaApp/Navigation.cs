using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace cinemaApp {
    class Navigation {

        public static void OwnerNavigation() {
            Console.WriteLine("\nPlease pick a option.\n1: Look at sale data.\n2: Adjust seat prices.\n3: Add, Edit or Remove Films\n4: See all Reservations.\n0: Exit application.");
            int choice = Program.ChoiceInput(0,4);
            while (choice != 0) {
                if (choice == 1) {
                    //look at sale data
                    UserData.ShowSaleData();
                } else if (choice == 2) {
                    //adjust seat prices
                    Rooms.Manager();
                } else if (choice == 3) {
                    //add, edit or remove films
                    Films.FilmMain();
                } else if (choice == 4) {
                    //see amount of customers
                    Console.WriteLine("NOT IMPLEMENTED YET");
                }
                Console.WriteLine("\nPlease pick a option.\n1: Look at sale data.\n2: Adjust seat prices.\n3: Add, Edit or Remove Films\n4: See all Reservations.\n0: Exit application.");
                choice = Program.ChoiceInput(0, 4);
            }
            Environment.Exit(0);
            //MUST HAVES
            //see all reservations

            //SHOULD HAVES
            //see epected amount of customers

            //COULD
            //reply to comments
        }

        public static void ManagerNavigation() {
            Console.WriteLine("\nPlease pick a option.\n1: Look at sale data.\n2: Input sale data.\n3: See all reservations.\n0: Exit application.");
            int choice = Program.ChoiceInput(0, 3);
            while (choice != 0) {
                if (choice == 1) {
                    //show sale data
                    UserData.ShowSaleData();
                } else if (choice == 2) {
                    //input sale data
                    UserData.UserDataInput();
                } else if (choice == 3) {
                    //see all reservations
                    Console.WriteLine("NOT IMPLEMENTED YET");
                }
                Console.WriteLine("\nPlease pick a option.\n1: Look at sale data.\n2: Input sale data.\n3: See all reservations.\n0: Exit application.");
                choice = Program.ChoiceInput(0, 3);
            }
            Environment.Exit(0);

            //SHOULD HAVES
            //see amount of customers
        }

        public static void CatererNavigation() {
            Console.WriteLine("\nPlease pick a option.\n1: Adjust / look at the menu.\n2: Look at all orders.\n3 Look at the expected amount of customers.");
            int choice = Program.ChoiceInput(0, 3);
            while (choice != 0) {
                if (choice == 1) {
                    //adjust menu

                    jsonFood.jsonFoodMain();

                } else if (choice == 2) {
                    //Look at all orders
                    Console.WriteLine("NOT IMPLEMENTED YET");
                } else if (choice == 3) {
                    //look at expected customers
                    expected.ExpectedCustomers();
                }
                Console.WriteLine("\nPlease pick a option.\n1: Adjust / look at the menu.\n2: Look at all orders.\n3 Look at the expected amount of customers.");
                choice = Program.ChoiceInput(0, 3);
            }
                //MUST HAVES
                //see all food orders
                //Add, edit or remove products from menu

                //SHOULD HAVES
                //see expected amount of customers
            }

        public static void CustomerNavigation(User user) {
            Console.WriteLine("\nPlease pick a option.\n1: Look at all movies.\n2: Search by genre.\n3: Look at future movies.\n4: Order food\n5: Reserve tickets.\n6: Go to shopping cart.\n0: Exit application.");
            int choice = Program.ChoiceInput(0, 6);
            while (choice != 0) {
                if (choice == 1) {
                    //show all movies
                    Filters.AgeFilter(user);
                } else if (choice == 2) {
                    //search by genre
                    Filters.GenreFilter(user);
                } else if (choice == 3) {
                    //look at futute movies
                } else if (choice == 4) {
                    //order food
                } else if (choice == 5) {
                    //reserve tickets
                    ReserveTickets.ReserveTicketsMain(user);
                } else if (choice == 6) {
                    //shopping cart
                    Console.WriteLine("NOT IMPLEMENTED YET");
                }
                Console.WriteLine("\nPlease pick a option.\n1: Look at all movies.\n2: Search by genre.\n3: Look at future movies.\n4: Order food\n5: Reserve tickets.\n6: Go to shopping cart.\n0: Exit application.");
                choice = Program.ChoiceInput(0, 6);
            }
            Environment.Exit(0);
            //Must Haves
            //Look at Shopping cart

            //COULD HAVES
            //comment
        }

    }
}
