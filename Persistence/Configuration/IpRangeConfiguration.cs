namespace Lefish.Persistence.Configuration;

class IpRangeConfiguration : IEntityTypeConfiguration<IpRange>
{
    public void Configure(EntityTypeBuilder<IpRange> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Range)
            .HasMaxLength(MaxLengths.Common.IpAddress.IPv6);
    }
}
