using MediplusMVC.Models;
using MediplusMVC.Models.Base;

namespace MediplusMVC.Models
{
    public class Patient: BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Finkod { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }

    }
}
