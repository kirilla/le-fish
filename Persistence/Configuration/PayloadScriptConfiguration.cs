namespace Lefish.Persistence.Configuration;

class PayloadScriptConfiguration : IEntityTypeConfiguration<PayloadScript>
{
    public void Configure(EntityTypeBuilder<PayloadScript> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.PayloadScript.Name);

        builder.Property(p => p.Script)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.PayloadScript.Script);

        builder.HasMany(x => x.PageTokens)
            .WithOne(x => x.PayloadScript)
            .HasForeignKey(x => x.PayloadScriptId);
    }
}
