namespace Lefish.Domain.Entities;

public class Company : ICreatedDateTime, IUpdatedDateTime
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool Locked { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }
}
