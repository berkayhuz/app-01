using SiteManager.CLIENT.Models.Enums;
using System.Text.Json.Serialization;

namespace SiteManager.CLIENT.Models
{
	public class AddressModel
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("addressName")]
		public string AddressName { get; set; }

		[JsonPropertyName("firstName")]
		public string FirstName { get; set; }

		[JsonPropertyName("lastName")]
		public string LastName { get; set; }

		[JsonPropertyName("phoneNumber")]
		public string PhoneNumber { get; set; }

		[JsonPropertyName("alternativePhoneNumber")]
		public string? AlternativePhoneNumber { get; set; }

		[JsonPropertyName("email")]
		public string Email { get; set; }

		[JsonPropertyName("city")]
		public string City { get; set; }

		[JsonPropertyName("district")]
		public string District { get; set; }

		[JsonPropertyName("neighborhood")]
		public string Neighborhood { get; set; }

		[JsonPropertyName("addressInfo")]
		public string AddressInfo { get; set; }

		[JsonPropertyName("isInvoiceAddress")]
		public bool IsInvoiceAddress { get; set; }

		[JsonPropertyName("invoiceAddressName")]
		public string? InvoiceAddressName { get; set; }

		[JsonPropertyName("invoiceFirstName")]
		public string? InvoiceFirstName { get; set; }

		[JsonPropertyName("invoiceLastName")]
		public string? InvoiceLastName { get; set; }

		[JsonPropertyName("invoicePhoneNumber")]
		public string? InvoicePhoneNumber { get; set; }

		[JsonPropertyName("invoiceEmail")]
		public string? InvoiceEmail { get; set; }

		[JsonPropertyName("invoiceCity")]
		public string? InvoiceCity { get; set; }

		[JsonPropertyName("invoiceDistrict")]
		public string? InvoiceDistrict { get; set; }

		[JsonPropertyName("invoiceNeighborhood")]
		public string? InvoiceNeighborhood { get; set; }

		[JsonPropertyName("invoiceAddressInfo")]
		public string? InvoiceAddressInfo { get; set; }

		[JsonPropertyName("tcNumber")]
		public string? TCNumber { get; set; }

		[JsonPropertyName("passaportNumber")]
		public string? PassaportNumber { get; set; }

		[JsonPropertyName("passaportCountry")]
		public string? PassaportCountry { get; set; }

		[JsonPropertyName("commercialTitle")]
		public string? CommercialTitle { get; set; }

		[JsonPropertyName("taxAdministration")]
		public string? TaxAdministration { get; set; }

		[JsonPropertyName("taxNumber")]
		public string? TaxNumber { get; set; }

		[JsonPropertyName("invoiceType")]
		public InvoiceType InvoiceType { get; set; }

		[JsonPropertyName("invoicePayer")]
		public bool InvoicePayer { get; set; }
	}
}
