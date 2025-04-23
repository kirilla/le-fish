using Lefish.Application.Commands.EmailAccounts.AddEmailAccount;

namespace Lefish.Web.Pages.EmailAccounts;

public class AddEmailAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddEmailAccountCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public AddEmailAccountCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddEmailAccountCommandModel();

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

            return Redirect($"/show-email-account/{id}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.FromAddress),
                "Det finns ett annat epostkonto med samma adress.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
