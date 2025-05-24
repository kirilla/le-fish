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

            PageVisit = await database.PageVisits
                .Where(x => x.Id == id)
                .Select(x => new PageVisitPlus()
                {
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.PageKey.PayloadPage.Name,
                    PayloadPageId = x.PageKey.PayloadPageId,
                    TargetName = x.PageKey.EmailMessage.EmailTarget.Name,
                    TargetAddress = x.PageKey.EmailMessage.EmailTarget.Address,
                    EmailTargetId = x.PageKey.EmailMessageId,
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
