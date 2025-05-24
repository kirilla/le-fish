namespace Lefish.Web.Pages.PageKeys;

public class ShowPageKeyModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public PageKeyPlus PageKey { get; set; }

    public List<PageVisitPlus> PageVisits { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PageKey = await database.PageKeys
                .Where(x => x.Id == id)
                .Select(x => new PageKeyPlus()
                {
                    Id = x.Id,
                    Token = x.Token,
                    Created = x.Created,
                    PageName = x.PayloadPage.Name,
                    PayloadPageId = x.PayloadPageId,
                    TargetName = x.EmailMessage.EmailTarget.Name,
                    TargetAddress = x.EmailMessage.EmailTarget.Address,
                    EmailMessageId = x.EmailMessageId,
                    EmailMessageSubject = x.EmailMessage.Subject,
                    EmailTargetId = x.EmailMessage.EmailTargetId,
                    //PageVisitCount = x.PageVisits.Count(),
                })
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PageVisits = await database.PageVisits
                .Where(x => x.PageKeyId == id)
                .OrderBy(x => x.Created)
                .Select(x => new PageVisitPlus()
                {
                    Id = x.Id,
                    Created = x.Created,
                    Url = x.Url,
                    Method = x.Method,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    PageName = x.PageKey.PayloadPage.Name,
                    PayloadPageId = x.PageKey.PayloadPageId,
                    //TargetName = x.PageKey.EmailMessage.EmailTarget.Name,
                    //TargetAddress = x.PageKey.EmailMessage.EmailTarget.Address,
                    //EmailTargetId = x.PageKey.EmailMessage.EmailTargetId,
                })
                .ToListAsync();

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
