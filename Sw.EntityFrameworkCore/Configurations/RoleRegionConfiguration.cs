using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Configurations
{
    public class RoleRegionConfiguration : IEntityTypeConfiguration<RoleRegion>
    {
        public void Configure(EntityTypeBuilder<RoleRegion> builder)
        {
            builder.HasKey(x => new { x.RoleId, x.RegionId });
        }
    }
}
