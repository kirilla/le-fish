using Lefish.Application.Commands.EmailAccounts.EditEmailAccount;
using Lefish.Application.Commands.EmailAccounts.RemoveEmailAccount;

namespace Lefish.Web.Pages.EmailAccounts;

public class ShowEmailAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditEmailAccountCommand editEmailAccountCommand,
    IRemoveEmailAccountCommand removeEmailAccountCommand) : UserTokenPageModel(userToken)
{
    public EmailAccount EmailAccount { get; set; }

    public bool CanEditEmailAccount { get; set; }
        = editEmailAccountCommand.IsPermitted(userToken);

    public bool CanRemoveEmailAccount { get; set; }
        = removeEmailAccountCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            EmailAccount = await database.EmailAccounts
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
}
