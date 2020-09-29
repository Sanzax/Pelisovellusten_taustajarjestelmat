using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assignment_5
{
    [ApiController]
    [Route("/players")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IRepository _repository;

        public PlayersController(ILogger<PlayersController> logger, IRepository r)
        {
            _logger = logger;
            _repository = r;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<Player> Get(Guid id)
        {
            return await _repository.GetPlayer(id);
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<Player[]> GetAll()
        {
            return await _repository.GetAllPlayers();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<Player> Create(NewPlayer player)
        {
            _logger.LogInformation("Creating player with name " + player.Name);
            Player p = new Player()
            {
                Name = player.Name,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                Items = new List<Item>()
            };

            await _repository.CreatePlayer(p);

            return p;
        }

        /*
        [HttpPost]
        [Route("Modify")]
        public async Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            return await _repository.ModifyPlayer(id, player);
        }*/

        [HttpPost]
        [Route("Delete")]
        public async Task<Player> Delete(Guid id)
        {
            return await _repository.DeletePlayer(id);
        }

        [HttpGet]
        [Route("GetPlayersWithScoreMoreThan")]
        public async Task<Player[]> GetPlayersWithScoreMoreThan(int minScore)
        {
            return await _repository.GetPlayersWithScoreMoreThan(minScore);
        }

        [HttpGet]
        [Route("GetPlayerWithName")]
        public async Task<Player> GetPlayerWithName(string name)
        {
            return await _repository.GetPlayerWithName(name);
        }

        [HttpGet]
        [Route("GetPlayersWithTag")]
        public async Task<Player[]> GetPlayersWithTag(Tags tag)
        {
            return await _repository.GetPlayersWithTag(tag);
        }

        [HttpGet]
        [Route("GetTop10")]
        public async Task<Player[]> GetTop10Players()
        {
            return await _repository.GetTop10Players();
        }
    }
}
