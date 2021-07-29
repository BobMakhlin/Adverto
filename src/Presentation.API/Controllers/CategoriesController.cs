using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Categories.Model;
using Application.CQRS.Categories.Queries.CategoryStorage;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.ControllerAbstractions;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : MyBaseController
    {
        [HttpGet("list")]
        public async Task<ActionResult<List<CategoryDto>>> GetListOfCategoriesAsync()
        {
            List<CategoryDto> categories = await Mediator.Send(new GetListOfCategoriesQuery());
            return Ok(categories);
        }

        [HttpGet("paged-list")]
        public async Task<ActionResult<List<CategoryDto>>> GetPagedListOfCategoriesAsync(
            [FromQuery] GetPagedListOfCategoriesQuery request)
        {
            IPagedList<CategoryDto> categories = await Mediator.Send(request);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryByIdAsync([FromRoute] Guid id)
        {
            CategoryDto category = await Mediator.Send(new GetCategoryByIdQuery {CategoryId = id});
            return Ok(category);
        }
    }
}