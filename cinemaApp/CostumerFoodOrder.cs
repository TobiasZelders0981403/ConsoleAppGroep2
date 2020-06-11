using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cinemaApp
{
        class CostumerFoodOrder
    {

        public List<FoodOrder> Orders;

        public CostumerFoodOrder(List<FoodOrder> orders)
        {
            this.Orders = orders;
        }

        public CostumerFoodOrder()
        {
            this.Orders = new List<FoodOrder>();
        }
        public static void Costumer(User user)
        {
            bool busy = true;
            var TheMenu = FoodMenu.getMenu();
            

            while (busy)
            {
                Console.WriteLine("\nHi, what do you wanna do?\n[1] See menu\n[2] Search menu\n[3] New food order\n[0] Exit...");
                var choice = Program.ChoiceInput(0, 5);
                if (choice == 0)
                {
                    busy = false;
                    Navigation.CustomerNavigation(user);
                    break;
                    
                }
                else if (choice == 1)
                {
                    TheMenu.viewCategory();
                }
                else if (choice == 2)
                {
                    TheMenu.search();
                }
                else if (choice == 3)
                {
                    FoodOrder.Add(user);
                }
            }
        }
    }
}
