using MediplusMVC.Models.Base;

namespace MediplusMVC.Models
{
	public class Schedule: BaseAuditableEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Owner {  get; set; }
		public string ButtonUrl { get; set; }
	}
}
