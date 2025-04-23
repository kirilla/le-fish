namespace Lefish.Web.Pages.PageVisits;

public class ShowPageVisitsModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<PageVisit> PageVisits { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            PageVisits = await database.PageVisits
                .AsNoTracking()
                .Include(x => x.EmailTarget)
                .Include(x => x.PayloadPage)
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
