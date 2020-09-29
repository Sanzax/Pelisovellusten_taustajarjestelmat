using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Assignment_5
{
    public class FileRepository : IRepository
    {
        public Task<Player> GetPlayer(Guid id)
        {
            Task<Player[]> players = GetAllPlayers();

            foreach (Player p in players.Result)
            {
                if (p.Id == id)
                    return Task.FromResult(p);
            }
            return null;
        }
        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            Task<Item[]> items = GetAllItems(playerId);

            foreach(Item i in items.Result)
            {
                if(i.Id == itemId)
                {
                    return Task.FromResult(i);
                }
            }
  
            return null;
        }

        public Task<Player[]> GetAllPlayers()
        {
            Player[] players;

            string text = File.ReadAllText("game-dev.txt");
            if(text == "")
            {
                return Task.FromResult(Array.Empty<Player>());
            }

            players = JsonConvert.DeserializeObject<Player[]>(text);

            return Task.FromResult(players);
        }
        public Task<Item[]> GetAllItems(Guid playerId)
        {
            Task<Player[]> players = GetAllPlayers();

            foreach (Player p in players.Result)
            {
                if (p.Id == playerId)
                {
                    return Task.FromResult(p.Items.ToArray());
                }
            }
            return null;
        }

        public Task<Player> CreatePlayer(Player player)
        {
            List<Player> playerList = GetAllPlayers().Result.ToList<Player>();

            playerList.Add(player);

            CreateJson(playerList.ToArray(), "game-dev.txt");

            return Task.FromResult(player);
        }

        public Task<Item> CreateItem(Guid playerId, Item item)
        {
            List<Player> playerList = GetAllPlayers().Result.ToList<Player>();

            foreach(Player p in playerList)
            {
                if(p.Id == playerId)
                {
                    p.Items.Add(item);
                    CreateJson(playerList.ToArray(), "game-dev.txt");
                    return Task.FromResult(item);
                }
            }

            return null;
        }

        /*public Task<Player> ModifyPlayer(Guid id, ModifiedPlayer player)
        {
            Player[] players = GetAllPlayers().Result;

            foreach(Player p in players)
            {
                if(p.Id == id)
                {
                    p.Score = player.Score;
                    CreateJson(players, "game-dev.txt");
                    return Task.FromResult(p);
                }
            }
            return null;
        }*/

        public Task<Player> DeletePlayer(Guid id)
        {
            List<Player> playerList = GetAllPlayers().Result.ToList<Player>();

            foreach (Player p in playerList)
            {
                if (p.Id == id)
                {
                    playerList.Remove(p);
                    CreateJson(playerList.ToArray(), "game-dev.txt");
                    return Task.FromResult(p);
                }
            }
            return null;
        }
        public Task<Item> DeleteItem(Guid playerId, Item item)
        {
            List<Item> items = GetAllItems(playerId).Result.ToList<Item>();
            List<Player> playerList = GetAllPlayers().Result.ToList<Player>();

            Player player = null;
            foreach (Player p in playerList)
            {
                if (p.Id == playerId)
                {
                    player = p;
                }
            }

            foreach (Item i in items)
            {
                if(i == item)
                {
                    items.Remove(i);
                    player.Items = items;
                    CreateJson(playerList.ToArray(), "game-dev.txt");
                    return Task.FromResult(i);
                }
            }

            return null;
        }

        private void CreateJson(Player[] players, string path)
        {
            string jsonString = JsonConvert.SerializeObject(players);
            File.WriteAllText(path, jsonString);
        }

        public Task<Player> UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public Task<Item> UpdateItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }

        public Task<Player[]> GetPlayersWithScoreMoreThan(int x)
        {
            return null;
        }

        public Task<Player> GetPlayerWithName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Player[]> GetPlayersWithTag(Tags tag)
        {
            throw new NotImplementedException();
        }

        public Task<Player[]> GetTop10Players()
        {
            throw new NotImplementedException();
        }
    }
}
