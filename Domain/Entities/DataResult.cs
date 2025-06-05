namespace Lefish.Domain.Entities;

public class DataResult : ICreatedDateTime
{
    public int Id { get; set; }

    public string JsonData { get; set; }

    public DateTime? Created { get; set; }

    public int AttackId { get; set; }
    public Attack Attack { get; set; }
}
