namespace Lefish.Domain.Entities;

public class InstructionSet
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Instruction> Instructions { get; set; }
}
