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
                    PageName = x.PageToken.PayloadPage.Name,
                    PayloadPageId = x.PageToken.PayloadPageId,
                    TargetName = x.PageToken.EmailMessage.EmailTarget.Name,
                    TargetAddress = x.PageToken.EmailMessage.EmailTarget.Address,
                    EmailTargetId = x.PageToken.EmailMessageId,
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
