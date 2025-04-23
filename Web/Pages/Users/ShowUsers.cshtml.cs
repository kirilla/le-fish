namespace Lefish.Web.Pages.Users;

public class ShowUsersModel(
    IDatabaseService database,
    IUserToken userToken) : UserTokenPageModel(userToken)
{
    public List<User> Users { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            Users = await database.Users
                .AsNoTracking()
                .OrderByDescending(x => x.Name)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
