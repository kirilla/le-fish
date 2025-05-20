using Lefish.Application.Commands.PayloadScripts.EditPayloadScript;

namespace Lefish.Web.Pages.PayloadScripts;

public class EditPayloadScriptModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditPayloadScriptCommand command) : UserTokenPageModel(userToken)
{
    public PayloadScript PayloadScript { get; set; }

    [BindProperty]
    public EditPayloadScriptCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PayloadScript = await database.PayloadScripts
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditPayloadScriptCommandModel()
            {
                PayloadScriptId = PayloadScript.Id,
				Name = PayloadScript.Name,
				Script = PayloadScript.Script,
            };

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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PayloadScript = await database.PayloadScripts
                .Where(x => x.Id == CommandModel.PayloadScriptId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-payload-script/{id}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns ett annat skript med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
