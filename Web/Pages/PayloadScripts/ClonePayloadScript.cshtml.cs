using Lefish.Application.Commands.PageScripts.ClonePageScript;

namespace Lefish.Web.Pages.PayloadScripts;

public class ClonePageScriptModel(
    IUserToken userToken,
    IDatabaseService database,
    IClonePageScriptCommand command) : UserTokenPageModel(userToken)
{
    public PageScript PayloadScript { get; set; }

    [BindProperty]
    public ClonePageScriptCommandModel CommandModel { get; set; }

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

            CommandModel = new ClonePageScriptCommandModel()
            {
                PayloadScriptId = PayloadScript.Id,
                Name = PayloadScript.Name,
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
                .Where(x => x.Id == CommandModel.PayloadScriptId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            var cloneId = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-payload-script/{cloneId}");
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
