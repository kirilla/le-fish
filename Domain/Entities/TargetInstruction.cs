namespace Lefish.Domain.Entities;

public class TargetInstruction : ICreatedDateTime
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Script { get; set; }

    public int Reference { get; set; }

    public DateTime? Created { get; set; }

    public PageKey PageKey { get; set; }
    public int PageKeyId { get; set; }
}
