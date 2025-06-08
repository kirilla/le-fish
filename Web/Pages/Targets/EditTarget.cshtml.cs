using Lefish.Application.Commands.Targets.EditTarget;

namespace Lefish.Web.Pages.Targets;

public class EditTargetModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditTargetCommand command) : UserTokenPageModel(userToken)
{
    public Target Target { get; set; }

    [BindProperty]
    public EditTargetCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Target = await database.Targets
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditTargetCommandModel()
            {
                Id = Target.Id,
                Name = Target.Name,
                Address = Target.Address,
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

            Target = await database.Targets
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-target/{id}");
        }
        catch (BlockedByAddressException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Address),
                "Det finns ett annat målkonto med samma adress.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
