namespace Lefish.Web.Models;

public class AttackSummary
{
    public int Id { get; set; }

    public int Value { get; set; }

    public DateTime? Created { get; set; }

    public string PageName { get; set; }
    public string ScriptName { get; set; }
    public string TargetName { get; set; }
    public string TargetAddress { get; set; }
    
    public int WebPageId { get; set; }
    public int PageScriptId { get; set; }
    public int TargetId { get; set; }
}
