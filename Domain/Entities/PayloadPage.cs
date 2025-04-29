namespace Lefish.Domain.Entities;

public class PayloadPage
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Html { get; set; }

    public int? PageKey { get; set; }

    public List<PageVisit> PageVisits { get; set; }
}
