using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace cinemaApp
{
    class VIP
    {
        public double discountMovie = 0.9;
        public double discountFood = 0.9;
        
        public static void Membership(User user)
        {
            //var user = (User.username);
            // input real user name...
            bool VIP = false;
            //what does the VIPmembership include
            Console.WriteLine(
                "VIP Member \n\n" +
                "As a VIP Member you have many benefits:\n" +
                " - You will have a discount on the movies of 10%.\n" +
                " - You will have a discount on snacks and drinks of 10%.\n\n");


            // check if already VIP
            using (StreamReader sr = new StreamReader("VIP.txt"))
            {
                string line;
                int counter = 0;

                while ((line = sr.ReadLine()) != null)
                {

                    counter++;


                    if (line == user.username)
                    {
                        VIP = true;
                    }


                }

            }
            // You are a VIP
            // if user already a VIPmember go to main
            if (VIP == true)
            {
                Console.WriteLine("You are already a VIP Member");
            }
            // You are not a VIP
            else
            {
                Console.WriteLine("Would you like to become a VIP Member for just 15,- euro ?\n" +
                "Yes or No");

                string newVIP = "";
                //newVIP = Console.ReadLine();

                int corrantw = 0;
                while (corrantw == 0)
                {
                    newVIP = Console.ReadLine();
                    if ((newVIP.ToLower()) == "yes")
                    {
                        //add to account
                        using (StreamWriter sw = File.AppendText("VIP.txt"))
                        {
                            sw.WriteLine(user.username);
                            sw.Close();
                        }
                        Console.WriteLine("\nThank you for becoming a VIP Member!\nEnjoy your discounts.");
                        corrantw = 1;
                    }
                    else if ((newVIP.ToLower()) == "no")
                    {
                        corrantw = 1;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input try again");
                    }
                }
            }

        }    //back to main
    }
}
