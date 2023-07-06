using Exo_Linq_Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Entities;
using SampleApi.Models;
using SampleApi.Repositories;
using System.Net;

namespace SampleApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SectionController : ControllerBase
	{
		private IContext _ctx;
		private IRepository<SectionPOCO> _sectionRepo;
		public SectionController(IContext context, IRepository<SectionPOCO> sec)
		{
			_ctx = context;
			_sectionRepo= sec;
		}
		/// <summary>
		/// Permet de récupérer toutes les sections
		/// </summary>
		/// <returns>Un <see cref="IEnumerable{Section}"/></returns>
		
		[HttpGet] 		
		public IActionResult GetAll()
		{
			try
			{
				return Ok(_ctx.Sections);
			}
			catch (Exception)
			{
				return NotFound();
			}
		}

		[HttpGet]
		[Route("{id:int}")]
		public IActionResult Get(int id)
		{ 
			SectionPOCO section = _sectionRepo.Get(id);
			if (section != null)
			{
			//MAPPER
			SectionDTO DTOSection = new SectionDTO();
			DTOSection.Section_Id = section.Section_Id;
			DTOSection.Section_Name = section.Section_Name;
			DTOSection.Delegate_id = section.Delegate_id;
			
				return Ok(DTOSection);
			}else
			{
				return NotFound(id);
			}
		}

		[HttpDelete]
		[Route("{id:int}")]
		public IActionResult Delete(int id)
		{
			Section secToDelete = _ctx.Sections.SingleOrDefault(s => s.Section_ID == id);
			if(secToDelete != null)
			{
				_ctx.Sections.Remove(secToDelete);
				return NoContent();
			}
			else
			{
				return NotFound();
			}
			 
		}

		[HttpPost]	 
		public IActionResult CreateSection([FromBody]Section sec)
		{

			try
			{
				_ctx.Sections.Add(sec);
				return new CreatedResult($"https://localhost:7025/api/Section/{sec.Section_ID}", sec);
				 
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
	}
}
