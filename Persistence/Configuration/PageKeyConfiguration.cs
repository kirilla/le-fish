namespace Lefish.Persistence.Configuration;

class PageKeyConfiguration : IEntityTypeConfiguration<PageKey>
{
    public void Configure(EntityTypeBuilder<PageKey> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Token).IsRequired();

        builder.HasIndex(p => p.Token).IsUnique();

        builder.HasMany(x => x.PageVisits)
            .WithOne(x => x.PageKey)
            .HasForeignKey(x => x.PageTokenId);

        builder.HasMany(x => x.ScriptVisits)
            .WithOne(x => x.PageKey)
            .HasForeignKey(x => x.PageTokenId);
    }
}
