using Lefish.Application.Commands.AttackEvents.RemoveAttackEvents;

namespace Lefish.Web.Pages.AttackEvents;

public class RemoveAttackEventsModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveAttackEventsCommand command) : UserTokenPageModel(userToken)
{
    public Attack Attack { get; set; }
    public Target Target { get; set; }

    [BindProperty]
    public RemoveAttackEventsCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Attack = await database.Attacks
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Target = await database.Targets
                .Where(x => x.Id == Attack.TargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveAttackEventsCommandModel()
            {
                AttackId = Attack.Id,
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

            Attack = await database.Attacks
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Target = await database.Targets
                .Where(x => x.Id == Attack.TargetId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-attack-events/{Attack.Id}");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du vill ta bort händelserna.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
