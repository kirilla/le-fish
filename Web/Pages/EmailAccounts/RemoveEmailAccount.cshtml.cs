using Lefish.Application.Commands.EmailAccounts.RemoveEmailAccount;

namespace Lefish.Web.Pages.EmailAccounts;

public class RemoveEmailAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveEmailAccountCommand command) : UserTokenPageModel(userToken)
{
    public EmailAccount EmailAccount { get; set; }

    [BindProperty]
    public RemoveEmailAccountCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailAccount = await database.EmailAccounts
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveEmailAccountCommandModel()
            {
                EmailAccountId = EmailAccount.Id,
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

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailAccount = await database.EmailAccounts
                .Where(x => x.Id == CommandModel.EmailAccountId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-accounts");
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
