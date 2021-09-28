using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using POC.DataLayer.Data.Mappings;
using POC.DataLayer.Data;
using POC.DataLayer.Data.Store;

namespace POC.DataLayer.Web.Controllers
{
    public abstract class BaseController<INTERNAL, EXTERNAL> : ControllerBase, IBaseController<EXTERNAL> where INTERNAL : IRequiredProperties where EXTERNAL : IRequiredProperties
    {
        private readonly ILogger<BaseController<INTERNAL, EXTERNAL>> logger;
        private readonly IDataStore<INTERNAL> dataStore;
        private readonly IDataMap<INTERNAL, EXTERNAL> dataMapper;

        public BaseController(ILogger<BaseController<INTERNAL, EXTERNAL>> logger, IDataStore<INTERNAL> dataStore, IDataMap<INTERNAL, EXTERNAL> dataMapper)
        {
            this.logger = logger;

            this.dataStore = dataStore;
            this.dataMapper = dataMapper;
        }

        [HttpGet]
        public virtual async IAsyncEnumerable<EXTERNAL> GetList()
        {
            logger.LogTrace($"Get fruits");

            var entities = dataStore.GetEntityListAsync();

            await foreach (var fruit in entities)
            {
                yield return dataMapper.IntlToExt(fruit);
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(long id)
        {
            logger.LogTrace($"Get fruit with id {id}");

            var result = await dataStore.GetEntityAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dataMapper.IntlToExt(result));
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([Bind("id, name, color, taste")] EXTERNAL fruit)
        {
            logger.LogTrace($"Create fruit");

            var intl = dataMapper.ExtToIntl(fruit);
            var result = await dataStore.CreateEntityAsync(intl);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dataMapper.IntlToExt(result));
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update([Bind("id, name, color, taste")] EXTERNAL fruit)
        {
            logger.LogTrace($"Update fruit with id {fruit.Id}");

            var intl = dataMapper.ExtToIntl(fruit);
            var result = await dataStore.UpdateEntityAsync(intl);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dataMapper.IntlToExt(result));
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(long id)
        {
            logger.LogTrace($"delete fruit with id {id}");

            var result = await dataStore.DeleteEntityAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dataMapper.IntlToExt(result));
            }
        }
    }
}
