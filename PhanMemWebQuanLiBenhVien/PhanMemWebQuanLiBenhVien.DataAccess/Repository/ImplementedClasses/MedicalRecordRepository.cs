﻿using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class MedicalRecordRepository : Repository<MedicalRecord>, IMedicalRecordRepository
    {
        private ApplicationDbContext _db;
        public MedicalRecordRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MedicalRecord medicalRecord)
        {
            throw new NotImplementedException();
        }
    }
}
