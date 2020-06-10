using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace cinemaApp
{
    public class Shoppingcart
    {
        static string[][] data;

        public static void ShoppingcartNav(User user)
        {
            ShoppincartConverter(user);
            //seeCart(user);
            ShoppincartShowItems(user);

            Console.WriteLine("\nPlease pick a option.\n[1] Checkout.\n[2] Remove item from shoppingcart.\n[3] Continue shopping.\n[4] Back.");
            int choice = Program.ChoiceInput(0, 4);
            while(choice != 0) {
                if (choice == 1)
                {
                    if (data.Length > 0)
                    {
                        ShoppincartShowItems(user);
                        Console.WriteLine("Are you sure you want to checkout with these items?\n[1]Yes.\n[2] No.");                        
                        int choiceCreditConformation = Program.ChoiceInput(0, 2);
                        while (choiceCreditConformation != 0)
                        {
                            if (choiceCreditConformation == 1)
                            {
                                ShoppingcartCheckout(user);
                            }
                            else if (choiceCreditConformation == 2)
                            {
                                ShoppingcartNav(user);
                            }
                        }                       
                    }
                    else
                    {
                        Console.WriteLine("\nSorry you don't have any items in your shoppingcart\nPlease come back when you have added an item to your shoppincart.");
                        ShoppingcartNav(user);
                    }
                }
                else if (choice == 2)
                {
                    if (data.Length > 0)
                    {
                        Console.WriteLine("Are you sure you want to remove an item of your shoppingcart?\n[1] Yes.\n[2] No.");
                        int choiceItemRemoveConformation = Program.ChoiceInput(0, 2);
                        while (choiceItemRemoveConformation != 0)
                        {
                            if (choiceItemRemoveConformation == 1)
                            {
                                ShoppingcartRemove(user);
                            }
                            else if (choiceItemRemoveConformation == 2)
                            {
                                ShoppingcartNav(user);
                            }
                        }                          
                    }
                    else
                    {
                        Console.WriteLine("\nSorry you don't have any items in your shoppingcart\nPlease come back when you have added an item to your shoppincart.");
                        ShoppingcartNav(user);
                    }
                }
                else if (choice == 3)
                {
                    Console.WriteLine("\nPlease pick a option.\n[1] Reserve tickets\n[2] Buy food.\n[3] Back.");
                    int choiceContinueShopping = Program.ChoiceInput(0, 3);
                    while (choiceContinueShopping != 0)
                    {
                        if (choiceContinueShopping == 1)
                        {
                            ReserveTickets.ReserveTicketsMain(user);
                        }
                        else if (choiceContinueShopping == 2)
                        {
                            CostumerFoodOrder.Costumer(user);
                        }
                        else if (choiceContinueShopping == 3)
                        {
                            Shoppingcart.ShoppingcartNav(user);
                        }
                    }
                }
                else if (choice == 4)
                {
                    Navigation.CustomerNavigation(user);
                }  
            }
        }


        public static void ShoppingcartRemove(User user)
        {
            ShoppincartShowItems(user);
            Console.WriteLine("\nPlease pick an item to remove.\n");
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine("[{0}]",i+1);
            }
            string filename = $"{user.username}-ShoppingCart.json";
            int choiceRemove = Program.ChoiceInput(1, data.Length) - 1;
            //reset removed seats
                //check if it is a movie
            if (data[choiceRemove][0] != user.username) {
                RestoreSeatAvalibility(choiceRemove);
            }
            //remove foodOrder for caterer

            //remove item from shopping cart
            data = data.Where(w => w != data[choiceRemove]).ToArray();
            string shoppingData = JsonConvert.SerializeObject(data);
            File.WriteAllText(filename, shoppingData);
            ShoppingcartNav(user);
        }
        static void ShoppincartConverter(User user)
        {
            string filename = $"{user.username}-ShoppingCart.json";
            if (user.username != "Guest")
            {
                if (File.Exists(@filename))
                {
                    string rawJSON = File.ReadAllText(filename);
                    data = JsonConvert.DeserializeObject<string[][]>(rawJSON);
                }
                else {
                    //user.shoppingCart;
                }

            }
        }

        static void ShoppincartShowItems(User user)
        {

            if (data.Length > 0)
            {
                Console.WriteLine("\nYour current shoppingcart:\n____________________________________");
                for (int i = 0; i < data.Length; i++)
                {
                    Console.WriteLine("\n{0}.", i + 1);
                    Console.WriteLine("---------------");
                    for (int j = 0; j < data[i].Length; j++)
                    {
                        Console.WriteLine(data[i][j]);
                    }
                }
                Console.WriteLine("____________________________________");
            }
            else
            {
                Console.WriteLine("--------------------------------------------------\nCurrently there are no items in your shoppingcart.\n--------------------------------------------------");
            }

        }
        /*
        public static void ReservationCode(User user)
        {
            int reservationCode = 0;

            using (var reader = new StreamReader("ReseravationCodes+TicketOrders.txt"))
            {
                int R = 0;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    R = Convert.ToInt32(line);
                    //checking last created reservationcode
                }
                R = R + 1;
                //making it unique
                reservationCode = R;
            }
            using (StreamWriter sw = File.AppendText("ReseravationCodes+TicketOrders.txt"))
            {
                sw.WriteLine(reservationCode);
                sw.WriteLine(reservatedMovies);
                sw.Close();
            }



            Console.WriteLine("\n\nyour reservationcode is: " + reservationCode);

        }
        */
        public static void ShoppingcartCheckout(User user)
        {
            double totalCheckout = 0;
            double currentMoviePrice;
            double rest;
            List<String> reservatedMovies = new List<String>();
            int count = 4;
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (data[i][0] == user.username)
                    {
                        if (count < data[i].Length)
                        {
                            totalCheckout += double.Parse(data[i][count]);
                            count += 3;
                        }
                    }
                    else if (j==0)
                    {
                        Double.TryParse(data[i][0], out currentMoviePrice);
                        totalCheckout += currentMoviePrice;

                    }
                    if (Double.TryParse(data[i][0], out rest))
                    {
                        reservatedMovies.Add(data[i][j]);
                    }
                }

            }
            Console.WriteLine(totalCheckout);

            int creditcardMaxNumbers = 5;
            bool creditcardCheck = false;
            Console.WriteLine("Your total is: $" + totalCheckout.ToString() + "\nPlease enter the 5 characters of your creditcard to complete the transaction.");
            var creditcardInput = Console.ReadLine();
            while (creditcardCheck == false)
            {
                if (int.TryParse(creditcardInput, out int integer) && creditcardInput.Length == creditcardMaxNumbers)
                {
                    creditcardCheck = true;
                    Console.WriteLine("True");
                }
                else
                {
                    Console.WriteLine("Invalid input, please try again.");
                    creditcardInput = Console.ReadLine();
                }
            }

            int reservationCode = 0;

            using (var reader = new StreamReader("ReseravationCodes+TicketOrders.txt"))
            {
                int R = 0;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    R = Convert.ToInt32(line);
                    //checking last created reservationcode
                }
                R = R + 1;
                //making it unique
                reservationCode = R;
            }
            using (StreamWriter sw = File.AppendText("ReseravationCodes+TicketOrders.txt"))
            {
                sw.WriteLine(reservationCode);
                sw.WriteLine(reservatedMovies);
                sw.Close();
            }

            Console.WriteLine("Thank you for your purchase!\nHere is your reservation code: {0}", reservationCode);
            Navigation.CustomerNavigation(user);
        }

        static void RestoreSeatAvalibility(int choiceRemove) {
            List<string> timeTemplate = new List<string>() { "12:00", "14:00", "16:00", "18:00", "20:00", "22:00", "24:00" };
            string seatFileName = data[choiceRemove][2] + "/" + timeTemplate.IndexOf(data[choiceRemove][3]) + "-Seats.txt";
            //read file
            StreamReader streamreader = new StreamReader(seatFileName);
            string line;
            int index = 0;
            string[][] RoomSeats = new string[50][];
            while (!string.IsNullOrWhiteSpace(line = streamreader.ReadLine())) {
                string[] components = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                RoomSeats[index] = components;
                index++;
            }
            streamreader.Close();
            int row, seat;
            int.TryParse(data[choiceRemove][5], out row);
            int.TryParse(data[choiceRemove][6], out seat);
            RoomSeats[row][seat] = "0";

            //save seat
            StreamWriter streamwriter = new StreamWriter(seatFileName);
            string s = "";
            int i;
            for (i = 0; i < 10; i++) {
                for (int j = 0; j < 15; j++) {
                    s += RoomSeats[i][j] + " ";
                }
                s += "\n";
            }
            for (i = 10; i < 25; i++) {
                for (int j = 0; j < 20; j++) {
                    s += RoomSeats[i][j] + " ";
                }
                s += "\n";
            }
            for (i = 25; i < 50; i++) {
                for (int j = 0; j < 25; j++) {
                    s += RoomSeats[i][j] + " ";
                }
                if (i != 49) {
                    s += "\n";
                }
            }
            streamwriter.WriteLine(s);
            streamwriter.Close();
        }
        public static void seeCart(User user)
        {
            string fileName = "allOrders.json";
            string rawJson = File.ReadAllText(fileName);
            List<FoodOrder> all = JsonConvert.DeserializeObject<List<FoodOrder>>(rawJson);
            var allOrders = new CostumerFoodOrder(all);

            string username = user.username;
            List<FoodOrder> userOrders = new List<FoodOrder>();
            foreach (var order in all)
            {
                if (order.UserName == username && !order.Paid && !order.Made)
                {
                    userOrders.Add(order);
                }
            }



            Console.WriteLine("\nIn your cart");
            foreach (var fo in userOrders)
            {
                Console.WriteLine("\nOrder id: " + fo.OrderId);
                fo.displayTime();
                fo.Order.payOverview();
            }



        }

    }
}
        
