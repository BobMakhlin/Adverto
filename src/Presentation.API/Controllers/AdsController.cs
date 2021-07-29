using System;
using System.Threading.Tasks;
using Application.CQRS.Ads.Commands.AdCategory;
using Application.CQRS.Ads.Commands.AdDisabled;
using Application.CQRS.Ads.Commands.AdStorage;
using Application.CQRS.Ads.Commands.AdTag;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.ControllerAbstractions;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdsController : MyBaseController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> PostAdAsync([FromBody] CreateAdCommand request)
        {
            Guid createdAdId = await Mediator.Send(request);
            return Ok(createdAdId);
        }

        [HttpPost("categories")]
        public async Task<IActionResult> AddCategoryToAdAsync([FromBody] AddCategoryToAdCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpDelete("categories")]
        public async Task<IActionResult> DeleteCategoryOfAdAsync([FromBody] DeleteCategoryOfAdCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpPost("tags")]
        public async Task<IActionResult> AddTagToAdAsync([FromBody] AddTagToAdCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpDelete("tags")]
        public async Task<IActionResult> DeleteTagOfAdAsync([FromBody] DeleteTagOfAdCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpPost("{id}/disable")]
        public async Task<IActionResult> DisableAdAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DisableAdCommand {AdId = id});
            return NoContent();
        }
    }
}