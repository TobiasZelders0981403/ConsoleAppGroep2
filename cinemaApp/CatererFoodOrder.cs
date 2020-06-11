using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cinemaApp
{
    class CatererFoodOrder
    {
        public List<FoodOrder> Orders;

        public CatererFoodOrder(List<FoodOrder> orders)
        {
            this.Orders = orders;
        }


        public void caterView()
        {
            Console.WriteLine("\n[1] Uncompleted orders\n[2] Completed orders\n[0] Exit...");
            var choice = Program.ChoiceInput(0, 2);

            if (choice == 1)
            {
                Console.WriteLine("\nOrders not completed:");
                foreach (var fo in Orders)
                {
                    if (!fo.Made)
                    {
                        Console.WriteLine("\nOrder id: " + fo.OrderId);
                        fo.displayTime();
                        Console.WriteLine(fo.Order);
                    }
                }
            }

            else if (choice == 2)
            {
                Console.WriteLine("\nOrders completed:");
                foreach (var fo in Orders)
                {
                    if (fo.Made)
                    {
                        Console.WriteLine("\nOrder id: " + fo.OrderId);
                        fo.displayTime();
                        Console.WriteLine(fo.Order);
                    }
                }
            }
        }


        public void isDone()
        {
            Console.WriteLine("\nEnter Order Id: ");
            var idDelete = Console.ReadLine();
            int id;
            bool sucess = Int32.TryParse(idDelete, out id);

            if ((sucess) & (id <= ID.orderMax()) & (id >= 0))
            {
                Console.WriteLine();
                foreach (var f in Orders)
                {
                    if (f.OrderId == id)
                    {
                        f.Made = true;
                    }
                }
            }
        }

        public void pickedUp()
        {
            Console.WriteLine("\nEnter Order Id: ");
            var idDelete = Console.ReadLine();
            int id;
            bool sucess = Int32.TryParse(idDelete, out id);

            if ((sucess) & (id <= ID.orderMax()) & (id >= 0))
            {
                Console.WriteLine();
                FoodOrder toRemove = null;
                foreach (var f in Orders)
                {
                    toRemove = f;
                }
                Orders.Remove(toRemove);
            }
        }

        public static void Caterer()
        {
            bool busy = true;
            while (busy)
            {
                string fileName = "allOrders.json";
                string rawJson = File.ReadAllText(fileName);
                string[] allOrders = JsonConvert.DeserializeObject<string[]>(rawJson);
                Console.WriteLine("\n[1] View Orders\n[2] Remove a picked up order\n[0] Exit....");
                var choice = Program.ChoiceInput(0, 3);
                if (choice == 0)
                {
                    busy = false;
                    break;
                }
                else if (choice == 1)
                {
                    if (allOrders.Length != 0) {
                        for (int i = 0; i < allOrders.Length; i++) {
                            Console.WriteLine($"[{i}] {allOrders[i]}");
                        }
                    } else {
                        Console.WriteLine("\nThere are no orders.");
                    }
                }
                else if (choice == 2)
                {
                    if (allOrders.Length != 0) {
                        Console.WriteLine("\nWhat item would you like to remove?");
                        for (int i = 0; i < allOrders.Length; i++) {
                            Console.WriteLine($"[{i}] {allOrders[i]}");
                        }
                        int removeChoice = Program.ChoiceInput(0, allOrders.Length - 1);
                        List<string> list = new List<string>(allOrders);
                        list.RemoveAt(removeChoice);
                        rawJson = JsonConvert.SerializeObject(list.ToArray());
                        if (list.Count == 0) {
                            rawJson = "[]";
                        }
                        File.WriteAllText(fileName, rawJson);
                    } else {
                        Console.WriteLine("\nThere are no orders.");
                    }
                }
            }
        }
    }
}

