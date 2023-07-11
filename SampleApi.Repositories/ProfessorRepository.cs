using Microsoft.Data.SqlClient;
using SampleApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Repositories
{
	public class ProfessorRepository : IRepository<ProfessorPOCO, int>
	{

		private string _cnstr;
		public ProfessorRepository(string cnstr)
		{
			_cnstr = cnstr;
		}
		public int Add(ProfessorPOCO obj)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			ProfessorPOCO todel = new ProfessorPOCO();


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
						string requete = "Select * FROM Professor WHERE professor_id = @id";
						ocmd.CommandText = requete;

						ocmd.Parameters.AddWithValue("id", id);

						SqlDataReader oDr = ocmd.ExecuteReader();
						if (oDr.Read())
						{
							//4 -  je mets les données dans mon object
							//Mapping
							todel = Map(oDr);

						}

						oDr.Close();
						if (todel != null)
						{							 
							ocmd.CommandText = "DELETE FROM PROFESSOR WHERE Professor_Id= @id";
							ocmd.Parameters.AddWithValue("id", id);

						}

					}

					
					
					 
					 
				}
				catch (Exception)
				{

					throw;
				}
			}
			//5 je retourne l'objet 
		 
		}

		public ProfessorPOCO Get(int id)
		{
			ProfessorPOCO monRetour = new ProfessorPOCO();


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
						string requete = "Select * FROM Professor WHERE professor_id = @id";
						ocmd.CommandText = requete;
						 
						ocmd.Parameters.AddWithValue("id", id);

						SqlDataReader oDr = ocmd.ExecuteReader();
						if (oDr.Read())
						{
							//4 -  je mets les données dans mon object
							//Mapping
							monRetour = Map(oDr);

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

		public IEnumerable<ProfessorPOCO> GetAll()
		{
			List<ProfessorPOCO> monRetour = new List<ProfessorPOCO>();


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
						string requete = "Select * FROM Professor";
						ocmd.CommandText = requete;
						 

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

		public void Update(ProfessorPOCO obj)
		{
			throw new NotImplementedException();
		}

		private ProfessorPOCO Map(SqlDataReader oDr)
		{
			ProfessorPOCO monRetour = new ProfessorPOCO();
			monRetour.Professor_ID = (int)oDr["Professor_ID"];
			if (oDr["professor_name"] != DBNull.Value)
			{
				monRetour.Professor_Name = (string)oDr["professor_name"];
			}
			if (oDr["Professor_Surname"] != DBNull.Value)
			{
				monRetour.Professor_Surname = (string)oDr["Professor_Surname"];
			}
			if (oDr["Section_ID"] != DBNull.Value)
			{
				monRetour.Section_ID = (int)oDr["Section_ID"];

			}
			if (oDr["Professor_office"] != DBNull.Value)
			{
				monRetour.Professor_Office = (int)oDr["Professor_office"];

			}
			if (oDr["professor_email"] != DBNull.Value)
			{
				monRetour.Professor_Email = (string)oDr["professor_email"];
			}
			if (oDr["professor_Hire_Date"] != DBNull.Value)
			{
				monRetour.Professor_HireDate = (DateTime)oDr["professor_Hire_Date"];
			}
			if (oDr["Professor_Wage"] != DBNull.Value)
			{
				monRetour.Professor_Wage = (int)oDr["Professor_Wage"];

			}

			return monRetour;
		}
	
	
	    
	
	}
}
