namespace Lefish.Persistence.Configuration;

class AttackConfiguration : IEntityTypeConfiguration<Attack>
{
    public void Configure(EntityTypeBuilder<Attack> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Value).IsRequired();

        builder.HasIndex(p => p.Value).IsUnique();

        builder.HasMany(x => x.DataDumps)
            .WithOne(x => x.Attack)
            .HasForeignKey(x => x.AttackId);

        builder.HasMany(x => x.EmailMessages)
            .WithOne(x => x.Attack)
            .HasForeignKey(x => x.AttackId);

        builder.HasMany(x => x.Visits)
            .WithOne(x => x.Attack)
            .HasForeignKey(x => x.AttackId);

        builder.HasMany(x => x.TargetInstructions)
            .WithOne(x => x.Attack)
            .HasForeignKey(x => x.AttackId);
    }
}
