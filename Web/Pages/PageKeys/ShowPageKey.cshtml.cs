namespace Lefish.Web.Pages.PageKeys;

public class ShowPageKeyModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public PageKeyPlus PageKey { get; set; }

    public List<PageVisitPlus> PageVisits { get; set; }
    public List<ScriptVisitPlus> ScriptVisits { get; set; }

    public List<QueuedInstructionPlus> QueuedInstructions { get; set; }

    public List<Visit> Visits { get; set; }

    public List<string> IpAddresses { get; set; }
    public List<string> UserAgents { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PageKey = await database.PageKeys
                .Where(x => x.Id == id)
                .Select(x => new PageKeyPlus()
                {
                    Id = x.Id,
                    Value = x.Value,
                    Created = x.Created,
                    PageName = x.PayloadPage.Name,
                    PayloadPageId = x.PayloadPageId,
                    TargetName = x.EmailMessage.EmailTarget.Name,
                    TargetAddress = x.EmailMessage.EmailTarget.Address,
                    EmailMessageId = x.EmailMessageId,
                    EmailMessageSubject = x.EmailMessage.Subject,
                    EmailTargetId = x.EmailMessage.EmailTargetId,
                    //PageVisitCount = x.PageVisits.Count(),
                    ScriptName = x.PayloadScript.Name,
                    PayloadScriptId = x.PayloadScriptId,
                })
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PageVisits = await database.PageVisits
                .Where(x => x.PageKeyId == id)
                .OrderBy(x => x.Created)
                .Select(x => new PageVisitPlus()
                {
                    Id = x.Id,
                    Created = x.Created,
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.PageKey.PayloadPage.Name,
                    PayloadPageId = x.PageKey.PayloadPageId,
                })
                .ToListAsync();

            ScriptVisits = await database.ScriptVisits
                .Where(x => x.PageKeyId == id)
                .OrderBy(x => x.Created)
                .Select(x => new ScriptVisitPlus()
                {
                    Id = x.Id,
                    Created = x.Created,
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    ScriptName = x.PageKey.PayloadScript.Name,
                    PayloadScriptId = x.PageKey.PayloadScriptId,
                })
                .ToListAsync();

            var pageVisits = await database.PageVisits
                .OrderBy(x => x.Created)
                .Where(x => x.PageKeyId == id)
                .Select(x => new Visit()
                {
                    VisitKind = VisitKind.Page,
                    Id = x.Id,
                    PageKeyId = x.PageKeyId,
                    Created = x.Created,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.PageKey.PayloadPage.Name,
                    PayloadPageId = x.PageKey.PayloadPageId,
                })
                .ToListAsync();

            var scriptVisits = await database.ScriptVisits
                .OrderBy(x => x.Created)
                .Where(x => x.PageKeyId == id)
                .Select(x => new Visit()
                {
                    VisitKind = VisitKind.Script,
                    Id = x.Id,
                    PageKeyId = x.PageKeyId,
                    Created = x.Created,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    ScriptName = x.PageKey.PayloadScript.Name,
                    PayloadScriptId = x.PageKey.PayloadScriptId,
                })
                .ToListAsync();

            Visits = pageVisits
                .Union(scriptVisits)
                .OrderBy(x => x.Created)
                .ToList();

            QueuedInstructions = await database.QueuedInstructions
                .Where(x => x.PageKeyId == id)
                .OrderBy(x => x.Created)
                .Select(x => new QueuedInstructionPlus()
                {
                    Id = x.Id,
                    Created = x.Created,
                    Name = x.Instruction.Name,
                    InstructionId = x.InstructionId,
                })
                .ToListAsync();

            IpAddresses = Visits
                .Select(x => x.IpAddress)
                .Where(x => x != null)
                .Cast<string>()
                .Distinct()
                .ToList();

            UserAgents = Visits
                .Select(x => x.UserAgent)
                .Where(x => x != null)
                .Cast<string>()
                .Distinct()
                .ToList();

            return Page();
        }
        catch (NotFoundException)
        {
            return Redirect("/help/notfound");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
