namespace Lefish.Domain.Entities;

public class DataDump : ICreatedDateTime
{
    public int Id { get; set; }

    public string JsonData { get; set; }

    public DateTime? Created { get; set; }

    public int PageKeyId { get; set; }
    public PageKey PageKey { get; set; }
}
