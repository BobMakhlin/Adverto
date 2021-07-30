using System.Collections.Generic;
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
        [HttpGet("views-of-each-adtype")]
        public async Task<ActionResult<Dictionary<AdType, int>>> GetViewsOfEachAdTypeAsync()
        {
            Dictionary<AdType, int> viewsOfEachAdType = await Mediator.Send(new GetViewsOfEachAdTypeQuery());
            return Ok(viewsOfEachAdType);
        }

        [HttpGet("top3-ad-categories")]
        public async Task<ActionResult<List<CategoryDto>>> GetTop3AdCategoriesAsync()
        {
            List<CategoryDto> top3AdCategories = await Mediator.Send(new GetTop3AdCategoriesQuery());
            return Ok(top3AdCategories);
        }

        [HttpGet("top10-ads-by-views")]
        public async Task<ActionResult<List<AdDto>>> GetTop10AdsByViewsAsync()
        {
            List<AdDto> top10AdsByViews = await Mediator.Send(new GetTop10AdsByViewsQuery());
            return Ok(top10AdsByViews);
        }
        
        [HttpGet("top15-tags-by-views")]
        public async Task<ActionResult<List<TagDto>>> GetTop15TagsByViewsAsync()
        {
            List<TagDto> top15TagsByViews = await Mediator.Send(new GetTop15TagsByViewsQuery());
            return Ok(top15TagsByViews);
        }
    }
}