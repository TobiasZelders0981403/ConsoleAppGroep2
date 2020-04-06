using System;

namespace cinemaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            //Login(user) or Register(user);
            //ReserveTickets.ReserveTicketsMain(user);
            //UserData.UserDataMain();

            Console.Read();
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
