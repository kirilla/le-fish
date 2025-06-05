namespace Lefish.Web.Pages.DataResults;

public class ShowDataResultsModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<DataResult> DataResults { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            DataResults = await database.DataResults
                .AsNoTracking()
                //.OrderBy(x => x.Created)
                //.ThenBy(x => x.Name)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
