namespace Lefish.Web.Pages.AttackEvents;

public class ShowAttackEventsModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public Attack Attack { get; set; }
    public Target Target { get; set; }

    public AttackEventKindSummary AttackEventKindSummary { get; set; }

    public List<AttackEvent> AttackEvents { get; set; }

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
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Target = await database.Targets
                .Where(x => x.Id == Attack.TargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var events = await database.AttackEvents
                .Where(x => x.AttackId == id)
                .OrderBy(x => x.Created)
                .ToListAsync();

            AttackEventKindSummary = new AttackEventKindSummary
            {
                PageVisitCount = events
                    .Count(x => x.AttackEventKind == AttackEventKind.PageVisit),
                ScriptDownloadCount = events
                    .Count(x => x.AttackEventKind == AttackEventKind.ScriptDownload),
                FetchInstructionCount = events
                    .Count(x => x.AttackEventKind == AttackEventKind.FetchInstruction),
                UploadDataCount = events
                    .Count(x => x.AttackEventKind == AttackEventKind.UploadData),
            };

            AttackEvents = await database.AttackEvents
                .Where(x => x.AttackId == id)
                .OrderByDescending(x => x.Created)
                .ToListAsync();

            IpAddresses = AttackEvents
                .Select(x => x.IpAddress)
                .Where(x => x != null)
                .Cast<string>()
                .Distinct()
                .ToList();

            UserAgents = AttackEvents
                .Select(x => x.UserAgent)
                .Where(x => x != null)
                .Cast<string>()
                .Distinct()
                .ToList();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
