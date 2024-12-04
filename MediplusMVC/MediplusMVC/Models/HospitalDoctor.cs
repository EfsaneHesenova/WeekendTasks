using MediplusMVC.Models.Base;

namespace MediplusMVC.Models
{
    public class HospitalDoctor: BaseAuditableEntity
    {
        public int HospitalId { get; set; }
        public int DoctorId { get; set; }
        public Hospital? Hospital { get; set; }
        public Doctor? Doctor { get; set; }
        
    }
}
