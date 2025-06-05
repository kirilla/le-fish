using Lefish.Application.Commands.PayloadScripts.AddPayloadScript;

namespace Lefish.Web.Pages.PayloadScripts;

public class ShowPayloadScriptsModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddPayloadScriptCommand addPayloadScriptCommand) : UserTokenPageModel(userToken)
{
    public List<PageScript> PayloadScripts { get; set; }

    public bool CanAddPayloadScript { get; set; }
        = addPayloadScriptCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PayloadScripts = await database.PageScripts
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
