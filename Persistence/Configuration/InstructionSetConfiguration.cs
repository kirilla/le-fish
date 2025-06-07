namespace Lefish.Persistence.Configuration;

class InstructionSetConfiguration : IEntityTypeConfiguration<InstructionSet>
{
    public void Configure(EntityTypeBuilder<InstructionSet> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.InstructionSet.Name);

        builder.HasMany(x => x.Instructions)
            .WithOne(x => x.InstructionSet)
            .HasForeignKey(x => x.InstructionSetId);
    }
}
