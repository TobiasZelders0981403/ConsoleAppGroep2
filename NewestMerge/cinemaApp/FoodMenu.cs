using CinemaApp;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cinemaApp
{
    public class FoodMenu
    {
        public List<Food> Menu;

        public FoodMenu(List<Food> menu)
        {
            this.Menu = menu;
        }

        void searchCategory()
        {
            Console.WriteLine("Search: ");
            string search = Console.ReadLine().ToLower();
            foreach (var food in this.Menu)
            {
                if (food.category.ToLower().Contains(search))
                {
                    food.display();
                }
            }
        }

        void searchsubCategory()
        {
            Console.WriteLine("Search: ");
            string search = Console.ReadLine().ToLower();
            foreach (var food in this.Menu)
            {
                if (food.subCategory.ToLower().Contains(search))
                {
                    food.display();
                }
            }
        }

        void searchName()
        {
            Console.WriteLine("Search: ");
            string search = Console.ReadLine().ToLower();
            foreach (var food in this.Menu)
            {
                if (food.name.ToLower().Contains(search))
                {
                    food.display();
                }
            }

        }

        public void search()
        {
            bool busy = true;
            while (busy)
            {
                Console.WriteLine("[1] Search by name\n[2] Category\n[3] Sub-category\n[0] Exit...");
                var choice = Program.ChoiceInput(0, 3);
                if (choice == 0)
                {
                    busy = false;
                    break;
                }
                else if (choice == 1)
                {
                    searchName();
                }
                else if (choice == 2)
                {
                    searchCategory();
                }
                else if (choice == 3)
                {
                    searchsubCategory();
                }
                else
                {
                    Console.WriteLine("Please choose a number between 1 and 3");
                }
            }
        }

        public void overview()
        {
            string[] snacksSubs = { "Popcorn", "Sweets", "Hot Dog", "Nachos", "Pizza" };
            string[] drinkSubs = { "Water", "Soda", "Juice", "Milkshake", "Slushie" };
            Console.WriteLine();
            foreach (var food in this.Menu)
            {
                if (food.category == "Snack")
                {
                    for (int i = 0; i < snacksSubs.Length; i++)
                    {
                        if (food.subCategory == snacksSubs[i])
                        {
                            food.display();
                        }
                    }
                }
            }

            foreach (var food in this.Menu)
            {
                if (food.category == "Drink")
                {
                    for (int i = 0; i < drinkSubs.Length; i++)
                    {
                        if (food.subCategory == drinkSubs[i])
                        {
                            food.display();
                        }
                    }
                }
            }
        }

        public void payOverview()
        {
            double price = 0;
            foreach (var p in Menu)
            {
                price += p.price;
                Console.Write(p.name + " || ");
            }
            Console.WriteLine("Price: " + price);
        }

        public void addItem()
        {
            Console.WriteLine("\nAdd item:");

            Console.WriteLine("The name:");
            string name = Console.ReadLine();

            string category = Food.getCategory();

            string subCat = Food.getSubCategory(category);

            string size = Food.getSize();

            Console.WriteLine("The price:");
            string price = Console.ReadLine();
            double userPrice = Food.convertPrice(price);

            string varName = size + " " + name;


            var newFood = new Food(varName, category, subCat, size, userPrice);
            this.Menu.Add(newFood);
            newFood.display();
        }

        public void editItem()
        {
            Console.WriteLine();
            List<Food> newMenu = new List<Food>();
            bool busy = true;

            while (busy)
            {
                overview();
                Console.WriteLine("Enter the id of the product you want to edit.\nPress q to quit....");
                var idDelete = Console.ReadLine();

                if (idDelete == "q")
                {
                    busy = false;
                    break;
                }
                int id;
                //int id = Convert.ToInt32(idDelete);
                bool sucess = Int32.TryParse(idDelete, out id);

                if ((sucess) & (id <= this.Menu.Count) & (id >= 0))
                {

                    Console.WriteLine("Type 'y' to change 'n' to leave be");

                    bool changeName = false;
                    string newName = "";
                    Console.WriteLine("\nChange name? ");
                    var choice = Console.ReadKey().Key;
                    if (choice == ConsoleKey.Y)
                    {
                        Console.WriteLine("New name: ");
                        newName = Console.ReadLine();
                        changeName = true;
                    }

                    bool changeCat = false;
                    string newCat = "";
                    Console.WriteLine("\nChange category?");
                    choice = Console.ReadKey().Key;
                    if (choice == ConsoleKey.Y)
                    {
                        Console.WriteLine("New category:");
                        newCat = Food.getCategory();
                        changeCat = true;
                    }

                    bool changeSub = false;
                    string newSub = "";
                    Console.WriteLine("\nChange sub-category?");
                    choice = Console.ReadKey().Key;
                    if (choice == ConsoleKey.Y)
                    {
                        if (changeCat)
                        {
                            Console.WriteLine("\nNew Sub-category:");
                            newSub = Food.getSubCategory(newCat);
                            changeSub = true;
                        }
                        else
                        {
                            Console.WriteLine("\nNew Sub-category:");
                            newSub = Food.getSubCategory(this.Menu[id].category);
                            changeSub = true;
                        }
                    }

                    bool changeSize = false;
                    string newSize = "";
                    Console.WriteLine("\nChange size?");
                    choice = Console.ReadKey().Key;
                    if (choice == ConsoleKey.Y)
                    {
                        Console.WriteLine("\nNew size: ");
                        newSize = Food.getSize();
                        changeSize = true;
                    }

                    bool changePrice = false;
                    string newPrice = "";
                    Console.WriteLine("\nChange price?");
                    choice = Console.ReadKey().Key;
                    if (choice == ConsoleKey.Y)
                    {
                        Console.WriteLine("\nNew price: ");
                        newPrice = Console.ReadLine();
                        changePrice = true;
                    }

                    foreach (var food in this.Menu)
                    {
                        if (id == food.id)
                        {
                            if (changeName)
                            {
                                food.name = newName;
                            }

                            if (changeCat)
                            {
                                food.category = newCat;
                            }

                            if (changeSub)
                            {
                                food.subCategory = newSub;
                            }

                            if (changeSize)
                            {
                                food.size = newSize;
                            }

                            if (changePrice)
                            {
                                food.price = Food.convertPrice(newPrice);
                            }
                            food.display();
                        }
                    }


                }
                else
                {
                    Console.WriteLine("Invalid input");
                    editItem();
                }

            }

        }

        public void Delete()
        {
            Console.WriteLine();
            bool busy = true;

            while (busy)
            {
                overview();
                Console.WriteLine("Enter the id of the product you want to delete or q to quit...");
                var idDelete = Console.ReadLine();

                int id;
                //int id = Convert.ToInt32(idDelete);
                bool sucess = Int32.TryParse(idDelete, out id);

                if (idDelete == "q")
                {
                    busy = false;
                    break;
                }

                else if ((sucess) & (id <= this.Menu.Count) & (id >= 0))
                {

                    Console.WriteLine();
                    Food toRemove = null;
                    foreach (var food in this.Menu)
                    {
                        if (food.id == id)
                        {
                            toRemove = food;
                        }
                    }
                    Menu.Remove(toRemove);
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    Delete();
                }
            }


        }

        public static FoodMenu getMenu()
        {
            string fileName = "food.json";
            string rawJson = File.ReadAllText(fileName);
            List<Food> mainMenu = JsonConvert.DeserializeObject<List<Food>>(rawJson);
            return new FoodMenu(mainMenu);
        }

        public static void Caterer()
        {
            bool busy = true;
            //List<Food> menu = new List<Food>();
            string fileName = "food.json";
            string rawJson = File.ReadAllText(fileName);
            List<Food> mainMenu = JsonConvert.DeserializeObject<List<Food>>(rawJson);
            FoodMenu menu = new FoodMenu(mainMenu);

            while (busy)
            {

                Console.WriteLine("\nWhat do you wanna do?\n[1] See all avalaible items\n[2] Add a new item\n[3] Edit an item\n[4] Remove an item\n[5] Search for an item(5)\n[0] Exit...");
                int userChoice = Program.ChoiceInput(0, 5);

                if (userChoice == 0)
                {
                    busy = false;
                    break;
                }
                else if (userChoice == 1)
                {
                    menu.overview();
                }
                else if (userChoice == 2)
                {
                    menu.addItem();
                }
                else if (userChoice == 3)
                {
                    menu.editItem();
                }
                else if (userChoice == 4)
                {
                    menu.Delete();
                }
                else if (userChoice == 5)
                {
                    menu.search();
                }
                else
                {
                    Console.WriteLine("Please choose between 1, 2, 3, 4 and 5");
                }
            }
            string newJson = JsonConvert.SerializeObject(mainMenu);
            File.WriteAllText(fileName, newJson);
        }
    }

}
