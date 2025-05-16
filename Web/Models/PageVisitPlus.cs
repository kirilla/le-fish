namespace Lefish.Web.Models;

public class PageVisitPlus
{
    public int Id { get; set; }

    public DateTime? Created { get; set; }

    public string? Url { get; set; }
    public string? Method { get; set; }

    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }

    public string PageName { get; set; }
    
    public int PayloadPageId { get; set; }

    public string TargetName { get; set; }
    public string TargetAddress { get; set; }

    public int EmailTargetId { get; set; }
}
