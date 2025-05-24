namespace Lefish.Persistence.Configuration;

class PayloadPageConfiguration : IEntityTypeConfiguration<PayloadPage>
{
    public void Configure(EntityTypeBuilder<PayloadPage> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.PayloadPage.Name);

        builder.Property(p => p.Html)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.PayloadPage.Html);

        builder.HasMany(x => x.PageKeys)
            .WithOne(x => x.PayloadPage)
            .HasForeignKey(x => x.PayloadPageId);
    }
}
