using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Configurations
{
    public class MemberExpressCompanyConfiguration : IEntityTypeConfiguration<MemberExpressCompany>
    {
        public void Configure(EntityTypeBuilder<MemberExpressCompany> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
