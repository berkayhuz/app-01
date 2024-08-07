using Authentication.LIB.Enums;

namespace Authentication.LIB.Models
{
	public class AddressDTO
	{
		public Guid Id { get; set; }
		public string AddressName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string? AlternativePhoneNumber { get; set; }
		public string Email { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string Neighborhood { get; set; }
		public string AddressInfo { get; set; }
		public bool IsInvoiceAddress { get; set; }
		public string? InvoiceAddressName { get; set; }
		public string? InvoiceFirstName { get; set; }
		public string? InvoiceLastName { get; set; }
		public string? InvoicePhoneNumber { get; set; }
		public string? InvoiceEmail { get; set; }
		public string? InvoiceCity { get; set; }
		public string? InvoiceDistrict { get; set; }
		public string? InvoiceNeighborhood { get; set; }
		public string? InvoiceAddressInfo { get; set; }
		public string? TCNumber { get; set; }
		public string? PassaportNumber { get; set; }
		public string? PassaportCountry { get; set; }
		public string? CommercialTitle { get; set; }
		public string? TaxAdministration { get; set; }
		public string? TaxNumber { get; set; }
		public InvoiceType InvoiceType { get; set; }
		public bool InvoicePayer { get; set; }
	}
}