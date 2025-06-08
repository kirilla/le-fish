using Lefish.Application.Commands.Targets.AddTarget;

namespace Lefish.Web.Pages.Targets;

public class AddTargetModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddTargetCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public AddTargetCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddTargetCommandModel();

            return Page();
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

            if (!ModelState.IsValid)
                return Page();

            var id = await command.Execute(UserToken, CommandModel);

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
