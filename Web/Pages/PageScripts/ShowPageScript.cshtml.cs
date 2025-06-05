using Lefish.Application.Commands.PageScripts.ClonePageScript;
using Lefish.Application.Commands.PageScripts.EditPageScript;
using Lefish.Application.Commands.PageScripts.RemovePageScript;

namespace Lefish.Web.Pages.PageScripts;

public class ShowPageScriptModel(
    IUserToken userToken,
    IDatabaseService database,
    IClonePageScriptCommand clonePayloadScriptCommand,
    IEditPageScriptCommand editPayloadScriptCommand,
    IRemovePageScriptCommand removePayloadScriptCommand,
    IOptions<TemplateConfiguration> templateConfiguration) : UserTokenPageModel(userToken)
{
    public readonly TemplateConfiguration Config = templateConfiguration.Value;

    public PageScript PayloadScript { get; set; }

    public bool CanClonePayloadScript { get; set; }
        = clonePayloadScriptCommand.IsPermitted(userToken);

    public bool CanEditPayloadScript { get; set; }
        = editPayloadScriptCommand.IsPermitted(userToken);

    public bool CanRemovePayloadScript { get; set; }
        = removePayloadScriptCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PayloadScript = await database.PageScripts
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
