using Exo_Linq_Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers.V2
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[ApiVersion("2.0")]
	public class SectionController : ControllerBase
	{
		private readonly IContext _ctx; 

		public SectionController(IContext context)
		{
			_ctx = context;
		}
		/// <summary>
		/// Permet de récupérer toutes les sections
		/// </summary>
		/// <returns>Un <see cref="IEnumerable{Section}"/></returns>
		
		[HttpGet]
		[ResponseCache(Duration = 0)]
		[Produces("application/json")]
		[ProducesResponseType(typeof(IEnumerable<Section>), StatusCodes.Status400BadRequest)] 
		[ProducesResponseType(typeof(IEnumerable<Section>), StatusCodes.Status200OK)]
		public IEnumerable<Section> GetAll()
		{
			 return _ctx.Sections;
		}

		[HttpGet]
		[Route("{section_id:int}")]
		[ResponseCache(Duration = 0)]
		[Produces("application/json")]
		[ProducesResponseType(typeof(Section), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(Section), StatusCodes.Status200OK)]
		public IActionResult GetById(int section_id)
		{
			return Ok(_ctx.Sections.SingleOrDefault(s=>s.Section_ID==section_id));
		}


		[HttpPost]
		[Produces("application/json")]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
		[MapToApiVersion("2.0")]
		public IActionResult CreateSection([FromBody]Section sec)
		{
			try
			{
				 
				_ctx.Sections.Add(sec);
				  return new CreatedResult($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/Section/{sec.Section_ID}", sec);
				}
			catch (Exception)
			{
				return new BadRequestResult();
			};
		}
	}
}
