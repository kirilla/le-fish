using Lefish.Application.Commands.Account.ChangePassword;
using Lefish.Application.Commands.Account.DeleteAccount;
using Lefish.Application.Commands.Account.EditAccount;
using Lefish.Application.Commands.UserEmails.AddUserEmail;
using Lefish.Application.Commands.UserEmails.RemoveUserEmail;

namespace Lefish.Web.Pages.Account;

public class ShowAccountModel(
    IDatabaseService database,
    IUserToken userToken,
    IChangePasswordCommand changePasswordCommand,
    IDeleteAccountCommand deleteAccountCommand,
    IEditAccountCommand editAccountCommand,
    IAddUserEmailCommand addUserEmailCommand,
    IRemoveUserEmailCommand removeUserEmailCommand) : UserTokenPageModel(userToken)
{
    public new User User { get; set; }

    public List<UserEmail> UserEmails { get; set; }

    public bool CanChangePassword { get; set; } 
        = changePasswordCommand.IsPermitted(userToken);

    public bool CanDeleteAccount { get; set; }
        = deleteAccountCommand.IsPermitted(userToken);

    public bool CanEditAccount { get; set; }
        = editAccountCommand.IsPermitted(userToken);

    public bool CanAddUserEmail { get; set; }
        = addUserEmailCommand.IsPermitted(userToken);

    public bool CanRemoveUserEmail { get; set; }
        = removeUserEmailCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            User = await database.Users
                .AsNoTracking()
                .Where(x => x.Id == UserToken.UserId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            UserEmails = await database.UserEmails
                .AsNoTracking()
                .Where(x => x.UserId == UserToken.UserId!.Value)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
