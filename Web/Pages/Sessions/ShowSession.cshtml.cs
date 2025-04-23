using Lefish.Application.Commands.Sessions.EndSession;

namespace Lefish.Web.Pages.Sessions;

public class ShowSessionModel(
    IUserToken userToken,
    IDatabaseService database,
    IEndSessionCommand endSessionCommand) : UserTokenPageModel(userToken)
{
    public Session Session { get; set; }

    public bool CanEndSession { get; set; }
        = endSessionCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            Session = await database.Sessions
                .Where(x =>
                    x.Id == id &&
                    x.UserId == UserToken.UserId!.Value)
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
