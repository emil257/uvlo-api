using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uvlo_core.Entities;

namespace uvlo_infrastructure.Data.Configuration
{
  internal sealed class PostConfiguration : IEntityTypeConfiguration<Post>
  {
    public void Configure(EntityTypeBuilder<Post> builder)
    {
      builder.ToTable("AppPosts");

      builder.HasKey(p => p.Id);

      builder.HasQueryFilter(p => p.DeletedAt == null);
      builder.HasMany(p => p.Comments)
            .WithOne()
            .HasForeignKey(p => p.ParentId);

      builder.Property(p => p.Type)
            .HasConversion<string>()
            .HasMaxLength(20);

      builder.Property(p => p.Content)
            .HasMaxLength(600);
    }
  }
}
