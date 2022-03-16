using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sword.Domain;

namespace Sword.EntityFrameworkCore.Configurations
{
    public class RoleRescConfiguration : IEntityTypeConfiguration<RoleResc>
    {
        public void Configure(EntityTypeBuilder<RoleResc> builder)
        {
            builder.HasKey(x => new { x.RoleId, x.RescId });
        }
    }
}
