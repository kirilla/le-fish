namespace Lefish.Web.Pages.BlockedRequests;

public class ShowBlockedRequestsModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<BlockedRequest> BlockedRequests { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            BlockedRequests = await database.BlockedRequests
                .AsNoTracking()
                .OrderByDescending(x => x.Created)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
