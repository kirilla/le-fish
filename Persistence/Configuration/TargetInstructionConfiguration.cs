namespace Lefish.Persistence.Configuration;

class TargetInstructionConfiguration : IEntityTypeConfiguration<TargetInstruction>
{
    public void Configure(EntityTypeBuilder<TargetInstruction> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.Instruction.Name);

        builder.Property(p => p.Script)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.Instruction.Script);
    }
}
