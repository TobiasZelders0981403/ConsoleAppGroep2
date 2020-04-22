using System;

namespace cinemaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            Login(user);
            //Register(user);
            //ReserveTickets.ReserveTicketsMain(user);
            //UserData.UserDataMain();
            //RoomOptions.RoomOptionsMain();
            //Films.FilmMain();
            Filters.GenreFilter(user);
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
