using Lefish.Application.Commands.BlockedRequests.RemoveBlockedRequest;

namespace Lefish.Web.Pages.BlockedRequests;

public class ShowBlockedRequestModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveBlockedRequestCommand removeBlockedRequestCommand) : UserTokenPageModel(userToken)
{
    public BlockedRequest BlockedRequest { get; set; }

    public bool CanRemoveBlockedRequest { get; set; }
        = removeBlockedRequestCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            BlockedRequest = await database.BlockedRequests
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
