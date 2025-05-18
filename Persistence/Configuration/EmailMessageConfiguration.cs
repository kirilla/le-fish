namespace Lefish.Persistence.Configuration;

class EmailMessageConfiguration : IEntityTypeConfiguration<EmailMessage>
{
    public void Configure(EntityTypeBuilder<EmailMessage> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Subject)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailMessage.Subject);

        builder.Property(p => p.HtmlBody)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailMessage.HtmlBody);

        builder.Property(p => p.TextBody)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailMessage.TextBody);

        builder.HasMany(x => x.PageTokens)
            .WithOne(x => x.EmailMessage)
            .HasForeignKey(x => x.EmailMessageId);
    }
}
