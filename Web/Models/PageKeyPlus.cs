namespace Lefish.Web.Models;

public class PageKeyPlus
{
    public int Id { get; set; }

    public int Token { get; set; }

    public DateTime? Created { get; set; }

    public string PageName { get; set; }
    public string ScriptName { get; set; }
    public string EmailMessageSubject { get; set; }
    public string TargetName { get; set; }
    public string TargetAddress { get; set; }
    
    public int PayloadPageId { get; set; }
    public int PayloadScriptId { get; set; }
    public int EmailMessageId { get; set; }
    public int EmailTargetId { get; set; }

    public int PageVisitCount { get; set; }
}
