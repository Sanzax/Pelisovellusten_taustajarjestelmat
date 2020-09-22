using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assignment_4
{
    [ApiController]
    [Route("/players/{playerId}/items")]
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly IRepository _repository;

        public ItemsController(ILogger<ItemsController> logger, IRepository r)
        {
            _logger = logger;
            _repository = r;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<Item> Get(Guid playerId, Guid id)
        {
            return await _repository.GetItem(playerId, id);
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<Item[]> GetAll(Guid playerId)
        {
            return await _repository.GetAllItems(playerId);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<Item> Create(Guid playerId, NewItem item)
        {
            Item i = new Item()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now
            };

            try
            {
                await _repository.CreateItem(playerId, i);
            }
            catch
            {
                throw new NotFoundException("Incorrect player id!");
            }

            return i;
        }

        /*[HttpPost]
        [Route("Modify")]
        public async Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            return await _repository.ModifyPlayer(id, player);
        }*/

        [HttpPost]
        [Route("Delete")]
        public async Task<Item> Delete(Guid playerId, [FromBody]Item item)
        {
            return await _repository.DeleteItem(playerId, item);
        }
    }
}
