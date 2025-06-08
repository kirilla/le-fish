namespace Lefish.Web.Pages.AttackEvents;

public class ShowAttackEventsModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public Attack Attack { get; set; }
    public Target Target { get; set; }

    public AttackEventKindSummary AttackEventKindSummary { get; set; }

    public List<AttackEventPlus> AttackEvents { get; set; }

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
                .Select(x => new AttackEventPlus()
                {
                    Id = x.Id,
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.Attack.WebPage.Name,
                    PayloadPageId = x.Attack.WebPageId,
                    TargetName = x.Attack.Target.Name,
                    TargetAddress = x.Attack.Target.Address,
                    TargetId = x.Attack.TargetId,
                })
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
