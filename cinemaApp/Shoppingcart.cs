using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cinemaApp
{
    public class Shoppingcart
    {
        
        public static void ShoppingcartNav(User user)
        {
            //Eerst current shoppingcart laten zien
            Console.WriteLine("\nPlease pick a option.\n[1] Checkout.\n[2] Edit shoppingcart.\n[3] Continue shopping.\n[4] Back.");
            int choice = Program.ChoiceInput(0, 4);
            while(choice != 0) {
                if (choice == 1)
                {
                    ShoppingcartCheckout(user);
                }
                else if (choice == 2)
                {

                }
                else if (choice == 3)
                {
                    Console.WriteLine("\nPlease pick a option.\n[1] Reserve tickets\n[2] Buy food.\n[3] Back.");
                    int choice2 = Program.ChoiceInput(0, 3);
                    while (choice2 != 0)
                    {
                        if (choice2 == 1)
                        {
                            ReserveTickets.ReserveTicketsMain(user);
                        }
                        else if (choice2 == 2)
                        {
                            Console.WriteLine("Buy food.");
                            CostumerFoodOrder.Costumer();
                        }
                        else if (choice2 == 3)
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
        static void ShoppingcartMain()
        {
            var ShoppingcartItems = new[] { "Film", "Burger", "Friet", "Water" };
            foreach (var item in ShoppingcartItems)
            {

            }

        }
        static void ShoppincartShowItems()
        {
            
            //code connecten aan reservetickets.savetoshoppingcart
        }
        static void ShoppingcartCheckout(User user)
        {
            float totalCheckout = 0f;
            int creditcardMaxNumbers = 5;
            bool creditcardCheck = false;
            int number;
            Console.WriteLine("Your total is: $" + totalCheckout.ToString() + "\nPlease enter the 5 characters of your creditcard to complete the transaction.");
            var creditcardInput = Console.ReadLine();
            while (creditcardCheck == false && creditcardInput.Length != creditcardMaxNumbers)
            {
                if (int.TryParse(creditcardInput, out number))
                {
                    creditcardCheck = true;
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("Not numbers or to short, please try again.");
                    creditcardInput = Console.ReadLine();
                }
            }
            Console.WriteLine("Thank you for your purchase!\nHere is your reservation code: (reservation code)");
            ShoppingcartNav(user);
        }
    }
}
        
