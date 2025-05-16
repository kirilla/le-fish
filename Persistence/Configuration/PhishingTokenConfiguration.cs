namespace Lefish.Persistence.Configuration;

class PhishingTokenConfiguration : IEntityTypeConfiguration<PhishingToken>
{
    public void Configure(EntityTypeBuilder<PhishingToken> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Token).IsRequired();

        builder.HasIndex(p => p.Token).IsUnique();
    }
}
