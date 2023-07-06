using Microsoft.Data.SqlClient;
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

		public int Add(StudentPOCO obj)
		{
			int id;
			try
			{
				using (SqlConnection conn = new SqlConnection(_cnstr))
				{
					conn.Open();
					using (SqlCommand cmd = conn.CreateCommand())
					{
						cmd.CommandText = "INSERT INTO STUDENT OUTPUT inserted.student_id VALUES ( @firstname, @lastname, @birthdate, @login, @sectionid, @yearresult, @courseid)";

						cmd.Parameters.Add("@firstname", System.Data.SqlDbType.VarChar).Value = obj.First_Name;
						cmd.Parameters.AddWithValue("lastname", obj.Last_Name);
						cmd.Parameters.AddWithValue("birthdate", obj.BirthDate);
						cmd.Parameters.AddWithValue("login", obj.Login);
						cmd.Parameters.AddWithValue("sectionid", obj.Section_ID);
						cmd.Parameters.AddWithValue("yearresult", obj.Year_Result);
						cmd.Parameters.AddWithValue("courseid", obj.Course_ID);

					 id=(int)cmd.ExecuteScalar();
					}
					conn.Close();
					return id;
				}
			}
			catch (Exception ex)
			{
				//TODO : manage exception
				throw;
			}
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public StudentPOCO Get(int id)
		{
			StudentPOCO monRetour = new StudentPOCO();
			 
			
			using (SqlConnection oConn = new SqlConnection(_cnstr))
			{
				try
				{
					/*1- Connect */
					oConn.Open();
					//2- je prépare ma requête
					using (SqlCommand ocmd = oConn.CreateCommand())
					{
						//3 -  je récupère les données
						string requete = "Select * FROM Student WHERE student_id = @id";
						ocmd.CommandText = requete;
						//SqlParameter monId = new SqlParameter();
						//monId.Value = id;
						//monId.SqlDbType = System.Data.SqlDbType.Int;
						//monId.ParameterName= "id";
						//ocmd.Parameters.Add(monId);
						ocmd.Parameters.AddWithValue("id", id);

						SqlDataReader oDr = ocmd.ExecuteReader();
						if (oDr.Read())
						{

							//4 -  je mets les données dans mon object
							//Mapping
							monRetour.Student_ID = (int)oDr["student_id"];
							if (oDr["first_name"] != DBNull.Value)
							{
								monRetour.First_Name = oDr["first_name"].ToString();
							}
							if (oDr["last_name"] != DBNull.Value)
							{
								monRetour.Last_Name = oDr["last_name"].ToString();
							}
							if (oDr["course_id"] != DBNull.Value)
							{
								monRetour.Course_ID = oDr["course_id"].ToString();

							}
							if (oDr["Section_ID"] != DBNull.Value)
							{
								monRetour.Section_ID = (int)oDr["Section_ID"];

							}
							if (oDr["birth_date"] != DBNull.Value)
							{
								monRetour.BirthDate = (DateTime)oDr["birth_date"];
							}
							if (oDr["Login"] != DBNull.Value)
							{
								monRetour.Login = oDr["Login"].ToString();
							}
							if (oDr["year_result"] != DBNull.Value)
							{
								monRetour.Year_Result = (int)oDr["year_result"];

							}
						}
						 
						oDr.Close();
						
					}
					
						oConn.Close();
					return monRetour;
				}
				catch (Exception)
				{

					throw;
				}
			}
			//5 je retourne l'objet 
		}

		public IEnumerable<StudentPOCO> GetAll()
		{
			throw new NotImplementedException();
		}

		public void Update(StudentPOCO obj)
		{
			throw new NotImplementedException();
		}
	}
}
