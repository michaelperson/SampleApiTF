using Microsoft.Data.SqlClient;
using SampleApi.EF;
using SampleApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Repositories
{
	public class StudentRepository : IRepository<StudentPOCO,int>
	{
		private string _cnstr;
		public StudentRepository(string cnstr)
		{
			_cnstr = cnstr;
		}

		#region ADO
		//public int Add(StudentPOCO obj)
		//{
		//	int id;
		//	try
		//	{
		//		using (SqlConnection conn = new SqlConnection(_cnstr))
		//		{
		//			conn.Open();
		//			using (SqlCommand cmd = conn.CreateCommand())
		//			{
		//				cmd.CommandText = "INSERT INTO STUDENT OUTPUT inserted.student_id VALUES ( @firstname, @lastname, @birthdate, @login, @sectionid, @yearresult, @courseid)";

		//				cmd.Parameters.Add("@firstname", System.Data.SqlDbType.VarChar).Value = obj.First_Name;
		//				cmd.Parameters.AddWithValue("lastname", obj.Last_Name);
		//				cmd.Parameters.AddWithValue("birthdate", obj.BirthDate);
		//				cmd.Parameters.AddWithValue("login", obj.Login);
		//				cmd.Parameters.AddWithValue("sectionid", obj.Section_ID);
		//				cmd.Parameters.AddWithValue("yearresult", obj.Year_Result);
		//				cmd.Parameters.AddWithValue("courseid", obj.Course_ID);

		//			 id=(int)cmd.ExecuteScalar();
		//			}
		//			conn.Close();
		//			return id;
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		//TODO : manage exception
		//		throw;
		//	}
		//} 
		#endregion
		public int Add(StudentPOCO studentPOCO)
		{
			DbSlideContext ctx = new DbSlideContext();
			Student s = Map(studentPOCO);
			ctx.Students.Add(s);

			try
			{
				ctx.SaveChanges();
				return (int)s.StudentId;
			}
			catch (Exception)
			{
				return -1;
			}
			 
		}
		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public StudentPOCO Get(int id)
		{
			StudentPOCO monRetour = new StudentPOCO();
			DbSlideContext ctx = new DbSlideContext();

			monRetour = Map(ctx.Students.FirstOrDefault(x => x.StudentId == id));
		    return monRetour;
				 
		}

		public IEnumerable<StudentPOCO> GetAll()
		{
			DbSlideContext ctx = new DbSlideContext();

			return ctx.Students.Select(E=> StudentRepository.Map(E));
		}

		

		public void Update(StudentPOCO obj)
		{
			throw new NotImplementedException();
		}
		private static Student Map(StudentPOCO e)
		{
			return new Student()
			{
				BirthDate = e.BirthDate,
				CourseId =  e.Course_ID,
				FirstName = e.First_Name,
				LastName = e.Last_Name,
				SectionId = e.Section_ID,
				StudentId = e.Student_ID,
				YearResult = e.Year_Result

			};
		}
		private static StudentPOCO Map(Student e)
		{
			return new StudentPOCO()
			{
				BirthDate = e.BirthDate.HasValue ? e.BirthDate.Value : DateTime.MinValue,
				Course_ID = e.CourseId,
				First_Name = e.FirstName,
				Last_Name = e.LastName,
				Section_ID = e.SectionId.HasValue ? e.SectionId.Value : 0,
				Student_ID = e.StudentId,
				Year_Result = e.YearResult.HasValue ? e.YearResult.Value : 0

			};
		}
	}
}
