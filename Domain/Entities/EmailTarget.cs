namespace Lefish.Domain.Entities;

public class EmailTarget
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Address { get; set; }

    public List<DataDump> DataDumps { get; set; }
    public List<EmailMessage> EmailMessages { get; set; }
    public List<PageVisit> PageVisits { get; set; }
    public List<PhishingToken> PhishingTokens { get; set; }
}
