using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_2
{
    public interface IPlayer
    {
        int Score { get; set; }
        public Guid Id { get; set; }
    }
}
