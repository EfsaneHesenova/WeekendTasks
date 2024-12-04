using MediplusMVC.Models.Base;

namespace MediplusMVC.Models
{
    public class Hospital : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<HospitalDoctor>? HospitalDoctors { get; set; }
    }
}
