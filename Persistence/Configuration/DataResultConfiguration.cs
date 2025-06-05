namespace Lefish.Persistence.Configuration;

class DataResultConfiguration : IEntityTypeConfiguration<DataResult>
{
    public void Configure(EntityTypeBuilder<DataResult> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.JsonData)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.DataResult.JsonData);
    }
}
