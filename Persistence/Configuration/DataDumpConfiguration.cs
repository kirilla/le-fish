namespace Lefish.Persistence.Configuration;

class DataDumpConfiguration : IEntityTypeConfiguration<DataDump>
{
    public void Configure(EntityTypeBuilder<DataDump> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.JsonData)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.DataDump.JsonData);
    }
}
