using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Tags.Models;
using Application.CQRS.Tags.Queries;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.ControllerAbstractions;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : MyBaseController
    {
        [HttpGet("list")]
        public async Task<ActionResult<List<TagDto>>> GetListOfTagsAsync()
        {
            List<TagDto> tags = await Mediator.Send(new GetListOfTagsQuery());
            return Ok(tags);
        }

        [HttpGet("paged-list")]
        public async Task<ActionResult<IPagedList<TagDto>>> GetPagedListOfTagsAsync([FromQuery] GetPagedListOfTagsQuery request)
        {
            IPagedList<TagDto> tags = await Mediator.Send(request);
            return Ok(tags);
        }
    }
}