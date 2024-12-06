using EmployeeMVC.Models.Base;

namespace EmployeeMVC.Models
{
	public class ServicePhoto: BaseAuditableEntity
	{
		public string ImageUrl { get; set; }
		public int ServiceId { get; set; }
		public Service? Service { get; set; }
	}
}
