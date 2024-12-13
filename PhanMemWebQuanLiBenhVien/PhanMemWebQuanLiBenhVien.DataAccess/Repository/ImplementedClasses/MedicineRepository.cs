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
                if (medicine.Name != null) objFromDb.Name = medicine.Name;
                if (medicine.Usage != null) objFromDb.Usage = medicine.Usage;
                if (medicine.Unit != null) objFromDb.Unit = medicine.Unit;
                if (medicine.Price != 0) objFromDb.Price = medicine.Price;
                if (medicine.Quantity != 0) objFromDb.Quantity = medicine.Quantity;
                if (medicine.ExpiryDate != default(DateTime)) objFromDb.ExpiryDate = medicine.ExpiryDate;
                _db.SaveChanges();
            }
        }
    }
}
