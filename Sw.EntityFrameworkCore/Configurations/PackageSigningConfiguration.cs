using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Configurations
{
    public class PackageSigningConfiguration : IEntityTypeConfiguration<PackageSigning>
    {
        public void Configure(EntityTypeBuilder<PackageSigning> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
