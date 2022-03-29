using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Configurations
{
    public class PackageNumberConfiguration : IEntityTypeConfiguration<PackageNumber>
    {
        public void Configure(EntityTypeBuilder<PackageNumber> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
