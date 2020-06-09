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
        static void ShoppingcartCheckout(User user)
        {
            double totalCheckout = 0;
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
                            count += 5;
                        }
                    }
                    else if (Double.TryParse(data[i][0],out totalCheckout))
                    {
                        totalCheckout += double.Parse(data[i][0]);
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
            Console.WriteLine("Thank you for your purchase!\nHere is your reservation code: (reservation code)");
            Navigation.CustomerNavigation(user);
        }
    }
}
        
