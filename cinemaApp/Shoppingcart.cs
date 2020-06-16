using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
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
            data = null;
            ShoppingcartConverter(user);
            //seeCart(user);
            ShoppingcartShowItems(user);
            
            //double totalPrice = 0;
            //Console.WriteLine("------------------\nYour total price: $"+ totalPrice + "\n------------------");
            Console.WriteLine("\nPlease pick an option.\n[1] Checkout.\n[2] Remove item from shoppingcart.\n[3] Continue shopping.\n[4] Back.");
            int choice = Program.ChoiceInput(0, 4);
            while(choice != 0) {
                if (choice == 1)
                {
                    if (data == null || data.Length == 0)
                    {
                        Console.WriteLine("\nSorry you don't have any items in your shoppingcart\nPlease come back when you have added an item to your shoppingcart.");
                        ShoppingcartNav(user);
                    }
                    else if (data.Length > 0 || data != null)
                    {
                        ShoppingcartShowItems(user);
                        Console.WriteLine("Are you sure you want to checkout with these items?\nYour total is: $"+ShoppingcartTotalPrice(user)+"\n[1] Yes.\n[2] No.");
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
                }
                else if (choice == 2)
                {
                    if (data == null || data.Length == 0)
                    {
                        Console.WriteLine("\nSorry you don't have any items in your shoppingcart\nPlease come back when you have added an item to your shoppingcart.");
                        ShoppingcartNav(user);
                    }                   
                    else if (data.Length > 0)
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
                }
                else if (choice == 3)
                {
                    Console.WriteLine("\nPlease pick an option.\n[1] Reserve tickets\n[2] Buy food.\n[3] Back.");
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
            ShoppingcartShowItems(user);
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
                if (user.username == "Guest") {
                    user.shoppingCart.Remove(data[choiceRemove]);
                }
            }
            //remove item from shopping cart
            data = data.Where(w => w != data[choiceRemove]).ToArray();
            string shoppingData = JsonConvert.SerializeObject(data);
            File.WriteAllText(filename, shoppingData);
            ShoppingcartNav(user);
        }
        public static void ShoppingcartConverter(User user)
        {
            string filename = $"{user.username}-ShoppingCart.json";
            if (user.username != "Guest")
            {
                if (File.Exists(@filename))
                {
                    string rawJSON = File.ReadAllText(filename);
                    data = JsonConvert.DeserializeObject<string[][]>(rawJSON);
                }
            }
            else
            {
                data = (user.shoppingCart).ToArray();
            }
        }

        static void ShoppingcartShowItems(User user)
        {
            if (data == null || data.Length == 0)
            {
                Console.WriteLine("--------------------------------------------------\nCurrently there are no items in your shoppingcart.\n--------------------------------------------------");
            }
            else if (data.Length > 0)
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
        }
        public static double ShoppingcartTotalPrice(User user)
        {
            double totalPrice = 0;
            double currentMoviePrice;
            double rest;
            List<String> reservatedMovies = new List<String>();
            int count = 3;
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (data[i][0] == user.username)
                    {
                        if (count < data[i].Length)
                        {
                            totalPrice += double.Parse(data[i][count]);
                            count += 5;
                        }
                    }
                    else if (j == 0)
                    {
                        Double.TryParse(data[i][0], out currentMoviePrice);
                        totalPrice += currentMoviePrice;

                    }
                    if (Double.TryParse(data[i][0], out rest))
                    {
                        reservatedMovies.Add(data[i][j]);
                    }
                    
                }
                count = 3;
            }
            return totalPrice;
        }
        public static void ShoppingcartCheckout(User user)
        {

            double totalPrice = ShoppingcartTotalPrice(user);
            int creditcardMaxNumbers = 5;
            bool creditcardCheck = false;
            string ticketData = "";
            Console.WriteLine("Your total is: $" + totalPrice + "\nPlease enter the 5 characters of your creditcard to complete the transaction.");
            var creditcardInput = Console.ReadLine();
            while (creditcardCheck == false)
            {
                if (int.TryParse(creditcardInput, out int integer) && creditcardInput.Length == creditcardMaxNumbers)
                {
                    //add to foodOrders
                    string filename2 = "allOrders.json";
                    string rawJSON = File.ReadAllText(filename2);
                    string[] data2 = JsonConvert.DeserializeObject<string[]>(rawJSON);
                    if (data2.Length == 0) {
                        data2 = new string[1];
                    }
                    for ( int i = 0; i < data.Length; i++) {
                        if (data[i][0] == user.username) {
                            string s = "";
                            for (int j = 0; j < data[i].Length; j++) {
                                s += data[i][j] + " ";
                            }
                            if (data2[0] == null) {
                                Array.Resize(ref data2, data2.Length + 1);
                            }
                            data2[data2.Length - 1] = s;
                            string newJSON = JsonConvert.SerializeObject(data2);
                            File.WriteAllText(filename2, newJSON);
                        } else {
                            for (int j = 0; j < data[i].Length; j++) {
                                ticketData += data[i][j] + " ";
                            } 
                            ticketData += " | ";
                            double TicketPrice;
                            double.TryParse(data[i][0], out TicketPrice);
                            SaleData.AddData(data[i][2], TicketPrice);
                        }
                    }
                    creditcardCheck = true;
                    //add to sale data

                }
                else
                {
                    Console.WriteLine("Invalid input, please try again.");
                    creditcardInput = Console.ReadLine();
                }
            }

            //Reservation code
            string reservationCode = "";
            if (ticketData != "") {
                using (var reader = new StreamReader(@"ReservationID.txt")) {
                    int R = 0;

                    while (!reader.EndOfStream) {
                        string line = reader.ReadLine();
                        R = Convert.ToInt32(line);
                        //checking last created reservationcode
                    }
                    R = R + 1;
                    //making it unique
                    reservationCode = R.ToString();
                }
                using (StreamWriter sw = File.AppendText(@"ReservationID.txt")) {
                    sw.Write(reservationCode);
                }
                using (StreamWriter sw = File.AppendText("ReservationCodes+TicketOrders.txt")) {
                    sw.WriteLine("[" + reservationCode + "] "+ ticketData);
                    sw.Close();
                }

                //foreach (var r in reservatedMovies)
                //{
                //  Console.WriteLine(r);
                //}
                Console.WriteLine("Thank you for your purchase!\nHere is your reservation code: {0}", reservationCode);
            }
            //clearing data list and deleting json shoppingcart file
            if (creditcardCheck == true)
            {
                Array.Clear(data, 0, data.Length);
                string filename = $"{user.username}-ShoppingCart.json";
                File.Delete(filename);
            }
            Navigation.CustomerNavigation(user);

            
        }

        static void RestoreSeatAvalibility(int choiceRemove) {
            List<string> timeTemplate = new List<string>() { "12:00", "14:00", "16:00", "18:00", "20:00", "22:00", "00:00" };
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
    }
}
        
