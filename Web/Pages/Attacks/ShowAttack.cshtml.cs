namespace Lefish.Web.Pages.Attacks;

public class ShowAttackModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public AttackSummary Attack { get; set; }

    public List<DataDump> DataDumps { get; set; }
    public List<EmailHeader> EmailHeaders { get; set; }

    public List<PageVisitPlus> PageVisits { get; set; }
    public List<ScriptVisitPlus> ScriptVisits { get; set; }

    public List<TargetInstructionSummary> TargetInstructions { get; set; }

    public List<Visit> Visits { get; set; }

    public List<string> IpAddresses { get; set; }
    public List<string> UserAgents { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            Attack = await database.Attacks
                .Where(x => x.Id == id)
                .Select(x => new AttackSummary()
                {
                    Id = x.Id,
                    Value = x.Value,
                    Created = x.Created,
                    PageName = x.PayloadPage.Name,
                    PayloadPageId = x.PayloadPageId,
                    TargetName = x.EmailTarget.Name,
                    TargetAddress = x.EmailTarget.Address,
                    EmailTargetId = x.EmailTargetId,
                    ScriptName = x.PayloadScript.Name,
                    PayloadScriptId = x.PayloadScriptId,
                })
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            DataDumps = await database.DataDumps
                .Where(x => x.AttackId == id)
                .OrderBy(x => x.Created)
                .ToListAsync();

            EmailHeaders = await database.EmailMessages
                .Where(x => x.AttackId == id)
                .OrderBy(x => x.Created)
                .Select(x => new EmailHeader()
                {
                    Id = x.Id,
                    AttackId = x.AttackId,
                    ToName = x.ToName,
                    ToAddress = x.ToAddress,
                    FromName = x.EmailAccount!.FromName,
                    FromAddress = x.EmailAccount!.FromAddress,
                    ReplyToName = x.EmailAccount.ReplyToName,
                    ReplyToAddress = x.EmailAccount.ReplyToAddress,
                    Subject = x.Subject,
                    EmailStatus = x.EmailStatus,
                    Created = x.Created,
                    Sent = x.Sent,
                })
                .ToListAsync();

            PageVisits = await database.PageVisits
                .Where(x => x.AttackId == id)
                .OrderBy(x => x.Created)
                .Select(x => new PageVisitPlus()
                {
                    Id = x.Id,
                    Created = x.Created,
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.Attack.PayloadPage.Name,
                    PayloadPageId = x.Attack.PayloadPageId,
                })
                .ToListAsync();

            ScriptVisits = await database.ScriptVisits
                .Where(x => x.AttackId == id)
                .OrderBy(x => x.Created)
                .Select(x => new ScriptVisitPlus()
                {
                    Id = x.Id,
                    Created = x.Created,
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    ScriptName = x.Attack.PayloadScript.Name,
                    PayloadScriptId = x.Attack.PayloadScriptId,
                })
                .ToListAsync();

            var pageVisits = await database.PageVisits
                .OrderBy(x => x.Created)
                .Where(x => x.AttackId == id)
                .Select(x => new Visit()
                {
                    VisitKind = VisitKind.Page,
                    Id = x.Id,
                    AttackId = x.AttackId,
                    Created = x.Created,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.Attack.PayloadPage.Name,
                    PayloadPageId = x.Attack.PayloadPageId,
                })
                .ToListAsync();

            var scriptVisits = await database.ScriptVisits
                .OrderBy(x => x.Created)
                .Where(x => x.AttackId == id)
                .Select(x => new Visit()
                {
                    VisitKind = VisitKind.Script,
                    Id = x.Id,
                    AttackId = x.AttackId,
                    Created = x.Created,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    ScriptName = x.Attack.PayloadScript.Name,
                    PayloadScriptId = x.Attack.PayloadScriptId,
                })
                .ToListAsync();

            Visits = pageVisits
                .Union(scriptVisits)
                .OrderBy(x => x.Created)
                .ToList();

            TargetInstructions = await database.TargetInstructions
                .Where(x => x.AttackId == id)
                .OrderBy(x => x.Created)
                .Select(x => new TargetInstructionSummary()
                {
                    Id = x.Id,
                    Created = x.Created,
                    Fetched = x.Fetched,
                    Name = x.Name,
                    Reference = x.Reference,
                    InstructionStatus = x.InstructionStatus,
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
