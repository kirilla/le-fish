using Lefish.Application.Commands.DataResults.EditDataResult;

namespace Lefish.Web.Pages.DataResults;

public class EditDataResultModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditDataResultCommand command) : UserTokenPageModel(userToken)
{
    public DataResult DataResult { get; set; }

    [BindProperty]
    public EditDataResultCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            DataResult = await database.DataResults
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditDataResultCommandModel()
            {
                Id = DataResult.Id,
                JsonData = DataResult.JsonData,
            };

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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            DataResult = await database.DataResults
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-data-result/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
