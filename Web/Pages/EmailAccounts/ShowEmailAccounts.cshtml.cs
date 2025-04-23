using Lefish.Application.Commands.EmailAccounts.AddEmailAccount;

namespace Lefish.Web.Pages.EmailAccounts;

public class ShowEmailAccountsModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddEmailAccountCommand addEmailAccountCommand) : UserTokenPageModel(userToken)
{
    public List<EmailAccount> EmailAccounts { get; set; }

    public bool CanAddEmailAccount { get; set; }
        = addEmailAccountCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            EmailAccounts = await database.EmailAccounts
                .AsNoTracking()
                .OrderBy(x => x.FromName)
                .ThenBy(x => x.FromAddress)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
