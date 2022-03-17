using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Configurations
{
    public class RoleRescConfiguration : IEntityTypeConfiguration<RoleResc>
    {
        public void Configure(EntityTypeBuilder<RoleResc> builder)
        {
            builder.HasKey(x => new { x.RoleId, x.RescId });
        }
    }
}
