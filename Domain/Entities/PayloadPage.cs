
namespace Lefish.Domain.Entities;

public class PayloadPage : ICreatedDateTime
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Html { get; set; }

    public DateTime? Created { get; set; }

    public List<PageVisit> PageVisits { get; set; }
    public List<PageToken> PageTokens { get; set; }
}
