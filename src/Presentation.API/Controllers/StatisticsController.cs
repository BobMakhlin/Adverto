using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.CQRS.Ads.Models;
using Application.CQRS.Categories.Model;
using Application.CQRS.Statistics.Queries;
using Application.CQRS.Tags.Models;
using Domain.Primary.Entities;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.ControllerAbstractions;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : MyBaseController
    {
        /// <summary>
        /// Returns the count of views by each ad-type.
        /// </summary>
        [HttpGet("views-of-each-adtype")]
        public async Task<ActionResult<KeyValuePair<AdType, int>>> GetViewsOfEachAdTypeAsync()
        {
            Dictionary<AdType, int> viewsOfEachAdType = await Mediator.Send(new GetViewsOfEachAdTypeQuery());
            return Ok(viewsOfEachAdType.ToList());
        }

        /// <summary>
        /// Returns top-3 categories of ads by frequency of use.
        /// </summary>
        [HttpGet("top3-ad-categories")]
        public async Task<ActionResult<List<CategoryDto>>> GetTop3AdCategoriesAsync()
        {
            List<CategoryDto> top3AdCategories = await Mediator.Send(new GetTop3AdCategoriesQuery());
            return Ok(top3AdCategories);
        }

        /// <summary>
        /// Returns top-10 ads by views.
        /// </summary>
        [HttpGet("top10-ads-by-views")]
        public async Task<ActionResult<List<AdDto>>> GetTop10AdsByViewsAsync()
        {
            List<AdDto> top10AdsByViews = await Mediator.Send(new GetTop10AdsByViewsQuery());
            return Ok(top10AdsByViews);
        }
        
        /// <summary>
        /// Returns top-15 tags by views.
        /// </summary>
        [HttpGet("top15-tags-by-views")]
        public async Task<ActionResult<List<TagDto>>> GetTop15TagsByViewsAsync()
        {
            List<TagDto> top15TagsByViews = await Mediator.Send(new GetTop15TagsByViewsQuery());
            return Ok(top15TagsByViews);
        }
    }
}