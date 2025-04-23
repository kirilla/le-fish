namespace Lefish.Persistence.Configuration;

class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Subject)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailTemplate.Subject);

        builder.Property(p => p.HtmlBody)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailTemplate.HtmlBody);

        builder.Property(p => p.TextBody)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailTemplate.TextBody);

        builder.HasMany(x => x.EmailAttachments)
            .WithOne(x => x.EmailTemplate)
            .HasForeignKey(x => x.EmailTemplateId);

        builder.HasMany(x => x.EmailImages)
            .WithOne(x => x.EmailTemplate)
            .HasForeignKey(x => x.EmailTemplateId);
    }
}
