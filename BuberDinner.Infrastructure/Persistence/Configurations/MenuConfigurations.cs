using BuberDinner.Domain.Hosts.ValueObjects;
using BuberDinner.Domain.Menus;
using BuberDinner.Domain.Menus.Entites;
using BuberDinner.Domain.Menus.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuberDinner.Infrastructure.Persistence.Configurations;

internal sealed class MenuConfigurations : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        ConfigureMenusTable(builder);
        ConfigureMenuSectionsTable(builder);
        ConfigureMenuDinnerIdsTable(builder);
        ConfigureMenuReviewIdsTable(builder);
    }

    private void ConfigureMenusTable(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");

        builder.HasKey(m => m.Id);

        builder
            .Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MenuId.Create(value));

        builder
            .Property(m => m.Name)
            .HasMaxLength(100);

        builder
            .Property(m => m.Description)
            .HasMaxLength(100);

        builder.OwnsOne(m => m.AverageRating);

        builder
            .Property(m => m.HostId)
            .HasConversion(
                id => id.Value,
                value => HostId.Create(value));
    }

    private void ConfigureMenuSectionsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.Sections, sectionBuilder =>
        {
            sectionBuilder.ToTable("MenuSections");

            sectionBuilder
                .WithOwner()
                .HasForeignKey("MenuId");

            sectionBuilder.HasKey("Id", "MenuId");

            sectionBuilder.Property(s => s.Id)
                .HasColumnName("MenuSectionId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => MenuSectionId.Create(value));

            sectionBuilder
                .Property(s => s.Name)
                .HasMaxLength(100);

            sectionBuilder
                .Property(s => s.Description)
                .HasMaxLength(100);

            sectionBuilder.OwnsMany(
                s => s.Items,
                itemBuilder => ConfigureMenuItemsTable(itemBuilder));

            sectionBuilder
                .Navigation(s => s.Items).Metadata
                .SetField("_items");

            sectionBuilder
                .Navigation(s => s.Items)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.Metadata
            .FindNavigation(nameof(Menu.Sections))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureMenuItemsTable(OwnedNavigationBuilder<MenuSection, MenuItem> itemBuilder)
    {
        itemBuilder.ToTable("MenuItems");

        itemBuilder
            .WithOwner()
            .HasForeignKey("MenuSectionId", "MenuId");

        itemBuilder.HasKey(nameof(MenuItem.Id), "MenuSectionId", "MenuId");

        itemBuilder
            .Property(i => i.Id)
            .HasColumnName("MenuItemId")
            .ValueGeneratedOnAdd()
            .HasConversion(
                id => id.Value,
                value => MenuItemId.Create(value));

        itemBuilder
            .Property(i => i.Name)
            .HasMaxLength(100);

        itemBuilder
            .Property(i => i.Description)
            .HasMaxLength(100);
    }

    private void ConfigureMenuDinnerIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.DinnerIds, dinnerBuilder =>
        {
            dinnerBuilder.ToTable("MenuDinnerIds");

            dinnerBuilder
                .WithOwner()
                .HasForeignKey("MenuId");

            dinnerBuilder.HasKey("Id");

            dinnerBuilder
                .Property(d => d.Value)
                .HasColumnName("DinnerId")
                .ValueGeneratedNever();
        });

        builder.Metadata
            .FindNavigation(nameof(Menu.DinnerIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenuReviewIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.MenuReviewIds, reviewBuilder =>
        {
            reviewBuilder.ToTable("MenuReviewIds");

            reviewBuilder
                .WithOwner()
                .HasForeignKey("MenuId");

            reviewBuilder.HasKey("Id");

            reviewBuilder
                .Property(r => r.Value)
                .HasColumnName("ReviewId")
                .ValueGeneratedNever();
        });

        builder.Metadata
            .FindNavigation(nameof(Menu.MenuReviewIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}