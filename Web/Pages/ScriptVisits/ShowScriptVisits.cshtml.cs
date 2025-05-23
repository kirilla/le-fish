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
                    ScriptName = x.PageToken.PayloadScript.Name,
                    PayloadScriptId = x.PageToken.PayloadScriptId,
                    TargetName = x.PageToken.EmailMessage.EmailTarget.Name,
                    TargetAddress = x.PageToken.EmailMessage.EmailTarget.Address,
                    EmailTargetId = x.PageToken.EmailMessageId,
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
