using Lefish.Application.Commands.AttackEvents.RemoveAttackEvents;

namespace Lefish.Web.Pages.AttackEvents;

public class RemoveVisitsModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveAttackEventsCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public RemoveAttackEventsCommandModel CommandModel { get; set; }

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

            CommandModel = new RemoveAttackEventsCommandModel();

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

            return Redirect("/show-attack-events");
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
