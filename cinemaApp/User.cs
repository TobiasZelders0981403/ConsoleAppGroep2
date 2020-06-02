using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace cinemaApp
{
    public class User
    {
        public string username { get; set; }
        private string password { get; set; }

        public int age { get; set;  }

        public bool accountVerified = false;
        
        int checkedage = 0;

        //All usernames and passwords will be stored in these lists
        private List<string> usernames = new List<string>();
        private List<string> passwords = new List<string>();
        private List<string> ageList = new List<string>();

        public List<string> shoppingCart;

        //When a account is made all input will be stored inside accounts.txt and the user will automatically login
        public void CreateAccount()
        {
            //Checks if accounts.txt has already been read
            if (usernames.Count == 0)
            {
                ReadingAccounts();
            }
            Console.WriteLine("\nRegister");
            checkUsername();

            //Checks if username is not used by someone else
            while (usernames.Contains(username))
            {
                Console.WriteLine("That username is not available!\nPlease enter a different username:");
                username = Console.ReadLine();

            }

            checkPassword();
            CheckAge();

            age = checkedage;
            string data = username + " " + password + " " + age.ToString();

            StreamWriter streamwriter = new StreamWriter(@"accounts.txt", append: true);
            streamwriter.WriteLineAsync(data);
            streamwriter.Close();

            accountVerified = true;
        }

        //ReadingAccoutns will store all usernames and passwords from accounts.txt into the Lists above
        private void ReadingAccounts()
        {
            StreamReader streamreader = new StreamReader(@"accounts.txt");
            string line;
            while ((line = streamreader.ReadLine()) != null)
            {
                string[] components = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                usernames.Add(components[0]);
                passwords.Add(components[1]);
                ageList.Add(components[2]);
            }
            streamreader.Close();
        }

        public void ContinueAsGuest() {
            username = "Guest";
            password = "Guest";
            shoppingCart = new List<string>();
            CheckAge();
            age = checkedage;
        }
        
        //AccountVerify will check if the entered username and password are inside the made lists and if they belong together
        public void VerifyLogin()
        {
            ReadingAccounts();
            Console.WriteLine("\nLogin");
            checkUsername();
            checkPassword();

            if ((usernames.Contains(username)) && (passwords.Contains(password)) && (passwords[usernames.IndexOf(username)] == password))
            {
                accountVerified = true;
                Console.WriteLine("You have logged in succesfully!");
                int.TryParse(ageList[usernames.IndexOf(username)], out checkedage);
                this.age = checkedage;
            }
        }

        //checks if age is numeric
        public void CheckAge()
        {
            Console.WriteLine("Please enter your age (you must be 12 or older):");
            string ageInput = Console.ReadLine();
            int.TryParse(ageInput, out checkedage);
            while (checkedage <= 11)
            {
                Console.WriteLine("Invalid Input! Please enter your age:");
                ageInput = Console.ReadLine();
                int.TryParse(ageInput, out checkedage);
            }
        }

        //checks if the username or password is not blanc or only spaces
        public void checkUsername()
        {
            Console.WriteLine("Please enter a username:");
            string usernameInput = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(usernameInput))
            {
                Console.WriteLine("Invalid Input! Please enter a username:");
                usernameInput = Console.ReadLine();
            }
            username = usernameInput;
        }
        public void checkPassword()
        {
            Console.WriteLine("Please enter a password:");
            string passwordInput = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(passwordInput))
            {
                Console.WriteLine("Invalid Input! Please enter a password:");
                passwordInput = Console.ReadLine();
            }
            password = passwordInput;
        }

    }
}
