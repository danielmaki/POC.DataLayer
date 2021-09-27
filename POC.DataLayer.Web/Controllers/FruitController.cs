using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using POC.DataLayer.Data.DTO;
using POC.DataLayer.Data.Mappings;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.ORM;
using POC.DataLayer.Data.Store;

namespace POC.DataLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FruitController : ControllerBase
    {
        private readonly ILogger<FruitController> logger;
        private readonly FruitDataStore fruitDataStore;
        private readonly IDataMapping<FruitModel, FruitORM, FruitDTO> dataMapper;

        public FruitController(ILogger<FruitController> logger, FruitDataStore fruitDataStore, IDataMapping<FruitModel, FruitORM, FruitDTO> dataMapper)
        {
            this.logger = logger;

            this.fruitDataStore = fruitDataStore;
            this.dataMapper = dataMapper;
        }

        [HttpGet]
        public async IAsyncEnumerable<FruitDTO> GetList()
        {
            logger.LogTrace($"Get fruits");

            var entities = fruitDataStore.GetEntityListAsync();

            await foreach (var fruit in entities)
            {
                yield return dataMapper.ModelToDTO(fruit);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            logger.LogTrace($"Get fruit with id {id}");

            var result = await fruitDataStore.GetEntityAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dataMapper.ModelToDTO(result));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("id, name, color, taste")] FruitDTO fruit)
        {
            logger.LogTrace($"Create fruit {fruit.Name}");

            var model = dataMapper.DTOToModel(fruit);
            var result = await fruitDataStore.CreateEntityAsync(model);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dataMapper.ModelToDTO(result));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([Bind("id, name, color, taste")] FruitDTO fruit)
        {
            logger.LogTrace($"Update fruit {fruit.Name} with id {fruit.Id}");

            var model = dataMapper.DTOToModel(fruit);
            var result = await fruitDataStore.UpdateEntityAsync(model);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dataMapper.ModelToDTO(result));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            logger.LogTrace($"delete fruit with id {id}");

            var result = await fruitDataStore.DeleteEntityAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dataMapper.ModelToDTO(result));
            }
        }
    }
}
