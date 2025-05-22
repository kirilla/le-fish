using Lefish.Application.Commands.ScriptVisits.RemoveScriptVisits;

namespace Lefish.Web.Pages.ScriptVisits;

public class RemoveScriptVisitsModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveScriptVisitsCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public RemoveScriptVisitsCommandModel CommandModel { get; set; }

    public List<EmailTarget> EmailTargets { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailTargets = await database.EmailTargets
                .OrderBy(x => x.Address)
                .ToListAsync();

            CommandModel = new RemoveScriptVisitsCommandModel();

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

            EmailTargets = await database.EmailTargets
                .OrderBy(x => x.Address)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-script-visits");
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
