namespace Lefish.Domain.Entities;

public class PayloadPage
{
    public int Id { get; set; }

    public string PageKey { get; set; }
    public string? Comment { get; set; }

    public string Html { get; set; }

    public List<PageVisit> PageVisits { get; set; }
}
