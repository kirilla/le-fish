using Lefish.Application.Commands.PayloadScripts.RemovePayloadScript;

namespace Lefish.Web.Pages.PayloadScripts;

public class RemovePayloadScriptModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemovePayloadScriptCommand command) : UserTokenPageModel(userToken)
{
    public PayloadScript PayloadScript { get; set; }

    [BindProperty]
    public RemovePayloadScriptCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PayloadScript = await database.PageScripts
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemovePayloadScriptCommandModel()
            {
                PayloadScriptId = PayloadScript.Id,
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

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PayloadScript = await database.PageScripts
                .Where(x => x.Id == CommandModel.PayloadScriptId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-payload-scripts");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
