namespace Lefish.Persistence.Configuration;

class TargetConfiguration : IEntityTypeConfiguration<Target>
{
    public void Configure(EntityTypeBuilder<Target> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.Target.Name);

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.Target.Address);

        builder.HasMany(x => x.Attacks)
            .WithOne(x => x.Target)
            .HasForeignKey(x => x.TargetId);
    }
}
