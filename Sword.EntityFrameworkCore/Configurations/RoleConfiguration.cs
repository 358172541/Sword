﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sword.Domain;

namespace Sword.EntityFrameworkCore.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.RoleId);
            builder.Property(x => x.Display).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.Display).IsUnique();
        }
    }
}
