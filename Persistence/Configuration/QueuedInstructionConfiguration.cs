namespace Lefish.Persistence.Configuration;

class QueuedInstructionConfiguration : IEntityTypeConfiguration<QueuedInstruction>
{
    public void Configure(EntityTypeBuilder<QueuedInstruction> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
