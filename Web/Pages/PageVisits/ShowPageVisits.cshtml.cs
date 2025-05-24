namespace Lefish.Web.Pages.PageVisits;

public class ShowPageVisitsModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<PageVisitPlus> PageVisits { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PageVisits = await database.PageVisits
                .OrderByDescending(x => x.Created)
                .Select(x => new PageVisitPlus()
                {
                    Id = x.Id,
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
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
