using Lefish.Application.Commands.PayloadScripts.ClonePayloadScript;
using Lefish.Application.Commands.PayloadScripts.EditPayloadScript;
using Lefish.Application.Commands.PayloadScripts.RemovePayloadScript;

namespace Lefish.Web.Pages.PayloadScripts;

public class ShowPayloadScriptModel(
    IUserToken userToken,
    IDatabaseService database,
    IClonePayloadScriptCommand clonePayloadScriptCommand,
    IEditPayloadScriptCommand editPayloadScriptCommand,
    IRemovePayloadScriptCommand removePayloadScriptCommand,
    IOptions<TemplateConfiguration> templateConfiguration) : UserTokenPageModel(userToken)
{
    public readonly TemplateConfiguration Config = templateConfiguration.Value;

    public PayloadScript PayloadScript { get; set; }

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

            PayloadScript = await database.PayloadScripts
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
