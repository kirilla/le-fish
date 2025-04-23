using Lefish.Application.Commands.Account.EditAccount;

namespace Lefish.Web.Pages.Account;

public class EditAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditAccountCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public EditAccountCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(userToken))
                throw new NotPermittedException();

            var user = await database.Users
                .Where(p => p.Id == UserToken.UserId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditAccountCommandModel()
            {
                Name = user.Name,
            };

            return Page();
        }
        catch (Exception ex)
        {
            return Redirect("/help/notpermitted");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!command.IsPermitted(userToken))
                throw new NotPermittedException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/account/showaccount");
        }
        catch (Exception ex)
        {
            return Redirect("/help/notpermitted");
        }
    }
}
