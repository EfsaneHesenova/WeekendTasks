using MediplusMVC.Models;
using MediplusMVC.Models.Base;
using System.Numerics;

namespace MediplusMVC.Models
{
    public class Appointment: BaseAuditableEntity
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public bool IsActive { get; set; } 
    }
}
