using System.Text.Json.Serialization;
using Authentication.LIB.Entities;
using Authentication.LIB.Enums;

public class Address : BaseEntity
{
    private string? _tcNumber;
    private string? _passaportNumber;
    private string? _passaportCountry;

    public Address()
    {
        AddressName = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        PhoneNumber = string.Empty;
        AlternativePhoneNumber = string.Empty;
        Email = string.Empty;
        City = string.Empty;
        District = string.Empty;
        Neighborhood = string.Empty;
        AddressInfo = string.Empty;
        IsInvoiceAddress = false;
        InvoiceAddressName = string.Empty;
        InvoiceFirstName = string.Empty;
        InvoiceLastName = string.Empty;
        InvoicePhoneNumber = string.Empty;
        InvoiceEmail = string.Empty;
        InvoiceCity = string.Empty;
        InvoiceDistrict = string.Empty;
        InvoiceNeighborhood = string.Empty;
        InvoiceAddressInfo = string.Empty;
        PassaportNumber = string.Empty;
        CommercialTitle = string.Empty;
        TaxAdministration = string.Empty;
        TaxNumber = string.Empty;
        PassaportCountry = string.Empty;
    }

    public Guid AppUserId { get; set; }
    [JsonIgnore]
    public AppUser AppUser { get; set; }
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
    public string? TCNumber
    {
        get => _tcNumber;
        set
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(_passaportNumber))
            {
                throw new InvalidOperationException("Both TCNumber and PassaportNumber cannot have values at the same time.");
            }
            if (!string.IsNullOrEmpty(value) && !IsValidTCNumber(value))
            {
                throw new InvalidOperationException($"The value '{value}' is not valid for TCNumber.");
            }
            _tcNumber = value;
        }
    }
    public string? PassaportNumber
    {
        get => _passaportNumber;
        set
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(_tcNumber))
            {
                throw new InvalidOperationException("Both TCNumber and PassaportNumber cannot have values at the same time.");
            }
            if (!string.IsNullOrEmpty(value) && string.IsNullOrEmpty(_passaportCountry))
            {
                throw new InvalidOperationException("PassaportCountry must be provided when PassaportNumber is set.");
            }
            _passaportNumber = value;
        }
    }
    public string? PassaportCountry
    {
        get => _passaportCountry;
        set
        {
            if (!string.IsNullOrEmpty(_passaportNumber) && string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException("PassaportCountry must be provided when PassaportNumber is set.");
            }
            _passaportCountry = value;
        }
    }
    public string? CommercialTitle { get; set; }
    public string? TaxAdministration { get; set; }
    public string? TaxNumber { get; set; }
    public InvoiceType InvoiceType { get; set; }
    public bool InvoicePayer { get; set; }

    private bool IsValidTCNumber(string tcNumber)
    {
        if (tcNumber.Length != 11 || !long.TryParse(tcNumber, out _))
        {
            return false;
        }

        if (tcNumber[0] == '0')
        {
            return false;
        }

        var digits = tcNumber.Select(d => int.Parse(d.ToString())).ToArray();
        var checksum1 = (digits[0] + digits[2] + digits[4] + digits[6] + digits[8]) * 7;
        var checksum2 = digits[1] + digits[3] + digits[5] + digits[7];
        var checksum = (checksum1 - checksum2) % 10;

        if (checksum != digits[9])
        {
            return false;
        }

        var total = digits.Take(10).Sum() % 10;
        return total == digits[10];
    }
}
