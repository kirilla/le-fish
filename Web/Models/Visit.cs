namespace Lefish.Web.Models;

public class Visit
{
    public int Id { get; set; }

    public int EmailTargetId { get; set; }
    public int PageTokenId { get; set; }

    public int? PayloadPageId { get; set; }
    public int? PayloadScriptId { get; set; }

    public DateTime? Created { get; set; }

    public string? Url { get; set; }
    public string? Method { get; set; }

    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }

    public string? ScriptName { get; set; }
    public string? PageName { get; set; }

    public VisitKind VisitKind { get; set; }
}
