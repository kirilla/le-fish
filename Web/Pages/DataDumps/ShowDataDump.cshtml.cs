using Lefish.Application.Commands.DataDumps.EditDataDump;
using Lefish.Application.Commands.DataDumps.RemoveDataDump;

namespace Lefish.Web.Pages.DataDumps;

public class ShowDataDumpModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditDataDumpCommand editDataDumpCommand,
    IRemoveDataDumpCommand removeDataDumpCommand) : UserTokenPageModel(userToken)
{
    public DataDump DataDump { get; set; }

    public bool CanEditDataDump { get; set; }
        = editDataDumpCommand.IsPermitted(userToken);

    public bool CanRemoveDataDump { get; set; }
        = removeDataDumpCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            DataDump = await database.DataDumps
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
