using NuGet.Versioning;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class GlobalFunctions
    {
        private IUnitOfWork _unitofwork;
        private Doctor _doctor;
        private Patient _patient;
        private Nurse _nurse;
        public GlobalFunctions(IUnitOfWork unitofwork, Doctor doctor = null, Nurse nurse = null, Patient patient = null)
        {
            _unitofwork = unitofwork;
            _doctor = doctor;
            _patient = patient;
            _nurse = nurse;
        }
        public bool ExistDuplicateCCCD()
        {
            if (_patient != null)
            {
                var listpatient = _unitofwork.PatientRepository.GetAll();
                var listdoctor = _unitofwork.DoctorRepository.GetAll();
                var listnurse = _unitofwork.NurseRepository.GetAll();
                foreach (var obj in listdoctor)
                {
                    if (obj != null)
                    {
                        if (obj.DoctorCCCD == _patient.CCCD) return true;
                    }
                }
                foreach (var obj in listnurse)
                {
                    if (obj != null)
                    {
                        if (obj.NurseCCCD == _patient.CCCD) return true;
                    }
                }
                foreach (var obj in listpatient)
                {
                    if (obj!=null)
                    {
                        if (obj.CCCD == _patient.CCCD) return true;
                    }
                }
                return false;
            }
            if (_doctor != null)
            {
                var listpatient = _unitofwork.PatientRepository.GetAll();
                var listnurse = _unitofwork.NurseRepository.GetAll();
                var listdoctor = _unitofwork.DoctorRepository.GetAll();
                foreach (var obj in listpatient)
                {
                    if (obj != null)
                    {
                        if (obj.CCCD == _doctor.DoctorCCCD) return true;
                    }
                }
                foreach (var obj in listnurse)
                {
                    if (obj != null)
                    {
                        if (obj.NurseCCCD == _doctor.DoctorCCCD) return true;
                    }
                }
                foreach (var obj in listdoctor)
                {
                    if (obj!=null)
                    {
                        if (obj.DoctorCCCD == _doctor.DoctorCCCD) return true;
                    }
                }
                return false;
            }
            if (_nurse != null)
            {
                var listpatient = _unitofwork.PatientRepository.GetAll();
                var listdoctor = _unitofwork.DoctorRepository.GetAll();
                var listnurse = _unitofwork.NurseRepository.GetAll();
                foreach (var obj in listpatient)
                {
                    if (obj != null)
                    {
                        if (obj.CCCD == _nurse.NurseCCCD) return true;
                    }
                }
                foreach (var obj in listdoctor)
                {
                    if (obj != null)
                    {
                        if (obj.DoctorCCCD == _nurse.NurseCCCD) return true;
                    }
                }
                foreach (var obj in listnurse)
                {
                    if (obj!=null)
                    {
                        if (obj.NurseCCCD == _nurse.NurseCCCD) return true;
                    }
                }
                return false;
            }
            return false;
        }
    }
}
