﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_5
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Item> Items { get; set; }
    }
}
