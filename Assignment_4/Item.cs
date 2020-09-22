using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Assignment_4
{
    public enum ItemType
    {
        SWORD, POTION, SHIELD 
    }

    public class Item
    {
        public Guid Id { get; set; }

        [Range(1, 99)]
        public int Level { get; set; }

        [Range(0, 2)]
        public ItemType ItemType { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
