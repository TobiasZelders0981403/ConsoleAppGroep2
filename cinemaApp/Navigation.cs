using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace cinemaApp {
    class Navigation {

        public static void OwnerNavigation() {
            Console.WriteLine("\nPlease pick an option.\n[1] Look at sale data.\n[2] Adjust seat prices.\n[3] Add, edit or remove Films\n[4] See all Reservations.\n[5] Look at the expected amount of customers.\n[0] Exit application.");
            int choice = Program.ChoiceInput(0,5);
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
                    //see all reservations
                    Console.WriteLine("NOT IMPLEMENTED YET");
                } else if (choice == 5) {
                    //look at expected customers
                    expected.ExpectedCustomers();
                }
                Console.WriteLine("\nPlease pick an option.\n[1] Look at sale data.\n[2] Adjust seat prices.\n[3] Add, edit or remove Films\n[4] See all Reservations.\n[5] Look at the expected amount of customers.\n[0] Exit application.");
                choice = Program.ChoiceInput(0, 5);
            }
            Environment.Exit(0);
            //MUST HAVES
            //see all reservations

            //COULD
            //reply to comments
        }

        public static void ManagerNavigation() {
            Console.WriteLine("\nPlease pick an option.\n[1] Look at sale data.\n[2] Input sale data.\n[3] See all reservations.\n[4] Look at the expected amount of customers.\n[0] Exit application.");
            int choice = Program.ChoiceInput(0, 4);
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
                } else if (choice == 4) {
                    //look at expected customers
                    expected.ExpectedCustomers();
                }
                Console.WriteLine("\nPlease pick an option.\n[1] Look at sale data.\n[2] Input sale data.\n[3] See all reservations.\n[4] Look at the expected amount of customers.\n[0] Exit application.");
                choice = Program.ChoiceInput(0, 4);
            }
            Environment.Exit(0);
        }

        public static void CatererNavigation() {
            Console.WriteLine("\nPlease pick an option.\n[1] Adjust / look at the menu.\n[2] Look at all orders.\n[3] Look at the expected amount of customers.\n[0] Exit application.");
            int choice = Program.ChoiceInput(0, 3);
            while (choice != 0) {
                if (choice == 1) {
                    //adjust menu
                    FoodMenu.Caterer();
                } else if (choice == 2) {
                    //Look at all orders
                    CatererFoodOrder.Caterer();
                } else if (choice == 3) {
                    //look at expected customers
                    expected.ExpectedCustomers();
                }
                Console.WriteLine("\nPlease pick an option.\n[1] Adjust / look at the menu.\n[2] Look at all orders.\n[3] Look at the expected amount of customers.\n[0] Exit application.");
                choice = Program.ChoiceInput(0, 3);
            }
            //MUST HAVES
            //Add, edit or remove products from menu
            //see all food orders
        }

        public static void CustomerNavigation(User user) {
            Console.WriteLine("\nPlease pick an option.\n[1] Look at all movies.\n[2] Search by genre.\n[3] Look at future movies.\n[4] Reserve tickets.\n[5] Order food.\n[6] Go to shopping cart.\n[7] VIP.\n[0] Exit application.");
            int choice = Program.ChoiceInput(0, 7);
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
                    //reserve tickets
                    ReserveTickets.ReserveTicketsMain(user);
                } else if (choice == 5) {
                    //order food
                    CostumerFoodOrder.Costumer(user);
                } else if (choice == 6) {
                    //shopping cart
                    Shoppingcart.ShoppingcartNav(user);
                    Console.WriteLine("NOT IMPLEMENTED YET");
                } else if (choice == 7) { 
                    //membership
                    VIP.Membership(user);
                }

                Console.WriteLine("\nPlease pick an option.\n[1] Look at all movies.\n[2] Search by genre.\n[3] Look at future movies.\n[4] Reserve tickets.\n[5] Order food.\n[6] Go to shopping cart.\n[0] Exit application.");
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
