using Lefish.Application.Commands.PageScripts.RemovePageScript;

namespace Lefish.Web.Pages.PayloadScripts;

public class RemovePageScriptModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemovePageScriptCommand command) : UserTokenPageModel(userToken)
{
    public PageScript PayloadScript { get; set; }

    [BindProperty]
    public RemovePageScriptCommandModel CommandModel { get; set; }

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

            CommandModel = new RemovePageScriptCommandModel()
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
