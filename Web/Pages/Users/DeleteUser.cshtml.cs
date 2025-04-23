using Lefish.Application.Commands.Users.DeleteUser;

namespace Lefish.Web.Pages.Users;

public class DeleteUserModel(
    IDatabaseService database,
    IDeleteUserCommand command,
    IUserToken userToken) : UserTokenPageModel(userToken)
{
    public new User User { get; set; }

    [BindProperty]
    public DeleteUserCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            User = await database.Users
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new DeleteUserCommandModel()
            {
                Id = User.Id,
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

            User = await database.Users
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/admin/all-users");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort användaren.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
