using Exo_Linq_Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Entities;
using SampleApi.Models;
using SampleApi.Repositories;

namespace SampleApi.Controllers.v2
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[ApiVersion("2.0")]
	public class ProfessorController : ControllerBase
	{

		private IRepository<ProfessorPOCO, int> _Repo;
		IRepository<SectionPOCO, int> _secRepo;
		private CoursRepository _coursRepo;

		public ProfessorController(IContext context, IRepository<ProfessorPOCO, int> repo,
			IRepository<SectionPOCO, int> sec,
			IRepository<CoursePOCO, string> cours)
		{

			_Repo = repo;
			_secRepo = sec;
			_coursRepo = (CoursRepository)cours;
		}
		[HttpGet]
		[Route("{id}")]
		public IActionResult Get(int id)
		{
			return Ok(Map(_Repo.Get(id)));
		}
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_Repo.GetAll().Select(s => Map(s)));
		}

		private ProfessorDTO Map(ProfessorPOCO pOCO)
		{

			return new ProfessorDTO()
			{
				Email = pOCO.Professor_Email,
				Prof_ID = pOCO.Professor_ID,
				Name = pOCO.Professor_Name + "( " + pOCO.Professor_Surname + ")",
				Office = pOCO.Professor_Office,
				Section_name = _secRepo.Get(pOCO.Section_ID).Section_Name,
				Courses = _coursRepo.GetByProfessorId(pOCO.Professor_ID).Select(cp => Map(cp))

			};
		}

		private CourseDTO Map(CoursePOCO cp)
		{
			CourseDTO dt = new CourseDTO()
			{
				Professor_ID = cp.Professor_ID,
				Course_ID = cp.Course_ID,
				Course_Ects = cp.Course_Ects,
				Course_Name = cp.Course_Name,
			};
			 return dt;
		}
	}
}
