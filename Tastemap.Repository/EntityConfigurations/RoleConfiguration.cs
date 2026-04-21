using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tastemap.Core.Entities.RoleEntities;

namespace Tastemap.Repository.EntityConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasIndex(r => r.Name).IsUnique();
        
        builder.Property(r => r.Description)
            .HasMaxLength(500);
        
        builder.Property(r => r.IsFullAdmin)
            .IsRequired()
            .HasDefaultValue(false);
    }
}