using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Categories.Model;
using Application.CQRS.Categories.Queries.CategoryStorage;
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
    }
}