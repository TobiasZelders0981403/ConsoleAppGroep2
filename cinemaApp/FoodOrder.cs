using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cinemaApp
{
    public class FoodOrder
    {

        public int OrderId;
        //public FoodMenu menu;
        public FoodMenu Order;
        public string Day;
        public int Hour;
        public int Minute;
        public string UserName;
        public bool Paid;
        public bool Made;

        public FoodOrder(FoodMenu order, string day, int hour, int minute)
        {
            this.OrderId = ID.orderId();
            this.Order = order;
            this.Day = day;
            this.Hour = hour;
            this.Minute = minute;
            this.UserName = "default";//User.username
            this.Paid = false;
            this.Made = false;
            
        }

        public static string day()
        {
            string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            Console.WriteLine("\nThe Day: ");
            int i = 1;
            foreach (string d in days)
            {
                Console.WriteLine(i + ". " + d);
                i++;
            }
            Console.WriteLine("Choose number between 1 and 7: ");
            string chosen = Console.ReadLine();
            int chose = Program.ChoiceInput(1,7);
            if (chose > 0 && chose < 8)
            {
                return days[chose - 1];
            }
            else
            {
                Console.WriteLine("Invalid input. Please choose a number between 1 and 7");
                return day();
            }
        }

        public static int hour()
        {
            Console.Write("Hour(9-23): ");
            string userHour = Console.ReadLine();
            int h = Convert.ToInt32(userHour);
            if (h < 9 || h > 23)
            {
                Console.WriteLine("Invalid input. Choose hour between 9 and 23");
                return hour();
            }
            else
            {
                return h;
            }
        }

        public static int minute()
        {
            Console.Write("Minute(0-60): ");
            string um = Console.ReadLine();
            int userMinute = Convert.ToInt32(um);
            if (userMinute < 0 || userMinute > 60)
            {
                Console.WriteLine("Invalid input. Choose hour between 0 and 60");
                return minute();
            }
            else
            {
                return userMinute;
            }
        }

        public void displayTime()
        {
            if (Minute < 10)
            {
                Console.WriteLine(Day + " " + Hour + ": 0" + Minute);
            }
            else
            {
                Console.WriteLine(Day + " " + Hour + ":" + Minute);
            }
        }

        public void addFoodOrder()
        {
            string fileName = "food.json";
            string rawJson = File.ReadAllText(fileName);
            List<Food> menu = JsonConvert.DeserializeObject<List<Food>>(rawJson);
            string newJson = JsonConvert.SerializeObject(menu);
            var TheMenu = new FoodMenu(menu);

            bool items = true;
            List<Food> userOrder = new List<Food>();
            while (items)
            {
                Console.WriteLine("\nEnter the product id or press q to quit...");
                int userId;
                string idStr = Console.ReadLine();
                if (idStr == "q")
                {
                    items = false;
                    break;
                }
                bool sucess = Int32.TryParse(idStr, out userId);

                if ((sucess) & (userId <= menu.Count) & (userId >= 0))
                {
                    Console.WriteLine("You selected: ");
                    foreach (var food in menu)
                    {
                        if (food.id == userId)
                        {
                            food.display();
                            userOrder.Add(food);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    addFoodOrder();
                }

                Console.WriteLine("When do you want it to be ready?\n");
                string userDay = day();

                Console.WriteLine("The time: ");
                int userHour = hour();
                int userMinute = minute();


            }
        }

        public static FoodOrder Add(User user)
        {
            string fileName = "food.json";
            string rawJson = File.ReadAllText(fileName);
            List<Food> menu = JsonConvert.DeserializeObject<List<Food>>(rawJson);
            var TheMenu = new FoodMenu(menu);

            bool items = true;
            List<Food> userOrder = new List<Food>();
            while (items)
            {
                bool addFood = true;
                while (addFood)
                {
                    Console.WriteLine("\nEnter the product id or press q to quit...");
                    int userId;
                    string idStr = Console.ReadLine();
                    if (idStr == "q")
                    {
                        if (userOrder.Count == 0)
                        {
                            items = false;
                            break;
                        }
                        else
                        {
                            addFood = false;
                            break;
                        }
                    }
                    bool sucess = Int32.TryParse(idStr, out userId);

                    if ((sucess) & (userId <= ID.foodMax()) & (userId >= 0))
                    {
                        Console.WriteLine("You selected: ");
                        foreach (var food in menu)
                        {
                            if (food.id == userId)
                            {
                                food.display();
                                userOrder.Add(food);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                        Add(user);
                    }
                }
                Console.WriteLine("When do you want it to be ready?\n");
                string userDay = day();

                Console.WriteLine("The time: ");
                int userHour = hour();
                int userMinute = minute();
                //save to shoppingCart

                string filename = $"{user.username}-ShoppingCart.json";
                string[] s = { user.username, userDay, userHour.ToString(), userMinute.ToString() };
                for (int i = 0; i < userOrder.Count; i++) {
                    Food order = userOrder[i];
                    string[] s2 = { order.price.ToString(), order.name, order.size, order.category, order.subCategory};
                    List<string> myList = new List<string>();
                    myList.AddRange(s);
                    myList.AddRange(s2);
                    s = myList.ToArray();
                }
                if (user.username != "Guest") {
                    if (File.Exists(@filename)) {
                        string rawJSON = File.ReadAllText(filename);
                        string[][] data = JsonConvert.DeserializeObject<string[][]>(rawJSON);
                        Array.Resize(ref data, data.Length + 1);
                        data[data.Length - 1] = s;
                        string shoppingData = JsonConvert.SerializeObject(data);
                        File.WriteAllText(filename, shoppingData);
                    } else {
                        string[][] data = new string[1][] { s };
                        string shoppingData = JsonConvert.SerializeObject(data);
                        File.AppendAllText(filename, shoppingData);
                    }
                } else {
                    user.shoppingCart.Add(s);
                }

                //return to Caterers FoodOrders
                return new FoodOrder(new FoodMenu(userOrder), userDay, userHour, userMinute);
            }
            return new FoodOrder(new FoodMenu(userOrder), day(), hour(), minute());
        }

        //public static 
    }
}