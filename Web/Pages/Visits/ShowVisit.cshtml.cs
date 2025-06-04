using Lefish.Application.Commands.Visits.RemoveVisit;

namespace Lefish.Web.Pages.Visits;

public class ShowVisitModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveVisitCommand removeVisitCommand) : UserTokenPageModel(userToken)
{
    public VisitPlus Visit { get; set; }

    public bool CanRemoveVisit { get; set; }
        = removeVisitCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            Visit = await database.Visits
                .Where(x => x.Id == id)
                .Select(x => new VisitPlus()
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
