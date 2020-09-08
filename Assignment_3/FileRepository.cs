using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Assignment_3
{
    public class FileRepository : IRepository
    {
        public Task<Player> Get(Guid id)
        {
            Task<Player[]> players = GetAll();

            foreach (Player p in players.Result)
            {
                if (p.Id == id)
                    return Task.FromResult(p);
            }
            return null;
        }
        public Task<Player[]> GetAll()
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
        public Task<Player> Create(Player player)
        {
            List<Player> playerList = GetAll().Result.ToList<Player>();

            playerList.Add(player);

            CreateJson(playerList.ToArray(), "game-dev.txt");

            return Task.FromResult(player);
        }
        public Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            Player[] players = GetAll().Result;

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
        }

        public Task<Player> Delete(Guid id)
        {
            List<Player> playerList = GetAll().Result.ToList<Player>();

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

        private void CreateJson(Player[] players, string path)
        {
            string jsonString = JsonConvert.SerializeObject(players);
            File.WriteAllText(path, jsonString);
        }
    }
}
