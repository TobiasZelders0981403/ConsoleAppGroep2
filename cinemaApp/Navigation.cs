using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace cinemaApp {
    class Navigation {
        //navigation system for the owner with all its options.
        public static void OwnerNavigation() {
            Console.WriteLine("\nPlease pick an option.\n[1] Look at sale data.\n[2] Adjust seat prices.\n[3] Show all movies.\n[4] Add, edit or remove Films\n[5] Show all upcoming movies.\n[6] Add, edit or remove upcoming Films\n[7] See all Reservations.\n[8] Look at the expected amount of customers.\n[9] Go to the comments / reviews.\n[0] Exit application.");
            int choice = Program.ChoiceInput(0,9);
            while (choice != 0) {
                if (choice == 1) {
                    //look at sale data
                    SaleData.ShowSaleData();
                } else if (choice == 2) {
                    //adjust seat prices
                    Rooms.Manager();
                } else if (choice == 3) {
                    //show all movies
                    Console.WriteLine("\n" + File.ReadAllText(@"filmlist.txt"));
                } else if (choice == 4) {
                    //add, edit or remove films
                    Films.FilmMain();
                } else if (choice == 5) {
                    Console.WriteLine("\n" + File.ReadAllText(@"futurefilm.txt"));
                } else if (choice == 6) {
                    //add, edit or remove Future films
                    FutureFilm.FutureMain();
                } else if (choice == 7) {
                    //see all reservations
                    Reservations.ShowReservations();
                } else if (choice == 8) {
                    //look at expected customers
                    expected.ExpectedCustomers();
                } else if (choice == 9) {
                    //go to the comments / revies
                    OwnerComment.CoMaMain();
                }
                Console.WriteLine("\nPlease pick an option.\n[1] Look at sale data.\n[2] Adjust seat prices.\n[3] Show all movies.\n[4] Add, edit or remove Films\n[5] Show all upcoming movies.\n[6] Add, edit or remove upcoming Films\n[7] See all Reservations.\n[8] Look at the expected amount of customers.\n[9] Go to the comments.\n[0] Exit application.");
                choice = Program.ChoiceInput(0, 9);
            }
            Environment.Exit(0);
        }

        public static void ManagerNavigation() {
            //navigation system for the manager with all its options.
            Console.WriteLine("\nPlease pick an option.\n[1] Look at sale data.\n[2] Input sale data.\n[3] See all reservations.\n[4] Look at the expected amount of customers.\n[0] Exit application.");
            int choice = Program.ChoiceInput(0, 4);
            while (choice != 0) {
                if (choice == 1) {
                    //show sale data
                    SaleData.ShowSaleData();
                } else if (choice == 2) {
                    //input sale data
                    SaleData.SaleDatainput();
                } else if (choice == 3) {
                    //see all reservations
                    Reservations.ReservationMain();
                } else if (choice == 4) {
                    //look at expected customers
                    expected.ExpectedCustomers();
                }
                Console.WriteLine("\nPlease pick an option.\n[1] Look at sale data.\n[2] Input sale data.\n[3] See all reservations.\n[4] Look at the expected amount of customers.\n[0] Exit application.");
                choice = Program.ChoiceInput(0, 4);
            }
            //see al reservations
            Environment.Exit(0);
        }

        public static void CatererNavigation() {
            //navigation system for the caterer with all its options.
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
        }

        public static void CustomerNavigation(User user) {
            //navigation system for the cystomer with all its options.
            Console.WriteLine("\nPlease pick an option.\n[1] Look at all movies.\n[2] Search by genre.\n[3] Look at upcoming movies.\n[4] Reserve tickets.\n[5] Order food.\n[6] Go to comments. \n[7] Go to shopping cart.\n[0] Exit application.");
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
                    Filters.FutureMovies(user);
                } else if (choice == 4) {
                    //reserve tickets
                    ReserveTickets.ReserveTicketsMain(user);
                } else if (choice == 5) {
                    //order food
                    CostumerFoodOrder.Costumer(user);
                } else if (choice == 6) {
                    CommentSystem.CommentMain(user);
                } else if (choice == 7) {
                    //shopping cart
                    Shoppingcart.ShoppingcartNav(user);
                }
                Console.WriteLine("\nPlease pick an option.\n[1] Look at all movies.\n[2] Search by genre.\n[3] Look at upcoming movies.\n[4] Reserve tickets.\n[5] Order food.\n[6] Go to comments. \n[7] Go to shopping cart.\n[0] Exit application.");
                choice = Program.ChoiceInput(0, 7);
            }
            Environment.Exit(0);
        }

    }
}
