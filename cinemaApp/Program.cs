using System;

namespace cinemaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            //Login(user) or Register(user);
        }

        static void Login(User user)
        {
            while (user.accountVerified == false)
            {
                user.VerifyLogin();
            }
        }

        static void Register(User user)
        {
            user.CreateAccount();
        }
    }
}
