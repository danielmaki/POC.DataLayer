using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using POC.DataLayer.Data;
using POC.DataLayer.Data.Mappings;
using POC.DataLayer.Data.Store;

namespace POC.DataLayer.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FruitController : BaseController<FruitModel, FruitDTO>
    {
        private readonly ILogger<FruitController> logger;

        public FruitController(ILogger<FruitController> logger, IDataStore<FruitModel> dataStore, IDataMap<FruitModel, FruitDTO> dataMapper)
            : base(logger, dataStore, dataMapper)
        {
            this.logger = logger;
        }
    }
}
