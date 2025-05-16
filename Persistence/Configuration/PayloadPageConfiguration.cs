namespace Lefish.Persistence.Configuration;

class PayloadPageConfiguration : IEntityTypeConfiguration<PayloadPage>
{
    public void Configure(EntityTypeBuilder<PayloadPage> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.PageKey)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.PayloadPage.PageKey);

        builder.Property(p => p.Comment)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.PayloadPage.Comment);

        builder.Property(p => p.Html)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.PayloadPage.Html);

        builder.HasMany(x => x.PageVisits)
            .WithOne(x => x.PayloadPage)
            .HasForeignKey(x => x.PayloadPageId);

        builder.HasMany(x => x.PhishingTokens)
            .WithOne(x => x.PayloadPage)
            .HasForeignKey(x => x.PayloadPageId);
    }
}
