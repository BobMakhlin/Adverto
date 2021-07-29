    using System;
    using System.Threading.Tasks;
    using Application.CQRS.Ads.Commands.AdCategory;
    using Application.CQRS.Ads.Commands.AdStorage;
    using Microsoft.AspNetCore.Mvc;
using Presentation.Common.ControllerAbstractions;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdController : MyBaseController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> PostAdAsync([FromBody] CreateAdCommand request)
        {
            Guid createdAdId = await Mediator.Send(request);
            return Ok(createdAdId);
        }
        
        [HttpPost("categories")]
        public async Task<ActionResult<Guid>> AddCategoryToAdAsync([FromBody] AddCategoryToAdCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }
        
        [HttpDelete("categories")]
        public async Task<ActionResult<Guid>> DeleteCategoryOfAdAsync([FromBody] DeleteCategoryOfAdCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }
    }
}