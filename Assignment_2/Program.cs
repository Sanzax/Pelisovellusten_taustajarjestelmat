using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = InstantiatePlayers(1000000);

            Player player = new Player();
            player.Items = new List<Item>() { new Item(10), new Item(3), new Item(5), new Item(65), new Item(24) };

            Item highestLvlItem = player.GetHighestLevelItem();

            Console.WriteLine("Highest level of an item is " + highestLvlItem.Level);
            Console.WriteLine("");
            foreach (Item i in GetItems(player))
                Console.WriteLine("Item id: " + i.Id);

            foreach (Item i in GetItemsWithLinq(player))
                Console.WriteLine("Item id with linq: " + i.Id);
            Console.WriteLine("");
            Console.WriteLine("Id of the first item: " + FirstItem(player).Id);
            Console.WriteLine("Id of the first item with linq: " + FirstItemWithLinq(player).Id);
            Console.WriteLine("");
            ProcessEachItem(player, item => Console.WriteLine("Id: " + item.Id + ", Level: " + item.Level));
           
            ProcessEachItem(player, PrintItem);
            Console.WriteLine("");

            Game<Player> game = new Game<Player>(InstantiatePlayers(20));
            Game<PlayerForAnotherGame> anotherGame = new Game<PlayerForAnotherGame>(InstantiateAnotherPlayers(20));
            foreach (Player p in game.GetTop10Players())
            {
                Console.WriteLine("Id: " + p.Id + ", score: " + p.Score);
            }
            Console.WriteLine("");

            foreach (PlayerForAnotherGame p in anotherGame.GetTop10Players())
            {
                Console.WriteLine("Id: " + p.Id + ", score: " + p.Score);
            }
        }

        static List<Player> InstantiatePlayers(int amount)
        {
            List<Player> players = new List<Player>();
            HashSet<Guid> hashSet = new HashSet<Guid>();
            for(int i = 0; i < amount; i++)
            {
                Guid id = Guid.NewGuid();

                if(hashSet.Contains(id))
                {
                    i--;
                    Console.WriteLine("Dublicate found!");
                    continue;
                }

                hashSet.Add(id);
                players.Add(new Player(id));
                
            }
            return players;
        }

        static List<PlayerForAnotherGame> InstantiateAnotherPlayers(int amount)
        {
            List<PlayerForAnotherGame> players = new List<PlayerForAnotherGame>();
            HashSet<Guid> hashSet = new HashSet<Guid>();
            for (int i = 0; i < amount; i++)
            {
                Guid id = Guid.NewGuid();

                if (hashSet.Contains(id))
                {
                    i--;
                    Console.WriteLine("Dublicate found!");
                    continue;
                }

                hashSet.Add(id);
                players.Add(new PlayerForAnotherGame(id));

            }
            return players;
        }

        static Item[] GetItems(Player player)
        {
            Item[] items = new Item[player.Items.Count];

            for(int i = 0; i < items.Length; i++)
            {
                items[i] = player.Items[i];
            }


            return items;
        }


        static Item[] GetItemsWithLinq(Player player)
        {
            return player.Items.ToArray();
        }


        static Item FirstItem(Player player)
        {
            if (player.Items.Count == 0)
                return null;

            return player.Items[0];
        }

        static Item FirstItemWithLinq(Player player)
        {
            return player.Items.First();
        }

        static void ProcessEachItem(Player player, Action<Item> process)
        {
            foreach(Item i in player.Items)
                process(i);
        }

        static void PrintItem(Item item)
        {
            Console.WriteLine("Id: " + item.Id + ", Level: " + item.Level);
        }

    }

    public static class PlayerExtension
    {
        public static Item GetHighestLevelItem(this Player player)
        {
            Item highest = player.Items[0];
            foreach (Item i in player.Items)
                if (i.Level > highest.Level)
                    highest = i;

            return highest;
        }
    }
}
