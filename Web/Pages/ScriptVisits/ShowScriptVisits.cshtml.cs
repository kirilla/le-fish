namespace Lefish.Web.Pages.ScriptVisits;

public class ShowScriptVisitsModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<ScriptVisitPlus> ScriptVisits { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            ScriptVisits = await database.ScriptVisits
                .OrderByDescending(x => x.Created)
                .Select(x => new ScriptVisitPlus()
                {
                    Id = x.Id,
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    ScriptName = x.Attack.PayloadScript.Name,
                    PayloadScriptId = x.Attack.PayloadScriptId,
                    TargetName = x.Attack.EmailTarget.Name,
                    TargetAddress = x.Attack.EmailTarget.Address,
                    EmailTargetId = x.Attack.EmailTargetId,
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
