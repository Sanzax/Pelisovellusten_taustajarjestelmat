using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_2
{
    public class Item
    {
        public Guid Id { get; set; }
        public int Level { get; set; }

        public Item(int level)
        {
            Id = Guid.NewGuid();
            Level = level;
        }

    }
}
