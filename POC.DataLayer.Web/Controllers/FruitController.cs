using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using POC.DataLayer.Data.Mappings.Abstractions;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Store.Abstractions;
using POC.DataLayer.Web.Controllers.Abstractions;

namespace POC.DataLayer.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FruitController : BaseController<Fruit, FruitDto>
    {
        private readonly ILogger<FruitController> logger;

        public FruitController(ILogger<FruitController> logger, IDataStore<Fruit> dataStore, IDataMap<Fruit, FruitDto> dataMapper)
            : base(logger, dataStore, dataMapper)
        {
            this.logger = logger;
        }
    }
}
