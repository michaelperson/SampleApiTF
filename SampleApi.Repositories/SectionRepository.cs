using Microsoft.Data.SqlClient;
using SampleApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Repositories
{
	public class SectionRepository : IRepository<SectionPOCO, int>
	{
		private string _cnstr;

		public SectionRepository(string cnstr)
		{
			_cnstr = cnstr;
		}

		public int Add(SectionPOCO obj)
		{
			int id;
			try
			{
				using (SqlConnection conn = new SqlConnection(_cnstr))
				{
					conn.Open();
					using (SqlCommand cmd = conn.CreateCommand())
					{
						cmd.CommandText = "INSERT INTO Section OUTPUT inserted.section_id VALUES ( @section_id, @sectionname, @delegateid)";

						cmd.Parameters.Add("@section_id", System.Data.SqlDbType.VarChar).Value = obj.Section_Id;
						 
						cmd.Parameters.AddWithValue("sectionname", obj.Section_Name);
						cmd.Parameters.AddWithValue("delegateid", obj.Delegate_id);
						 

						id = (int)cmd.ExecuteScalar();
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

		public SectionPOCO Get(int id)
		{
			SectionPOCO SectionRecuperee=null;


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
						string requete = "Select * FROM Section WHERE section_id = @section_id";
						ocmd.CommandText = requete; 
						ocmd.Parameters.AddWithValue("section_id", id);
						SqlDataReader oDr = ocmd.ExecuteReader();
						if(oDr.Read())
						{  
							SectionRecuperee= new SectionPOCO();
							//4 -  je mets les données dans mon object
							//Mapping
							SectionRecuperee.Section_Id = (int)oDr["Section_id"];
							if (oDr["Section_name"]!=DBNull.Value)
							{
								SectionRecuperee.Section_Name = (string)oDr["Section_name"] ;
							}
							if (oDr["Delegate_Id"] != DBNull.Value)
							{
								SectionRecuperee.Delegate_id = (int)oDr["Delegate_Id"];
							}
						}
						oDr.Close();

					}

					oConn.Close();
					return SectionRecuperee;
				}
				catch (Exception)
				{

					throw;
				}
			}
			//5 je retourne l'objet  
		}

		public IEnumerable<SectionPOCO> GetAll()
		{
			throw new NotImplementedException();
		}

		public void Update(SectionPOCO obj)
		{
			using (SqlConnection oCon = new SqlConnection(_cnstr))
			{
				try
				{
					oCon.Open();
					using (SqlCommand oCmd = oCon.CreateCommand())
					{
						string query = "UPDATE SECTION SET section_name=@sectionname, " +
							"delegate_id=@delegateid WHERE section_id=@sectionid";
						oCmd.CommandText = query;

						oCmd.Parameters.AddWithValue("sectionid", obj.Section_Id); oCmd.Parameters.AddWithValue("delegateid", obj.Delegate_id); oCmd.Parameters.AddWithValue("sectionname", obj.Section_Name);
					 
						oCmd.ExecuteNonQuery();	


					}
					oCon.Close();	
				}
				catch (Exception)
				{

					throw;
				}
			}
		}
	}
}
