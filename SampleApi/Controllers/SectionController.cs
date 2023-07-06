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
		private IRepository<SectionPOCO, int> _sectionRepo;
		public SectionController(IContext context, IRepository<SectionPOCO,int> sec)
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
		public IActionResult CreateSection([FromBody]SectionDTO sec)
		{

			try
			{
				SectionPOCO loco = new SectionPOCO();
				loco.Section_Name = sec.Section_Name;
				loco.Section_Id= sec.Section_Id;
				loco.Delegate_id= sec.Delegate_id;

				int id = _sectionRepo.Add(loco);
				return new CreatedResult($"https://localhost:7025/api/Section/{id}", sec);
				 
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		[HttpPut] 
		public IActionResult UpdateSection (int section_id, [FromBody]SectionUpDTO upSection)
		{
			try
			{
				SectionPOCO exSec = _sectionRepo.Get(section_id);
				if (exSec != null)
				{
					 	exSec.Section_Name=			  upSection.Section_Name;
					exSec.Delegate_id= upSection.Delegate_id;

					_sectionRepo.Update(exSec);
					return Ok(exSec);
				}
				else
				{
					return NotFound(section_id);
				}
			}
			catch (Exception)
			{
				return NotFound(section_id);
			}
		}
	}
}
