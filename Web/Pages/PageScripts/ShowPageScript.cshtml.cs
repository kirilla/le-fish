using Lefish.Application.Commands.PageScripts.ClonePageScript;
using Lefish.Application.Commands.PageScripts.EditPageScript;
using Lefish.Application.Commands.PageScripts.RemovePageScript;

namespace Lefish.Web.Pages.PageScripts;

public class ShowPageScriptModel(
    IUserToken userToken,
    IDatabaseService database,
    IClonePageScriptCommand clonePageScriptCommand,
    IEditPageScriptCommand editPageScriptCommand,
    IRemovePageScriptCommand removePageScriptCommand,
    IOptions<TemplateConfiguration> templateConfiguration) : UserTokenPageModel(userToken)
{
    public readonly TemplateConfiguration Config = templateConfiguration.Value;

    public PageScript PageScript { get; set; }

    public bool CanClonePageScript { get; set; }
        = clonePageScriptCommand.IsPermitted(userToken);

    public bool CanEditPageScript { get; set; }
        = editPageScriptCommand.IsPermitted(userToken);

    public bool CanRemovePageScript { get; set; }
        = removePageScriptCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PageScript = await database.PageScripts
                .Where(x => x.Id == id)
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
