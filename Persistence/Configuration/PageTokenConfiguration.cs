namespace Lefish.Persistence.Configuration;

class PageTokenConfiguration : IEntityTypeConfiguration<PageToken>
{
    public void Configure(EntityTypeBuilder<PageToken> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Token).IsRequired();

        builder.HasIndex(p => p.Token).IsUnique();

        builder.HasMany(x => x.PageVisits)
            .WithOne(x => x.PageToken)
            .HasForeignKey(x => x.PageTokenId);
    }
}
