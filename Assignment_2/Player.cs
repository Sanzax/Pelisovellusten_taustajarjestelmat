using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_2
{
    public class Player : IPlayer
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public List<Item> Items { get; set; }

        public Player(Guid id)
        {
            Id = id;
            Random r = new Random();
            Score = r.Next(0, 100);
        }

        public Player()
        {
            Id = Guid.NewGuid();
            Random r = new Random();
            Score = r.Next(0, 100);
        }
    }
}
