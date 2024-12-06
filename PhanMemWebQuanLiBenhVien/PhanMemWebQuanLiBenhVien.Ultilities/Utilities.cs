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
        public enum EViTriLamViec
        {
            bsngoaitru,
            bsnoitru,
            bscapcuu
        }
        public enum ETrangThaiDieuTri
        {
            noitru, 
            ngoaitru
        }
        public enum ETrangThaiBenhAn
        {
            dangchuatri,
            ketthucchuatri
        }
        public enum ETinhTrangBenhNhan
        {
            khongtrieuchung,
            cotrieuchung,
            tronang
        }
    }
}
