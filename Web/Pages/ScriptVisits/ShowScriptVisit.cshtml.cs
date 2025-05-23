using Lefish.Application.Commands.ScriptVisits.RemoveScriptVisit;

namespace Lefish.Web.Pages.ScriptVisits;

public class ShowScriptVisitModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveScriptVisitCommand removeScriptVisitCommand) : UserTokenPageModel(userToken)
{
    public ScriptVisitPlus ScriptVisit { get; set; }

    public bool CanRemoveScriptVisit { get; set; }
        = removeScriptVisitCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            ScriptVisit = await database.ScriptVisits
                .Where(x => x.Id == id)
                .Select(x => new ScriptVisitPlus()
                {
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
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
