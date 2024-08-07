using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class AddressMap : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.AddressName).IsRequired().HasMaxLength(100);
        builder.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.LastName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(15);
        builder.Property(a => a.AlternativePhoneNumber).HasMaxLength(15);
        builder.Property(a => a.Email).IsRequired().HasMaxLength(100);
        builder.Property(a => a.City).IsRequired().HasMaxLength(50);
        builder.Property(a => a.District).IsRequired().HasMaxLength(50);
        builder.Property(a => a.Neighborhood).IsRequired().HasMaxLength(50);
        builder.Property(a => a.AddressInfo).HasMaxLength(250);
        builder.Property(a => a.IsInvoiceAddress).IsRequired();
        builder.Property(a => a.InvoiceAddressName).HasMaxLength(100);
        builder.Property(a => a.InvoiceFirstName).HasMaxLength(50);
        builder.Property(a => a.InvoiceLastName).HasMaxLength(50);
        builder.Property(a => a.InvoicePhoneNumber).HasMaxLength(15);
        builder.Property(a => a.InvoiceEmail).HasMaxLength(100);
        builder.Property(a => a.InvoiceCity).HasMaxLength(50);
        builder.Property(a => a.InvoiceDistrict).HasMaxLength(50);
        builder.Property(a => a.InvoiceNeighborhood).HasMaxLength(50);
        builder.Property(a => a.InvoiceAddressInfo).HasMaxLength(250);
        builder.Property(a => a.TCNumber).HasMaxLength(11).IsFixedLength();
        builder.Property(a => a.PassaportNumber).HasMaxLength(20);
        builder.Property(a => a.PassaportCountry).HasMaxLength(50);
        builder.Property(a => a.CommercialTitle).HasMaxLength(100);
        builder.Property(a => a.TaxAdministration).HasMaxLength(100);
        builder.Property(a => a.TaxNumber).HasMaxLength(20);
        builder.Property(a => a.InvoiceType).IsRequired();
        builder.Property(a => a.InvoicePayer).IsRequired();

        builder.HasOne(a => a.AppUser)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Addresses");
    }
}
