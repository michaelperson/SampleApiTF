using Microsoft.Data.SqlClient;
using SampleApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Repositories
{
	public class CoursRepository : IRepository<CoursePOCO, string>
	{
		private string _cnstr;

		public CoursRepository(string cnstr)
		{
			_cnstr = cnstr;
		}
		public string Add(CoursePOCO obj)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public CoursePOCO Get(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CoursePOCO> GetAll()
		{
			throw new NotImplementedException();
		}

		public void Update(CoursePOCO obj)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CoursePOCO>GetByProfessorId(int Pid)
		{
			List<CoursePOCO> monRetour = new List<CoursePOCO>();


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
						string requete = "Select  * FROM Course WHERE professor_id=@id";
						ocmd.CommandText = requete;
						ocmd.Parameters.AddWithValue("id", Pid);

						SqlDataReader oDr = ocmd.ExecuteReader();
						while (oDr.Read())
						{
							//4 -  je mets les données dans mon object
							//Mapping
							monRetour.Add(Map(oDr));

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

		private CoursePOCO Map(SqlDataReader oDr)
		{
			CoursePOCO cp = new CoursePOCO();

			cp.Course_Name = (string)oDr["Course_name"];
			cp.Course_Ects = (decimal)oDr["Course_Ects"];
			cp.Course_ID = (string)oDr["Course_id"];
			cp.Professor_ID = (int)oDr["professor_id"];

			return cp;
		}
	}
}
