using Lefish.Application.Commands.DataDumps.UploadDataDump;

namespace Lefish.Web.Pages.DataDumps;

public class ShowDataDumpsModel(
    IUserToken userToken,
    IDatabaseService database,
    IUploadDataDumpCommand addTargetCommand) : UserTokenPageModel(userToken)
{
    public List<DataDump> DataDumps { get; set; }

    public bool CanAddTarget { get; set; }
        = addTargetCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            DataDumps = await database.DataDumps
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
