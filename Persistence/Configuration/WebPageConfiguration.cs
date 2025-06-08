namespace Lefish.Persistence.Configuration;

class WebPageConfiguration : IEntityTypeConfiguration<WebPage>
{
    public void Configure(EntityTypeBuilder<WebPage> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.WebPage.Name);

        builder.Property(p => p.Html)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.WebPage.Html);

        builder.HasMany(x => x.Attacks)
            .WithOne(x => x.WebPage)
            .HasForeignKey(x => x.WebPageId);
    }
}
