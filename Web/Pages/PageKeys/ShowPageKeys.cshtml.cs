namespace Lefish.Web.Pages.PageKeys;

public class ShowPageKeysModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<PageKeyPlus> PageKeys { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PageKeys = await database.Attacks
                .OrderBy(x => x.Created)
                .ThenBy(x => x.Value)
                .Select(x => new PageKeyPlus()
                {
                    Id = x.Id,
                    Value = x.Value,
                    Created = x.Created,
                    PageName = x.PayloadPage.Name,
                    PayloadPageId = x.PayloadPageId,
                    TargetName = x.EmailTarget.Name,
                    TargetAddress = x.EmailTarget.Address,
                    EmailTargetId = x.EmailTargetId,
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
