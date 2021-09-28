using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using POC.DataLayer.Data;

namespace POC.DataLayer.Web.Controllers
{
    public interface IBaseController<EXTERNAL> where EXTERNAL : IRequiredProperties
    {
        public IAsyncEnumerable<EXTERNAL> GetList();

        public Task<IActionResult> Get(long id);

        public Task<IActionResult> Create(EXTERNAL ext);

        public Task<IActionResult> Update(EXTERNAL ext);

        public Task<IActionResult> Delete(long id);
    }
}
