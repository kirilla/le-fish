namespace Lefish.Web.Models;

public class AttackEventKindSummary
{
    public int PageVisitCount { get; set; }
    public int ScriptDownloadCount { get; set; }
    public int FetchInstructionCount { get; set; }
    public int UploadDataCount { get; set; }

    public int Total =>
        PageVisitCount +
        ScriptDownloadCount +
        FetchInstructionCount +
        UploadDataCount;
}
