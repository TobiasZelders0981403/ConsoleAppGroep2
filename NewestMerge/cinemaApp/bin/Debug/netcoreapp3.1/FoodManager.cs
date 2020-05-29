using System;
using System.Collections.Generic;
using System.Text;

namespace cinemaApp {
    class FoodManager
    {
        public List<Food> Menu;

        public FoodManager(List<Food> menu)
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
                Console.WriteLine("[1] search by name, [2] category or [3] sub-category\nPress [0] to quit.");
                var choice = Console.ReadKey().Key;
                if (choice == ConsoleKey.Q)
                {
                    busy = false;
                    break;
                }
                else if (choice == ConsoleKey.D1)
                {
                    searchName();
                }
                else if (choice == ConsoleKey.D2)
                {
                    searchCategory();
                }
                else if (choice == ConsoleKey.D3)
                {
                    searchsubCategory();
                }
                else
                {
                    Console.WriteLine("Please choose a number between [1] and [3]");
                }
            }
        }

        public void overview()
        {
            Console.WriteLine();
            foreach (var food in this.Menu)
            {
                food.display();
            }
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
            

            var newFood = new Food( varName, category, subCat, size, userPrice);
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
                Console.WriteLine("Enter the id of the product you want to edit.\nPress [0] to quit....");
                var idDelete = Console.ReadLine();

                int id;
                //int id = Convert.ToInt32(idDelete);
                bool sucess = Int32.TryParse(idDelete, out id);

                if ((sucess) & (id <= this.Menu.Count) & (id >= 0))
                {

                    Console.WriteLine("Type [y] to change [n] to leave be");

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
            List<Food> newMenu = new List<Food>();
            bool busy = true;

            while (busy)
            {
                overview();
                Console.WriteLine("Enter the id of the product you want to delete");
                var idDelete = Console.ReadLine();

                int id;
                //int id = Convert.ToInt32(idDelete);
                bool sucess = Int32.TryParse(idDelete, out id);

                if ((sucess) & (id <= this.Menu.Count) & (id >= 0))
                {

                    Console.WriteLine();
                    foreach (var food in this.Menu)
                    {
                        if (food.id == id)
                        {
                            this.Menu.Remove(food);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    Delete();
                }
            }
            

        }

        public void Caterer()
        {
            bool busy = true;
            //List<Food> menu = new List<Food>();

            while (busy)
            {

                Console.WriteLine("\nWhat do you wanna do?\n[1] See all avalaible items\n[2] Add a new item\n[3] Edit an item\n[4] Remove an item\n[5] Search for an item\nPress [0] to quit.");
                var userChoice = Console.ReadKey().Key;

                if (userChoice == ConsoleKey.Q)
                {
                    busy = false;
                    break;
                }
                else if (userChoice == ConsoleKey.D1)
                {
                    overview();
                }
                else if (userChoice == ConsoleKey.D2)
                {
                    addItem();
                }
                else if (userChoice == ConsoleKey.D3)
                {
                    editItem();
                }
                else if (userChoice == ConsoleKey.D4)
                {
                    Delete();
                }
                else if (userChoice == ConsoleKey.D5)
                {
                    search();
                }
                else
                {
                    Console.WriteLine("Please choose between [1], [2], [3] and [4]");
                }

            }
        }
    }
}
