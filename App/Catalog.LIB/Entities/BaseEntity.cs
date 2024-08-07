using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.LIB.Entities
{
	public class BaseEntity : IBaseEntity
	{
		public virtual string CreatedBy { get; set; } = "Undefined";
		public virtual string? ModifiedBy { get; set; } = "UnModified";
		public virtual string? DeletedBy { get; set; } = "NotDeleted";
		public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
		public virtual DateTime? ModifiedDate { get; set; }
		public virtual DateTime? DeletedDate { get; set; }
		public virtual bool IsDeleted { get; set; } = false;
		public virtual Guid Id { get; set; } = Guid.NewGuid();
	}
}
