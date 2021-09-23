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
    [Route("[controller]")]
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
        public async Task<IEnumerable<FruitDTO>> Get()
        {
            logger.LogTrace($"Get fruits");

            var result = await fruitDataStore.GetEntityListAsync();

            var fruits = new List<FruitDTO>();

            foreach (var fruit in result)
            {
                fruits.Add(dataMapper.ModelToDTO(fruit));
            }

            return fruits;
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("id, name, color, taste")] FruitDTO fruit)
        {
            logger.LogTrace($"Create fruit: {fruit.Name}");

            if (ModelState.IsValid)
            {
                var model = dataMapper.DTOToModel(fruit);
                var result = await fruitDataStore.CreateEntityAsync(model);

                if (result == null)
                    return NotFound();
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
