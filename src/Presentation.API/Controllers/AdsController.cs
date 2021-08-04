using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Ads.Commands.AdCategory;
using Application.CQRS.Ads.Commands.AdDisabled;
using Application.CQRS.Ads.Commands.AdStorage.Create.Realisations;
using Application.CQRS.Ads.Commands.AdStorage.Delete;
using Application.CQRS.Ads.Commands.AdStorage.Update.Realisations;
using Application.CQRS.Ads.Commands.AdTag;
using Application.CQRS.Ads.Commands.AdViewed;
using Application.CQRS.Ads.Models;
using Application.CQRS.Ads.Queries.AdStorage;
using Application.CQRS.Tags.Models;
using Application.Pagination.Common.Models.PagedList;
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
        public async Task<ActionResult<IPagedList<AdDto>>> FilterAdsAsync(FilterAdsQuery request)
        {
            IPagedList<AdDto> ads = await Mediator.Send(request);
            return Ok(ads);
        }

        #region Create

        [HttpPost("text-ad")]
        public async Task<ActionResult<Guid>> PostTextAdAsync([FromBody] CreateTextAdCommand request)
        {
            Guid createdAdId = await Mediator.Send(request);
            return Ok(createdAdId);
        }
        
        [HttpPost("banner-ad")]
        public async Task<ActionResult<Guid>> PostBannerAdAsync([FromBody] CreateBannerAdCommand request)
        {
            Guid createdAdId = await Mediator.Send(request);
            return Ok(createdAdId);
        }
        
        [HttpPost("video-ad")]
        public async Task<ActionResult<Guid>> PostVideoAdAsync([FromBody] CreateVideoAdCommand request)
        {
            Guid createdAdId = await Mediator.Send(request);
            return Ok(createdAdId);
        }
        
        [HttpPost("html-ad")]
        public async Task<ActionResult<Guid>> PostHtmlAdAsync([FromBody] CreateHtmlAdCommand request)
        {
            Guid createdAdId = await Mediator.Send(request);
            return Ok(createdAdId);
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the ad, making its AdType equal to TextAd.
        /// </summary>
        [HttpPut("text-ad/{id}")]
        public async Task<IActionResult> PutTextAdAsync([FromRoute] Guid id, [FromBody] UpdateTextAdCommand request)
        {
            if (id != request.AdId) return BadRequest();
            await Mediator.Send(request);
            return NoContent();
        }

        /// <summary>
        /// Updates the ad, making its AdType equal to BannerAd.
        /// </summary>
        [HttpPut("banner-ad/{id}")]
        public async Task<IActionResult> PutBannerAdAsync([FromRoute] Guid id, [FromBody] UpdateBannerAdCommand request)
        {
            if (id != request.AdId) return BadRequest();
            await Mediator.Send(request);
            return NoContent();
        }

        /// <summary>
        /// Updates the ad, making its AdType equal to VideoAd.
        /// </summary>
        [HttpPut("video-ad/{id}")]
        public async Task<IActionResult> PutVideoAdAsync([FromRoute] Guid id, [FromBody] UpdateVideoAdCommand request)
        {
            if (id != request.AdId) return BadRequest();
            await Mediator.Send(request);
            return NoContent();
        }

        /// <summary>
        /// Updates the ad, making its AdType equal to HtmlAd.
        /// </summary>
        [HttpPut("html-ad/{id}")]
        public async Task<IActionResult> PutHtmlAdAsync([FromRoute] Guid id, [FromBody] UpdateHtmlAdCommand request)
        {
            if (id != request.AdId) return BadRequest();
            await Mediator.Send(request);
            return NoContent();
        }

        #endregion

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

        /// <summary>
        /// Attach tags to the ad of the type 'TextAd' automatically by extracting the content keywords.
        /// </summary>
        /// <returns>
        /// The attached tags.
        /// </returns>
        [HttpPost("text-ad/{id}/tags/auto")]
        public async Task<ActionResult<List<TagDto>>> AttachTagsToTextAdByExtractingContentKeywordsAsync(
            [FromRoute] Guid id)
        {
            List<TagDto> attachedTags = await Mediator.Send(new AttachTagsToTextAdByExtractingContentKeywordsCommand
                {AdId = id});
            return Ok(attachedTags);
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