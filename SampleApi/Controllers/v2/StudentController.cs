using Exo_Linq_Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers.V2
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[ApiVersion("2.0")]
	public class StudentController : ControllerBase
	{
		private IContext _ctx;

		public StudentController(IContext context)
		{
			_ctx = context;
		}

		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType(typeof(Student), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
		public IEnumerable<Student> GetAllStudent()
		{
			 
			return _ctx.Students;
		}

		[HttpGet]
		[Route("{idStudent:int}")]
		[Produces("application/json")]
		[ProducesResponseType(typeof(Student), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
		public IActionResult GetStudentById(int idStudent)
		{

			try
			{
				return Ok(_ctx.Students.SingleOrDefault(s => s.Student_ID == idStudent));
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("section/{sectionId:int}")]
		[Produces("application/json")]
		[ProducesResponseType(typeof(IEnumerable<Student>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Student>), StatusCodes.Status200OK)]
		public IActionResult GetStudentBySectionId(int sectionId)
		{

			try
			{
				return Ok(_ctx.Students.Where(s => s.Section_ID == sectionId));
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("{name:alpha}")]
		[Produces("application/json")]
		[ProducesResponseType(typeof(IEnumerable<Student>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Student>), StatusCodes.Status200OK)]
		public IActionResult GetStudentByName(string name)
		{

			try
			{
				return Ok(_ctx.Students.SingleOrDefault(s => s.Last_Name == name));
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}


	}
}
