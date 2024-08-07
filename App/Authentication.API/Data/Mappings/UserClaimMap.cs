﻿using Authentication.LIB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.API.Data.Mappings;

public class UserClaimMap : IEntityTypeConfiguration<AppUserClaim>
{
    public void Configure(EntityTypeBuilder<AppUserClaim> builder)
    {
        // Primary key
        builder.HasKey(uc => uc.Id);

        // Maps to the AspNetUserClaims table
        builder.ToTable("AspNetUserClaims");
    }
}