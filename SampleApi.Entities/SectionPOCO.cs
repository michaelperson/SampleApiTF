using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Entities
{
	/// <summary>
	/// Classe permettant de stocker les données
	/// de la table section
	/// </summary>
	public class SectionPOCO
	{
		public int Section_Id { get; set; }
		public string Section_Name { get; set;}
		public int Delegate_id { get; set; }
	}
}
