using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreImageExample.Model;

namespace StoreImageExample.Data.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(i => i.FileName).HasMaxLength(50);
            builder.Property(i => i.ImageFile).IsRequired();
        }
    }
}
