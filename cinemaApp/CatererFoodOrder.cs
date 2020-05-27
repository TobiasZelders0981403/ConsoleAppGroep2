using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema_
{
    class CatererFoodOrder
    {
        public List<FoodOrder> ToDo;
        public List<FoodOrder> Done;

        public CatererFoodOrder(List<FoodOrder> td, List<FoodOrder> d)
        {
            this.ToDo = td;
            this.Done = d;
        }

        public CatererFoodOrder(List<FoodOrder> td)
        {
            this.Done = new List<FoodOrder>();
        }

        public CatererFoodOrder()
        {
            this.ToDo = new List<FoodOrder>(); 
            this.Done = new List<FoodOrder>();
        }

        public void caterView()
        {
            Console.WriteLine("\nTo see uncompleted orders press 1" + "\nTo see completed orders press 2");
            var choice = Console.ReadKey().Key;

            if (choice == ConsoleKey.D1)
            {
                Console.WriteLine("\nOrders not completed:");
                foreach (var o in ToDo)
                {
                    if (o.Minute < 10)
                    {
                        Console.WriteLine("Time: " + o.Hour + ": 0" + o.Minute);
                    }
                    else
                    {
                        Console.WriteLine("Time: " + o.Hour + ":" + o.Minute);
                    }
                    o.Order.overview();
                    /*
                    foreach (var p in o.Order)
                    {
                        Console.WriteLine("Order Id: ", o.OrderId);
                        p.display();
                    }
                    */
                }
            }

            else if (choice == ConsoleKey.D2)
            {
                Console.WriteLine("\nOrders completed:");
                foreach (var o in Done)
                {
                    if (o.Minute < 10)
                    {
                        Console.WriteLine("Time: " + o.Hour + ": 0" + o.Minute);
                    }
                    else
                    {
                        Console.WriteLine("Time: " + o.Hour + ":" + o.Minute);
                    }
                    o.Order.overview();
                    /*
                    foreach (var p in o.Order)
                    {
                        Console.WriteLine("Order Id: ", o.OrderId);
                        p.display();
                    }
                    */
                }
            }
        }
        public void isDone()
        {
            Console.WriteLine("\nEnter Order Id: ");
            var idDelete = Console.ReadLine();
            List<FoodOrder> holder = new List<FoodOrder>();
            int id;
            bool sucess = Int32.TryParse(idDelete, out id);

            if ((sucess) & (id <= ToDo.Count) & (id >= 0))
            {
                Console.WriteLine();
                foreach (var f in ToDo)
                {
                    if (f.OrderId == id)
                    {
                        Done.Add(f);
                    }
                    if (f.OrderId != id)
                    {
                        holder.Add(f);
                    }
                }

                foreach (var g in holder)
                {
                    if (g.OrderId > id)
                    {
                        g.OrderId--;

                    }
                }
            }
            ToDo = holder;
        }
        public void Cater()
        {

            bool busy = true;

            while (busy)
            {
                Console.WriteLine("\n1.View Orders\n2.Add item to done\npress q to quit....");
                var choice = Console.ReadKey().Key;
                if (choice == ConsoleKey.Q)
                {
                    busy = false;
                    break;
                }
                else if (choice == ConsoleKey.D1)
                {
                    caterView();
                }
                else if (choice == ConsoleKey.D2)
                {
                    isDone();
                }
            }
        }

    }
}
