using BuberDinner.Domain.Hosts;
using BuberDinner.Domain.Hosts.ValueObjects;
using BuberDinner.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuberDinner.Infrastructure.Persistence.Configurations;

public class HostConfigurations : IEntityTypeConfiguration<Host>
{
    public void Configure(EntityTypeBuilder<Host> builder)
    {
        ConfigureHostsTable(builder);
        ConfigureHostMenuIdsTable(builder);
        ConfigureHostDinnerIdsTable(builder);
    }

    private static void ConfigureHostsTable(EntityTypeBuilder<Host> builder)
    {
        builder
            .ToTable("Hosts");

        builder
            .HasKey(h => h.Id);

        builder
            .Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => HostId.Create(value));

        builder
            .Property(h => h.FirstName)
            .HasMaxLength(100);

        builder
            .Property(h => h.LastName)
            .HasMaxLength(100);

        builder
            .Property(h => h.ProfileImage);

        builder
            .OwnsOne(h => h.AverageRating);

        builder
            .Property(h => h.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
    }

    private static void ConfigureHostMenuIdsTable(EntityTypeBuilder<Host> builder)
    {
        builder.OwnsMany(h => h.MenuIds, mib =>
        {
            mib.WithOwner().HasForeignKey("HostId");

            mib.ToTable("HostMenuIds");

            mib.HasKey("Id");

            mib.Property(mi => mi.Value)
                .ValueGeneratedNever()
                .HasColumnName("HostMenuId");
        });

        builder.Metadata
            .FindNavigation(nameof(Host.MenuIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureHostDinnerIdsTable(EntityTypeBuilder<Host> builder)
    {
        builder.OwnsMany(h => h.DinnerIds, dib =>
        {
            dib.WithOwner().HasForeignKey("HostId");

            dib.ToTable("HostDinnerIds");

            dib.HasKey("Id");

            dib.Property(di => di.Value)
                .ValueGeneratedNever()
                .HasColumnName("HostDinnerId");
        });

        builder.Metadata
            .FindNavigation(nameof(Host.DinnerIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}