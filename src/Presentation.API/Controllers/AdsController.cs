using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Ads.Commands.AdCategory;
using Application.CQRS.Ads.Commands.AdDisabled;
using Application.CQRS.Ads.Commands.AdStorage;
using Application.CQRS.Ads.Commands.AdTag;
using Application.CQRS.Ads.Commands.AdViewed;
using Application.CQRS.Ads.Models;
using Application.CQRS.Ads.Queries.AdStorage;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.ControllerAbstractions;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdsController : MyBaseController
    {
        #region AdStorage

        [HttpGet("queue")]
        public async Task<ActionResult<AdDto>> GetAdFromQueueAsync()
        {
            AdDto adDto = await Mediator.Send(new GetAdFromQueueQuery());
            return Ok(adDto);
        }
        
        [HttpPost("filter")]
        public async Task<ActionResult<List<AdDto>>> FilterAdsAsync(FilterAdsQuery request)
        {
            List<AdDto> ads = await Mediator.Send(request);
            return Ok(ads);
        }
        
        [HttpPost]
        public async Task<ActionResult<Guid>> PostAdAsync([FromBody] CreateAdCommand request)
        {
            Guid createdAdId = await Mediator.Send(request);
            return Ok(createdAdId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdAsync([FromRoute] Guid id, [FromBody] UpdateAdCommand request)
        {
            if (id != request.AdId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteAdCommand {AdId = id});
            return NoContent();
        }

        #endregion

        #region AdCategory

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

        #endregion

        #region AdTag

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

        #endregion

        #region AdDisabled

        [HttpPost("{id}/disable")]
        public async Task<IActionResult> DisableAdAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DisableAdCommand {AdId = id});
            return NoContent();
        }

        #endregion

        #region AdViewed

        [HttpPost("{id}/view")]
        public async Task<IActionResult> ViewAdAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new ViewAdCommand {AdId = id});
            return NoContent();
        }

        #endregion
    }
}