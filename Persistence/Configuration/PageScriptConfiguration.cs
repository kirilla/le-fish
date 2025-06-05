namespace Lefish.Persistence.Configuration;

class PageScriptConfiguration : IEntityTypeConfiguration<PageScript>
{
    public void Configure(EntityTypeBuilder<PageScript> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.PageScript.Name);

        builder.Property(p => p.Script)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.PageScript.Script);

        builder.HasMany(x => x.Attacks)
            .WithOne(x => x.PayloadScript)
            .HasForeignKey(x => x.PageScriptId);
    }
}
