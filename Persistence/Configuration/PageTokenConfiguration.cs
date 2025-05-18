namespace Lefish.Persistence.Configuration;

class PageTokenConfiguration : IEntityTypeConfiguration<PageToken>
{
    public void Configure(EntityTypeBuilder<PageToken> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Token).IsRequired();

        builder.HasIndex(p => p.Token).IsUnique();
    }
}
