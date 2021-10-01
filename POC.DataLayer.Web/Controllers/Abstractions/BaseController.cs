using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using POC.DataLayer.Data.Mappings.Abstractions;
using POC.DataLayer.Data.Models.Abstractions;
using POC.DataLayer.Data.Store.Abstractions;

namespace POC.DataLayer.Web.Controllers.Abstractions
{
    public abstract class BaseController<MODEL, DTO> : ControllerBase, IBaseController<DTO> where MODEL : IModel where DTO : IModel
    {
        private readonly ILogger<BaseController<MODEL, DTO>> logger;

        private readonly IDataStore<MODEL> dataStore;
        private readonly IDataMap<MODEL, DTO> dataMapper;

        private readonly string typeName;

        public BaseController(ILogger<BaseController<MODEL, DTO>> logger, IDataStore<MODEL> dataStore, IDataMap<MODEL, DTO> dataMapper)
        {
            this.logger = logger;

            this.dataStore = dataStore;
            this.dataMapper = dataMapper;

            typeName = typeof(MODEL).ToString();
        }

        [HttpGet]
        public virtual async IAsyncEnumerable<DTO> GetAll()
        {
            logger.LogTrace($"Get all {typeName}");

            var entities = dataStore.GetAllAsync();

            await foreach (var entity in entities)
            {
                yield return dataMapper.ToExternal(entity);
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(long id)
        {
            logger.LogTrace($"Get {typeName}");

            var result = await dataStore.GetAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dataMapper.ToExternal(result));
            }
        }

        public virtual Task<IActionResult> GetBatch(IEnumerable<long> id)
        {
            throw new System.NotImplementedException();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(DTO dto)
        {
            logger.LogTrace($"Create {typeName}");

            var model = dataMapper.ToModel(dto);
            var result = await dataStore.CreateAsync(model);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dataMapper.ToExternal(result));
            }
        }

        public virtual Task<IActionResult> CreateBatch(IEnumerable<DTO> dto)
        {
            throw new System.NotImplementedException();
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(DTO dto)
        {
            logger.LogTrace($"Update {typeName}");

            var model = dataMapper.ToModel(dto);
            var result = await dataStore.UpdateAsync(model);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dataMapper.ToExternal(result));
            }
        }

        public virtual Task<IActionResult> UpdateBatch(IEnumerable<DTO> dto)
        {
            throw new System.NotImplementedException();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(long id)
        {
            logger.LogTrace($"Delete {typeName}");

            var result = await dataStore.DeleteAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dataMapper.ToExternal(result));
            }
        }

        public virtual Task<IActionResult> DeleteBatch(IEnumerable<long> id)
        {
            throw new System.NotImplementedException();
        }
    }
}
