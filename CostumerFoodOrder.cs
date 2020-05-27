using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cinema_
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

            Console.WriteLine("In your cart");
            foreach (var o in userOrders)
            {
                o.Paid = true;
                o.Order.payOverview();
            }
        }

        public void Costumer()
        {
            bool busy = true;
            string fileName = "food.json";
            string rawJson = File.ReadAllText(fileName);
            List<Food> menu = JsonConvert.DeserializeObject<List<Food>>(rawJson);
            var TheMenu = new FoodMenu(menu);
            

            while (busy)
            {
                Console.WriteLine("\nHi, what do you wanna do?\n1. See menu\n2. Search menu\n3. See what's in your cart\n4. Add something to cart\n5. Checkout\npress q to quit....");
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
                    seeCart();
                }
                else if (choice == ConsoleKey.D4)
                {
                    //addFoodOrder();
                    string fName = "allOrders.json";
                    string rJson = File.ReadAllText(fName);
                    List<FoodOrder> all = JsonConvert.DeserializeObject<List<FoodOrder>>(rJson);
                    all.Add(FoodOrder.Add());
                    CostumerFoodOrder allOrders = new CostumerFoodOrder(all);
                    string newJson = JsonConvert.SerializeObject(all);
                    File.WriteAllText(fName, newJson);
                }
                else if (choice == ConsoleKey.D5)
                {
                    checkout();
                }
            }
        }
    }
}
