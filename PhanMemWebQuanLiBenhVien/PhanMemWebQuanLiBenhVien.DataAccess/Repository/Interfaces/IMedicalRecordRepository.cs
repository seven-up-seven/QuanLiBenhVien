using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces
{
    public interface IMedicalRecordRepository: IRepository<MedicalRecord>
    {
        public void Update(MedicalRecord medicalRecord);
    }
}
