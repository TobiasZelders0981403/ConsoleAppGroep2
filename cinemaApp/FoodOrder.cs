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
        public string Order;
        public string Day;
        public int Hour;
        public int Minute;
        public string UserName;
        public bool Paid;
        public bool Made;

        public FoodOrder(string order, string day, int hour, int minute, string un)
        {
            this.OrderId = ID.orderId();
            this.Order = order;
            this.Day = day;
            this.Hour = hour;
            this.Minute = minute;
            this.UserName = un;
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
            int chose = Program.ChoiceInput(1,7);
            return days[chose-1];
        }

        public static int hour()
        {
            Console.Write("Hour(9-23): ");
            int hour = Program.ChoiceInput(9, 23);
            return hour;
        }

        public static int minute()
        {
            Console.Write("Minute(0-60): ");
            int min = Program.ChoiceInput(0, 60);
            return min;
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
                    Console.WriteLine("\nEnter the product id or press q to continue...");
                    int userId;
                    string idStr = Console.ReadLine();
                    if (idStr == "q")
                    {
                        break;
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

                if(userOrder.Count == 0)
                {
                    break;
                }
                string s5 = "";
                foreach (var item in userOrder) {
                    s5 += item.name;
                }
                Console.WriteLine("When do you want it to be ready?\n");
                string userDay = day();

                Console.WriteLine("The time: ");
                int userHour = hour();
                int userMinute = minute();
                //save to shoppingCart

                string filename = $"{user.username}-ShoppingCart.json";
                string[] s = { user.username, userDay, userHour.ToString(), userMinute.ToString() };
                string s3 = "";
                for (int i = 0; i < userOrder.Count; i++) {
                    Food order = userOrder[i];
                    string[] s2 = { order.price.ToString(), order.name, order.size, order.category, order.subCategory};
                    s3 += order.price.ToString() + order.name + order.size + order.category + order.subCategory + " | ";
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
                return new FoodOrder(s5, userDay, userHour, userMinute, user.username);
            }
            return null;
        }

        
    }
}
