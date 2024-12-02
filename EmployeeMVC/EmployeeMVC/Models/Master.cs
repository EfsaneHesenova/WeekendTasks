using EmployeeMVC.Models.Base;

namespace EmployeeMVC.Models
{
    public class Master: BaseAuditableEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public int ExperienceYear { get; set; }
        public bool IsActive { get; set; } = true;
        public int ServiceId { get; set; }
        public Service? Service { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
