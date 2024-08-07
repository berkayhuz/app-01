using Authentication.LIB.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.API.Data.Mappings
{
	public class UserMap : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.HasKey(u => u.Id);

			// Indexes for "normalized" username and email, to allow efficient lookups
			builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
			builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

			// Maps to the AspNetUsers table
			builder.ToTable("AspNetUsers");

			// A concurrency token for use with the optimistic concurrency checking
			builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

			// Limit the size of columns to use efficient database types
			builder.Property(u => u.UserName).HasMaxLength(50);
			builder.Property(u => u.NormalizedUserName).HasMaxLength(50);
			builder.Property(u => u.Email).HasMaxLength(50);
			builder.Property(u => u.NormalizedEmail).HasMaxLength(50);

			// Ensure ExpressConsentText and KvkkPolicy are always true
			builder.Property(u => u.ExpressConsentText).HasDefaultValue(true).ValueGeneratedNever();
			builder.Property(u => u.KvkkPolicy).HasDefaultValue(true).ValueGeneratedNever();

			// The relationships between User and other entity types
			// Note that these relationships are configured with no navigation properties

			// Each User can have many UserClaims
			builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

			// Each User can have many UserLogins
			builder.HasMany<AppUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

			// Each User can have many UserTokens
			builder.HasMany<AppUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

			// Each User can have many entries in the UserRole join table
			builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

			var superadmin = new AppUser("huzberkay@icloud.com", true, true)
			{
				Id = Guid.Parse("B3402EC4-AB10-4F89-BD4A-9D95B601316F"),
				NormalizedUserName = "HUZBERKAY@ICLOUD.COM",
				PhoneNumber = "+905438018574",
				FirstName = "Berkay",
				LastName = "Huz",
				Slug = "berkay-huz",
				PhoneNumberConfirmed = true,
				EmailConfirmed = true,
				SecurityStamp = Guid.NewGuid().ToString()
			};
			superadmin.PasswordHash = CreatePasswordHash(superadmin, "Berkayzuh16!");

			var admin = new AppUser("deneme@icloud.com", true, true)
			{
				Id = Guid.Parse("31FEB16F-8C01-4E22-9A6F-DF79B7E5582A"),
				NormalizedUserName = "DENEME@ICLOUD.COM",
				PhoneNumber = "+905524130669",
				FirstName = "Deneme",
				LastName = "Deneme",
				Slug = "deneme-deneme",
				PhoneNumberConfirmed = true,
				EmailConfirmed = true,
				SecurityStamp = Guid.NewGuid().ToString()
			};
			admin.PasswordHash = CreatePasswordHash(admin, "Berkayzuh16!");

			builder.HasData(superadmin, admin);

		}

		private string CreatePasswordHash(AppUser user, string password)
		{
			var passwordHasher = new PasswordHasher<AppUser>();
			return passwordHasher.HashPassword(user, password);
		}
	}
}
