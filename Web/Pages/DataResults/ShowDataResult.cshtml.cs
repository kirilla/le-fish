using Lefish.Application.Commands.DataResults.EditDataResult;
using Lefish.Application.Commands.DataResults.RemoveDataResult;

namespace Lefish.Web.Pages.DataResults;

public class ShowDataResultModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditDataResultCommand editDataResultCommand,
    IRemoveDataResultCommand removeDataResultCommand) : UserTokenPageModel(userToken)
{
    public DataResult DataResult { get; set; }

    public bool CanEditDataResult { get; set; }
        = editDataResultCommand.IsPermitted(userToken);

    public bool CanRemoveDataResult { get; set; }
        = removeDataResultCommand.IsPermitted(userToken);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            DataResult = await database.DataResults
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
