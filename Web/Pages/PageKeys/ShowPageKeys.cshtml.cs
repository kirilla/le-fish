namespace Lefish.Web.Pages.PageKeys;

public class ShowPageKeysModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<PageKeyPlus> PageTokens { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PageTokens = await database.PageKeys
                .OrderBy(x => x.Created)
                .ThenBy(x => x.Token)
                .Select(x => new PageKeyPlus()
                {
                    Id = x.Id,
                    Token = x.Token,
                    Created = x.Created,
                    PageName = x.PayloadPage.Name,
                    PayloadPageId = x.PayloadPageId,
                    TargetName = x.EmailMessage.EmailTarget.Name,
                    TargetAddress = x.EmailMessage.EmailTarget.Address,
                    EmailTargetId = x.EmailMessage.EmailTargetId,
                    PageVisitCount = x.PageVisits.Count(),
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
