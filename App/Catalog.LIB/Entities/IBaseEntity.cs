namespace Catalog.LIB.Entities
{
	public interface IBaseEntity
	{
		Guid Id { get; set; }
		DateTime CreatedDate { get; set; }
		DateTime? ModifiedDate { get; set; }
		DateTime? DeletedDate { get; set; }
		bool IsDeleted { get; set; }
		string CreatedBy { get; set; }
		string? ModifiedBy { get; set; }
		string? DeletedBy { get; set; }
	}
}
