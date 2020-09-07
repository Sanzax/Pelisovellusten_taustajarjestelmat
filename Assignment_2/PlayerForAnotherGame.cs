using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_2
{
    public class PlayerForAnotherGame : IPlayer
    {
        public int Score { get; set; }
        public Guid Id { get; set; }

        public PlayerForAnotherGame(Guid id)
        {
            Id = id;
            Random r = new Random();
            Score = r.Next(0, 100);
        }
    }
}
