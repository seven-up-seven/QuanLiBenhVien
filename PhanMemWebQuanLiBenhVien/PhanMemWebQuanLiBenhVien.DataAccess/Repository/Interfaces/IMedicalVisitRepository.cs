using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces
{
	public interface IMedicalVisitRepository : IRepository<MedicalVisit>
	{
		public void Update(MedicalVisit medicalVisit);
	}
}
