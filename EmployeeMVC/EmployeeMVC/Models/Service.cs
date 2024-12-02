using EmployeeMVC.Models.Base;

namespace EmployeeMVC.Models
{
    public class Service: BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Master>? Masters { get; set; }
    }
}
