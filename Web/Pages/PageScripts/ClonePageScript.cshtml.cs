using Lefish.Application.Commands.PageScripts.ClonePageScript;

namespace Lefish.Web.Pages.PageScripts;

public class ClonePageScriptModel(
    IUserToken userToken,
    IDatabaseService database,
    IClonePageScriptCommand command) : UserTokenPageModel(userToken)
{
    public PageScript PageScript { get; set; }

    [BindProperty]
    public ClonePageScriptCommandModel CommandModel { get; set; }

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

            CommandModel = new ClonePageScriptCommandModel()
            {
                Id = PageScript.Id,
                Name = PageScript.Name,
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

            var cloneId = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-page-script/{cloneId}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns ett skript med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
