using Lefish.Application.Commands.EmailTargets.AddEmailTarget;

namespace Lefish.Web.Pages.EmailTargets;

public class AddEmailTargetModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddEmailTargetCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public AddEmailTargetCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddEmailTargetCommandModel();

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

            return Redirect($"/show-email-target/{id}");
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
