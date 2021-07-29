using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Tags.Commands.TagStorage;
using Application.CQRS.Tags.Models;
using Application.CQRS.Tags.Queries.TagStorage;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.ControllerAbstractions;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : MyBaseController
    {
        #region TagStorage

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

        [HttpGet("{id}")]
        public async Task<ActionResult<TagDto>> GetTagByIdAsync([FromRoute] Guid id)
        {
            TagDto tag = await Mediator.Send(new GetTagByIdQuery {TagId = id});
            return Ok(tag);
        }
        
        [HttpPost]
        public async Task<ActionResult<Guid>> PostTagAsync([FromBody] CreateTagCommand request)
        {
            Guid tagId = await Mediator.Send(request);
            return Ok(tagId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTagAsync([FromRoute] Guid id, [FromBody] UpdateTagCommand request)
        {
            if (id != request.TagId)
            {
                return BadRequest();
            }
            
            await Mediator.Send(request);
         
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTagAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteTagCommand {TagId = id});
            return NoContent();
        }

        #endregion
    }
}