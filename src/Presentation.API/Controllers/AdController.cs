    using System;
    using System.Threading.Tasks;
    using Application.CQRS.Ads.Commands;
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
    }
}