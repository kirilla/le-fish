using Lefish.Application.Commands.PageScripts.AddPageScript;

namespace Lefish.Web.Pages.PageScripts;

public class ShowPageScriptsModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddPageScriptCommand addPageScriptCommand) : UserTokenPageModel(userToken)
{
    public List<PageScript> PageScripts { get; set; }

    public bool CanAddPageScript { get; set; }
        = addPageScriptCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PageScripts = await database.PageScripts
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
