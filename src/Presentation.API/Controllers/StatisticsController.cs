using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Statistics.Queries;
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
        public async Task<IActionResult> GetViewsOfEachAdType()
        {
            Dictionary<AdType, int> viewsOfEachAdType = await Mediator.Send(new GetViewsOfEachAdTypeQuery());
            return Ok(viewsOfEachAdType);
        }
    }
}