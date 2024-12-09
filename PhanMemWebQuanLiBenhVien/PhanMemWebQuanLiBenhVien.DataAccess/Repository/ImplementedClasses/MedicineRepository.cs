using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class MedicineRepository: Repository<Medicine> , IMedicineRepository
    {
        ApplicationDbContext _db; 
        public MedicineRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;   
        }
        public void Update(Medicine medicine)
        {
            var objFromDb = _db.medicines.FirstOrDefault(m => m.MedicineId == medicine.MedicineId);
            if (objFromDb != null)
            {
                objFromDb.Name = medicine.Name;
                objFromDb.Usage = medicine.Usage;
                objFromDb.Unit = medicine.Unit;
                objFromDb.Price = medicine.Price;
                objFromDb.Quantity = medicine.Quantity;
                objFromDb.ExpiryDate = medicine.ExpiryDate;
                _db.SaveChanges();
            }
        }
    }
}
