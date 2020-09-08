using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assignment_3
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
            return await _repository.Get(id);
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<Player[]> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<Player> Create(NewPlayer player)
        {
            _logger.LogInformation("Creating player with name " + player.Name);
            Player p = new Player()
            {
                Name = player.Name,
                Id = Guid.NewGuid()
            };

            p.CreationTime = DateTime.Now;

            await _repository.Create(p);

            return p;
        }

        [HttpPost]
        [Route("Modify")]
        public async Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            return await _repository.Modify(id, player);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<Player> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}
