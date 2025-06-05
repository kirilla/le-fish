using Lefish.Application.Commands.PageScripts.EditPageScript;

namespace Lefish.Web.Pages.PageScripts;

public class EditPageScriptModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditPageScriptCommand command) : UserTokenPageModel(userToken)
{
    public PageScript PayloadScript { get; set; }

    [BindProperty]
    public EditPageScriptCommandModel CommandModel { get; set; }

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

            CommandModel = new EditPageScriptCommandModel()
            {
                Id = PayloadScript.Id,
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

            PayloadScript = await database.PageScripts
                .Where(x => x.Id == CommandModel.Id)
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
