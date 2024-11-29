using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.Ultilities
{
    public class Utilities
    {
        public enum EGender
        {
            male,
            female
        }
        public enum ERole
        { 
            admin,
            doctor,
            nurse,
            moderator
        }
        public enum ETrangThaiDieuTri
        {
            nhapvien,
            xuatvien,
            chikham
        }
        public enum ETinhTrangBenhNhan
        {
            khongtrieuchung,
            cotrieuchung,
            tronang
        }
    }
}
