using Lefish.Application.Commands.PageVisits.RemovePageVisit;

namespace Lefish.Web.Pages.PageVisits;

public class ShowPageVisitModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemovePageVisitCommand removePageVisitCommand) : UserTokenPageModel(userToken)
{
    public PageVisitPlus PageVisit { get; set; }

    public bool CanRemovePageVisit { get; set; }
        = removePageVisitCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PageVisit = await database.Visits
                .Where(x => x.Id == id)
                .Select(x => new PageVisitPlus()
                {
                    Id = x.Id,
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.Attack.PayloadPage.Name,
                    PayloadPageId = x.Attack.PayloadPageId,
                    TargetName = x.Attack.EmailTarget.Name,
                    TargetAddress = x.Attack.EmailTarget.Address,
                    EmailTargetId = x.Attack.EmailTargetId,
                })
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
