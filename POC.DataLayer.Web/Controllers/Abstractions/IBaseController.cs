using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using POC.DataLayer.Data.Models.Abstractions;

namespace POC.DataLayer.Web.Controllers.Abstractions
{
    public interface IBaseController<DTO> where DTO : IDto
    {
        public IAsyncEnumerable<DTO> GetAll();

        public Task<IActionResult> Get(long id);

        public Task<IActionResult> GetBatch(IEnumerable<long> id);

        public Task<IActionResult> Create(DTO dto);

        public Task<IActionResult> CreateBatch(IEnumerable<DTO> dto);

        public Task<IActionResult> Update(DTO dto);

        public Task<IActionResult> UpdateBatch(IEnumerable<DTO> dto);

        public Task<IActionResult> Delete(long id);

        public Task<IActionResult> DeleteBatch(IEnumerable<long> id);
    }
}
