using PhanMemWebQuanLiBenhVien.DataAccess;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models.Models;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class ActivityTrackingFunction
    {
        private ApplicationDbContext _db;
        private IUnitOfWork _unitofwork;
        public ActivityTrackingFunction(ApplicationDbContext db, IUnitOfWork unitOfWork)
        {
            _db = db;
            _unitofwork = unitOfWork;
        }
        public void TrackingActivity(int MemberId, string MemberName, ETypeOfActivity type, ERole MemberRole, int ObjectId, object Object, List<string>? UpdateDetails)
        {
            ActivityHistory entity= new ActivityHistory();
            entity.MemberId = MemberId; 
            if (ObjectId != null) entity.ObjectId = ObjectId;
            entity.MemberName = MemberName;
            entity.MemberRole = MemberRole;
            entity.ActivityTime= DateTime.Now;
            string ObjectName = Object.GetType().Name;
            if (Object.GetType().Name == "CustomedUser")
            {
                ObjectName = "tài khoản";
                var obj =(CustomedUser)Object;
                entity.UpdateDetails = entity.UpdateDetails + "Tài khoản " + obj.UserRole;
            }
            string VaiTro="";
            if (MemberRole == ERole.nurse) VaiTro = "Y tá ";
            else if (MemberRole == ERole.doctor) VaiTro = "Bác sĩ ";
            else if (MemberRole == ERole.admin) VaiTro = "Admin ";
            else if (MemberRole == ERole.quanlinhansu) VaiTro = "Quản lí nhân sự ";
            else if (MemberRole==ERole.quanlivattu) VaiTro = "Quản lí vật tư ";
            else if (MemberRole == ERole.quanlibenhnhan) VaiTro = "Quản lí bệnh nhân ";
            if (type == ETypeOfActivity.sua)
            {
                entity.Activity = VaiTro + MemberName + " Đã sửa " + ObjectName + " với ID là " + ObjectId;
                if (UpdateDetails!=null && UpdateDetails.Count>0)
                {
                    foreach (var detail in UpdateDetails)
                    {
                        entity.UpdateDetails = entity.UpdateDetails + detail + ",";
                    }
                    entity.UpdateDetails.TrimEnd(',');
                }
                if (ObjectName=="MedicalRecord")
                {
                    var mr = (MedicalRecord)Object;
                    var true_mr = _unitofwork.MedicalRecordRepository.Get(u => u.MedicalRecordId == mr.MedicalRecordId);
                    var cccd = _unitofwork.PatientRepository.Get(u => u.PatientId == true_mr.PatientId).CCCD;
                    entity.Activity = entity.Activity + " của bệnh nhân với ID là " + true_mr.PatientId + " và CCCD là " + cccd;
                }
                if (ObjectName=="MedicalVisit")
                {
                    var mv = (MedicalVisit)Object;
                    var true_mv = _unitofwork.MedicalVisitRepository.Get(u => u.VisitId == mv.VisitId);
                    var mr = _unitofwork.MedicalRecordRepository.Get(u => u.MedicalRecordId == true_mv.MedicalRecordId);
                    var pt = _unitofwork.PatientRepository.Get(u => u.PatientId == mr.PatientId);
                    entity.Activity = entity.Activity + " của bệnh án với ID là " + mr.MedicalRecordId + " của bệnh nhân với ID là " + pt.PatientId;
                }
                if (UpdateDetails!=null && UpdateDetails.Count == 0) return;
            }
            else if (type==ETypeOfActivity.them)
            {
                entity.Activity = VaiTro + MemberName + " đã thêm " + ObjectName + " với ID là " + ObjectId;
                if (ObjectName == "MedicalRecord")
                {
                    var mr = (MedicalRecord)Object;
                    var true_mr = _unitofwork.MedicalRecordRepository.Get(u => u.MedicalRecordId == mr.MedicalRecordId);
                    var cccd = _unitofwork.PatientRepository.Get(u => u.PatientId == true_mr.PatientId).CCCD;
                    entity.Activity = entity.Activity + " của bệnh nhân với ID là " + true_mr.PatientId + " và CCCD là " + cccd;
                }
                if (ObjectName == "MedicalVisit")
                {
                    var mv = (MedicalVisit)Object;
                    var true_mv = _unitofwork.MedicalVisitRepository.Get(u => u.VisitId == mv.VisitId);
                    var mr = _unitofwork.MedicalRecordRepository.Get(u => u.MedicalRecordId == true_mv.MedicalRecordId);
                    var pt = _unitofwork.PatientRepository.Get(u => u.PatientId == mr.PatientId);
                    entity.Activity = entity.Activity + " của bệnh án với ID là " + mr.MedicalRecordId + " của bệnh nhân với ID là " + pt.PatientId;
                }
            }               
            else if (type==ETypeOfActivity.xoa)
            {
                entity.Activity = VaiTro + MemberName + " đã xóa " + ObjectName + " với ID là " + ObjectId;
                if (ObjectName == "MedicalRecord")
                {
                    var mr = (MedicalRecord)Object;
                    var true_mr = _unitofwork.MedicalRecordRepository.Get(u => u.MedicalRecordId == mr.MedicalRecordId);
                    var cccd = _unitofwork.PatientRepository.Get(u => u.PatientId == true_mr.PatientId).CCCD;
                    entity.Activity = entity.Activity + " của bệnh nhân với ID là " + true_mr.PatientId + " và CCCD là " + cccd;
                }
                if (ObjectName == "MedicalVisit")
                {
                    var mv = (MedicalVisit)Object;
                    var true_mv = _unitofwork.MedicalVisitRepository.Get(u => u.VisitId == mv.VisitId);
                    var mr = _unitofwork.MedicalRecordRepository.Get(u => u.MedicalRecordId == true_mv.MedicalRecordId);
                    var pt = _unitofwork.PatientRepository.Get(u => u.PatientId == mr.PatientId);
                    entity.Activity = entity.Activity + " của bệnh án với ID là " + mr.MedicalRecordId + " của bệnh nhân với ID là " + pt.PatientId;
                }
            }
            else if (type==ETypeOfActivity.dongbenhan)
            {
                entity.Activity = VaiTro + MemberName + " đã đóng " + ObjectName + " với ID là " + ObjectId;
                if (ObjectName == "MedicalRecord")
                {
                    var mr = (MedicalRecord)Object;
                    var true_mr = _unitofwork.MedicalRecordRepository.Get(u => u.MedicalRecordId == mr.MedicalRecordId);
                    var cccd = _unitofwork.PatientRepository.Get(u => u.PatientId == true_mr.PatientId).CCCD;
                    entity.Activity = entity.Activity + " của bệnh nhân với ID là " + true_mr.PatientId + " và CCCD là " + cccd;
                }
                if (ObjectName == "MedicalVisit")
                {
                    var mv = (MedicalVisit)Object;
                    var true_mv = _unitofwork.MedicalVisitRepository.Get(u => u.VisitId == mv.VisitId);
                    var mr = _unitofwork.MedicalRecordRepository.Get(u => u.MedicalRecordId == true_mv.MedicalRecordId);
                    var pt = _unitofwork.PatientRepository.Get(u => u.PatientId == mr.PatientId);
                    entity.Activity = entity.Activity + " của bệnh án với ID là " + mr.MedicalRecordId + " của bệnh nhân với ID là " + pt.PatientId;
                }
            }               
            else if (type==ETypeOfActivity.trichxuatthuoc) entity.Activity=VaiTro + MemberName + "đã trích xuất " + ObjectName + " với ID là " + ObjectId;
            else entity.Activity = VaiTro + MemberName + "đã hoàn thành " + ObjectName + " với ID là " + ObjectId;
            _unitofwork.ActivityHistoryRepository.Add(entity);
            _unitofwork.Save();
        }
    }
}
