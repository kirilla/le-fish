namespace Lefish.Persistence.Configuration;

class InstructionConfiguration : IEntityTypeConfiguration<Instruction>
{
    public void Configure(EntityTypeBuilder<Instruction> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.Instruction.Name);

        builder.Property(p => p.Script)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.Instruction.Script);

        builder.HasMany(x => x.TargetInstructions)
            .WithOne(x => x.Instruction)
            .HasForeignKey(x => x.InstructionId);
    }
}
