using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Assignment_2
{
    public class Game<T> where T : IPlayer
    {
        private List<T> _players;

        public Game(List<T> players)
        {
            _players = players;
        }

        public T[] GetTop10Players()
        {
            int amount = (_players.Count > 9 ? 10 : _players.Count);
            T[] topPlayers = new T[amount];

            List<T> players = _players;

            for(int i = 0; i < amount; i++)
            {
                T highest = players[0];
                foreach (T p in players)
                {
                    if (p.Score > highest.Score)
                    {
                        highest = p;
                    }
                }
                _players.Remove(highest);
                topPlayers[i] = highest;
            }

            return topPlayers;

        }
    }
}
