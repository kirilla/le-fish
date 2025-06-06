namespace Lefish.Web.Pages.AttackEvents;

public class ShowAttackEventsModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<AttackEventPlus> AttackEvents { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AttackEvents = await database.AttackEvents
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
