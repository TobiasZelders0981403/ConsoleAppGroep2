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

        public void checkout(User user)
        {
            //collect orders of username
            string username = user.username;
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

            Console.WriteLine("Choose your bank:\n[1] ABN AMRO\n[2] ING\n[3] Rabobank\n[4] Paypal\n[0] Exit..");
            var bank = Program.ChoiceInput(0, 4);

            if (bank == 0)
            {

            }
            else if (bank == 1)
            {
                Console.Write("NL02 ABNA ");
                Console.ReadLine();
                Console.WriteLine("Confirming purchase...");
            }
            else if (bank == 2)
            {
                Console.Write("NL38 INGB ");
                Console.ReadLine();
                Console.WriteLine("Confirming purchase...");
            }
            else if (bank == 3)
            {
                Console.Write("NL30 RABO ");
                Console.ReadLine();
                Console.WriteLine("Confirming purchase...");
            }
            else if (bank == 4)
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

        public void seeCart(User user)
        {
            string username = user.username;
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

        public void Remove(User user)
        {
            string username = user.username;
            List<FoodOrder> userOrders = new List<FoodOrder>();
            foreach (var order in Orders)
            {
                if (order.UserName == username && order.Made == false)
                {
                    userOrders.Add(order);
                }
            }

            seeCart(user);
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
                TheMenu.overview();
                Console.WriteLine("\nHi, what do you wanna do?\n[1] See menu\n[2] Search menu\n[3] See what's in your cart\n[4] New food order\n[5] Checkout\n[0] Exit....");
                var choice = Program.ChoiceInput(0, 5);
                if (choice == 0)
                {
                    busy = false;
                    break;
                }
                else if (choice == 1)
                {
                    TheMenu.overview();
                }
                else if (choice == 2)
                {
                    TheMenu.search();
                }
                else if (choice == 3)
                {
                    allOrders.seeCart(user);
                }
                else if (choice == 4)
                {
                    all.Add(FoodOrder.Add(user));
                    allOrders = new CostumerFoodOrder(all);

                }
                else if (choice == 5)
                {
                    allOrders.checkout(user);
                }
            }
            string newJson = JsonConvert.SerializeObject(all);
            File.WriteAllText(fileName, newJson);
        }
    }
}
