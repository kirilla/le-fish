namespace Lefish.Web.Pages.DataDumps;

public class ShowDataDumpsModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<DataResult> DataDumps { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            DataDumps = await database.DataResults
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
