using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Assignment_5
{
    public class MongoDbRepository : IRepository
    {
        private readonly IMongoCollection<Player> _playerCollection;
        private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;

        public MongoDbRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("game");
            _playerCollection = database.GetCollection<Player>("players");

            _bsonDocumentCollection = database.GetCollection<BsonDocument>("players");
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            await _playerCollection.InsertOneAsync(player);

            return player;
        }

        public async Task<Player[]> GetAllPlayers()
        {
            var players = await _playerCollection.Find(new BsonDocument()).ToListAsync();
            return players.ToArray();
        }

        public Task<Player> GetPlayer(Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
            return _playerCollection.Find(filter).FirstAsync();
        }

        public async Task<Player> DeletePlayer(Guid playerId)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            return await _playerCollection.FindOneAndDeleteAsync(filter);
        }

        public Task<Player> UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public Task<Item> CreateItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }

        public Task<Item> DeleteItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }

        public Task<Item[]> GetAllItems(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Item> UpdateItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }

        public async Task<Player[]> GetPlayersWithScoreMoreThan(int x)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Gt(player => player.Score, x);

            List<Player> players = await _playerCollection.Find(filter).ToListAsync();

            return players.ToArray();
        }

        public async Task<Player> GetPlayerWithName(string name)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(player => player.Name, name);

            return await _playerCollection.Find(filter).FirstAsync();
        }

        public async Task<Player[]> GetPlayersWithTag(Tags tag)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Empty;

            List<Player> players = await _playerCollection.Find(filter).ToListAsync();

            List<Player> finalPlayers = new List<Player>();

            foreach(Player p in players)
            {
                foreach(Tags t in p.Tags)
                {
                    if(t == tag)
                    {
                        finalPlayers.Add(p);
                    }
                }
            }

            return finalPlayers.ToArray();
        }

        public async Task<Player[]> GetTop10Players()
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Empty;
            SortDefinition<Player> sortDef = Builders<Player>.Sort.Descending(player => player.Score);

            IFindFluent<Player, Player> cursor = _playerCollection.Find(filter).Sort(sortDef).Limit(10);

            List<Player> playerList = await cursor.ToListAsync();

            return playerList.ToArray();
        }
    }
}
