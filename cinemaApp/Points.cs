using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace cinemaApp
{
    class Points
    {
        int Punten;
        string UserName;
        Tuple<int, string>[] Rewards;
        bool[] Redeemed;

        Tuple<int, string>[] rewards =
        {
                Tuple.Create(10,"5% off"),
                Tuple.Create(20, "10% off"),
                Tuple.Create(30,"Free drink"),
                Tuple.Create(40,"Free small popcorn"),
                Tuple.Create(50,"Free pizza"),
                Tuple.Create(60,"25% off"),
                Tuple.Create(70,"Talk with owner"),
                Tuple.Create(80,"Free hotdog"),
                Tuple.Create(90,"Free ticket"),
                Tuple.Create(100,"Free ticket, popcorn and drink"),
        };


        public Points(int p, string u)
        {
            this.Punten = p;
            this.UserName = u;
            this.Rewards = rewards;
            this.Redeemed = new bool[10];
        }

        public void seePoints()
        { 
            
            Console.WriteLine($"You have {Punten} points");
            int nextReward = Punten / 10 ;
            Console.WriteLine($"You're next reward is {Rewards[nextReward].Item2}");
            Console.WriteLine();
            Console.WriteLine();
            
        }
    }
}
