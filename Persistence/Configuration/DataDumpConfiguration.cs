namespace Lefish.Persistence.Configuration;

class DataDumpConfiguration : IEntityTypeConfiguration<DataDump>
{
    public void Configure(EntityTypeBuilder<DataDump> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.DataDump.Name);

        builder.Property(p => p.ContentType)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.DataDump.ContentType);
    }
}
