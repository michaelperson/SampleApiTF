using Exo_Linq_Context;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Entities;
using SampleApi.Models;
using SampleApi.Repositories;

namespace SampleApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private IContext _ctx;
		private IRepository<StudentPOCO,int> repo;
		public StudentController(IContext context, IRepository<StudentPOCO,int> repost)
		{
			_ctx = context;
			repo = repost;
		}

		[HttpGet]
	
		public IActionResult GetAllStudent()
		{
			 
			return Ok(_ctx.Students);
		}

		[HttpGet]
		[Route("{idStudent:int}")]
		public IActionResult GetStudentById(int idStudent)
		{
			 
			StudentPOCO st = repo.Get(idStudent);
			if (st.Student_ID != 0)
			{
				//Mapper
				StudentDTO toReturn = new StudentDTO()
				{
					Section_ID = st.Section_ID,
					Student_ID = st.Student_ID,
					FullName = $"{st.First_Name} {st.Last_Name}",
					Course_ID = st.Course_ID,
					BirthDate = st.BirthDate,
					Login = st.Login,
					Year_Result = st.Year_Result
				};

				return Ok(toReturn);
			}
			else
			{
				return NotFound(idStudent);
			}
			 
		}

		[HttpGet]
		[Route("section/{sectionId:int}")]
		public IActionResult GetStudentBySectionId(int sectionId)
		{
			 
			return Ok(_ctx.Students.Where(s => s.Section_ID == sectionId));
		}

		[HttpGet]
		[Route("{name:alpha}")]
		public IActionResult GetStudentByName(string name)
		{
			 
			return Ok(_ctx.Students.SingleOrDefault(s => s.Last_Name == name));
		}

		[HttpPost]
		public IActionResult PostStudent( StudentCreateDTO student) 
		{
			try
			{
				StudentPOCO studentPOCO = new StudentPOCO()
				{
					Section_ID = student.Section_ID,
					First_Name = student.First_Name,
					BirthDate = student.BirthDate,
					Login = student.Login,
					Course_ID = student.Course_ID,
					Last_Name = student.Last_Name,
					Year_Result = student.Year_Result,
					
					

				} ;

				int studentId = repo.Add(studentPOCO);

				return new CreatedResult($"https://localhost:7025/api/Student/{studentId}", studentPOCO);
			}
			catch (Exception)
			{
				return BadRequest(student);
			}
		}
	}
}
