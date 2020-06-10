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
        /*
        public void checkout()
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
            if (bank == ConsoleKey.D1)
            {
                Console.Write("NL02 ABNA ");
                Console.ReadLine();
                Console.WriteLine("Confirming purchase...");
            }
            else if (bank == ConsoleKey.D2)
            {
                Console.Write("NL38 INGB ");
                Console.ReadLine();
                Console.WriteLine("Confirming purchase...");
            }
            else if (bank == ConsoleKey.D3)
            {
                Console.Write("NL30 RABO ");
                Console.ReadLine();
                Console.WriteLine("Confirming purchase...");
            }
            else if (bank == ConsoleKey.D4)
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

        public void seeCart()
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
            foreach (var fo in userOrders)
            {
                Console.WriteLine("\nOrder id: " + fo.OrderId);
                fo.displayTime();
                fo.Order.payOverview();
            }

        }

        public void Remove()
        {
            string username = "default";
            List<FoodOrder> userOrders = new List<FoodOrder>();
            foreach (var order in Orders)
            {
                if (order.UserName == username && order.Made == false)
                {
                    userOrders.Add(order);
                }
            }

            seeCart();
            bool busy = true;
            while (busy)
            {
                Console.WriteLine("\nEnter the order id or press q to quit...");
                int userId;
                string idStr = Console.ReadLine();
                if (idStr == "q")
                {
                    busy = false;
                    break;
                }
                bool sucess = Int32.TryParse(idStr, out userId);

                FoodOrder temp = null;
                if ((sucess) & (userId <= userOrders.Count) & (userId >= 0))
                {
                    Console.WriteLine("You selected: ");
                    foreach (var fo in userOrders)
                    {
                        if (fo.OrderId == userId)
                        {
                            temp = fo;
                            Console.WriteLine("\nOrder id: " + fo.OrderId);
                            fo.displayTime();
                            fo.Order.overview();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Order not found");
                }
                Orders.Remove(temp);
            }
        }
        */
        public static void Costumer(User user)
        {
            bool busy = true;
            var TheMenu = FoodMenu.getMenu();

            string fileName = "allOrders.json";
            string rawJson = File.ReadAllText(fileName);
            List<FoodOrder> all = JsonConvert.DeserializeObject<List<FoodOrder>>(rawJson);
            var allOrders = new CostumerFoodOrder(all);


            while (busy)
            {
                Console.WriteLine("\nHi, what do you wanna do?\n[1] See menu\n[2] Search menu\n[3] New food order\n[0] Exit...");
                var choice = Program.ChoiceInput(0, 5);
                if (choice == 0)
                {
                    busy = false;
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
                    all.Add(FoodOrder.Add(user));
                    allOrders = new CostumerFoodOrder(all);
                }
            }
            string newJson = JsonConvert.SerializeObject(all);
            File.WriteAllText(fileName, newJson);
        }
    }
}
