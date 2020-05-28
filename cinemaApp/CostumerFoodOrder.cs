using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CinemaApp
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

        public  void checkout()
        {
            //collect orders of username
            string username = "default";
            List<FoodOrder> userOrders = new List<FoodOrder>();
            foreach (var order in Orders)
            {
                if (order.UserName == username)
                {
                    userOrders.Add(order);
                }
            }

        
            Console.WriteLine("In your cart");
            foreach (var order in userOrders)
            {
                order.Paid = true;
                order.displayTime();
                order.Order.payOverview();
            }

            Console.WriteLine("Choose your bank:\n1. ABN AMRO\n2. ING\n3. Rabobank\n4. Paypal");
            var bank = Console.ReadKey().Key;
            if(bank == ConsoleKey.D1)
            {
                Console.Write("NL02 ABNA ");
                Console.ReadLine();
                Console.WriteLine("Confirming purchase...");
            }
            else if( bank == ConsoleKey.D2)
            {
                Console.Write("NL38 INGB ");
                Console.ReadLine();
                Console.WriteLine("Confirming purchase...");
            }
            else if(bank == ConsoleKey.D3)
            {
                Console.Write("NL30 RABO ");
                Console.ReadLine();
                Console.WriteLine("Confirming purchase...");
            }
            else if(bank == ConsoleKey.D4)
            {
                Console.WriteLine("Email: ");
                Console.ReadLine();
                Console.WriteLine("Confirming purchase...");
            }
            else
            {
                Console.WriteLine("Checkout failed");
                foreach (var order in userOrders)
                {
                    order.Paid = false;
                }
            }
            

        }

        public  void seeCart()
        {
            string username = "default";
            List<FoodOrder> userOrders = new List<FoodOrder>();
            foreach (var order in Orders)
            {
                if (order.UserName == username)
                {
                    userOrders.Add(order);
                }
            }

            Console.WriteLine("\nIn your cart");
            foreach (var o in userOrders)
            {
                o.displayTime();
                o.Order.payOverview();
            }
           
        }

        public void Remove()
        {
            string username = "default";
            List<FoodOrder> userOrders = new List<FoodOrder>();
            foreach (var order in Orders)
            {
                if (order.UserName == username)
                {
                    userOrders.Add(order);
                }
            }


        }

        public static void Costumer()
        {
            bool busy = true;
            var TheMenu = FoodMenu.getMenu();

            string fileName = "allOrders.json";
            string rawJson = File.ReadAllText(fileName);
            List<FoodOrder> all = JsonConvert.DeserializeObject<List<FoodOrder>>(rawJson);
            var allOrders = new CostumerFoodOrder(all);
            

            while (busy)
            {
                Console.WriteLine("\nHi, what do you wanna do?\n1. See menu\n2. Search menu\n3. See what's in your cart\n4. New food order\n5. Checkout\npress q to quit....");
                var choice = Console.ReadKey().Key;
                if (choice == ConsoleKey.Q)
                {
                    busy = false;
                    break;
                }
                else if (choice == ConsoleKey.D1)
                {
                    TheMenu.overview();
                }
                else if (choice == ConsoleKey.D2)
                {
                    TheMenu.search();
                }
                else if (choice == ConsoleKey.D3)
                {
                    allOrders.seeCart();
                }
                else if (choice == ConsoleKey.D4)
                {
                    all.Add(FoodOrder.Add());
                    allOrders = new CostumerFoodOrder(all);
                    
                }
                else if (choice == ConsoleKey.D5)
                {
                    allOrders.checkout();
                }
            }
            string newJson = JsonConvert.SerializeObject(all);
            File.WriteAllText(fileName, newJson);
        }
    }
}
