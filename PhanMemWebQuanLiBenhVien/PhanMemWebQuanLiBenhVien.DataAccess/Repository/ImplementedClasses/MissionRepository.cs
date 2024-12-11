using Microsoft.EntityFrameworkCore;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class MissionRepository : Repository<Mission>, IMissionRepository
    {
        private ApplicationDbContext _db;
        public MissionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Mission mission)
        {
            var oldobj = _db.missions.FirstOrDefault(u => u.MissionId == mission.MissionId);
            if (oldobj != null)
            {
                oldobj.Time = mission.Time;
                oldobj.EndTime = mission.EndTime;
                oldobj.Lever = mission.Lever;
                oldobj.Content = mission.Content;
                oldobj.DoctorId = mission.DoctorId;
                oldobj.IsCompleted= mission.IsCompleted;
                oldobj.RoomType = mission.RoomType;
                if (mission.RoomType == EPhong.phongkham)
                {

                    oldobj.PhongBenhId = null;
                    oldobj.PhongKhamId = mission.PhongKhamId;
                    oldobj.PhongKham = _db.phongKhams.FirstOrDefault(m => m.RoomId == mission.PhongKhamId);
                }
                else
                {
                    oldobj.PhongKhamId = null;
                    oldobj.PhongBenhId = mission.PhongBenhId;
                    oldobj.PhongBenh = _db.phongBenhs.FirstOrDefault(m => m.RoomId == mission.PhongBenhId);
                }
                _db.SaveChanges();
            }
        }
    }
}
