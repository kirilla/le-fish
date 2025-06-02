namespace Lefish.Persistence.Configuration;

class TargetInstructionConfiguration : IEntityTypeConfiguration<TargetInstruction>
{
    public void Configure(EntityTypeBuilder<TargetInstruction> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
