using Lefish.Application.Commands.PageVisits.RemovePageVisit;

namespace Lefish.Web.Pages.PageVisits;

public class ShowPageVisitModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemovePageVisitCommand removePageVisitCommand) : UserTokenPageModel(userToken)
{
    public PageVisit PageVisit { get; set; }

    public bool CanRemovePageVisit { get; set; }
        = removePageVisitCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PageVisit = await database.PageVisits
                .Include(x => x.EmailTarget)
                //.Include(x => x.PayloadPage)
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
