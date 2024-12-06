using EmployeeMVC.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeMVC.Models
{
    public class Service: BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;

        [NotMapped]
        public List<IFormFile> Images { get; set; }
        public List<ServicePhoto>? ServicePhotos { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Master>? Masters { get; set; }
    }
}
