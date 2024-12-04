using MediplusMVC.Models.Base;

namespace MediplusMVC.Models
{
    public class Doctor: BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Finkod {  get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Username { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public bool IsActive {  get; set; } = true;
        public ICollection<HospitalDoctor>? HospitalDoctors { get; set; }

    }
}
