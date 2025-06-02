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
