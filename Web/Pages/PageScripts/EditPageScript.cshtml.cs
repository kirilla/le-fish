using Lefish.Application.Commands.PageScripts.EditPageScript;

namespace Lefish.Web.Pages.PageScripts;

public class EditPageScriptModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditPageScriptCommand command) : UserTokenPageModel(userToken)
{
    public PageScript PageScript { get; set; }

    [BindProperty]
    public EditPageScriptCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PageScript = await database.PageScripts
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditPageScriptCommandModel()
            {
                Id = PageScript.Id,
				Name = PageScript.Name,
				Script = PageScript.Script,
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

            PageScript = await database.PageScripts
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-page-script/{id}");
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
