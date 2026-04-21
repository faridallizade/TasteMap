using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tastemap.Core.Entities.UserEntities;

namespace Tastemap.Repository.EntityConfigurations;

public class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
{
    public void Configure(EntityTypeBuilder<UserDetails> builder)
    {
        builder.HasKey(ud => ud.UserId);

        builder.Property(ud => ud.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ud => ud.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ud => ud.PhoneNumber)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(ud => ud.ProfileImageUrl)
            .HasMaxLength(500);
    }
}