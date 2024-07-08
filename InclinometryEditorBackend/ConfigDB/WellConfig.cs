using InclinometryEditorBackend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InclinometryEditorBackend.ConfigDB
{
    public class WellConfig : IEntityTypeConfiguration<WellEntity>
    {
        public void Configure(EntityTypeBuilder<WellEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreateDate).IsRequired();
        }
    }
}
